using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class StudentPredmet
{
    public short IdStudenta { get; set; }

    public short IdPredmeta { get; set; }

    public string SkolskaGodina { get; set; } = null!;
}
