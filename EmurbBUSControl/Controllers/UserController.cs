using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmurbBUSControl.Models.DataModels;
using EmurbBUSControl.Models.DataModels.BusinessRule;
using Microsoft.AspNetCore.Mvc;
using MailKit;
using MimeKit;
using MailKit.Net.Smtp;
using EmurbBUSControl.Models.SystemModels;

namespace EmurbBUSControl.Controllers
{
    [Route("API/{controller}/")]
    public class UserController : Controller
    {
        public UserController()
        {

        }

        [HttpPost]
        public ActionResult Add([FromBody] User user)
        {
            using (var userDAO = new UserDAO())
                return Json(userDAO.Add(user));
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            using (var userDAO = new UserDAO())
                return Json(userDAO.Get(id));
        }

        [HttpGet]
        [Route("Load/")]
        public ActionResult Load()
        {
            using (var userDAO = new UserDAO())
                return Json(userDAO.Load());
        }

        [HttpPatch]
        public ActionResult Change(int id, [FromBody] User user)
        {
            using (var userDAO = new UserDAO())
                return Json(userDAO.Change(id, user));
        }

        [HttpPost]
        [Route("RequestPassword/")]
        public ActionResult RequestPassword([FromBody] int id)
        {
            try
            {
                User user = null;
                
                using (var userDAO = new UserDAO())
                    user = userDAO.Get(id);

                Token token = new Token(user);

                using (var tokenDAO = new TokenDAO())
                    tokenDAO.Add(token);

                var message = new MimeMessage();

                message.From.Add(new MailboxAddress("Sistema Emurb", "sistema.emurb@gmail.com"));
                message.To.Add(new MailboxAddress(user.Email));

                message.Subject = "Nova Senha - " + user.Name;

                message.Body = new TextPart("plain")
                {
                    Text = string.Format(
                        @"Olá {0}, para fazer o login no sistema insira este email e a senha que você definir em {1}{2}/?t={3}.",
                        user.Name, (Request.IsHttps ? "https://" : "http://"), Request.Host, token.Hash
                        )
                };

                using (var client = new SmtpClient())
                {
                    #if DEBUG
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    #endif

                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("emurb.sistema@gmail.com", "fluxcontrol321");

                    client.Send(message);
                    client.Disconnect(true);
                }

                return StatusCode(201, new { Message = "Enviado" });
            }
            
            catch(Exception ex)
            {
                return StatusCode(424, new { Message = "Houve um erro ao enviar o token para o email deste usuário." });
            }

        }

        [HttpPost]
        public ActionResult SetPassword(string token, string password)
        {
            return null;
        }

        [HttpDelete]
        public ActionResult Remove(int id)
        {
            using (var userDAO = new UserDAO())
                return Json(userDAO.Remove(id));
        }
    }
}