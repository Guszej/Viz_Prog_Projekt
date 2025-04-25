using System;
using System.Collections.Generic;

namespace MainPage.Models;

public partial class Értékelé
{
    public int FelhasználóId { get; set; }

    public int GameId { get; set; }

    public double FelhasználóÉrtékelés { get; set; }

    public int Id { get; set; }

    public virtual Felhasználó Felhasználó { get; set; } = null!;

    public virtual Game Game { get; set; } = null!;
}
