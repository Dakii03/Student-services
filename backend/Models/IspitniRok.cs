using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class IspitniRok
{
    public int IdRoka { get; set; }

    public string Naziv { get; set; } = null!;

    public string SkolskaGod { get; set; } = null!;

    public string StatusRoka { get; set; } = null!;
}
