﻿using System;

namespace CSTraining.WebUI.Models
{
    public class Person
    {
        public Guid PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Race { get; set; }
        public DateTime BirthDate { get; set; }
    }
}