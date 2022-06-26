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
    }
}
