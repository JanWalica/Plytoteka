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
        private const string WSZYSTKO = "SELECT cz.id_artysty, concat(a.imie, \" \", a.nazwisko) AS artysta, cz.id_zespolu, z.nazwa AS nazwa_zespolu, cz.pocz_wsp, cz.koniec_wsp " +
            "FROM czlonkowie cz " +
            "JOIN artysci a ON cz.id_artysty = a.id_artysty " +
            "JOIN zespoly z ON cz.id_zespolu = z.id_zespolu;";
        private const string DODAJ = "INSERT INTO czlonkowie (id_artysty, id_zespolu, pocz_wsp, koniec_wsp) VALUES ";
        private const string USUN_PO_ARTYSCIE = "DELETE FROM czlonkowie WHERE id_artysty=";
        private const string USUN_PO_ZESPOLE = "DELETE FROM czlonkowie WHERE id_zespolu=";
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

        public static bool Usun(ushort? idAlbumu, ushort? idZespolu)
        {
            bool stan = false;

            using (var connection = DBConnection.Instance.Connection)
            {
                string USUN = $"DELETE FROM czlonkowie WHERE id_artysty={idAlbumu} AND id_zespolu={idZespolu}";
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


        public static bool UsunPoZespole(ushort? id)
        {
            bool stan = false;

            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{USUN_PO_ZESPOLE} {id}", connection);
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
