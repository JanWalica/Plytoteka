using Plytoteka.DAL.Entities;
using Plytoteka.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Plytoteka.Model
{
    class MainModel
    {
        public ObservableCollection<Album> Albumy { get; set; } = new ObservableCollection<Album>();
        public ObservableCollection<Artysta> Artysci { get; set; } = new ObservableCollection<Artysta>();
        public ObservableCollection<Czlonek> Czlonkowie { get; set; } = new ObservableCollection<Czlonek>();
        public ObservableCollection<Skladowa> Skladowe { get; set; } = new ObservableCollection<Skladowa>();
        public ObservableCollection<Utwor> Utwory { get; set; } = new ObservableCollection<Utwor>();
        public ObservableCollection<Wystapienie> Wystapienia { get; set; } = new ObservableCollection<Wystapienie>();
        public ObservableCollection<Zespol> Zespoly { get; set; } = new ObservableCollection<Zespol>();

        public MainModel()
        {
            var albumy = RepoAlbum.PobierzWszystko();
            foreach (var a in albumy)
                Albumy.Add(a);

            var artysci = RepoArtysta.PobierzWszystko();
            foreach (var a in artysci)
                Artysci.Add(a);

            var czlonkowie = RepoCzlonek.PobierzWszystko();
            foreach (var c in czlonkowie)
                Czlonkowie.Add(c);

            var skladowe = RepoSkladowa.PobierzWszystko();
            foreach (var s in skladowe)
                Skladowe.Add(s);

            var utwory = RepoUtwor.PobierzWszystko();
            foreach (var u in utwory)
                Utwory.Add(u);

            var wystapienia = RepoWystapienie.PobierzWszystko();
            foreach (var w in wystapienia)
                Wystapienia.Add(w);

            var zespoly = RepoZespol.PobierzWszystko();
            foreach (var z in zespoly)
                Zespoly.Add(z);
        }

        #region metody
        #region ALBUMY / SKLADOWE
        public void OdswiezAlbumy()
        {
            Albumy = new ObservableCollection<Album>();
            var albumy = RepoAlbum.PobierzWszystko();
            foreach (var a in albumy)
                Albumy.Add(a);
        }

        public void OdswiezSkladowe()
        {
            Skladowe = new ObservableCollection<Skladowa>();
            var skladowe = RepoSkladowa.PobierzWszystko();
            foreach (var s in skladowe)
                Skladowe.Add(s);
        }

        public Zespol ZnajdzZespolPoId(ushort? id)
        {
            foreach (var z in Zespoly)
            {
                if (z.Id == id)
                    return z;
            }
            return null;
        }

        public ObservableCollection<Skladowa> PobierzSkladoweAlbumu(Album album)
        {
            var skladowe = new ObservableCollection<Skladowa>();
            foreach (var skladowa in Skladowe)
            {
                if (skladowa.AlbumId == album.Id)
                {
                    skladowe.Add(skladowa);
                }
            }
            return skladowe;
        }

        public bool CzyAlbumJestJuzWRepozytorium(Album album) => Albumy.Contains(album);
        public bool DodajAlbumDoBazy(Album album)
        {
            if (!CzyAlbumJestJuzWRepozytorium(album))
            {
                if (RepoAlbum.Dodaj(album))
                {
                    Albumy.Add(album);
                    return true;
                }
            }
            return false;
        }
        public bool EdytujAlbumWBazie(Album album, ushort? idAlbumu)
        {
            if (RepoAlbum.Edytuj(album, idAlbumu))
            {
                for (int i = 0; i < Albumy.Count; i++)
                {
                    if (Albumy[i].Id == idAlbumu)
                    {
                        album.Id = idAlbumu;
                        Albumy[i] = new Album(album);
                    }
                }
                return true;
            }
            return false;
        }
        public bool UsunAlbumZBazy(ushort? idAlbumu)
        {
            // Usun skladowe albumu
            foreach(var skladowa in Skladowe)
            {
                if (skladowa.AlbumId == idAlbumu)
                {
                    if (RepoSkladowa.UsunPoAlbumie(idAlbumu))
                    {
                        for (int i = 0; i < Skladowe.Count; i++)
                        {
                            if (Skladowe[i].AlbumId == idAlbumu)
                            {
                                Skladowe.RemoveAt(i);
                            }
                        }
                    }
                }
            }
            // Usun album
            if (RepoAlbum.Usun(idAlbumu))
            {
                for (int i = 0; i < Albumy.Count; i++)
                {
                    if (Albumy[i].Id == idAlbumu)
                    {
                        Albumy.RemoveAt(i);
                    }
                }
                return true;
            }
            return false;
        }

        public bool CzySkladowaJestJuzWRepozytorium(Skladowa skladowa) => Skladowe.Contains(skladowa);
        public bool DodajSkladowaDoBazy(Skladowa skladowa)
        {
            if (!CzySkladowaJestJuzWRepozytorium(skladowa))
            {
                if (RepoSkladowa.Dodaj(skladowa))
                {
                    Skladowe.Add(skladowa);
                    return true;
                }
            }
            return false;
        }

        public bool UsunSkladowaZBazy(ushort? idAlbumu, ushort idUtworu)
        {
            if (RepoSkladowa.Usun(idAlbumu, idUtworu))
            {
                for (int i = 0; i < Albumy.Count; i++)
                {
                    if (Skladowe[i].AlbumId == idAlbumu && Skladowe[i].UtworId == idUtworu)
                    {
                        Skladowe.RemoveAt(i);
                    }
                }
                return true;
            }
            return false;
        }
        #endregion

        #region ARTYSCI
        public void OdswiezArtystow()
        {
            Artysci = new ObservableCollection<Artysta>();
            var artysci = RepoArtysta.PobierzWszystko();
            foreach (var a in artysci)
                Artysci.Add(a);
        }

        public bool CzyArtystaJestJuzWRepozytorium(Artysta artysta) => Artysci.Contains(artysta);
        public bool DodajArtysteDoBazy(Artysta artysta)
        {
            if (!CzyArtystaJestJuzWRepozytorium(artysta))
            {
                if (RepoArtysta.Dodaj(artysta))
                {
                    Artysci.Add(artysta);
                    return true;
                }
            }
            return false;
        }
        public bool EdytujArtysteWBazie(Artysta artysta, ushort idArtysty)
        {
            if (RepoArtysta.Edytuj(artysta, idArtysty))
            {
                for (int i = 0; i < Albumy.Count; i++)
                {
                    if (Artysci[i].Id == idArtysty)
                    {
                        artysta.Id = idArtysty;
                        Artysci[i] = new Artysta(artysta);
                    }
                }
                return true;
            }
            return false;
        }
        public bool UsunArtysteZBazy(ushort? idArtysty)
        {
            // Usun wystapienia artyscty
            foreach (var wystapienie in Wystapienia)
            {
                if (wystapienie.Artysta == idArtysty)
                {
                    if (RepoWystapienie.UsunPoArtyscie(idArtysty))
                    {
                        for (int i = 0; i < Wystapienia.Count; i++)
                        {
                            if (Wystapienia[i].Artysta == idArtysty)
                            {
                                Wystapienia.RemoveAt(i);
                            }
                        }
                    }
                }
            }
            // Usun czlonkow zespolu
            foreach (var czlonek in Czlonkowie)
            {
                if (czlonek.ArtystaId == idArtysty)
                {
                    if (RepoCzlonek.UsunPoArtyscie(idArtysty))
                    {
                        for (int i = 0; i < Wystapienia.Count; i++)
                        {
                            if (Czlonkowie[i].ArtystaId == idArtysty)
                            {
                                Czlonkowie.RemoveAt(i);
                            }
                        }
                    }
                }
            }
            // Usun artyste
            if (RepoArtysta.Usun(idArtysty))
            {
                for (int i = 0; i < Artysci.Count; i++)
                {
                    if (Artysci[i].Id == idArtysty)
                    {
                        Artysci.RemoveAt(i);
                    }
                }
                return true;
            }
            return false;
        }
        #endregion

        #region UTWORY
        public void OdswiezUtwory()
        {
            Utwory = new ObservableCollection<Utwor>();
            var utwory = RepoUtwor.PobierzWszystko();
            foreach (var u in utwory)
                Utwory.Add(u);
        }

        public bool CzyUtworJestJuzWRepozytorium(Utwor utwor) => Utwory.Contains(utwor);
        public bool DodajUtworDoBazy(Utwor utwor)
        {
            if (!CzyUtworJestJuzWRepozytorium(utwor))
            {
                if (RepoUtwor.Dodaj(utwor))
                {
                    Utwory.Add(utwor);
                    return true;
                }
            }
            return false;
        }
        public bool EdytujUtworWBazie(Utwor utwor, ushort idUtworu)
        {
            if (RepoUtwor.Edytuj(utwor, idUtworu))
            {
                for (int i = 0; i < Utwory.Count; i++)
                {
                    if (Utwory[i].Id == idUtworu)
                    {
                        utwor.Id = idUtworu;
                        Utwory[i] = new Utwor(utwor);
                    }
                }
                return true;
            }
            return false;
        }
        public bool UsunUtworZBazy(ushort? idUtworu)
        {
            // Usun skladowe
            foreach (var skladowe in Skladowe)
            {
                if (skladowe.UtworId == idUtworu)
                {
                    if (RepoSkladowa.UsunPoUtworze(idUtworu))
                    {
                        for (int i = 0; i < Skladowe.Count; i++)
                        {
                            if (Skladowe[i].UtworId == idUtworu)
                            {
                                Skladowe.RemoveAt(i);
                            }
                        }
                    }
                }
            }
            // Usun utwor
            if (RepoUtwor.Usun(idUtworu))
            {
                for (int i = 0; i < Utwory.Count; i++)
                {
                    if (Utwory[i].Id == idUtworu)
                    {
                        Utwory.RemoveAt(i);
                    }
                }
                return true;
            }
            return false;
        }
        #endregion

        #region ZESPOLY / CZLONKOWIE
        public void OdswiezZespoly()
        {
            Zespoly = new ObservableCollection<Zespol>();
            var zespoly = RepoZespol.PobierzWszystko();
            foreach (var z in zespoly)
                Zespoly.Add(z);
        }

        public void OdswiezCzlonkow()
        {
            Czlonkowie = new ObservableCollection<Czlonek>();
            var czlonkowie = RepoCzlonek.PobierzWszystko();
            foreach (var cz in czlonkowie)
                Czlonkowie.Add(cz);
        }

        public ObservableCollection<Czlonek> PobierzCzlonkowZespolu(Zespol zespol)
        {
            var czlonkowie = new ObservableCollection<Czlonek>();
            foreach (var czlonek in Czlonkowie)
            {
                if (czlonek.ZespolId == zespol.Id)
                {
                    czlonkowie.Add(czlonek);
                }
            }
            return czlonkowie;
        }

        public bool CzyZespolJestJuzWRepozytorium(Zespol zespol) => Zespoly.Contains(zespol);
        public bool DodajZespolDoBazy(Zespol zespol)
        {
            if (!CzyZespolJestJuzWRepozytorium(zespol))
            {
                if (RepoZespol.Dodaj(zespol))
                {
                    Zespoly.Add(zespol);
                    return true;
                }
            }
            return false;
        }
        public bool EdytujZespolWBazie(Zespol zespol, ushort idZespolu)
        {
            if (RepoZespol.Edytuj(zespol, idZespolu))
            {
                for (int i = 0; i < Zespoly.Count; i++)
                {
                    if (Zespoly[i].Id == idZespolu)
                    {
                        zespol.Id = idZespolu;
                        Zespoly[i] = new Zespol(zespol);
                    }
                }
                return true;
            }
            return false;
        }
        public bool UsunZespolZBazy(ushort? idZespolu)
        {
            // Usun czlonkow zespolu
            foreach (var czlonek in Czlonkowie)
            {
                if (czlonek.ZespolId == idZespolu)
                {
                    if (RepoCzlonek.UsunPoZespole(idZespolu))
                    {
                        for (int i = 0; i < Czlonkowie.Count; i++)
                        {
                            if (Czlonkowie[i].ZespolId == idZespolu)
                            {
                                Czlonkowie.RemoveAt(i);
                            }
                        }
                    }
                }
            }
            // Usun album
            if (RepoZespol.Usun(idZespolu))
            {
                for (int i = 0; i < Zespoly.Count; i++)
                {
                    if (Zespoly[i].Id == idZespolu)
                    {
                        Zespoly.RemoveAt(i);
                    }
                }
                return true;
            }
            return false;
        }

        public bool CzyCzlonekJestJuzWRepozytorium(Czlonek czlonek) => Czlonkowie.Contains(czlonek);
        public bool DodajCzlonkaDoBazy(Czlonek czlonek)
        {
            if (!CzyCzlonekJestJuzWRepozytorium(czlonek))
            {
                if (RepoCzlonek.Dodaj(czlonek))
                {
                    Czlonkowie.Add(czlonek);
                    return true;
                }
            }
            return false;
        }

        public bool UsunCzlonkaZBazy(ushort? idArtysty, ushort idZespolu)
        {
            if (RepoCzlonek.Usun(idArtysty, idZespolu))
            {
                for (int i = 0; i < Czlonkowie.Count; i++)
                {
                    if (Czlonkowie[i].ArtystaId == idArtysty && Czlonkowie[i].ZespolId == idZespolu)
                    {
                        Czlonkowie.RemoveAt(i);
                    }
                }
                return true;
            }
            return false;
        }
        #endregion
        #endregion
    }
}
