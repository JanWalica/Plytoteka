using MySql.Data.MySqlClient;
using Plytoteka.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plytoteka.DAL.Repositories
{
    static class RepoSkladowa
    {
        #region zapytania
        private const string WSZYSTKO = "SELECT s.id_albumu1, a.tytul AS tytul_albumu, s.id_utworu, u.tytul AS tytul_utworu, s.dlugosc, gatunek " +
            "FROM skladowe s " +
            "JOIN albumy a ON s.id_albumu1 = a.id_albumu " +
            "JOIN utwory u ON s.id_utworu = u.id_utworu;";
        private const string DODAJ = "INSERT INTO skladowe (id_albumu1, id_utworu, dlugosc, gatunek) VALUES ";
        private const string USUN_PO_ALBUMIE = "DELETE FROM skladowe WHERE id_albumu1=";
        private const string USUN_PO_UTWORZE = "DELETE FROM skladowe WHERE id_utworu=";
        #endregion

        #region CRUD
        public static List<Skladowa> PobierzWszystko()
        {
            List<Skladowa> skladowe = new List<Skladowa>();

            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(WSZYSTKO, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    skladowe.Add(new Skladowa(reader));
                connection.Close();
            }
            return skladowe;
        }

        public static bool Dodaj(Skladowa skladowa)
        {
            bool stan = false;

            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{DODAJ} {skladowa.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                stan = true;
                connection.Close();
            }
            return stan;
        }

        public static bool Edytuj(Skladowa skladowa, ushort idAlbumu, ushort idUtworu)
        {
            bool stan = false;

            using (var connection = DBConnection.Instance.Connection)
            {
                string EDYTUJ = $"UPDATE skladowe SET {skladowa.ToUpdate()} WHERE id_albumu1={idAlbumu} AND id_utworu={idUtworu}";

                MySqlCommand command = new MySqlCommand(EDYTUJ, connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                if (n == 1) stan = true;

                connection.Close();
            }
            return stan;
        }

        public static bool Usun(ushort? idAlbumu, ushort? idUtworu)
        {
            bool stan = false;

            using (var connection = DBConnection.Instance.Connection)
            {
                string USUN = $"DELETE FROM skladowe WHERE id_albumu1={idAlbumu} AND id_utworu={idUtworu}";
                MySqlCommand command = new MySqlCommand(USUN, connection);
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


        public static bool UsunPoUtworze(ushort? id)
        {
            bool stan = false;

            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{USUN_PO_UTWORZE} {id}", connection);
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
