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
    [Route("API/[controller]/")]
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
                    user.Id = userDAO.Add(user);

                    if (user.Id != 0)
                    {
                        Token token = new Token(user);
                        var emurbMail = new SystemMail();

                        if (emurbMail.SendNewPasswordMail(Request, token))
                        {
                            using (var tokenDAO = new TokenDAO())
                                tokenDAO.Add(token);

                            return StatusCode(201, new { Message = "Adicionado com sucesso" });
                        }
                            
                        return StatusCode(424, new { Message = "Falha ao enviar email" });
                    }
                }

                return StatusCode(304, new { Message = "Não adicionado" });
            }

            catch(Exception ex)
            {
                return StatusCode(500, new { Message = "Falha" });
            }
        }

        [HttpGet]
        [Route("Get/{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                using (var userDAO = new UserDAO())
                    return StatusCode(200, userDAO.Get(id));
            }

            catch(Exception ex)
            {
                return StatusCode(500, new { Message = "Falha ao obter usuário" });
            }
            
        }

        [HttpGet]
        [Route("Load/")]
        public ActionResult Load()
        {
            try
            {
                using (var userDAO = new UserDAO())
                    return StatusCode(200, userDAO.Load());
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { Message = "Falha ao carregar usuários" });
            }
            
        }

        [HttpPatch]
        [Route("Change/{id}")]
        public ActionResult Change(int id, [FromBody] User user)
        {
            try
            {
                using (var userDAO = new UserDAO())
                    if (userDAO.Change(id, user))
                        return StatusCode(200, new { Message = "Alterado com sucesso" });

                return StatusCode(304, new { Message = "Não alterado" });
            }

            catch(Exception ex)
            {
                return StatusCode(500, new { Message = "Falha ao alterar usuário" });
            }
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

                Token token = new Token(user);
                var emurbMail = new SystemMail();

                if (user != null && emurbMail.SendNewPasswordMail(Request, token))
                {
                    using (var tokenDAO = new TokenDAO())
                        tokenDAO.Add(token);

                    return StatusCode(200, new { Message = "Enviado" });
                }

                return StatusCode(424, new { Message = "Erro ao gerar Token" });
            }
            
            catch(Exception ex)
            {
                return StatusCode(500, new { Message = "Houve um erro ao enviar o token para o email deste usuário" });
            }

        }

        [HttpPost]
        [Route("GetToken/")]
        public ActionResult GetToken([FromBody] string token)
        {
            try
            {
                Token validToken;

                using (var tokenDAO = new TokenDAO())
                    validToken = tokenDAO.GetByHash(token);

                return (validToken != null) ? StatusCode(200, validToken) : StatusCode(406, new { Message = "Token inválido" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Falha ao obter token" });
            }

        }

        [HttpPost]
        [Route("DefinePassword/{token}")]
        public ActionResult SetPassword(string token, [FromBody] string password)
        {
            try
            {
                using (var tokenDAO = new TokenDAO())
                {
                    var validToken = tokenDAO.GetByHash(token);

                    if (validToken != null && !string.IsNullOrEmpty(password))
                    {
                        using (var userDAO = new UserDAO())
                            if (userDAO.SetPassword(validToken.User.Id, password))
                                tokenDAO.Remove(validToken.Code);
                        
                        return StatusCode(202, new { Message = "Senha definida" });
                    }

                    return StatusCode(406, new { Message = "Token inválido" });
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { Message = "Falha ao definir senha" });
            }
                
        }

        [HttpDelete]
        [Route("Remove/{id}")]
        public ActionResult Remove(int id)
        {
            try
            {
                using (var userDAO = new UserDAO())
                    if (userDAO.Remove(id))
                        return StatusCode(200, new { Message = "Removido" } );

                return StatusCode(304, new { Message = "Não Removido" });
            }
            
            catch(Exception ex)
            {
                return StatusCode(500, new { Message = "Falha ao remover" });
            }
        }
    }
}