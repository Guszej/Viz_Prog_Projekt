using System;
using System.Collections.Generic;

namespace MainPage.Models;

public partial class Game
{
    public int Id { get; set; }

    public string Név { get; set; } = null!;

    public string Megjelenés { get; set; } = null!;

    public string Készítő { get; set; } = null!;

    public string Típus { get; set; } = null!;

    public string Platform { get; set; } = null!;

    public string Mód { get; set; } = null!;

    public double GÉrtékelés { get; set; }

    public virtual ICollection<Kép> Képs { get; set; } = new List<Kép>();

    public virtual ICollection<Értékelé> Értékelés { get; set; } = new List<Értékelé>();
}
