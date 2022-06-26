using MySql.Data.MySqlClient;
using Plytoteka.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Plytoteka.DAL.Entities
{
    class Album : ICRUDStrings
    {
        #region wlasnosci
        public ushort? Id { get; set; }
        public ushort? ZespolId { get; set; }
        public string? Zespol { get; set; }
        public string Tytul { get; set; }
        public int? DataWydania { get; set; }
        public string? Dlugosc { get; set; }
        public int? IleUtworow { get; set; }
        public string? Wydawca { get; set; }
        public TypAlbumu? Typ { get; set; }
        #endregion

        #region konstruktory
        public Album(MySqlDataReader reader)
        {
            Id = ushort.Parse(reader["id_albumu"].ToString());
            ZespolId = ushort.TryParse(reader["id_zespolu1"].ToString(), out var z) ? z : default;
            Zespol = reader["nazwa"]?.ToString();
            Tytul = reader["tytul"].ToString();
            DataWydania = int.TryParse(reader["data_wyd"].ToString(), out var dw) ? dw : default;

            double seconds = int.TryParse(reader["dlugosc"].ToString(), out var d) ? d : default;
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            Dlugosc = time.ToString(@"hh\:mm\:ss");

            IleUtworow = int.TryParse(reader["ile_utworow"].ToString(), out var iu) ? iu : default;
            Wydawca = reader["wydawca"]?.ToString();
            Typ = (TypAlbumu?)(Enum.TryParse(typeof(TypAlbumu), reader["typ"].ToString().Trim().ToLower().Replace(" ", "_"), out var t) ? t : default);
        }

        public Album(ushort? zespol, string tytul, int? dataWydania, string wydawca, TypAlbumu? typ)
        {
            Id = null;
            ZespolId = zespol;
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
            ZespolId = album.ZespolId;
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
            return $"('{ZespolId}', " +
                $"'{Tytul}', " +
                $"'{DataWydania}', " +
                $"'{Wydawca}', " +
                $"'{Typ?.GetDisplayName()}')";
        }

        public string ToUpdate()
        {
            return $"id_zespolu1='{ZespolId}', " +
                $"tytul='{Tytul}', " +
                $"data_wyd='{DataWydania}', " +
                $"wydawca='{Wydawca}', " +
                $"typ='{Typ?.GetDisplayName()}'";
        }

        public override bool Equals(object obj)
        {
            var album = obj as Album;
            if (album is null) return false;
            if (ZespolId != album.ZespolId) return false;
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
