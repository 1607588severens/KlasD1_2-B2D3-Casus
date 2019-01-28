namespace D1_2_B2D3_Casus_Toetsgenerator.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }

        public virtual DbSet<Kenniselement> Kenniselement { get; set; }
        public virtual DbSet<Opgave> Opgave { get; set; }
        public virtual DbSet<Toets> Toets { get; set; }
        public virtual DbSet<Toetsmatrijs> Toetsmatrijs { get; set; }
        public virtual DbSet<ToetsOpgave> ToetsOpgave { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kenniselement>()
                .Property(e => e.inhoud)
                .IsUnicode(false);

            modelBuilder.Entity<Kenniselement>()
                .HasMany(e => e.Opgave)
                .WithRequired(e => e.Kenniselement)
                .HasForeignKey(e => e.elementID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Opgave>()
                .Property(e => e.inhoud)
                .IsUnicode(false);

            modelBuilder.Entity<Opgave>()
                .Property(e => e.typeScore)
                .IsUnicode(false);

            modelBuilder.Entity<Opgave>()
                .Property(e => e.categorie)
                .IsUnicode(false);

            modelBuilder.Entity<Opgave>()
                .Property(e => e.antwoorden)
                .IsUnicode(false);

            modelBuilder.Entity<Opgave>()
                .Property(e => e.makerID)
                .IsUnicode(false);

            modelBuilder.Entity<Opgave>()
                .Property(e => e.laatstGewijzigDoor)
                .IsUnicode(false);

            modelBuilder.Entity<Opgave>()
                .HasMany(e => e.ToetsOpgave)
                .WithRequired(e => e.Opgave)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Toets>()
                .Property(e => e.categorie)
                .IsUnicode(false);

            modelBuilder.Entity<Toets>()
                .Property(e => e.studiejaar)
                .IsUnicode(false);

            modelBuilder.Entity<Toets>()
                .Property(e => e.examinatoren)
                .IsUnicode(false);

            modelBuilder.Entity<Toets>()
                .Property(e => e.maker)
                .IsUnicode(false);

            modelBuilder.Entity<Toets>()
                .Property(e => e.laatstGewijzigdDoor)
                .IsUnicode(false);

            modelBuilder.Entity<Toets>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<Toets>()
                .HasMany(e => e.ToetsOpgave)
                .WithRequired(e => e.Toets)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Toetsmatrijs>()
                .Property(e => e.moduleNaam)
                .IsUnicode(false);

            modelBuilder.Entity<Toetsmatrijs>()
                .Property(e => e.moduleCode)
                .IsUnicode(false);

            modelBuilder.Entity<Toetsmatrijs>()
                .Property(e => e.makerID)
                .IsUnicode(false);

            modelBuilder.Entity<Toetsmatrijs>()
                .Property(e => e.laatstGewijzigdDoor)
                .IsUnicode(false);

            modelBuilder.Entity<Toetsmatrijs>()
                .HasMany(e => e.Kenniselement)
                .WithRequired(e => e.Toetsmatrijs)
                .HasForeignKey(e => e.matrijsID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Toetsmatrijs>()
                .HasMany(e => e.Toets)
                .WithRequired(e => e.Toetsmatrijs)
                .HasForeignKey(e => e.matrijsID)
                .WillCascadeOnDelete(false);
        }
    }
}
