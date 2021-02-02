using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using VkNet;
using VkNet.Abstractions;
using VkNet.Model;
using VkNet.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetChat.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallBackController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IVkApi _vkApi;
        public CallBackController(IConfiguration configuration, IVkApi api)
        {
            _configuration = configuration;
            _vkApi = api;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post([FromBody] Updates updates)
        {
            if (updates.Type == "confirmation")
                return Ok(_configuration["Config:Confirmation"]);

            if (updates.Type == "message_new")
            {
                var msg = Message.FromJson(new VkResponse(updates.Object));
                _vkApi.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams
                {
                    RandomId = new DateTime().Millisecond,
                    PeerId = msg.PeerId.Value,
                    Message = msg.Text
                });
            }

            return Ok("ok");
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
