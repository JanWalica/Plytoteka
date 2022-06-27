using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Plytoteka.Enums
{
    enum Gatunek
    {
        rock,
        jazz,
        pop,
        metal,
        [Display(Name = "disco polo")]
        disco_polo,
        reggae,
        blues,
        rap,
        country,
        [Display(Name = "muzyka klasyczna")]
        muzyka_klasyczna,
        punk
    }
}
