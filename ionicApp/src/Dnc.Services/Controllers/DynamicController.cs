using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dnc.DataAccessRepository.Repositories;
using Dnc.ApiModel;
using Dnc.Entities.Organization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Dnc.Entities.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IO;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Dnc.Services.Controllers
{
    [Route("api/[controller]")]
    public class DynamicController : Controller
    {
        private readonly IEntityRepository _DbService;
        private IHostingEnvironment hostingEnv;

        public DynamicController(IEntityRepository service, IHostingEnvironment env)
        {
            this._DbService = service;
            this.hostingEnv = env;
        }

        [HttpGet]
        [EnableCors("DncDemo")]
        public IEnumerable<DynamicApiModel> Get()
        {
            var count = _DbService.GetAll<Dynamic>().Count();
             if(count > 6 )
            {
                var boCollection = _DbService.GetAll<Dynamic>(x => x.Person).OrderBy(j => j.PublishDateTime).Skip(count - 6).OrderByDescending(s => s.PublishDateTime).ToList();

                var boAMCollection = new List<DynamicApiModel>();
                foreach (var item in boCollection)
                {
                    boAMCollection.Add(new DynamicApiModel(item));
                }
                return boAMCollection;
            }
            else
            {
                var boCollection = _DbService.GetAll<Dynamic>(x => x.Person).OrderByDescending(j => j.PublishDateTime).ToList();

                var boAMCollection = new List<DynamicApiModel>();
                foreach (var item in boCollection)
                {
                    boAMCollection.Add(new DynamicApiModel(item));
                }
                return boAMCollection;
            }
        }
        [HttpGet("GetLits/{number}")]
        [EnableCors("DncDemo")]
        public IEnumerable<DynamicApiModel> GetLits(int number)
        {
            var count = _DbService.GetAll<Dynamic>().Count();
            var count2 = count - number;
            if (count > 12)
            {
                var boCollection = _DbService.GetAll<Dynamic>(x => x.Person).OrderByDescending(j => j.PublishDateTime).Skip(number).OrderBy(j => j.PublishDateTime).Skip(count2 - 6).OrderByDescending(s => s.PublishDateTime).ToList();
                var boAMCollection = new List<DynamicApiModel>();
                foreach (var item in boCollection)
                {
                    boAMCollection.Add(new DynamicApiModel(item));
                }
                return boAMCollection;
            }
            else
            {
                var boCollection = _DbService.GetAll<Dynamic>(x => x.Person).OrderByDescending(j => j.PublishDateTime).Skip(6).ToList();
                var boAMCollection = new List<DynamicApiModel>();
                foreach (var item in boCollection)
                {
                    boAMCollection.Add(new DynamicApiModel(item));
                }
                return boAMCollection;
            }
        }

        [HttpGet("PersonDetailsGet/{id}")]
        [EnableCors("DncDemo")]
        public IEnumerable<DynamicDetailsApiModel> PersonDetailsGet(Guid id)
        {
            var boCollection = _DbService.GetAll<Dynamic>().Where(x => x.Person.ID == id).ToList();
            var boAMCollection = new List<DynamicDetailsApiModel>();
            foreach (var item in boCollection)
            {
                boAMCollection.Add(new DynamicDetailsApiModel(item));
            }
            return boAMCollection;
        }

        [HttpGet("HomeDetailsGet/{id}")]
        [EnableCors("DncDemo")]
        public DynamicApiModel HomeDetailsGet(Guid id)
        {
            var bo = _DbService.GetSingle<Dynamic>(id, x => x.Person);
            var boAM = new DynamicApiModel(bo);
            return boAM;
        }
        [HttpGet("{id}")]
        [EnableCors("DncDemo")]
        public IActionResult Delete(Guid id)
        {
            bool success;
            var dynamic = _DbService.GetSingleBy<Dynamic>(x => x.ID ==id);
            var message = _DbService.GetAll<Message>().Where(x => x.Dynamic.ID == id).ToList();
            if (message != null)
            { 
                foreach (var item in message)
                {
                    _DbService.DeleteAndSave(item);
                }
            }
            if (dynamic != null)
            {
                _DbService.DeleteAndSave(dynamic);
                success = true;
                return Ok(new { success });
            }
            else
                success = false;
            return Ok(new { success });
        }

        [HttpPost("Release")]
        [AllowAnonymous]
        [EnableCors("DncDemo")]
        public IActionResult Post([FromBody]DynamicInfo model)
        {

            string dyimagePath = @"../..";
            string wwwrdypath = @"/photo";
            bool success;
            var user = _DbService.GetSingleBy<Person>(x => x.ID == model.PersonID);

            if (user != null)
            {
                if (model.FileName != null)
                {
                    var r = new List<Dynamic>
                    {

                        new Dynamic
                        {
                            Person = user,
                            Content = model.Content,
                            Image = dyimagePath + wwwrdypath + $@"/{model.FileName}",
                            MessageNumber = 0,
                            Likes = 0,
                            PublishDateTime = DateTime.Now},

                        };
                    foreach (var item in r)
                    {
                        _DbService.AddAndSave(item);
                    }
                    success = true;
                    return Ok(new { success });
                }
                else
                {
                    var r = new List<Dynamic>
                    {

                        new Dynamic
                        {
                            Person = user,
                            Content = model.Content,
                            MessageNumber = 0,
                            Likes = 0,
                            PublishDateTime = DateTime.Now},

                        };
                    foreach (var item in r)
                    {
                        _DbService.AddAndSave(item);
                    }
                }
                success = true;
                return Ok(new { success });
            }
            else
            {
                success = false;
                return Ok(new { success });
            }

        }
        [HttpPost("UploadImage")]
        [AllowAnonymous]
        [EnableCors("DncDemo")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            long size = file.Length;
            string wwwrpath = @"\photo";
            int MaxContentLength = 1024 * 1024 * 10; //Size = 10 MB
            var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var filePath = hostingEnv.WebRootPath + wwwrpath + $@"\{filename}";
    
            if (file.Length < MaxContentLength)
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            return Ok(new { size, filePath });
        }
        [HttpPost("AddLikes")]
        [AllowAnonymous]
        [EnableCors("DncDemo")]
        public IActionResult PostAdd([FromBody]LikesInfo model)
        {
            bool success;
            var user = _DbService.GetSingleBy<Person>(x => x.ID == model.peid);
            var dynamic = _DbService.GetSingleBy<Dynamic>(x => x.ID == model.toid);
            if (user!=null&& dynamic!=null)
            {
                dynamic.Likes++;
                user.Likes++;
                _DbService.AddOrEditAndSave(dynamic);
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
            var dynamic = _DbService.GetSingleBy<Dynamic>(x => x.ID == model.toid);
            if (user != null && dynamic != null)
            {
                dynamic.Likes--;
                user.Likes--;
                _DbService.AddOrEditAndSave(dynamic);
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
