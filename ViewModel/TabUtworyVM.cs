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
    class TabUtworyVM : ViewModelBase
    {
        #region skladowe prywatne
        private MainModel model = null;
        private ObservableCollection<Utwor> utwory = null;

        private int indeksZaznaczonegoUtworu = -1;

        private ushort? id;
        private string tytul;
        private string? autor, jezyk;

        private bool dodawanieDostepne = true;
        private bool edycjaDostepna = false;
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

        public Utwor BiezacyUtwor { get; set; }

        public ObservableCollection<Utwor> Utwory
        {
            get { return utwory; }
            set
            {
                utwory = value;
                onPropertyChanged(nameof(Utwory));
            }
        }

        public ushort? Id
        {
            get => id;
            set
            {
                id = value;
                onPropertyChanged(nameof(Id));
            }
        }

        public string Tytul
        {
            get => tytul;
            set
            {
                tytul = value;
                onPropertyChanged(nameof(Tytul));
            }
        }

        public string? Autor
        {
            get => autor;
            set
            {
                autor = value;
                onPropertyChanged(nameof(Autor));
            }
        }

        public string? Jezyk
        {
            get => jezyk;
            set
            {
                jezyk = value;
                onPropertyChanged(nameof(Jezyk));
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
        public void OdswiezUtwory()
        {
            model.OdswiezUtwory();
            Utwory = model.Utwory;
            IndeksZaznaczonegoUtworu = -1;
        }

        private ICommand zaladujWszystkieUtwory = null;
        public ICommand ZaladujWszystkieUtwory
        {
            get
            {
                if (zaladujWszystkieUtwory == null)
                    zaladujWszystkieUtwory = new RelayCommand(
                        arg =>
                        {
                            OdswiezUtwory();
                        }
                        ,
                        arg => true
                        );

                return zaladujWszystkieUtwory;
            }
        }

        private void CzyscSzczegoly()
        {
            Tytul = "";
            Autor = "";
            Jezyk = "";

            DodawanieDostepne = true;
            EdycjaDostepna = false;
        }
        #endregion

        #region polecenia
        private ICommand pokazSzczegolyUtworu = null;
        public ICommand PokazSzczegolyUtworu
        {
            get
            {
                if (pokazSzczegolyUtworu == null)
                    pokazSzczegolyUtworu = new RelayCommand(
                        arg =>
                        {
                            CzyscSzczegoly();
                            if (IndeksZaznaczonegoUtworu > -1)
                            {
                                Tytul = BiezacyUtwor.Tytul;
                                Autor = BiezacyUtwor.Autor;
                                Jezyk = BiezacyUtwor.Jezyk;

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

                return pokazSzczegolyUtworu;
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

        private ICommand dodajUtwor = null;
        public ICommand DodajUtwor
        {
            get
            {
                if (dodajUtwor == null)
                    dodajUtwor = new RelayCommand(
                        arg =>
                        {
                            var utwor = new Utwor(Tytul, Autor, Jezyk);

                            if (model.DodajUtworDoBazy(utwor))
                            {
                                OdswiezUtwory();
                                CzyscSzczegoly();
                                MessageBox.Show("Utwor został dodany do bazy!");
                            }
                        }
                        ,
                        arg => (Tytul != "") && (Autor != "") && (Jezyk != "")
                        );
                return dodajUtwor;
            }

        }

        private ICommand edytujUtwor = null;
        public ICommand EdytujUtwor
        {
            get
            {
                if (edytujUtwor == null)
                    edytujUtwor = new RelayCommand(
                    arg =>
                    {
                        model.EdytujUtworWBazie(new Utwor(Tytul, Autor, Jezyk), (ushort)BiezacyUtwor.Id);
                        OdswiezUtwory();
                        CzyscSzczegoly();
                    }
                         ,
                    arg => (BiezacyUtwor?.Tytul != Tytul) || (BiezacyUtwor?.Autor != Autor) || (BiezacyUtwor?.Jezyk != Jezyk)
                   );
                return edytujUtwor;
            }
        }

        private ICommand usunUtwor = null;
        public ICommand UsunUtwor
        {
            get
            {
                if (usunUtwor == null)
                    usunUtwor = new RelayCommand(
                    arg =>
                    {
                        model.UsunUtworZBazy(BiezacyUtwor.Id);
                        OdswiezUtwory();
                        CzyscSzczegoly();
                        DodawanieDostepne = true;
                    }
                         ,
                    arg => IndeksZaznaczonegoUtworu > -1
                   );
                return usunUtwor;
            }
        }
        #endregion
    }
}
