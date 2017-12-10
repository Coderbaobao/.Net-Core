using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dnc.DataAccessRepository.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Dnc.Entities.Application;
using Microsoft.AspNetCore.Http;
using Dnc.Entities.Organization;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Dnc.Services.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IEntityRepository _Service;
        private IHostingEnvironment hostingEnv;

        public AccountController(IEntityRepository service, IHostingEnvironment env)
        {
            this._Service = service;
            this.hostingEnv = env;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [EnableCors("DncDemo")]
        public IActionResult Postlogin([FromBody]LogonInformation model)
        {
            var IsLogon = false;
            Guid UserId ;
            var user = _Service.GetSingleBy<Person>(x => x.Uumbers == model.Uumbers && x.Password == model.Password);
            if (user != null)
            {
                // 处理登录的状态
                IsLogon = true;
                UserId = user.ID;
            }
            return Ok(new { IsLogon, UserId });
        }
        [HttpPost("changePassword")]
        [AllowAnonymous]
        [EnableCors("DncDemo")]
        public IActionResult changePassword([FromBody]ChangePasswordInfo model)
        {
            var Ischange = false;
            var user = _Service.GetSingleBy<Person>(x => x.ID == model.userid);
            if (user != null)
            {
                if (user.Password == model.PasswordOld)
                {
                    user.Password = model.PasswordNew;
                    _Service.AddOrEditAndSave(user);
                    // 处理登录的状态
                    Ischange = true;
                }
                else
                {
                    Ischange = false;
                }
                
            }
            return Ok(new { Ischange });
        }
        [HttpPost("PutName")]
        [AllowAnonymous]
        [EnableCors("DncDemo")]
        public IActionResult PutName([FromBody]UpdateNameInfo model)
        {
            var Ischange = false;
            var user = _Service.GetSingleBy<Person>(x => x.ID == model.id);
            if (user != null)
            {
                user.Nickname = model.name;
                 _Service.AddOrEditAndSave(user);
                  // 处理登录的状态
                 Ischange = true;
            }
            return Ok(new { Ischange });
        }
        [HttpPost("PutDescriptione")]
        [AllowAnonymous]
        [EnableCors("DncDemo")]
        public IActionResult PutDescriptione([FromBody]UpdateNameInfo model)
        {
            var Ischange = false;
            var user = _Service.GetSingleBy<Person>(x => x.ID == model.id);
            if (user != null)
            {
                user.Description = model.name;
                _Service.AddOrEditAndSave(user);
                // 处理登录的状态
                Ischange = true;
            }
            return Ok(new { Ischange });
        }
        [HttpPost("PutImage")]
        [AllowAnonymous]
        [EnableCors("DncDemo")]
        public IActionResult PutImage([FromBody]UpdateImageInfo model)
        {
            string PermagePath = @"../..";
            string wwwrdypath = @"/portrait";
            var Ischange = false;
            var user = _Service.GetSingleBy<Person>(x => x.ID == model.id);
            if (user != null)
            {
                if (model.image != null)
                {
                    user.UserImage = PermagePath + wwwrdypath + $@"/{model.image}";
                    _Service.AddOrEditAndSave(user);
                    // 处理登录的状态
                    Ischange = true;
                }
                else
                    Ischange = false;
            }
            return Ok(new { Ischange });
        }
        [HttpPost("UploadImage")]
        [AllowAnonymous]
        [EnableCors("DncDemo")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            long size = file.Length;
            string wwwrpath = @"\portrait";
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
        [HttpPost("register")]
        [AllowAnonymous]
        [EnableCors("DncDemo")]
        public IActionResult Postregister([FromBody]LogonInformation model)
        {

            var IsRegister = false;
            var user = _Service.GetSingleBy<Person>(x => x.Uumbers == model.Uumbers);
            if (user == null)
            {
                Random rd = new Random();
                string str = "";
                while (str.Length < 10)
                {
                    int temp = rd.Next(0, 10);
                    if (!str.Contains(temp + ""))
                    {
                        str += temp;
                    }
                }
                var r = new List<Person>
                {
                    new Person
                    {
                        Nickname =  str,
                        Uumbers = model.Uumbers,
                        Password = model.Password,
                        UserImage ="../../portrait/wu.png",
                        Concern = 0,
                        Likes = 0,
                     },

                };
                foreach (var item in r)
                {
                    _Service.AddAndSave(item);
                }
                // 处理登录的状态
                IsRegister = true;
            }
            else
                IsRegister = false;
            return Ok(new { IsRegister });
        }
    }
}
