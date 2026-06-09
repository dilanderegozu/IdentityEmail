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
                    mimeMessage.From.Add(new MailboxAddress("Yusuf Tutkun", "dilan.deregizu@gmail.com"));
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
                return RedirectToAction("Index", "Dashboard");
            }
            else if (result.RequiresTwoFactor)
            {
                return RedirectToAction("TwoFactorLogin"); 
            }

            return View();
        }

        [HttpPost] 
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
    

