using System;
using System.Collections.Generic;

namespace L02P02_2021_CC_650_2021_YD_650.Models;

public partial class Autore
{
    public int Id { get; set; }

    public string? Autor { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
