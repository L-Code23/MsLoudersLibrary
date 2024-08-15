using System;
using System.Collections.Generic;

namespace Mrs_Louders_Library.Models-f;

public partial class Book
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Subtitle { get; set; }

    public string? BookCover { get; set; }

    public bool? CheckedOut { get; set; }

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}
