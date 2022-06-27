using MySql.Data.MySqlClient;
using Plytoteka.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plytoteka.DAL.Repositories
{
    static class RepoZespol
    {
        #region zapytania
        private const string WSZYSTKO = "SELECT * FROM zespoly";
        private const string DODAJ = "INSERT INTO zespoly (nazwa, data_zal, pochodzenie) VALUES ";
        private const string USUN = "DELETE FROM zespoly WHERE id_zespolu=";
        #endregion

        #region CRUD
        public static List<Zespol> PobierzWszystko()
        {
            List<Zespol> zespoly = new List<Zespol>();

            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(WSZYSTKO, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    zespoly.Add(new Zespol(reader));
                connection.Close();
            }
            return zespoly;
        }

        public static bool Dodaj(Zespol zespol)
        {
            bool stan = false;

            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{DODAJ} {zespol.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                stan = true;
                zespol.Id = (ushort)command.LastInsertedId;
                connection.Close();
            }
            return stan;
        }

        public static bool Edytuj(Zespol zespol, ushort id)
        {
            bool stan = false;

            using (var connection = DBConnection.Instance.Connection)
            {
                string EDYTUJ = $"UPDATE albumy SET {zespol.ToUpdate()} WHERE id_albumu={id}";

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
