using EmurbBUSControl.Models.DataModels;
using EmurbBUSControl.Models.DataModels.BusinessRule;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit;
using MimeKit;
using MailKit.Net.Smtp;

namespace EmurbBUSControl.Models.SystemModels
{
    public class SystemMail
    {
        public bool SendNewPasswordMail(User user, HttpRequest request)
        {
            try
            {
                Token token = new Token(user);

                var message = new MimeMessage();

                message.From.Add(new MailboxAddress("Sistema Emurb", "sistema.emurb@gmail.com"));
                message.To.Add(new MailboxAddress(user.Email));

                message.Subject = "Nova Senha - " + user.Name;

                message.Body = new TextPart("plain")
                {
                    Text = string.Format(
                        @"Olá {0}, para fazer o login no sistema insira este email e a senha que você definir em {1}{2}/?t={3}.",
                        user.Name, (request.IsHttps ? "https://" : "http://"), request.Host, token.Hash
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

                using (var tokenDAO = new TokenDAO())
                    return tokenDAO.Add(token);

            }
            catch(Exception ex)
            {
                return false;
            }
                
        }
    }
}
