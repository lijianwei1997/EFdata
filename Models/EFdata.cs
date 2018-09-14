namespace EFData
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using EFData.Models;

    public partial class EFdata : DbContext
    {
        public EFdata()
            : base("name=EFdata")
        {
        }

        public virtual DbSet<AreaInfo> AreaInfo { get; set; }
        public virtual DbSet<AreaRelation> AreaRelation { get; set; }
        public virtual DbSet<TownInfo> TownInfo { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<UserLogin> UserLogin { get; set; }
        public virtual DbSet<Error> Errors { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>()
                .Property(e => e.wxopenId)
                .IsUnicode(false);

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.sexy)
                .IsFixedLength();

            modelBuilder.Entity<UserLogin>()
                .Property(e => e.pid)
                .IsUnicode(false);

            modelBuilder.Entity<UserLogin>()
                .Property(e => e.mobileid)
                .IsUnicode(false);

            modelBuilder.Entity<UserLogin>()
                .Property(e => e.md5pass)
                .IsUnicode(false);
        }
    }
}
