namespace D2_B2D3_Toetsgenerator.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ToetsOpgave")]
    public partial class ToetsOpgave
    {
        public int ID { get; set; }

        public int toetsID { get; set; }

        public int opgaveID { get; set; }

        public virtual Opgave Opgave { get; set; }

        public virtual Toets Toets { get; set; }
    }
}
