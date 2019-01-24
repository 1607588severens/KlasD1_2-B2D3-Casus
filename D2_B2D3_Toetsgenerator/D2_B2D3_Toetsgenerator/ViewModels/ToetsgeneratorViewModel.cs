using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using D2_B2D3_Toetsgenerator.Models;

namespace D2_B2D3_Toetsgenerator.ViewModels
{
    public class ToetsgeneratorViewModel
    {
        public IEnumerable<Kenniselement> Kenniselement { get; set; }

        public IEnumerable<Opgave> Opgave { get; set; }

        public IEnumerable<Toets> Toets { get; set; }

        public IEnumerable<Toetsmatrijs> Toetsmatrijs { get; set; }

        public IEnumerable<ToetsOpgave> ToetsOpgave { get; set; }
    }
}