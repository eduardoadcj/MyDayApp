using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDayApp.Model {
    public class TaskManager {

        private List<Task> tasks { get; set; }

        public void Save(Task task) {
            tasks.Add(task);
            Persist();
        }

        public List<Task> Get() {
            if (App.Current.Properties.ContainsKey("tasks")) {
                tasks = (List<Task>)App.Current.Properties["tasks"];
            } else {
                tasks = new List<Task>();
            }
            return tasks;
        }

        public void Finalize(int index) {
            var oldTask = tasks.ElementAt(index);
            tasks.RemoveAt(index);
            oldTask.Finalization = new DateTime();
            tasks.Add(oldTask);
            Persist();
        }

        public void Remove(Task task) {
            tasks.Remove(task);
            Persist();
        }

        private void Persist() {
            if (App.Current.Properties.ContainsKey("tasks")) {
                App.Current.Properties.Remove("tasks");
            }
            App.Current.Properties.Add("tasks", tasks);
        }

    }
}
