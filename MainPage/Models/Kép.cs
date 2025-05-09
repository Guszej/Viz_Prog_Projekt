using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace MainPage.Models;

public partial class Kép
{
    public int GameId { get; set; }

    public int KépId { get; set; }

    public string Utvonal { get; set; }

    public virtual Game Game { get; set; } = null!;

}
