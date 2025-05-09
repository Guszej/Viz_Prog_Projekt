using System;
using System.Collections.Generic;

namespace MainPage.Models
{
    public partial class Logok
    {
        public int Id { get; set; }
        public int FelhasználóId { get; set; }
        public DateTime Datum { get; set; } = DateTime.Now;
        public string Muvelet { get; set; }

        public virtual Felhasználó Felhasználó { get; set; }
    }
}
