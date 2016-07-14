namespace WebApplication3
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

    

        public virtual DbSet<good> goods { get; set; }
        public virtual DbSet<SHOP> SHOPs { get; set; }
        public virtual DbSet<zaiko> zaikoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
