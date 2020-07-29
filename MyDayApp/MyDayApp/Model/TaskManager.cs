using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDayApp.Model {
    public class TaskManager {

        private List<Task> Tasks { get; set; }

        public TaskManager() {
            Tasks = Get();
        }

        public void Save(Task task) {
            Tasks.Add(task);
            Persist();
        }

        public List<Task> Get() {
            if (App.Current.Properties.ContainsKey("tasks")) {
                var json = App.Current.Properties["tasks"] as string;
                Tasks = JsonConvert.DeserializeObject<List<Task>>(json);
            } else {
                Tasks = new List<Task>();
            }
            return Tasks;
        }

        public void Finalize(int index) {
            var oldTask = Tasks.ElementAt(index);
            Tasks.RemoveAt(index);
            oldTask.Finalization = new DateTime();
            Tasks.Add(oldTask);
            Persist();
        }

        public void Remove(Task task) {
            Tasks.Remove(task);
            Persist();
        }

        private void Persist() {
            if (App.Current.Properties.ContainsKey("tasks")) {
                App.Current.Properties.Remove("tasks");
            }
            var json = JsonConvert.SerializeObject(Tasks);
            App.Current.Properties.Add("tasks", json);
        }

    }
}
