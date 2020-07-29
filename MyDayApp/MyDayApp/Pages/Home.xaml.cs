using MyDayApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyDayApp.Pages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage {

        private TaskManager TaskManager { get; set; }

        public Home() {
            InitializeComponent();
            TaskManager = new TaskManager();
            SetCurrentDate();
            LoadList();
        }

        private void GoToCreateTaskPage(object sender, EventArgs args) {
            Navigation.PushAsync(new CreateTaskPage());
        }

        private void SetCurrentDate() {
            var now = DateTime.Now;
            TodayDate.Text = now.DayOfWeek.ToString() + ", " + now.ToString("dd/MM/yyyy");
        }

        private void LoadList() {
            TasksLayout.Children.Clear();
            List<Task> list = TaskManager.Get();
            foreach(var task in list)
                AddLine(task);
        }

        private void AddLine(Task task) {

            Image delete = new Image() {
                VerticalOptions = LayoutOptions.Center,
                Source = ImageSource.FromFile("Delete.png")
            };
            if (Device.RuntimePlatform == Device.UWP) {
                delete.Source = ImageSource.FromFile("Resources/Delete.png");
            }

            Image priority = new Image() {
                VerticalOptions = LayoutOptions.Center,
                Source = ImageSource.FromFile("p" + task.Priority + ".png")
            };
            if (Device.RuntimePlatform == Device.UWP) {
                priority.Source = ImageSource.FromFile("Resources/p" + task.Priority + ".png");
            }

            object centerStack = null;
            if(task.Finalization == null) {
                centerStack = new Label() {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Text = task.Name
                };
            } else {
                centerStack = new StackLayout() {
                    VerticalOptions = LayoutOptions.Center,
                    Spacing = 0,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };

                Label taskName = new Label() {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Text = task.Name
                };

                DateTime finalizationDate = task.Finalization.Value;
                String finalizationText = "Ended in " +
                    finalizationDate.DayOfWeek.ToString() + ", " +
                    finalizationDate.ToString("dd/MM/yyyy - hh:mm" + "h");
                Label finalization = new Label() {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    TextColor = Color.Gray,
                    FontSize = 10,
                    Text = finalizationText
                };

                ((StackLayout)centerStack).Children.Add(taskName);
                ((StackLayout)centerStack).Children.Add(finalization);
            }

            Image check = new Image() {
                VerticalOptions = LayoutOptions.Center,
                Source = ImageSource.FromFile("CheckOff.png")
            };
            if(Device.RuntimePlatform == Device.UWP) {
                check.Source = ImageSource.FromFile("Resources/CheckOff.png");
            }

            StackLayout line = new StackLayout {
                Orientation = StackOrientation.Horizontal,
                Spacing = 15
            };

            line.Children.Add(check);
            line.Children.Add(centerStack as View);
            line.Children.Add(priority);
            line.Children.Add(delete);

            TasksLayout.Children.Add(line);

        }

    }
}