﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trimfit.Models
{
    public class CustomerClub
    {
        public int Customer_Club_Id { get; set; }
        public int Customer_Id { get; set; }
        public int Club_Id { get; set; }
    }
}