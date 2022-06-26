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
        private const string DODAJ = "INSERT INTO wystapienia (id_artysty1, id_albumu) VALUES ";
        private const string USUN_PO_ARTYSCIE = "DELETE FROM wystapienia WHERE id_artysty1=";
        private const string USUN_PO_ALBUMIE = "DELETE FROM wystapienia WHERE id_utworu=";
        #endregion

        #region CRUD
        public static List<Wystapienie> PobierzWszystko()
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

        public static bool Edytuj(Wystapienie wystapienie, ushort idArtysty, ushort idAlbumu)
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

        public static bool Usun(ushort? idArtysty, ushort? idAlbumu)
        {
            bool stan = false;

            using (var connection = DBConnection.Instance.Connection)
            {
                string USUN = $"DELETE FROM wystapienia WHERE id_artysty1={idArtysty} AND id_albumu={idAlbumu}";
                MySqlCommand command = new MySqlCommand(USUN, connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                if (n == 1) stan = true;

                connection.Close();
            }
            return stan;
        }

        public static bool UsunPoArtyscie(ushort? id)
        {
            bool stan = false;

            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{USUN_PO_ARTYSCIE} {id}", connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                if (n == 1) stan = true;

                connection.Close();
            }
            return stan;
        }


        public static bool UsunPoAlbumie(ushort? id)
        {
            bool stan = false;

            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{USUN_PO_ALBUMIE} {id}", connection);
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
