using System;

namespace CSTraining.WebUI.Models
{
    public class Animal
    {
        public Guid AnimalId { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public DateTime BirthDate { get; set; }
    }
}