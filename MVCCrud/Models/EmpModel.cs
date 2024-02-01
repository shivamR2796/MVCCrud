using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCCrud.Models
{
    public class EmpModel
    {
        public int EMP_ID { get; set; }
        [Required]
        public string EMP_NAME { get; set; }
        [Required]
        public Nullable<int> AGE { get; set; }
        [Required]
        public string CITY { get; set; }
    }
}