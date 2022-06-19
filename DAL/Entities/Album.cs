using MySql.Data.MySqlClient;
using Plytoteka.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plytoteka.DAL.Entities
{
    class Album : ICRUDStrings
    {
        #region wlasnosci
        public sbyte? Id { get; set; }
        public sbyte Zespol { get; set; }
        public string Tytul { get; set; }
        public int DataWydania { get; set; }
        public int? Dlugosc { get; set; }
        public int? IleUtworow { get; set; }
        public string Wydawca { get; set; }
        public TypAlbumu Typ { get; set; }
        #endregion

        #region konstruktory
        public Album(MySqlDataReader reader)
        {
            Id = sbyte.Parse(reader["id_albumu"].ToString());
            Zespol = sbyte.Parse(reader["id_zespolu1"].ToString());
            Tytul = reader["tytul"].ToString();
            DataWydania = int.Parse(reader["data_wyd"].ToString());
            Dlugosc = int.Parse(reader["dlugosc"].ToString());
            IleUtworow = int.Parse(reader["ile_utworow"].ToString());
            Wydawca = reader["wydawca"].ToString();
            Typ = (TypAlbumu)Enum.Parse(typeof(TypAlbumu), reader["typ"].ToString().Trim().ToLower().Replace(" ", "_"));
        }

        public Album(sbyte zespol, string tytul, int dataWydania, string wydawca, TypAlbumu typ)
        {
            Id = null;
            Zespol = zespol;
            Tytul = tytul;
            DataWydania = dataWydania;
            Dlugosc = null;
            IleUtworow = null;
            Wydawca = wydawca;
            Typ = typ;
        }

        public Album(Album album)
        {
            Id = album.Id;
            Zespol = album.Zespol;
            Tytul = album.Tytul;
            DataWydania = album.DataWydania;
            Dlugosc = album.Dlugosc;
            IleUtworow = album.IleUtworow;
            Wydawca = album.Wydawca;
            Typ = album.Typ;
        }
        #endregion

        #region metody
        public override string ToString()
        {
            return $"{Tytul}";
        }

        public string ToInsert()
        {
            return $"('{Zespol}', " +
                $"'{Tytul}', " +
                $"'{DataWydania}', " +
                $"'{Wydawca}', " +
                $"'{Typ.GetDisplayName()}')";
        }

        public string ToUpdate()
        {
            return $"id_zespolu1='{Zespol}', " +
                $"tytul='{Tytul}', " +
                $"data_wyd='{DataWydania}', " +
                $"wydawca='{Wydawca}', " +
                $"typ='{Typ.GetDisplayName()}'";
        }

        public override bool Equals(object obj)
        {
            var album = obj as Album;
            if (album is null) return false;
            if (Zespol != album.Zespol) return false;
            if (Tytul.ToLower() != album.Tytul.ToLower()) return false;
            if (DataWydania != album.DataWydania) return false;
            if (Dlugosc != album.Dlugosc) return false;
            if (IleUtworow != album.IleUtworow) return false;
            if (Wydawca.ToLower() != album.Wydawca.ToLower()) return false;
            if (Typ != album.Typ) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}
