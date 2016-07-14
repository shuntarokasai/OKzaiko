namespace WebApplication3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("good")]
    public partial class good
    {
        
        public int id { get; set; }

        [StringLength(30)]
        [Key]
        public string 商品コード { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(60)]
        public string 商品名 { get; set; }

        
        [StringLength(30)]
        public string JAN { get; set; }

        public float 標準原単価 { get; set; }

        public float 標準上代 { get; set; }

        [StringLength(30)]
        public string 仕入先コード { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(60)]
        public string 仕入先名 { get; set; }

        public virtual ICollection<zaiko> zaikoes { get; set; }
    }
}
