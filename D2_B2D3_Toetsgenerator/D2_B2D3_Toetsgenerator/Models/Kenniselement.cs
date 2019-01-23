namespace D2_B2D3_Toetsgenerator.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kenniselement")]
    public partial class Kenniselement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kenniselement()
        {
            Opgave = new HashSet<Opgave>();
        }

        public int ID { get; set; }

        public int matrijsID { get; set; }

        public string inhoud { get; set; }

        public int? aantalReproductie { get; set; }

        public int? aantalBegrip { get; set; }

        public int? aantalToepassing { get; set; }

        public virtual Toetsmatrijs Toetsmatrijs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Opgave> Opgave { get; set; }
    }
}
