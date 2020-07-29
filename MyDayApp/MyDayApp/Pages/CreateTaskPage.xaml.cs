using MyDayApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyDayApp.Pages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateTaskPage : ContentPage {
        
        public Task Task { get; set; }
        
        public CreateTaskPage() {
            InitializeComponent();
            Clear();
        }

        public void AttemptSave(object sender, EventArgs args) {
            Task.Name = TxtName.Text.Trim();
            if (Task.Name.Length == 0) {
                DisplayAlert("Erro", "A name is required!", "Ok");
                return;
            } 
            if(Task.Priority <= 0) {
                DisplayAlert("Erro", "Invalid priority!", "Ok");
                return;
            }
            Save();
        }

        private void Save() {
            new TaskManager().Save(this.Task);
            OnSuccess();
        }

        private void Clear() {
            this.Task = new Task();
            TxtName.Text = "";
        }

        private void OnSuccess() {
            Clear();
            DisplayAlert("Success!", "Task registered.", "OK");
            App.Current.MainPage = new NavigationPage(new Home());
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
            Task.Priority = byte.Parse(priority);
        }

    }
}