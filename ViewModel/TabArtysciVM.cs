using Plytoteka.DAL.Entities;
using Plytoteka.Model;
using Plytoteka.ViewModel.BaseClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Plytoteka.ViewModel
{
    class TabArtysciVM : ViewModelBase
    {
        #region skladowe prywatne
        private MainModel model = null;
        private ObservableCollection<Artysta> artysci = null;

        private int indeksZaznaczonegoArtysty = -1;
        #endregion

        #region konstruktory
        public TabArtysciVM(MainModel model)
        {
            this.model = model;
            artysci = model.Artysci;
        }
        #endregion

        #region wlasciwosci
        public int IndeksZaznaczonegoArtysty
        {
            get => indeksZaznaczonegoArtysty;
            set
            {
                indeksZaznaczonegoArtysty = value;
                onPropertyChanged(nameof(indeksZaznaczonegoArtysty));
            }
        }

        public Zespol BiezacyArtysta { get; set; }

        public ObservableCollection<Artysta> Artysci
        {
            get { return artysci; }
            set
            {
                artysci = value;
                onPropertyChanged(nameof(Artysci));
            }
        }
        #endregion

        #region metody
        public void OdswiezArtystow() => Artysci = model.Artysci;
        #endregion
    }
}
