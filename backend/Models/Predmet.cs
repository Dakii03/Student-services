using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class Predmet
{
    public short IdPredmeta { get; set; }

    public short IdProfesora { get; set; }

    public string Naziv { get; set; } = null!;

    public short Espb { get; set; }

    public string Status { get; set; } = null!;
}
