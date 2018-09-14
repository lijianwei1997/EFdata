namespace EFData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TownInfo")]
    public partial class TownInfo
    {
        public int id { get; set; }

        [StringLength(50)]
        public string townname { get; set; }
    }
}
