namespace EFData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserInfo")]
    public partial class UserInfo
    {
        public int id { get; set; }

        public int? userid { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [StringLength(50)]
        public string duty { get; set; }

        [StringLength(50)]
        public string company { get; set; }

        [StringLength(50)]
        public string qqnumber { get; set; }

        [StringLength(50)]
        public string wxnumber { get; set; }

        [StringLength(50)]
        public string zfbnumber { get; set; }

        [StringLength(500)]
        public string address { get; set; }

        public int? status { get; set; }

        public int? partymemberid { get; set; }

        [StringLength(500)]
        public string imgheader { get; set; }

        [StringLength(500)]
        public string partybrach { get; set; }

        public DateTime? insertTime { get; set; }

        [StringLength(200)]
        public string wxopenId { get; set; }

        public int? areaid { get; set; }

        [StringLength(50)]
        public string township { get; set; }

        [StringLength(50)]
        public string town { get; set; }

        public DateTime? partyinsertTime { get; set; }

        [StringLength(50)]
        public string educationXL { get; set; }

        [StringLength(50)]
        public string nationalityGJ { get; set; }

        [StringLength(50)]
        public string nationMZ { get; set; }

        [StringLength(10)]
        public string birthday { get; set; }

        [StringLength(4)]
        public string sexy { get; set; }
    }
}
