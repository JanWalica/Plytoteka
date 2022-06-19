using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plytoteka.DAL.Entities
{
    class Czlonek : ICRUDStrings
    {
        #region wlasciwosci
        public sbyte Artysta { get; set; }
        public sbyte Zespol { get; set; }
        public int StartWspolpracy { get; set; }
        public int? KoniecWspolpracy { get; set; }
        #endregion

        #region konstruktory
        public Czlonek(MySqlDataReader reader)
        {
            Artysta = sbyte.Parse(reader["id_artysty"].ToString());
            Zespol = sbyte.Parse(reader["id_zespolu"].ToString());
            StartWspolpracy = int.Parse(reader["pocz_wsp"].ToString());
            KoniecWspolpracy = int.Parse(reader["koniec_wsp"].ToString());
        }
        public Czlonek(sbyte artysta, sbyte zespol, int startWspolpracy, int? koniecWspolpracy)
        {
            Artysta = artysta;
            Zespol = zespol;
            StartWspolpracy = startWspolpracy;
            KoniecWspolpracy = koniecWspolpracy;
        }

        public Czlonek(Czlonek czlonek)
        {
            Artysta = czlonek.Artysta;
            Zespol = czlonek.Zespol;
            StartWspolpracy = czlonek.StartWspolpracy;
            KoniecWspolpracy = czlonek.KoniecWspolpracy;
        }
        #endregion

        #region metody
        //public override string ToString()
        //{
        //    return $"";
        //}

        public string ToInsert()
        {
            return $"('{Artysta}', " +
                $"'{Zespol}', " +
                $"'{StartWspolpracy}', " +
                $"'{KoniecWspolpracy}')";
        }

        public string ToUpdate()
        {
            return $"id_artysty='{Artysta}', " +
                $"id_zespolu='{Zespol}', " +
                $"pocz_wsp='{StartWspolpracy}', " +
                $"koniec_wsp='{KoniecWspolpracy}'";
        }

        public override bool Equals(object obj)
        {
            var czlonek = obj as Czlonek;
            if (czlonek is null) return false;
            if (Artysta != czlonek.Artysta) return false;
            if (Zespol != czlonek.Zespol) return false;
            if (StartWspolpracy != czlonek.StartWspolpracy) return false;
            if (KoniecWspolpracy != czlonek.KoniecWspolpracy) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}
