using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EFData
{
    public  partial class Error
    {  [Key]
        public DateTime? errorTime { get; set; }

        public string errorReason { get; set; }

        public string errorData { get; set; }

        public string errorTable { get; set; }
   
    }
}