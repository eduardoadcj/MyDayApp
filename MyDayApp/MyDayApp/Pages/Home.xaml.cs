using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyDayApp.Pages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage {
        public Home() {
            InitializeComponent();
        }

        private void GoToCreateTaskPage(object sender, EventArgs args) {
            Navigation.PushAsync(new CreateTaskPage());
        }

    }
}