using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TodoItem
    {
        public int TodoItemId { get; set; }
        public string TodoText { get; set; }
        public bool IsDone { get; set; }
        public int Order { get; set; }
    }
}
