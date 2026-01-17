using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class Zapisnik
{
    public short IdStudenta { get; set; }

    public short IdIspita { get; set; }

    public float Ocena { get; set; }

    public string Bodovi { get; set; } = null!;
}
