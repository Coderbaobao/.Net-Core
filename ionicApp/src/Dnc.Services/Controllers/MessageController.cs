using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dnc.DataAccessRepository.Repositories;
using Microsoft.AspNetCore.Cors;
using Dnc.Entities.Organization;
using Dnc.ApiModel;
using Microsoft.AspNetCore.Authorization;
using Dnc.Entities.Application;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Dnc.Services.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly IEntityRepository _DbService;

        public MessageController(IEntityRepository service)
        {
            this._DbService = service;
        }

        [HttpGet("GetByTypeID/{id}")]
        [EnableCors("DncDemo")]
        public IEnumerable<MessageApiModel> GetByTypeID(Guid id)
        {
            var boCollection = _DbService.GetAll<Message>(x => x.Person).Where(x => x.Dynamic.ID == id).OrderByDescending(j=>j.Likes).ToList();
            var boAMCollection = new List<MessageApiModel>();
            foreach (var item in boCollection)
            {
                boAMCollection.Add(new MessageApiModel(item));
            }
            return boAMCollection;
        }
        [HttpPost]
        [AllowAnonymous]
        [EnableCors("DncDemo")]
        public IActionResult Post ([FromBody] MessageInfo model)
        {
            var dynamic = _DbService.GetSingleBy<Dynamic>(x => x.ID == model.Dynamicid);
            var user = _DbService.GetSingleBy<Person>(x => x.ID == model.Personid);
            var r = new List<Message>
                {
                    new Message
                    {
                        Person = user,
                        Dynamic = dynamic,
                        Content = model.Content,
                        Likes = 0,
                        Messagestime = DateTime.Now
                    },

                };
            foreach (var item in r)
            {
                _DbService.AddAndSave(item);
            }
            dynamic.MessageNumber++;
            _DbService.AddOrEditAndSave(dynamic);
            return Ok(new { message = "OK" });
        }
        [HttpPost("AddLikes")]
        [AllowAnonymous]
        [EnableCors("DncDemo")]
        public IActionResult PostAdd([FromBody]LikesInfo model)
        {
            bool success;
            var user = _DbService.GetSingleBy<Person>(x => x.ID == model.peid);
            var message = _DbService.GetSingleBy<Message>(x => x.ID == model.toid);
            if (user != null && message != null)
            {
                message.Likes++;
                _DbService.AddOrEditAndSave(message);
                success = true;
                return Ok(new { success });
            }
            else
            {
                success = false;
                return Ok(new { success });
            }
        }
        [HttpPost("ReLikes")]
        [AllowAnonymous]
        [EnableCors("DncDemo")]
        public IActionResult PostRe([FromBody]LikesInfo model)
        {
            bool success;
            var user = _DbService.GetSingleBy<Person>(x => x.ID == model.peid);
            var message = _DbService.GetSingleBy<Message>(x => x.ID == model.toid);
            if (user != null && message != null)
            {
                message.Likes--;
                _DbService.AddOrEditAndSave(message);
                success = true;
                return Ok(new { success });
            }
            else
            {
                success = false;
                return Ok(new { success });
            }
        }

    }
}
