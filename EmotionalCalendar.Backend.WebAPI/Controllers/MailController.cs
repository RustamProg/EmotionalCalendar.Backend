using EmotionalCalendar.Backend.Constracts.ApplicationUserContracts;
using EmotionalCalendar.Backend.Models.MailModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для управления отправкой писем
    /// </summary>
    [Route("api/mail")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IUserService _userService;

        public MailController(IUserService userService)
        {
            _userService = userService;
        }

        private readonly List<string> AdminMails = new List<string>
        {
            "gabdulbarov23@gmail.com",
            "mrsnakeworld@gmail.com"
        };

        /// <summary>
        /// Тестовый контроллер отправки сообщения по указанному адресу (ПИСЬМО НЕ АНОНИМНОЕ!!!)
        /// "email": "vladislavplotnikov54@gmail.com", "password": "21wq34er"
        /// </summary>
        /// <param name="model">Тело письма</param>
        /// <returns></returns>
        [HttpPost("test")]
        public async Task<IActionResult> SendMail(EmailModel model)
        {
            using (MailMessage mm = new MailMessage(model.Email, model.To))
            {
                mm.Subject = model.Subject;
                mm.Body = model.Body + $"<br/><h3><i>Отправлено пользователем {_userService.User.Username}</i><h3>";
                mm.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential(model.Email, model.Password);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                }
            }

            return Ok($"Письмо успешно отправлено, подписано от лица {_userService.User.Username}");
        }

        [HttpPost("support")]
        public async Task<IActionResult> SendSupportMessage(SupportMessage message)
        {
            foreach (var toEmail in AdminMails)
            {
                using (MailMessage mm = new MailMessage("vladislavplotnikov54@gmail.com", toEmail))
                {
                    mm.Subject = message.Subject;
                    mm.Body = message.Body + $"<br/><hr/><h5><i>Отправлено пользователем {_userService.User.Username} с адреса {message.SenderEmail}</i><h5>";
                    mm.IsBodyHtml = true;
                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential("vladislavplotnikov54@gmail.com", "21wq34er");
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);
                    }
                }
            }

            return Ok("Письмо отправлено в техническую поддержку");
        }
    }
}
