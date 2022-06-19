using System;
using System.Collections.Generic;
using System.Text;

namespace Plytoteka.DAL.Entities
{
    interface ICRUDStrings
    {
        public string ToInsert();

        public string ToUpdate();
    }
}
