using System;

namespace Vereinsverwaltung.Model
{
    public class Member
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Name { get => FirstName + " " + LastName; }
    }
}
