using IdentityEmail.Dtos;
using IdentityEmail.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;


namespace IdentityEmail.Controllers
{
    public class AccountController:Controller
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
        public async Task<IActionResult> Register(CreateUserRegisterDto createUserRegisterDto)
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
      _config["Email:Password"]  // "opwixhvfqygpzktp"
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
        }
    }

