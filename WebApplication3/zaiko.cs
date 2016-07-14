namespace WebApplication3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("zaiko")]
    public partial class zaiko
    {
        [Key]
        public int id { get; set; }

        
        [StringLength(30)]
        public string JAN { get; set; }

        
        public int 店舗コード { get; set; }

        public int 在庫数 { get; set; }

        [StringLength(30)]
        public string 商品コード { get; set; }

        [ForeignKey("商品コード")]
        public virtual good good { get; set; }

        [ForeignKey("店舗コード")]
        public virtual SHOP SHOP { get; set; } 
    }

    //iqueryable型において，entityframeworkにおいて，iqueryableの結合したテーブルは，型が違うためキャストできない？
    //    そのため，テーブルを用意しておきlinqにて結合したテーブルを以下の変数に格納する形をとる．．．
    public class tablejoin
    {

        //updatemethodを用いる場合は，子クラスのidを新規テーブルに定義しておく
        public int id { get; set; }

        public SHOP shop { get; set; }

        public good good { get; set; }

        public string JAN { get; set; }

        public string 商品コード { get; set; }

        public string 商品名 { get; set; }

        public float 標準上代 { get; set; }

        public int 在庫数 { get; set; }

        public int 店舗コード { get; set; }

        public string 店舗名 { get; set; }

        public string 仕入先コード { get; set; }
    }

}
