using MySql.Data.MySqlClient;
using Plytoteka.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plytoteka.DAL.Repositories
{
    static class RepoCzlonek
    {
        #region zapytania
        private const string WSZYSTKO = "SELECT * FROM czlonkowie";
        private const string DODAJ = "INSERT INTO 'czlonkowie' ('id_artysty', 'id_zespolu', 'pocz_wsp', 'koniec_wsp') VALUES ";
        #endregion

        #region CRUD
        public static List<Czlonek> PobierzWszystko()
        {
            List<Czlonek> czlonkowie = new List<Czlonek>();

            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(WSZYSTKO, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    czlonkowie.Add(new Czlonek(reader));
                connection.Close();
            }
            return czlonkowie;
        }

        public static bool Dodaj(Czlonek czlonek)
        {
            bool stan = false;

            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{DODAJ} {czlonek.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                stan = true;
                connection.Close();
            }
            return stan;
        }

        public static bool Edytuj(Czlonek czlonek, ushort idArtysty, ushort idZespolu)
        {
            bool stan = false;

            using (var connection = DBConnection.Instance.Connection)
            {
                string EDYTUJ = $"UPDATE czlonkowie SET {czlonek.ToUpdate()} WHERE id_artysty={idArtysty} AND id_zespolu={idZespolu}";

                MySqlCommand command = new MySqlCommand(EDYTUJ, connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                if (n == 1) stan = true;

                connection.Close();
            }
            return stan;
        }

        public static bool Usun(ushort id)
        {
            return true;
        }
        #endregion
    }
}
