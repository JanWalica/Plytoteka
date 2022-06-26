using Plytoteka.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plytoteka.ViewModel
{
    class MainViewModel
    {
        private MainModel model = new MainModel();

        public TabAlbumyVM AlbumyLista { get; set; }
        public TabArtysciVM ArtysciLista { get; set; }
        public TabUtworyVM UtworyLista { get; set; }
        public TabZespolyVM ZespolyLista { get; set; }

        public MainViewModel()
        {
            AlbumyLista = new TabAlbumyVM(model);
            ArtysciLista = new TabArtysciVM(model);
            UtworyLista = new TabUtworyVM(model);
            ZespolyLista = new TabZespolyVM(model);
        }
    }
}
