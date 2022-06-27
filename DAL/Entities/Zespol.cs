using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plytoteka.DAL.Entities
{
    class Zespol : ICRUDStrings
    {
        #region wlasnosci
        public ushort? Id { get; set; }
        public string Nazwa { get; set; }
        public int? DataZalozenia { get; set; }
        public string? Pochodzenie { get; set; }
        #endregion

        #region konstruktory
        public Zespol(MySqlDataReader reader)
        {
            Id = ushort.Parse(reader["id_zespolu"].ToString());
            Nazwa = reader["nazwa"].ToString();
            DataZalozenia = int.TryParse(reader["data_zal"].ToString(), out var dz) ? dz : default;
            Pochodzenie = reader["pochodzenie"]?.ToString();
        }

        public Zespol(string nazwa, int dataZalozenia, string pochodzenie)
        {
            Id = null;
            Nazwa = nazwa.Trim();
            DataZalozenia = dataZalozenia;
            Pochodzenie = pochodzenie.Trim();
        }

        public Zespol(Zespol zespol)
        {
            Id = zespol.Id;
            Nazwa = zespol.Nazwa;
            DataZalozenia = zespol.DataZalozenia;
            Pochodzenie = zespol.Pochodzenie;
        }
        #endregion

        #region metody
        public override string ToString()
        {
            return $"{Nazwa}";
        }

        public string ToInsert()
        {
            return $"('{Nazwa}', " +
                $"{DataZalozenia}, " +
                $"'{Pochodzenie}')";
        }

        public string ToUpdate()
        {
            return $"nazwa='{Nazwa}', " +
                $"data_zal={DataZalozenia}, " +
                $"pochodzenie='{Pochodzenie}'";
        }

        public override bool Equals(object obj)
        {
            var zespol = obj as Zespol;
            if (zespol is null) return false;
            if (Nazwa.ToLower() != zespol.Nazwa.ToLower()) return false;
            if (DataZalozenia != zespol.DataZalozenia) return false;
            if (Pochodzenie.ToLower() != zespol.Pochodzenie.ToLower()) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}
