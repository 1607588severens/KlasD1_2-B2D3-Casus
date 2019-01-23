namespace D2_B2D3_Toetsgenerator.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Toets
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Toets()
        {
            ToetsOpgave = new HashSet<ToetsOpgave>();
        }

        public int ID { get; set; }

        public int matrijsID { get; set; }

        [StringLength(50)]
        public string categorie { get; set; }

        [StringLength(10)]
        public string studiejaar { get; set; }

        public int? blokperiode { get; set; }

        public int? toetsgelegenheid { get; set; }

        public int? tijdsduur { get; set; }

        public bool? schrapformulier { get; set; }

        public string examinatoren { get; set; }

        public int? makerID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? aanmaakDatum { get; set; }

        public int? laatstGewijzigdDoor { get; set; }

        [Column(TypeName = "date")]
        public DateTime? datumGewijzigd { get; set; }

        public bool? status { get; set; }

        public virtual Toetsmatrijs Toetsmatrijs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ToetsOpgave> ToetsOpgave { get; set; }
    }
}
