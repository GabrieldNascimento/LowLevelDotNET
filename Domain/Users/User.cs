using LowLevelDotNET.Exceptions.Domain;
using System.Text.RegularExpressions;
namespace LowLevelDotNET.Domain.Users
{
    public class User : Entity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public int Age => CalculateAge(DateOfBirth, DateTime.Today);

        private const string EmailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        public User(string name, string email, DateTime dateOfBirth)
        {
            Id = Guid.NewGuid();
            SetName(name);
            SetEmail(email);
            SetDateOfBirth(dateOfBirth);
        }
        public bool SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidUserNameException("The name cannot be null or white space!");   
            }
            if(string.Equals(name, Name, StringComparison.Ordinal)) return false;
            Name = name;
            return true;
        }

        private int CalculateAge(DateTime dateOfBirth, DateTime today)
        {
            var age = today.Year - dateOfBirth.Year;
            if(dateOfBirth.Date > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }
        public bool SetEmail(string email) {
            if (string.IsNullOrWhiteSpace(email)) {
                throw new InvalidUserEmailException("Student email cannot be empty.");
            }

            if (email.Length < 5 || email.Length > 254) {
                throw new InvalidUserEmailException("Student email length must be between 5 and 254 characters.");
            }

            if (!Regex.IsMatch(email, EmailPattern)) {
                throw new InvalidUserEmailException("Student email format is invalid.");
            }

            if (String.Equals(email, Email, StringComparison.Ordinal)) return false;

            Email = email;
            return true;
        }

        public bool SetDateOfBirth(DateTime dateOfBirth) {
            var today = DateTime.Today;

            if (dateOfBirth > today) {
                throw new InvalidUserDateOfBirthException("Date of birth cannot be in the future.");
            }

            if (DateOfBirth == dateOfBirth) return false;

            DateOfBirth = dateOfBirth;
            return true;
        }
    }
}