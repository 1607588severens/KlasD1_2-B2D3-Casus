namespace D1_2_B2D3_Casus_Toetsgenerator.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Toetsmatrijs
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Toetsmatrijs()
        {
            Kenniselement = new HashSet<Kenniselement>();
            Toets = new HashSet<Toets>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string moduleNaam { get; set; }

        [Required]
        [StringLength(50)]
        public string moduleCode { get; set; }

        public string makerID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? aanmaakdatum { get; set; }

        public string laatstGewijzigdDoor { get; set; }

        [Column(TypeName = "date")]
        public DateTime? datumGewijzigd { get; set; }

        public int prestatieIndicator { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kenniselement> Kenniselement { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Toets> Toets { get; set; }
    }
}
