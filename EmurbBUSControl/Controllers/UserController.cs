using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmurbBUSControl.Models.DataModels;
using EmurbBUSControl.Models.DataModels.BusinessRule;
using Microsoft.AspNetCore.Mvc;
using EmurbBUSControl.Models.SystemModels;

namespace EmurbBUSControl.Controllers
{
    [Route("API/{controller}/")]
    public class UserController : Controller
    {
        [HttpPost]
        [Route("Add/")]
        public ActionResult Add([FromBody] User user)
        {
            try
            {
                using (var userDAO = new UserDAO())
                {
                    if (userDAO.Add(user))
                    {
                        var emurbMail = new SystemMail();
                        emurbMail.SendNewPasswordMail(user, Request);

                        return StatusCode(201, new { Message = "Criado com sucesso" });
                    }

                }

                return StatusCode(201, new { Message = "Não criado" });
            }

            catch(Exception ex)
            {
                return StatusCode(424, new { Message = "Falha" });
            }
        }

        [HttpGet]
        [Route("Get/")]
        public ActionResult Get(int id)
        {
            try
            {
                using (var userDAO = new UserDAO())
                    return StatusCode(200, userDAO.Get(id));
            }

            catch(Exception ex)
            {
                return StatusCode(424, new { Message = "Erro ao obter usuário" });
            }
            
        }

        [HttpGet]
        [Route("Load/")]
        public ActionResult Load()
        {
            using (var userDAO = new UserDAO())
                return Json(userDAO.Load());
        }

        [HttpPatch]
        [Route("Change/")]
        public ActionResult Change(int id, [FromBody] User user)
        {
            using (var userDAO = new UserDAO())
                return Json(userDAO.Change(id, user));
        }

        [HttpPost]
        [Route("NewPassword/")]
        public ActionResult RequestPassword([FromBody] int id)
        {
            try
            {
                User user = null;
                
                using (var userDAO = new UserDAO())
                    user = userDAO.Get(id);

                var emurbMail = new SystemMail();

                if (user != null && emurbMail.SendNewPasswordMail(user, Request))
                    return StatusCode(201, new { Message = "Enviado" });

                return StatusCode(424, new { Message = "Erro ao gerar Token" });
            }
            
            catch(Exception ex)
            {
                return StatusCode(424, new { Message = "Houve um erro ao enviar o token para o email deste usuário" });
            }

        }

        [HttpPost]
        [Route("SetPassword/")]
        public ActionResult SetPassword(string token, string password)
        {
            return null;
        }

        [HttpDelete]
        [Route("Remove/")]
        public ActionResult Remove(int id)
        {
            using (var userDAO = new UserDAO())
                return Json(userDAO.Remove(id));
        }
    }
}