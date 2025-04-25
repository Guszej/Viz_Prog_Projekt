using System;
using System.Collections.Generic;

namespace MainPage.Models;

public partial class Felhasználó
{
    public int Id { get; set; }

    public string Név { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Jelszó { get; set; } = null!;

    public string Rang { get; set; } = null!;

    public virtual ICollection<Értékelé> Értékelés { get; set; } = new List<Értékelé>();
}
