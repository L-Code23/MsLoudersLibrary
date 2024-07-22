using System;
using System.Collections.Generic;

namespace Mrs_Louders_Library.Models;

public partial class Author
{
    public int Id { get; set; }

    public string? Author1 { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
