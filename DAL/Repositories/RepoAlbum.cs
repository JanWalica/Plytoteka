using MySql.Data.MySqlClient;
using Plytoteka.DAL.Entities;
using Plytoteka.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plytoteka.DAL.Repositories
{
    static class RepoAlbum
    {
        #region zapytania
        private const string WSZYSTKO = "SELECT id_albumu, id_zespolu1, nazwa, tytul, data_wyd, dlugosc, ile_utworow, wydawca, typ FROM albumy " +
            "JOIN zespoly ON id_zespolu1 = id_zespolu";
        private const string DODAJ = "INSERT INTO albumy (id_zespolu1, tytul, data_wyd, wydawca, typ) VALUES ";
        private const string USUN = "DELETE FROM albumy WHERE id_albumu=";
        #endregion

        #region CRUD
        public static List<Album> PobierzWszystko()
        {
            List<Album> albumy = new List<Album>();

            using(var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(WSZYSTKO, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    albumy.Add(new Album(reader));
                connection.Close();
            }
            return albumy;
        }

        public static bool Dodaj(Album album)
        {
            bool stan = false;

            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{DODAJ} {album.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                stan = true;
                album.Id = (ushort)command.LastInsertedId;
                connection.Close();
            }
            return stan;
        }

        public static bool Edytuj(Album album, ushort? id)
        {
            bool stan = false;

            using (var connection = DBConnection.Instance.Connection)
            {
                string EDYTUJ = $"UPDATE albumy SET {album.ToUpdate()} WHERE id_albumu={id}";

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
