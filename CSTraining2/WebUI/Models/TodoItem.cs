using System;

namespace CSTraining.WebUI.Models
{
    public class TodoItem
    {
        public Guid TodoItemId { get; set; }
        public string TodoText { get; set; }
        public bool IsDone { get; set; }
        public int Order { get; set; }
    }
}
