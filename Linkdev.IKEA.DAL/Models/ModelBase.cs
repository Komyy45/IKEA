﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkdev.IKEA.DAL.Models
{
    public class ModelBase
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
