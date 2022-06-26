using Plytoteka.DAL.Entities;
using Plytoteka.Model;
using Plytoteka.ViewModel.BaseClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Plytoteka.ViewModel
{
    class TabUtworyVM : ViewModelBase
    {
        #region skladowe prywatne
        private MainModel model = null;
        private ObservableCollection<Utwor> utwory = null;

        private int indeksZaznaczonegoUtworu = -1;
        #endregion

        #region konstruktory
        public TabUtworyVM(MainModel model)
        {
            this.model = model;
            utwory = model.Utwory;
        }
        #endregion

        #region wlasciwosci
        public int IndeksZaznaczonegoUtworu
        {
            get => indeksZaznaczonegoUtworu;
            set
            {
                indeksZaznaczonegoUtworu = value;
                onPropertyChanged(nameof(indeksZaznaczonegoUtworu));
            }
        }

        public Zespol BiezacyUtwor { get; set; }

        public ObservableCollection<Utwor> Utwory
        {
            get { return utwory; }
            set
            {
                utwory = value;
                onPropertyChanged(nameof(Utwory));
            }
        }
        #endregion

        #region metody
        public void OdswiezUtwory() => Utwory = model.Utwory;
        #endregion
    }
}
