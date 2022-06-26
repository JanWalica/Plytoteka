using Plytoteka.DAL.Entities;
using Plytoteka.Enums;
using Plytoteka.Model;
using Plytoteka.ViewModel.BaseClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Plytoteka.ViewModel
{
    class TabAlbumyVM : ViewModelBase
    {
        #region skladowe prywatne
        private MainModel model = null;
        private ObservableCollection<Album> albumy = null;
        private ObservableCollection<Zespol> zespoly = null;
        private ObservableCollection<Skladowa> skladowe = null;
        private List<string> typy = null;

        private int indeksZaznaczonegoAlbumu = -1;
        private int indeksZespolu = -1;
        private int indeksTypu = -1;
        private int indeksSkladowej = -1;

        private ushort? id, zespolId;
        private string? zespol, wydawca, dlugosc;
        private string tytul;
        private int? dataWydania, ileUtworow;
        private TypAlbumu? typ;

        private bool dodawanieDostepne = true;
        private bool edycjaDostepna = false;
        #endregion

        #region konstruktory
        public TabAlbumyVM(MainModel model)
        {
            this.model = model;
            albumy = model.Albumy;
            zespoly = model.Zespoly;
            typy = Enum.GetNames(enumType: typeof(TypAlbumu)).ToList();
        }
        #endregion

        #region wlasciwosci
        public int IndeksZaznaczonegoAlbumu
        {
            get => indeksZaznaczonegoAlbumu;
            set
            {
                indeksZaznaczonegoAlbumu = value;
                onPropertyChanged(nameof(IndeksZaznaczonegoAlbumu));
            }
        }

        public int IndeksZespolu
        {
            get => indeksZespolu;
            set
            {
                indeksZespolu = value;
                onPropertyChanged(nameof(IndeksZespolu));
            }
        }

        public int IndeksTypu
        {
            get => indeksTypu;
            set
            {
                indeksTypu = value;
                onPropertyChanged(nameof(IndeksTypu));
            }
        }

        public Album BiezacyAlbum { get; set; }

        public ObservableCollection<Album> Albumy
        {
            get => albumy;
            set
            {
                albumy = value;
                onPropertyChanged(nameof(Albumy));
            }
        }

        public ObservableCollection<Zespol> Zespoly
        {
            get => zespoly;
            set
            {
                zespoly = value;
                onPropertyChanged(nameof(Zespoly));
            }
        }

        public ObservableCollection<Skladowa> Skladowe
        {
            get => skladowe;
            set
            {
                 skladowe= value;
                onPropertyChanged(nameof(Skladowe));
            }
        }

        public List<string> Typy
        {
            get => typy;
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
        public ushort? ZespolId
        {
            get { return zespolId; }
            set
            {
                zespolId = value;
                onPropertyChanged(nameof(ZespolId));
            }
        }
        public string? Zespol
        {
            get { return zespol; }
            set
            {
                zespol = value;
                onPropertyChanged(nameof(Zespol));
            }
        }
        public string Tytul
        {
            get { return tytul; }
            set
            {
                tytul = value;
                onPropertyChanged(nameof(Tytul));
            }
        }
        public int? DataWydania
        {
            get { return dataWydania; }
            set
            {
                dataWydania = value;
                onPropertyChanged(nameof(DataWydania));
            }
        }
        public string? Dlugosc
        {
            get { return dlugosc; }
            set
            {
                dlugosc = value;
                onPropertyChanged(nameof(Dlugosc));
            }
        }
        public int? IleUtworow
        {
            get { return ileUtworow; }
            set
            {
                ileUtworow = value;
                onPropertyChanged(nameof(IleUtworow));
            }
        }
        public string? Wydawca
        {
            get { return wydawca; }
            set
            {
                wydawca = value;
                onPropertyChanged(nameof(Wydawca));
            }
        }
        public TypAlbumu? Typ
        {
            get { return typ; }
            set
            {
                typ = value;
                onPropertyChanged(nameof(Typ));
            }
        }

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
        public void OdswiezAlbumy()
        {
            model.OdswiezAlbumy();
            Albumy = model.Albumy;
            IndeksZaznaczonegoAlbumu = -1;
        }
        private ICommand zaladujWszystkieAlbumy = null;
        public ICommand ZaladujWszystkieAlbumy
        {
            get
            {
                if (zaladujWszystkieAlbumy == null)
                    zaladujWszystkieAlbumy = new RelayCommand(
                        arg =>
                        {
                            OdswiezAlbumy();
                        }
                        ,
                        arg => true
                        );

                return zaladujWszystkieAlbumy;
            }
        }

        private void CzyscSzczegoly()
        {
            ZespolId = null;
            Zespol = "";
            Tytul = "";
            DataWydania = null;
            Dlugosc = null;
            IleUtworow = null;
            Wydawca = null;
            Typ = null;
            Skladowe = null;

            DodawanieDostepne = true;
            EdycjaDostepna = false;
        }
        #endregion

        #region polecenia
        private ICommand pokazSzczegolyAlbumu = null;
        public ICommand PokazSzczegolyAlbumu
        {
            get
            {
                if (pokazSzczegolyAlbumu == null)
                    pokazSzczegolyAlbumu = new RelayCommand(
                        arg =>
                        {
                            if(IndeksZaznaczonegoAlbumu > -1)
                            {
                                ZespolId = BiezacyAlbum.ZespolId;
                                Zespol = BiezacyAlbum.Zespol;
                                Tytul = BiezacyAlbum.Tytul;
                                DataWydania = BiezacyAlbum.DataWydania;
                                Dlugosc = BiezacyAlbum.Dlugosc;
                                IleUtworow = BiezacyAlbum.IleUtworow;
                                Wydawca = BiezacyAlbum.Wydawca;
                                Typ = BiezacyAlbum.Typ;

                                IndeksZespolu = model.Zespoly.IndexOf(model.ZnajdzZespolPoId(ZespolId));
                                IndeksTypu = Typy.IndexOf(Typ.ToString());
                                Skladowe = model.PobierzSkladoweAlbumu(BiezacyAlbum);

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

                return pokazSzczegolyAlbumu;
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

        private ICommand dodajAlbum = null;
        public ICommand DodajAlbum
        {
            get
            {
                if (dodajAlbum == null)
                    dodajAlbum = new RelayCommand(
                        arg =>
                        {
                            ZespolId = Zespoly[indeksZespolu].Id;
                            var album = new Album(ZespolId, Tytul, DataWydania, Wydawca, Typ);

                            if (model.DodajAlbumDoBazy(album))
                            {
                                OdswiezAlbumy();
                                CzyscSzczegoly();
                                MessageBox.Show("Album został dodany do bazy!");
                            }
                        }
                        ,
                        arg => (Tytul != "") && (DataWydania >0) && (Wydawca != "") && (Typ != null)
                        );
                return dodajAlbum;
            }

        }

        private ICommand edytujAlbum = null;
        public ICommand EdytujAlbum
        {
            get
            {
                if (edytujAlbum == null)
                    edytujAlbum = new RelayCommand(
                    arg =>
                    {
                        ZespolId = Zespoly[indeksZespolu].Id;
                        model.EdytujAlbumWBazie(new Album(ZespolId, Tytul, DataWydania, Wydawca, Typ), BiezacyAlbum.Id);
                        OdswiezAlbumy();
                    }
                         ,
                    arg => (BiezacyAlbum?.Zespol != Zespol) || (BiezacyAlbum?.Tytul != Tytul) || (BiezacyAlbum?.DataWydania != DataWydania) || (BiezacyAlbum?.Wydawca != Wydawca) || (BiezacyAlbum?.Typ != Typ)
                   );
                return edytujAlbum;
            }
        }

        private ICommand usunAlbum = null;
        public ICommand UsunAlbum
        {
            get
            {
                if (usunAlbum == null)
                    usunAlbum = new RelayCommand(
                    arg =>
                    {
                        model.UsunAlbumZBazy(BiezacyAlbum.Id);
                        OdswiezAlbumy();
                        CzyscSzczegoly();
                        DodawanieDostepne = true;
                    }
                         ,
                    arg => IndeksZaznaczonegoAlbumu > -1
                   );
                return usunAlbum;
            }
        }
        #endregion
    }
}
