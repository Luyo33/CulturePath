using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Patate
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();            
        }
        void NextPage(object sender, EventArgs e)
        {
            NextPage(); // trouver le moyen de passer à la page suivante
        }
        void WalkMode(object sender, EventArgs e)
        {
            WalkMode();
        }
        void GameMode(object sender, EventArgs e)
        {
            Gamemode();
        }
        void TouristMode(object sender, EventArgs e)
        {
            TouristMode();
        }
    }
}
