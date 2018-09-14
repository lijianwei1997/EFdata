namespace EFData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserLogin")]
    public partial class UserLogin
    {
        public int id { get; set; }

        [StringLength(50)]
        public string loginname { get; set; }

        [StringLength(20)]
        public string loginpwd { get; set; }

        [StringLength(50)]
        public string pid { get; set; }

        [StringLength(50)]
        public string mobileid { get; set; }

        public int? roleid { get; set; }

        public int? status { get; set; }

        [StringLength(50)]
        public string md5pass { get; set; }
    }
}
