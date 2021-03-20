using System;
using System.ComponentModel.DataAnnotations;

namespace School.Core
{
    public class Pupil
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override bool Equals(object obj)
            => obj is Pupil pupil &&
                   FirstName == pupil.FirstName &&
                   LastName == pupil.LastName;

        public override int GetHashCode() 
            => HashCode.Combine(FirstName, LastName);

        public override string ToString()
            => $"{FirstName} {LastName}";
    }
}
