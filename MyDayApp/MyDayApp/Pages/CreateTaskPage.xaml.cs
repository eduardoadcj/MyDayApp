using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyDayApp.Pages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateTaskPage : ContentPage {
        
        public byte SelectedPriority { get; set; }
        
        public CreateTaskPage() {
            InitializeComponent();
        }

        public void OnPrioritySelected(object sender, EventArgs args) {
            SetSelectedPriorityMark(sender);
            SetSelectedPriority(sender);
        }

        private void SetSelectedPriorityMark(object selected) {
            var stacks = PrioritysLayout.Children;
            foreach(var stack in stacks) {
                Label label = ((StackLayout)stack).Children[1] as Label;
                label.TextColor = Color.Gray;
            }

            var selectedLabel = ((StackLayout)selected).Children[1] as Label;
            selectedLabel.TextColor = Color.Black;
        }

        private void SetSelectedPriority(object selected) {
            var stack = (StackLayout)selected;
            FileImageSource source = ((Image)stack.Children[0]).Source as FileImageSource;
            String priority = source.File.ToString().Replace("Resources/", "").Replace(".png", "").Replace("p", "");
            TxtName.Text = priority;
        }

    }
}