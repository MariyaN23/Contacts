namespace Contacts.Core.Models
{
    public class Contact
    {
        public const int MAX_NAME_LENGTH = 100;
        public const int MAX_JOB_TITLE_LENGTH = 100; 
        private Contact(Guid id, string name, string mobilePhone, string jobTitle, DateOnly birthDate)
        {
            Id = id;
            Name = name;
            MobilePhone = mobilePhone;
            JobTitle = jobTitle;
            BirthDate = birthDate;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MobilePhone { get; set; }
        public string JobTitle { get; set; }
        public DateOnly BirthDate { get; set; }

        public static (Contact contact, string Error) Create(Guid id, string name, string mobilePhone, string jobTitle, DateOnly birthDate)
        {
            var error = string.Empty;
            if (string.IsNullOrEmpty(name) || name.Length > MAX_NAME_LENGTH)
            {
                error = $"Name cannot be empty or longer than {MAX_NAME_LENGTH} symbols";
            }
            if (string.IsNullOrEmpty(jobTitle) || jobTitle.Length > MAX_JOB_TITLE_LENGTH)
            {
                error = $"Job title cannot be empty or longer than {MAX_JOB_TITLE_LENGTH} symbols";
            }
            if (string.IsNullOrEmpty(mobilePhone))
            {
                error = "Mobile phone cannot be empty";
            }
            if (birthDate > DateOnly.FromDateTime(DateTime.Today))
            {
                error = "Date cannot be in the future";
            }
            var contact = new Contact(id, name, mobilePhone, jobTitle, birthDate);

            return (contact, error);
        }
    }
}
