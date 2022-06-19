﻿using MySql.Data.MySqlClient;
using Plytoteka.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plytoteka.DAL.Repositories
{
    static class RepoSkladowa
    {
        #region zapytania
        private const string WSZYSTKO = "SELECT * FROM skladowe";
        private const string DODAJ = "INSERT INTO 'skladowe' ('id_albumu1', 'id_utworu', 'dlugosc', 'gatunek') VALUES ";
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

        public static bool Edytuj(Skladowa skladowa, sbyte idAlbumu, sbyte idUtworu)
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

        public static bool Usun(sbyte id)
        {
            return true;
        }
        #endregion
    }
}