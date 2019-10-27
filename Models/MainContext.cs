using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace cs_api.Models {
    public partial class MainContext : DbContext {
        public MainContext() { }

        public MainContext(DbContextOptions<MainContext> options) : base(options) { }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Person> Person { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Address>(entity => {
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Title).HasColumnName("TITLE").HasMaxLength(255).IsUnicode(false);
                entity.Property(e => e.Country).HasColumnName("COUNTRY").HasMaxLength(255).IsUnicode(false);
            });

            modelBuilder.Entity<Person>(entity => {
                entity.Property(e => e.PersonId).HasColumnName("PERSON_ID");
                entity.Property(e => e.AddressId).HasColumnName("ADDRESS_ID");
                entity.Property(e => e.CreatedDate).HasColumnName("CREATED_DATE").HasColumnType("datetime");
                entity.Property(e => e.Hobbies).HasColumnName("HOBBIES").IsUnicode(false);
                entity.HasOne(d => d.Address).WithMany(p => p.Person).HasForeignKey(d => d.AddressId).HasConstraintName("FK__Person__Address");
            });
        }
    }
}