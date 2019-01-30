namespace D1_2_B2D3_Casus_Toetsgenerator.Models
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

        [Required]
        [Display(Name = "Inhoud")]
        public string inhoud { get; set; }

        [Display(Name = "Score")]
        public int score { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Vraag type")]
        public string typeScore { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Categorie")]
        public string categorie { get; set; }

        [Required]
        [Display(Name = "Antwoorden")]
        public string antwoorden { get; set; }

        [Display(Name = "Openbaar")]
        public bool openbaar { get; set; }

        [Display(Name = "Aangemaakt door")]
        public string makerID { get; set; }

        [Display(Name = "Aanmaakdatum")]
        [Column(TypeName = "date")]
        public DateTime? aanmaakdatum { get; set; }

        [Display(Name = "Laatst gewijzigd door")]
        public string laatstGewijzigDoor { get; set; }

        [Display(Name = "Laats gewijzigd op")]
        [Column(TypeName = "date")]
        public DateTime? datumGewijzigd { get; set; }

        public bool isBackup { get; set; }

        public virtual Kenniselement Kenniselement { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ToetsOpgave> ToetsOpgave { get; set; }
    }
}
