using MySql.Data.MySqlClient;
using Plytoteka.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plytoteka.DAL.Repositories
{
    static class RepoWystapienie
    {
        #region zapytania
        private const string WSZYSTKO = "SELECT * FROM wystapienia";
        private const string DODAJ = "INSERT INTO 'wystapienia' ('id_artysty1', 'id_albumu') VALUES ";
        #endregion

        #region CRUD
        public static List<Wystapienie> PobierzWszysto()
        {
            List<Wystapienie> wystapienia = new List<Wystapienie>();

            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(WSZYSTKO, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    wystapienia.Add(new Wystapienie(reader));
                connection.Close();
            }
            return wystapienia;
        }

        public static bool Dodaj(Wystapienie wystapienie)
        {
            bool stan = false;

            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{DODAJ} {wystapienie.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                stan = true;
                connection.Close();
            }
            return stan;
        }

        public static bool Edytuj(Wystapienie wystapienie, sbyte idArtysty, sbyte idAlbumu)
        {
            bool stan = false;

            using (var connection = DBConnection.Instance.Connection)
            {
                string EDYTUJ = $"UPDATE albumy SET {wystapienie.ToUpdate()} WHERE id_artysty1={idArtysty} AND id_albumu={idAlbumu}";

                MySqlCommand command = new MySqlCommand(EDYTUJ, connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                if (n == 1) stan = true;

                connection.Close();
            }
            return stan;
        }

        public static bool Usun(sbyte id)
        {
            return true;
        }
        #endregion
    }
}
