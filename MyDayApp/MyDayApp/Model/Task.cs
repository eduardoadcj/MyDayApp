using System;
using System.Collections.Generic;
using System.Text;

namespace MyDayApp.Model {
    public class Task {

        public string Name { get; set; }
        public DateTime? Finalization { get; set; }
        public byte Priority { get; set; }

    }
}
