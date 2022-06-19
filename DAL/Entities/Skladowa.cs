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
        public sbyte Album { get; set; }
        public sbyte Utwor { get; set; }
        public int Dlugosc { get; set; }
        public Gatunek Gatunek { get; set; }
        #endregion

        #region konstruktory
        public Skladowa(MySqlDataReader reader)
        {
            Album = sbyte.Parse(reader["id_albumu1"].ToString());
            Utwor = sbyte.Parse(reader["id_utworu"].ToString());
            Dlugosc = int.Parse(reader["dlugosc"].ToString());
            Gatunek = (Gatunek)Enum.Parse(typeof(Gatunek), reader["gatunek"].ToString().Trim().ToLower().Replace(" ", "_"));
        }

        public Skladowa(sbyte album, sbyte utwor, int dlugosc, Gatunek gatunek)
        {
            Album = album;
            Utwor = utwor;
            Dlugosc = dlugosc;
            Gatunek = gatunek;
        }

        public Skladowa(Skladowa skladowa)
        {
            Album = skladowa.Album;
            Utwor = skladowa.Utwor;
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
            return $"('{Album}', " +
                $"'{Utwor}', " +
                $"'{Dlugosc}', " +
                $"'{Gatunek.GetDisplayName()}')";
        }

        public string ToUpdate()
        {
            return $"id_albumu1='{Album}', " +
                $"id_utworu='{Utwor}', " +
                $"dlugosc='{Dlugosc}', " +
                $"gatunek='{Gatunek.GetDisplayName()}'";
        }

        public override bool Equals(object obj)
        {
            var skladowa = obj as Skladowa;
            if (skladowa is null) return false;
            if (Album != skladowa.Album) return false;
            if (Utwor != skladowa.Utwor) return false;
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
