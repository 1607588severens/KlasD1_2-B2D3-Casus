namespace D2_B2D3_Toetsgenerator.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Opgave")]
    public partial class Opgave
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Opgave()
        {
            ToetsOpgave = new HashSet<ToetsOpgave>();
        }

        public int ID { get; set; }

        public int elementID { get; set; }

        public string inhoud { get; set; }

        public int? score { get; set; }

        public string antwoorden { get; set; }

        public bool? openbaar { get; set; }

        public int? makerID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? aanmaakdatum { get; set; }

        public int? laatstGewijzigDoor { get; set; }

        [Column(TypeName = "date")]
        public DateTime? datumGewijzigd { get; set; }

        [StringLength(50)]
        public string categorie { get; set; }

        public bool? isBackup { get; set; }

        public virtual Kenniselement Kenniselement { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ToetsOpgave> ToetsOpgave { get; set; }
    }
}
