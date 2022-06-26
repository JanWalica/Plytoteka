using Plytoteka.DAL.Entities;
using Plytoteka.Model;
using Plytoteka.ViewModel.BaseClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Plytoteka.ViewModel
{
    class TabZespolyVM : ViewModelBase
    {
        #region skladowe prywatne
        private MainModel model = null;
        private ObservableCollection<Zespol> zespoly = null;

        private int indeksZaznaczonegoZespolu = -1;
        #endregion

        #region konstruktory
        public TabZespolyVM(MainModel model)
        {
            this.model = model;
            zespoly = model.Zespoly;
        }
        #endregion

        #region wlasciwosci
        public int IndeksZaznaczonegoZespolu
        {
            get => indeksZaznaczonegoZespolu;
            set
            {
                indeksZaznaczonegoZespolu = value;
                onPropertyChanged(nameof(indeksZaznaczonegoZespolu));
            }
        }

        public Zespol BiezacyZespol { get; set; }

        public ObservableCollection<Zespol> Zespoly
        {
            get { return zespoly; }
            set
            {
                zespoly = value;
                onPropertyChanged(nameof(Zespoly));
            }
        }
        #endregion

        #region metody
        public void OdswiezZespoly() => Zespoly = model.Zespoly;
        #endregion


    }
}
