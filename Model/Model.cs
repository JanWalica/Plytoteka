using Plytoteka.DAL.Entities;
using Plytoteka.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Plytoteka.Model
{
    class Model
    {
        public ObservableCollection<Utwor> Utwory { get; set; } = new ObservableCollection<Utwor>();

        #region konstruktor
        public Model()
        {
            var utwory = RepoUtwor.PobierzWszystko();
            foreach (var u in utwory)
                Utwory.Add(u);
        }
        #endregion
    }
}
