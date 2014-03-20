﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSports.Models
{
    public class User
    {
        public int id { get; set; }
        //Person
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string email { get; set; }
        public DateTime dateOfBirth { get; set; }
        //Address 
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        //Address Type
        public string addressType { get; set; }
        //Phone
        public string number { get; set; }
        //Phone Type
        public string phoneType { get; set; }
    }
}