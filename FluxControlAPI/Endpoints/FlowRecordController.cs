using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluxControlAPI.Models;
using FluxControlAPI.Models.APIs;
using FluxControlAPI.Models.DataModels;
using FluxControlAPI.Models.DataModels.BusinessRule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FluxControlAPI.Endpoints
{
    
    [ApiController]
    // [Authorize("Bearer")]
    [Route("API/[controller]")]
    public class FlowRecordController : ControllerBase
    {
        [HttpPost]
        [Route("/ProcessImageBytes")]
        public ActionResult ProcessImageBytes([FromBody] string stringBytes)
        {
            try
            {
                if (!String.IsNullOrEmpty(stringBytes))
                {
                    byte[] bufferBytes = stringBytes.Split(",").Select(bs => Convert.ToByte(bs)).ToArray();

                    Task<string> recognizeTask = Task.Run(() => OpenALPR.ProcessImage(bufferBytes));
                    recognizeTask.Wait();

                    string response = recognizeTask.Result;

                    using (var recordFlowDAO = new FlowRecordDAO())
                        recordFlowDAO.Register(response, null);

                    // Printa resultado
                    SystemNotifier.SendNotificationAsync(response);

                    return StatusCode(200);
                }

                return StatusCode(406);
            }

            catch (Exception ex)
            {
                return StatusCode(500);
            }

            
        }

        [HttpPost]
        [Route("/Record")]
        public ActionResult Record(int busNumber, int userId)
        {
            try
            {
                User user = null;

                using (var userDAO = new UserDAO())
                    user = userDAO.Get(userId);

                if (user != null)
                {
                    int recordId = 0;

                    using (var FlowRecordDAO = new FlowRecordDAO())
                        FlowRecordDAO.Register(busNumber.ToString(), user);

                    if (recordId != 0)
                        return StatusCode(202, new { Message = "Registrado com sucesso" });
                }

                return StatusCode(404, new { Message = "Usuário não encontrado" });
            }

            catch (Exception ex)
            {
                return StatusCode(500, new { Exception = ex, Message = "Erro ao gravar registro no servidor" });
            }
            
        }
    }
}