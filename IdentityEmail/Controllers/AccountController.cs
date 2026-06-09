using IdentityEmail.Dtos;
using IdentityEmail.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;


namespace IdentityEmail.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _config;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]CreateUserRegisterDto createUserRegisterDto)
        {
            var existingUser = await _userManager.FindByEmailAsync(createUserRegisterDto.Email);
            if (existingUser != null)
            {
                return Json(new { success = false, message = "Bu e-posta adresi zaten kullanılıyor" });
            }

            if (createUserRegisterDto.Password != createUserRegisterDto.ConfirmPassword)
            {
                return Json(new { success = false, message = "Parolalar eşleşmiyor" });
            }
                 

            Random rnd = new Random();
            string code = rnd.Next(100000, 999999).ToString();

            var appUser = new AppUser
            {
                Name = createUserRegisterDto.Name,
                Surname = createUserRegisterDto.Surname,
                Email = createUserRegisterDto.Email,
                UserName = createUserRegisterDto.UserName,
                ConfirmCode = code

            };
            var result = await _userManager.CreateAsync(appUser, createUserRegisterDto.Password);
            if (result.Succeeded)
            {
                try
                {
                    MimeMessage mimeMessage = new MimeMessage();
                    mimeMessage.From.Add(new MailboxAddress("Yudi Kurumsal A.Ş", "dilan.deregizu@gmail.com"));
                    mimeMessage.To.Add(new MailboxAddress("Sayın Kullanıcı", appUser.Email));
                    mimeMessage.Subject = "E-Posta Onay Kodu";

                    var bodyBuilder = new BodyBuilder();
                    bodyBuilder.TextBody = "Kayıt işlemini tamamlamak için 6 haneli onay kodunuz: " + code;
                    mimeMessage.Body = bodyBuilder.ToMessageBody();

                    using (var client = new MailKit.Net.Smtp.SmtpClient())
                    {
                        await client.ConnectAsync("smtp.gmail.com", 587, false);
                        await client.AuthenticateAsync(
      _config["Email:Address"],
      _config["Email:Password"]  
  );
                        await client.SendAsync(mimeMessage);
                        await client.DisconnectAsync(true);
                    }

                    return Json(new { success = true, email = appUser.Email });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Mail gönderilirken hata oluştu: " + ex.Message });
                }
            }


            var errors = string.Join("<br>", result.Errors.Select(x => x.Description));
            return Json(new { success = false, message = errors });

        }

        [HttpGet]
        public IActionResult ConfirmEmail(string email)
        {
            return View(model: email); 
        }
        [HttpPost] 
        public async Task<IActionResult> ConfirmEmail(string email, string code)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return RedirectToAction("Register", "Account");
            }
            if (user.ConfirmCode == code)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
                return RedirectToAction("Login", "Account"); 

            }
            else
            {
                return RedirectToAction("ConfirmEmail", new { email = email }); // yanlış kod, geri dön
            }

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            var result = await _signInManager.PasswordSignInAsync(loginUserDto.UserName, loginUserDto.Password, loginUserDto.IsPersistent, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (result.RequiresTwoFactor)
            {
                return RedirectToAction("TwoFactorLogin"); 
            }


            ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
            return View(loginUserDto);
        }

        [HttpPost] 
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if (user == null)
            {
                return RedirectToAction("ForgotPasswordConfirmation"); // kullanıcı yok, yine de confirmation'a git
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = Url.Action("ResetPassword", "Account",
                new { email = forgotPasswordDto.Email, token = token }, Request.Scheme);

            MimeMessage mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("Yudi", _config["Email:Address"]));
            mimeMessage.To.Add(new MailboxAddress("Kullanıcı", forgotPasswordDto.Email));
            mimeMessage.Subject = "Şifre Sıfırlama";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = "Şifrenizi sıfırlamak için linke tıklayın: " + resetLink;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync(_config["Email:Address"], _config["Email:Password"]);
                await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);
            }

            return RedirectToAction("ForgotPasswordConfirmation"); // ✅ Json yerine bu
        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
                return RedirectToAction("Login");

            // Token ve email'i view'a taşımak için DTO kullanıyoruz
            var model = new ResetPasswordDto
            {
                Token = token,
                Email = email
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
                return RedirectToAction("Login");

            // Identity token'ı doğrular ve şifreyi değiştirir
            // Token yanlışsa veya süresi geçmişse hata döner
            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);

            if (result.Succeeded)
                return RedirectToAction("Login");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(resetPasswordDto);
        }
    }
}
    

