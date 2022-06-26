using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plytoteka.DAL.Entities
{
    class Artysta : ICRUDStrings
    {
        #region wlasciwosci
        public ushort? Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string? Pseudonim { get; set; }
        public DateTime DataUr { get; set; }
        public int? StartKariery { get; set; }
        #endregion

        #region konstruktory
        public Artysta(MySqlDataReader reader)
        {
            Id = ushort.Parse(reader["id_artysty"].ToString());
            Imie = reader["imie"].ToString();
            Nazwisko = reader["nazwisko"].ToString();
            Pseudonim = reader["pseudonim"]?.ToString();
            DataUr = DateTime.Parse(reader["data_ur"].ToString());
            StartKariery = int.TryParse(reader["pocz_kariery"].ToString(), out var sk) ? sk : default;
        }

        public Artysta(string imie, string nazwisko, string? pseudonim, DateTime dataUr, int? startKariery)
        {
            Id = null;
            Imie = imie.Trim();
            Nazwisko = nazwisko.Trim();
            Pseudonim = pseudonim.Trim();
            DataUr = dataUr;
            StartKariery = startKariery;
        }

        public Artysta(Artysta artysta)
        {
            Id = artysta.Id;
            Imie = artysta.Imie;
            Nazwisko = artysta.Nazwisko;
            Pseudonim = artysta.Pseudonim;
            DataUr = artysta.DataUr;
            StartKariery = artysta.StartKariery;
        }
        #endregion

        #region metody
        public override string ToString()
        {
            return $"{Imie} {Nazwisko} \"{Pseudonim}\" ur. {DataUr:dd.MM.yyyy}, początek kariery: {StartKariery}";
        }

        public string ToInsert()
        {
            return $"('{Imie}', '{Nazwisko}', '{Pseudonim}', '{DataUr:yyyy-MM-DD}', '{StartKariery}')";
        }

        public string ToUpdate()
        {
            return $"imie='{Imie}', " +
                $"nazwisko='{Nazwisko}', " +
                $"pseudonim='{Pseudonim}', " +
                $"data_ur='{DataUr:yyyy-MM-DD}', " +
                $"pocz_kariery='{StartKariery}'";
        }

        public override bool Equals(object obj)
        {
            var artysta = obj as Artysta;
            if (artysta is null) return false;
            if (Imie.ToLower() != artysta.Imie.ToLower()) return false;
            if (Nazwisko.ToLower() != artysta.Nazwisko.ToLower()) return false;
            if (Pseudonim.ToLower() != artysta.Pseudonim.ToLower()) return false;
            if (DataUr != artysta.DataUr) return false;
            if (StartKariery != artysta.StartKariery) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}
