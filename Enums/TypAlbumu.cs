using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Plytoteka.Enums
{
    enum TypAlbumu
    {
        [Display(Name = "album studyjny")]
        album_studyjny,
        soundtrack,
        kompilacja,
        [Display(Name = "album koncertowy")]
        album_koncertowy,
        remix,
        cover,
        demo,
        [Display(Name = "album internetowy")]
        album_internetowy,
        [Display(Name = "album promocyjny")]
        album_promocyjny,
        minialbum,
        split,
    }
}
