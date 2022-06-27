using Plytoteka.DAL.Entities;
using Plytoteka.Model;
using Plytoteka.ViewModel.BaseClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Plytoteka.ViewModel
{
    class TabZespolyVM : ViewModelBase
    {
        #region skladowe prywatne
        private MainModel model = null;
        private ObservableCollection<Zespol> zespoly = null;
        private ObservableCollection<Czlonek> czlonkowie = null;
        private ObservableCollection<Artysta> artysci = null;

        private int indeksZaznaczonegoZespolu = -1;
        private int indeksCzlonka = -1;
        private int indeksArtysty = -1;

        private ushort? id;
        private string nazwa;
        private int? dataZalozenia;
        private string? pochodzenie;

        private ushort? czlonekArtystaId;
        private string czlonekArtysta;
        private int czlonekStartWspolpracy;
        private int? czlonekKoniecWspolpracy;

        private bool dodawanieDostepne = true;
        private bool dodawanieArtystyDostepne = false;
        private bool edycjaDostepna = false;
        #endregion

        #region konstruktory
        public TabZespolyVM(MainModel model)
        {
            this.model = model;
            zespoly = model.Zespoly;
            artysci = model.Artysci;
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

        public int IndeksCzlonka
        {
            get => indeksCzlonka;
            set
            {
                indeksCzlonka = value;
                onPropertyChanged(nameof(IndeksCzlonka));
            }
        }

        public int IndeksArtysty
        {
            get => indeksArtysty;
            set
            {
                indeksArtysty = value;
                onPropertyChanged(nameof(IndeksArtysty));
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

        public ObservableCollection<Czlonek> Czlonkowie
        {
            get => czlonkowie;
            set
            {
                czlonkowie = value;
                onPropertyChanged(nameof(Czlonkowie));
            }
        }

        public ObservableCollection<Artysta> Artysci
        {
            get => artysci;
            set
            {
                artysci = value;
                onPropertyChanged(nameof(Artysci));
            }
        }

        // ZESPOLY
        public ushort? Id
        {
            get => id;
            set
            {
                id = value;
                onPropertyChanged(nameof(Id));
            }
        }
        public string Nazwa
        {
            get => nazwa;
            set
            {
                nazwa = value;
                onPropertyChanged(nameof(Nazwa));
            }
        }
        public int? DataZalozenia
        {
            get => dataZalozenia;
            set
            {
                dataZalozenia = value;
                onPropertyChanged(nameof(DataZalozenia));
            }
        }
        public string? Pochodzenie
        {
            get => pochodzenie;
            set
            {
                pochodzenie = value;
                onPropertyChanged(nameof(Pochodzenie));
            }
        }

        // CZLONKOWIE
        public ushort? CzlonekArtystaId
        {
            get => czlonekArtystaId;
            set
            {
                czlonekArtystaId = value;
                onPropertyChanged(nameof(CzlonekArtystaId));
            }
        }
        public string CzlonekArtysta
        {
            get => czlonekArtysta;
            set
            {
                czlonekArtysta = value;
                onPropertyChanged(nameof(CzlonekArtysta));
            }
        }
        public int CzlonekStartWspolpracy
        {
            get => czlonekStartWspolpracy;
            set
            {
                czlonekStartWspolpracy = value;
                onPropertyChanged(nameof(CzlonekStartWspolpracy));
            }
        }
        public int? CzlonekKoniecWspolpracy
        {
            get => czlonekKoniecWspolpracy;
            set
            {
                czlonekKoniecWspolpracy = value;
                onPropertyChanged(nameof(CzlonekKoniecWspolpracy));
            }
        }

        // INNE

        public bool DodawanieDostepne
        {
            get { return dodawanieDostepne; }
            set
            {
                dodawanieDostepne = value;
                onPropertyChanged(nameof(DodawanieDostepne));
            }
        }

        public bool DodawanieArtystyDostepne
        {
            get => dodawanieArtystyDostepne;
            set
            {
                dodawanieArtystyDostepne = value;
                onPropertyChanged(nameof(DodawanieArtystyDostepne));
            }
        }

        public bool EdycjaDostepna
        {
            get { return edycjaDostepna; }
            set
            {
                edycjaDostepna = value;
                onPropertyChanged(nameof(EdycjaDostepna));
            }
        }
        #endregion

        #region metody
        public void OdswiezZespoly()
        {
            model.OdswiezZespoly();
            Zespoly = model.Zespoly;
            IndeksZaznaczonegoZespolu = -1;
        }
        public void OdswiezCzlonkow()
        {
            model.OdswiezCzlonkow();
            Czlonkowie = model.PobierzCzlonkowZespolu(BiezacyZespol);
            IndeksCzlonka = -1;
        }

        private ICommand zaladujWszystkieZespoly = null;
        public ICommand ZaladujWszystkieZespoly
        {
            get
            {
                if (zaladujWszystkieZespoly == null)
                    zaladujWszystkieZespoly = new RelayCommand(
                        arg =>
                        {
                            OdswiezZespoly();
                        }
                        ,
                        arg => true
                        );

                return zaladujWszystkieZespoly;
            }
        }

        private void CzyscSzczegoly()
        {
            Nazwa = null;
            DataZalozenia = null;
            Pochodzenie = null;

            Czlonkowie = null;

            CzlonekArtysta = null;
            CzlonekArtystaId = null;
            CzlonekStartWspolpracy = 0;
            CzlonekKoniecWspolpracy = null;

            DodawanieDostepne = true;
            DodawanieArtystyDostepne = false;
            EdycjaDostepna = false;
        }
        #endregion

        #region polecenia
        private ICommand pokazSzczegolyZespolu = null;
        public ICommand PokazSzczegolyZespolu
        {
            get
            {
                if (pokazSzczegolyZespolu == null)
                    pokazSzczegolyZespolu = new RelayCommand(
                        arg =>
                        {
                            CzyscSzczegoly();
                            if (IndeksZaznaczonegoZespolu > -1)
                            {
                                Nazwa = BiezacyZespol.Nazwa;
                                DataZalozenia = BiezacyZespol.DataZalozenia;
                                Pochodzenie = BiezacyZespol.Pochodzenie;

                                Czlonkowie = model.PobierzCzlonkowZespolu(BiezacyZespol);

                                DodawanieDostepne = false;
                                DodawanieArtystyDostepne = true;
                                EdycjaDostepna = true;
                            }
                            else
                            {
                                CzyscSzczegoly();
                            }
                        }
                        ,
                        arg => true
                        );

                return pokazSzczegolyZespolu;
            }
        }

        private ICommand czyscFormularz = null;
        public ICommand CzyscFormularz
        {
            get
            {
                if (czyscFormularz == null)
                    czyscFormularz = new RelayCommand(
                        arg =>
                        {
                            CzyscSzczegoly();
                        }
                        ,
                        arg => true
                        );
                return czyscFormularz;
            }

        }

        private ICommand dodajZespol = null;
        public ICommand DodajZespol
        {
            get
            {
                if (dodajZespol == null)
                    dodajZespol = new RelayCommand(
                        arg =>
                        {
                            var zespol = new Zespol(Nazwa, (int)DataZalozenia, Pochodzenie);

                            if (model.DodajZespolDoBazy(zespol))
                            {
                                OdswiezZespoly();
                                CzyscSzczegoly();
                                MessageBox.Show("Zespol został dodany do bazy!");
                            }
                        }
                        ,
                        arg => (Nazwa != "")
                        );
                return dodajZespol;
            }
        }

        private ICommand edytujZespol = null;
        public ICommand EdytujZespol
        {
            get
            {
                if (edytujZespol == null)
                    edytujZespol = new RelayCommand(
                    arg =>
                    {
                        model.EdytujZespolWBazie(new Zespol(Nazwa, (int)DataZalozenia, Pochodzenie), (ushort)BiezacyZespol.Id);
                        OdswiezZespoly();
                        CzyscSzczegoly();
                    }
                         ,
                    arg => (BiezacyZespol?.Nazwa != Nazwa) || (BiezacyZespol?.DataZalozenia != DataZalozenia) || (BiezacyZespol?.Pochodzenie != Pochodzenie)
                   );
                return edytujZespol;
            }
        }

        private ICommand usunZespol = null;
        public ICommand UsunZespol
        {
            get
            {
                if (usunZespol == null)
                    usunZespol = new RelayCommand(
                    arg =>
                    {
                        model.UsunZespolZBazy(BiezacyZespol.Id);
                        OdswiezZespoly();
                        CzyscSzczegoly();
                        DodawanieDostepne = true;
                        DodawanieArtystyDostepne = false;
                    }
                         ,
                    arg => IndeksZaznaczonegoZespolu > -1
                   );
                return usunZespol;
            }
        }

        private ICommand dodajCzlonka = null;
        public ICommand DodajCzlonka
        {
            get
            {
                if (dodajCzlonka == null)
                    dodajCzlonka = new RelayCommand(
                        arg =>
                        {
                            CzlonekArtystaId = (ushort)Artysci[IndeksArtysty].Id;
                            var czlonek = new Czlonek((ushort)CzlonekArtystaId, (ushort)BiezacyZespol.Id, (int)CzlonekStartWspolpracy, CzlonekKoniecWspolpracy);

                            if (model.DodajCzlonkaDoBazy(czlonek))
                            {
                                OdswiezCzlonkow();
                                OdswiezZespoly();
                                CzyscSzczegoly();
                            }
                        }
                        ,
                        arg => (CzlonekArtysta != "") && (CzlonekStartWspolpracy > 1)
                        );
                return dodajCzlonka;
            }
        }

        private ICommand usunCzlonka = null;
        public ICommand UsunCzlonka
        {
            get
            {
                if (usunCzlonka == null)
                    usunCzlonka = new RelayCommand(
                    arg =>
                    {
                        CzlonekArtystaId = (ushort)Czlonkowie[IndeksCzlonka].ArtystaId;
                        model.UsunCzlonkaZBazy((ushort)CzlonekArtystaId, (ushort)BiezacyZespol.Id);
                        OdswiezCzlonkow();
                        OdswiezZespoly();
                        CzyscSzczegoly();
                        DodawanieDostepne = true;
                        DodawanieArtystyDostepne = false;
                    }
                         ,
                    arg => IndeksCzlonka > -1
                   );
                return usunCzlonka;
            }
        }
        #endregion
    }
}
