using Dnc.ApiModel;
using Dnc.DataAccessRepository.Repositories;
using Dnc.Entities.Application;
using Dnc.Entities.Organization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Services.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly IEntityRepository _DbService;

        public PersonController(IEntityRepository service)
        {
            this._DbService = service;
        }


        [HttpGet("GetById/{id}")]
        [EnableCors("DncDemo")]
        public DynamicPersonApiModel GetById(Guid id)
        {
            var bo = _DbService.GetSingleBy<Person>(x =>x.ID== id);
            var boAM = new DynamicPersonApiModel(bo);
            return boAM;
        }
    
        [HttpGet("GetSimpleByid/{id}")]
        [EnableCors("DncDemo")]
        public PersonSimpleApiModel GetSimpleByid(Guid id)
        {
            var bo = _DbService.GetSingleBy<Person>(x => x.ID == id);
            var boAM = new PersonSimpleApiModel(bo);
            return boAM;
        }
        [HttpGet("Concern/{id}")]
        [EnableCors("DncDemo")]
        public IEnumerable<ConcernApiModel> ConcernByid(Guid id)
        {
            var boCollection = _DbService.GetAll<Concern>(x =>x.toid).Where( x => x.myid.ID == id).ToList();
            var boAMCollection = new List<ConcernApiModel>();
            foreach (var item in boCollection)
            {
                boAMCollection.Add(new ConcernApiModel(item));
            }
            return boAMCollection;
        }
        
        [HttpPost("Concern")]
        [EnableCors("DncDemo")]
        public IActionResult GetConcernByid([FromBody]ConcernInfo model)
        {
            var hasConcern = false;
            Guid concernId ;
            var user1 = _DbService.GetSingleBy<Person>(x => x.ID == model.myid);
            var user2 = _DbService.GetSingleBy<Person>(x => x.ID == model.toid);
            var bo = _DbService.GetSingleBy<Concern>(x => x.toid == user2 && x.myid == user1);
            if (bo == null)
            {
                 hasConcern = true;
                 concernId = bo.ID;
            }
            else
            {
                hasConcern = true;
                concernId = bo.ID;
            }
            return Ok(new { hasConcern, concernId }); 
        }
        [HttpPost("AddConcern")]
        [AllowAnonymous]
        [EnableCors("DncDemo")]
        public IActionResult PostAdd([FromBody]ConcernInfo model)
        {
            bool success;
            var user1 = _DbService.GetSingleBy<Person>(x => x.ID == model.myid);
            var user2 = _DbService.GetSingleBy<Person>(x => x.ID == model.toid);
            if (user1 != null && user2!=null)
            {
                var r = new List<Concern>
                {
                    new Concern
                    {
                        myid = user1,
                        toid =user2
                     },

                };
                foreach (var item in r)
                {
                    _DbService.AddAndSave(item);
                }
                user1.Concern++;
                _DbService.AddOrEditAndSave(user1);
                success = true;
                return Ok(new { success });
            }
            else
            {
                success = false;
                return Ok(new { success });
            }
        }
        [HttpPost("ReConcern")]
        [AllowAnonymous]
        [EnableCors("DncDemo")]
        public IActionResult PostRe([FromBody]ConcernInfo model)
        {
            bool success;
            var user1 = _DbService.GetSingleBy<Person>(x => x.ID == model.myid);
            var user2 = _DbService.GetSingleBy<Person>(x => x.ID == model.toid);
            var concern = _DbService.GetSingleBy<Concern>(x => x.ID == model.id);
            if (user1 != null && user2 != null && concern!= null)
            {
                user1.Concern--;
                _DbService.AddOrEditAndSave(user1);
                _DbService.DeleteAndSave(concern);
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
