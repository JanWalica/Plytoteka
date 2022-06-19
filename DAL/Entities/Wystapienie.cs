﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plytoteka.DAL.Entities
{
    class Wystapienie : ICRUDStrings
    {
        #region wlasnosci
        public sbyte Artysta { get; set; }
        public sbyte Album { get; set; }
        #endregion

        #region konstruktory
        public Wystapienie(MySqlDataReader reader)
        {
            Artysta = sbyte.Parse(reader["id_artysty1"].ToString());
            Album = sbyte.Parse(reader["id_albumu"].ToString());
        }

        public Wystapienie(sbyte artysta, sbyte album)
        {
            Artysta = artysta;
            Album = album;
        }

        public Wystapienie(Wystapienie wystapienie)
        {
            Artysta = wystapienie.Artysta;
            Album = wystapienie.Album;
        }
        #endregion

        #region metody
        //public override string ToString()
        //{
        //    return $"";
        //}

        public string ToInsert()
        {
            return $"('{Artysta}', " +
                $"'{Album}')";
        }

        public string ToUpdate()
        {
            return $"$id_artysty1='{Artysta}', " +
                $"id_albumu='{Album}'";
        }

        public override bool Equals(object obj)
        {
            var wystapienie = obj as Wystapienie;
            if (wystapienie is null) return false;
            if (Artysta != wystapienie.Artysta) return false;
            if (Album != wystapienie.Album) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}
