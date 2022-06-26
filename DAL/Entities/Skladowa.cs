using MySql.Data.MySqlClient;
using Plytoteka.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plytoteka.DAL.Entities
{
    class Skladowa : ICRUDStrings
    {
        #region wlasnosci
        public ushort AlbumId { get; set; }
        public ushort UtworId { get; set; }
        public string Album { get; set; }
        public string Utwor { get; set; }
        public int Dlugosc { get; set; }
        public Gatunek Gatunek { get; set; }
        #endregion

        #region konstruktory
        public Skladowa(MySqlDataReader reader)
        {
            AlbumId = ushort.Parse(reader["id_albumu1"].ToString());
            UtworId = ushort.Parse(reader["id_utworu"].ToString());

            Album = reader["tytul_albumu"].ToString();
            Utwor = reader["tytul_utworu"].ToString();
            Dlugosc = int.Parse(reader["dlugosc"].ToString());

            Gatunek = (Gatunek)Enum.Parse(typeof(Gatunek), reader["gatunek"].ToString().Trim().ToLower().Replace(" ", "_"));
        }

        public Skladowa(ushort id_albumu, ushort id_utworu, int dlugosc, Gatunek gatunek)
        {
            AlbumId = id_albumu;
            UtworId = id_utworu;
            Dlugosc = dlugosc;

            Gatunek = gatunek;
        }

        public Skladowa(Skladowa skladowa)
        {
            AlbumId = skladowa.AlbumId;
            UtworId = skladowa.UtworId;
            Dlugosc = skladowa.Dlugosc;
            Gatunek = skladowa.Gatunek;
        }
        #endregion

        #region metody
        //public override string ToString()
        //{
        //    return $"";
        //}

        public string ToInsert()
        {
            return $"('{AlbumId}', " +
                $"'{UtworId}', " +
                $"{Dlugosc}, " +
                $"'{Gatunek.GetDisplayName()}')";
        }

        public string ToUpdate()
        {
            return $"id_albumu1='{AlbumId}', " +
                $"id_utworu='{UtworId}', " +
                $"dlugosc={Dlugosc}, " +
                $"gatunek='{Gatunek.GetDisplayName()}'";
        }

        public override bool Equals(object obj)
        {
            var skladowa = obj as Skladowa;
            if (skladowa is null) return false;
            if (AlbumId != skladowa.AlbumId) return false;
            if (UtworId != skladowa.UtworId) return false;
            if (Dlugosc != skladowa.Dlugosc) return false;
            if (Gatunek != skladowa.Gatunek) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}
