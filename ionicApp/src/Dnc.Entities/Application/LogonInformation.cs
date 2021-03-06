﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Entities.Application
{
    public class LogonInformation
    {
        [Required(ErrorMessage = "{0} 必须填写")]
        [Display(Name = "电话号码：")]
        public string Uumbers { get; set; }
        [Required(ErrorMessage = "{0} 必须填写")]
        [DataType(DataType.Password)]
        [Display(Name = "密码：")]
        public string Password { get; set; }
    }
}
