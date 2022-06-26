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
    class TabArtysciVM : ViewModelBase
    {
        #region skladowe prywatne
        private MainModel model = null;
        private ObservableCollection<Artysta> artysci = null;

        private int indeksZaznaczonegoArtysty = -1;

        private ushort? id;
        private string imie, nazwisko;
        private string? pseudonim, dataUr;
        private int? startKariery;

        private bool dodawanieDostepne = true;
        private bool edycjaDostepna = false;
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

        public Artysta BiezacyArtysta { get; set; }

        public ObservableCollection<Artysta> Artysci
        {
            get { return artysci; }
            set
            {
                artysci = value;
                onPropertyChanged(nameof(Artysci));
            }
        }
        public ushort? Id
        {
            get { return id; }
            set
            {
                id = value;
                onPropertyChanged(nameof(Id));
            }
        }

        public string Imie
        {
            get => imie;
            set
            {
                imie = value;
                onPropertyChanged(nameof(Imie));
            }
        }

        public string Nazwisko
        {
            get => nazwisko;
            set
            {
                nazwisko = value;
                onPropertyChanged(nameof(Nazwisko));
            }
        }

        public string? Pseudonim
        {
            get => pseudonim;
            set
            {
                pseudonim = value;
                onPropertyChanged(nameof(Pseudonim));
            }
        }

        public string? DataUr
        {
            get => dataUr;
            set
            {
                dataUr = value;
                onPropertyChanged(nameof(DataUr));
            }
        }

        public int? StartKariery
        {
            get => startKariery;
            set
            {
                startKariery = value;
                onPropertyChanged(nameof(StartKariery));
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
        public void OdswiezArtystow()
        {
            model.OdswiezArtystow();
            Artysci = model.Artysci;
            IndeksZaznaczonegoArtysty = -1;
        }

        private ICommand zaladujWszystkichArtystow = null;
        public ICommand ZaladujWszystkichArtystow
        {
            get
            {
                if (zaladujWszystkichArtystow == null)
                    zaladujWszystkichArtystow = new RelayCommand(
                        arg =>
                        {
                            OdswiezArtystow();
                        }
                        ,
                        arg => true
                        );

                return zaladujWszystkichArtystow;
            }
        }

        private void CzyscSzczegoly()
        {
            Imie = "";
            Nazwisko = "";
            Pseudonim = null;
            DataUr = null;
            StartKariery = null;
            
            DodawanieDostepne = true;
            EdycjaDostepna = false;
        }
        #endregion

        #region polecenia
        private ICommand pokazSzczegolyArtysty = null;
        public ICommand PokazSzczegolyArtysty
        {
            get
            {
                if (pokazSzczegolyArtysty == null)
                    pokazSzczegolyArtysty = new RelayCommand(
                        arg =>
                        {
                            CzyscSzczegoly();
                            if (IndeksZaznaczonegoArtysty > -1)
                            {
                                Imie = BiezacyArtysta.Imie;
                                Nazwisko = BiezacyArtysta.Nazwisko;
                                Pseudonim = BiezacyArtysta.Pseudonim;
                                DataUr = BiezacyArtysta.DataUr;
                                StartKariery = BiezacyArtysta.StartKariery;

                                DodawanieDostepne = false;
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

                return pokazSzczegolyArtysty;
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

        private ICommand dodajArtyste = null;
        public ICommand DodajArtyste
        {
            get
            {
                if (dodajArtyste == null)
                    dodajArtyste = new RelayCommand(
                        arg =>
                        {
                            var artysta = new Artysta(Imie, Nazwisko, Pseudonim, (string)DataUr, StartKariery);

                            if (model.DodajArtysteDoBazy(artysta))
                            {
                                OdswiezArtystow();
                                CzyscSzczegoly();
                                MessageBox.Show("Artysta został dodany do bazy!");
                            }
                        }
                        ,
                        arg => (Imie != "") && (Nazwisko != "") && (DataUr?.ToString() != "") && (StartKariery > 0)
                        );
                return dodajArtyste;
            }

        }

        private ICommand edytujArtyste = null;
        public ICommand EdytujArtyste
        {
            get
            {
                if (edytujArtyste == null)
                    edytujArtyste = new RelayCommand(
                    arg =>
                    {
                        model.EdytujArtysteWBazie(new Artysta(Imie, Nazwisko, Pseudonim, (string)DataUr, StartKariery), (ushort)BiezacyArtysta.Id);
                        OdswiezArtystow();
                        CzyscSzczegoly();
                    }
                         ,
                    arg => (BiezacyArtysta?.Imie != Imie) || (BiezacyArtysta?.Nazwisko != Nazwisko) || (BiezacyArtysta?.Pseudonim != Pseudonim) || (BiezacyArtysta?.DataUr != DataUr) || (BiezacyArtysta?.StartKariery != StartKariery)
                   );
                return edytujArtyste;
            }
        }

        private ICommand usunArtyste = null;
        public ICommand UsunArtyste
        {
            get
            {
                if (usunArtyste == null)
                    usunArtyste = new RelayCommand(
                    arg =>
                    {
                        model.UsunArtysteZBazy(BiezacyArtysta.Id);
                        OdswiezArtystow();
                        CzyscSzczegoly();
                        DodawanieDostepne = true;
                    }
                         ,
                    arg => IndeksZaznaczonegoArtysty > -1
                   );
                return usunArtyste;
            }
        }
        #endregion
    }
}
