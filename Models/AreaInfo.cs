namespace EFData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AreaInfo")]
    public partial class AreaInfo
    {
        public int id { get; set; }

        [StringLength(50)]
        public string areaname { get; set; }
    }
}
