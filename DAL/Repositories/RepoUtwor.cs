using MySql.Data.MySqlClient;
using Plytoteka.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plytoteka.DAL.Repositories
{
    static class RepoUtwor
    {
        #region zapytania
        private const string WSZYSTKO = "SELECT * FROM utwory";
        private const string DODAJ = "INSERT INTO utwory (tytul, autor_tekstu, jezyk) VALUES ";
        private const string USUN = "DELETE FROM utwory WHERE id_utworu=";

        #endregion

        #region CRUD
        public static List<Utwor> PobierzWszystko()
        {
            List<Utwor> utwory = new List<Utwor>();

            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(WSZYSTKO, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    utwory.Add(new Utwor(reader));
                connection.Close();
            }
            return utwory;
        }

        public static bool Dodaj(Utwor utwor)
        {
            bool stan = false;

            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{DODAJ} {utwor.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                stan = true;
                utwor.Id = (ushort)command.LastInsertedId;
                connection.Close();
            }
            return stan;
        }

        public static bool Edytuj(Utwor utwor, ushort id)
        {
            bool stan = false;

            using (var connection = DBConnection.Instance.Connection)
            {
                string EDYTUJ = $"UPDATE utwory SET {utwor.ToUpdate()} WHERE id_utworu={id}";

                MySqlCommand command = new MySqlCommand(EDYTUJ, connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                if (n == 1) stan = true;

                connection.Close();
            }
            return stan;
        }

        public static bool Usun(ushort? id)
        {
            bool stan = false;

            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{USUN} {id}", connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                if (n == 1) stan = true;

                connection.Close();
            }
            return stan;
        }
        #endregion
    }
}
