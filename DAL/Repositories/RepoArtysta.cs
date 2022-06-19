﻿using MySql.Data.MySqlClient;
using Plytoteka.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plytoteka.DAL.Repositories
{
    static class RepoArtysta
    {
        #region zapytania
        private const string WSZYSTKO = "SELECT * FROM artysci";
        private const string DODAJ = "INSERT INTO 'artysci'" +
            "('imie', 'nazwisko', 'pseudonim', 'data_ur', pocz_kariery') VALUES ";
        #endregion

        #region CRUD
        public static List<Artysta> PobierzWszysto()
        {
            List<Artysta> artysci = new List<Artysta>();

            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(WSZYSTKO, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    artysci.Add(new Artysta(reader));
                connection.Close();
            }
            return artysci;
        }

        public static bool Dodaj(Artysta artysta)
        {
            bool stan = false;

            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{DODAJ} {artysta.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                stan = true;
                artysta.Id = (sbyte)command.LastInsertedId;
                connection.Close();
            }
            return stan;
        }

        public static bool Edytuj(Artysta artysta, sbyte id)
        {
            bool stan = false;

            using (var connection = DBConnection.Instance.Connection)
            {
                string EDYTUJ = $"UPDATE artysci SET {artysta.ToUpdate()} WHERE id_artysty={id}";

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
