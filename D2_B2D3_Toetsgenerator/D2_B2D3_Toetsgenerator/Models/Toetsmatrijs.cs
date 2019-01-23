namespace D2_B2D3_Toetsgenerator.Models
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

        public int? makerID { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime? aanmaakdatum { get; set; }

        public int? laatstGewijzigdDoor { get; set; }

        [Column(TypeName = "date")]
        public DateTime? datumGewijzigd { get; set; }

        public int? prestatieIndicator { get; set; }

        [Required]
        [StringLength(50)]
        public string studiejaar { get; set; }

        [Required]
        public int? blokperiode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kenniselement> Kenniselement { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Toets> Toets { get; set; }
    }
}
