using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plytoteka.DAL.Entities
{
    class Utwor : ICRUDStrings
    {
        #region wlasnosci
        public ushort? Id { get; set; }
        public string Tytul { get; set; }
        public string? Autor { get; set; }
        public string? Jezyk { get; set; }
        #endregion

        #region konstruktory
        public Utwor(MySqlDataReader reader)
        {
            Id = ushort.Parse(reader["id_utworu"].ToString());
            Tytul = reader["tytul"].ToString();
            Autor = reader["autor_tekstu"]?.ToString();
            Jezyk = reader["jezyk"]?.ToString();
        }

        public Utwor(string tytul, string? autor, string? jezyk)
        {
            Id = null;
            Tytul = tytul;
            Autor = autor;
            Jezyk = jezyk;
        }

        public Utwor(Utwor utwor)
        {
            Id = utwor.Id;
            Tytul = utwor.Tytul;
            Autor = utwor.Autor;
            Jezyk = utwor.Jezyk;
        }
        #endregion

        #region metody
        public override string ToString()
        {
            return $"{Tytul} (słowa: {Autor})";
        }

        public string ToInsert()
        {
            return $"('{Tytul}', " +
                $"'{Autor}', " +
                $"'{Jezyk}')";
        }

        public string ToUpdate()
        {
            return $"tytul='{Tytul}', " +
                $"autor_tekstu='{Autor}', " +
                $"jezyk='{Jezyk}'";
        }

        public override bool Equals(object obj)
        {
            var utwor = obj as Utwor;
            if (utwor is null) return false;
            if (Tytul.ToLower() != utwor.Tytul.ToLower()) return false;
            if (Autor.ToLower() != utwor.Autor.ToLower()) return false;
            if (Jezyk.ToLower() != utwor.Jezyk.ToLower()) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}
