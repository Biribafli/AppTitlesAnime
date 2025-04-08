using System;
using System.Collections.Generic;

namespace AppTitlesAnime.Models;

public partial class TitlesStatus
{
    public int Id { get; set; }

    public short IdStatus { get; set; }

    public int IdAnime { get; set; }

    public DateOnly? DateStatus { get; set; }

    // Навигационные свойства

    public virtual AnimeTitle AnimeTitles { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;
}
