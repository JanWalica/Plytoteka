using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Plytoteka.DAL.Entities
{
    class Czlonek : ICRUDStrings
    {
        #region wlasciwosci
        public ushort ArtystaId { get; set; }
        public ushort ZespolId { get; set; }
        public string Artysta { get; set; }
        public string Zespol { get; set; }
        public int StartWspolpracy { get; set; }
        public int? KoniecWspolpracy { get; set; }
        #endregion

        #region konstruktory
        public Czlonek(MySqlDataReader reader)
        {
            ArtystaId = ushort.Parse(reader["id_artysty"].ToString());
            ZespolId = ushort.Parse(reader["id_zespolu"].ToString());

            Artysta = reader["artysta"].ToString();
            Zespol = reader["nazwa_zespolu"].ToString();

            StartWspolpracy = int.Parse(reader["pocz_wsp"].ToString());
            KoniecWspolpracy = int.TryParse(reader["koniec_wsp"].ToString(), out var kw) ? kw : default;
        }
        public Czlonek(ushort artysta, ushort zespol, int startWspolpracy, int? koniecWspolpracy)
        {
            ArtystaId = artysta;
            ZespolId = zespol;
            StartWspolpracy = startWspolpracy;
            KoniecWspolpracy = koniecWspolpracy;
        }

        public Czlonek(Czlonek czlonek)
        {
            ArtystaId = czlonek.ArtystaId;
            ZespolId = czlonek.ZespolId;
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
            var koniec = KoniecWspolpracy == null ? "NULL" : KoniecWspolpracy.ToString();
            return $"('{ArtystaId}', " +
                $"'{ZespolId}', " +
                $"'{StartWspolpracy}', " +
                $"{koniec})";
        }

        public string ToUpdate()
        {
            return $"id_artysty='{ArtystaId}', " +
                $"id_zespolu='{ZespolId}', " +
                $"pocz_wsp='{StartWspolpracy}', " +
                $"koniec_wsp='{KoniecWspolpracy}'";
        }

        public override bool Equals(object obj)
        {
            var czlonek = obj as Czlonek;
            if (czlonek is null) return false;
            if (ArtystaId != czlonek.ArtystaId) return false;
            if (ZespolId != czlonek.ZespolId) return false;
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
