namespace WebApplication3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SHOP")]
    public partial class SHOP
    {
        
        public int id { get; set; }

        public int 店舗区分 { get; set; }

        [Key]
        public int 店舗コード { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(60)]
        public string 店舗名 { get; set; }

        
        public virtual ICollection<zaiko> zaikoes { get; set; }
    }
}
