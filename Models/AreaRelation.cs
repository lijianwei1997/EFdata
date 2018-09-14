namespace EFData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AreaRelation")]
    public partial class AreaRelation
    {
        public int id { get; set; }

        public int? townid { get; set; }

        public int? parentid { get; set; }
    }
}
