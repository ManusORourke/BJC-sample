using System.ComponentModel.DataAnnotations;

namespace BJCWebApp.Models
{
    public class UserProfile
    {
        public UserProfile()
        {
            Name = String.Empty;
            PhoneNumber = String.Empty;
            Email = String.Empty;
            //Role = String.Empty;
            //Password = String.Empty;
            IsRegistered = false;
            ID = 0;
            Gender = String.Empty;
            DateTime dt = DateTime.Now;
            DateOfBirth = new DateTime(dt.Year - 25, dt.Month, dt.Day);
            HighestLevelOfEducation = 0;
            Location = String.Empty;
            PaymentType = String.Empty;
        }
        
        public string Name { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        [Display(Name = "Already registered with Ballymun Job Centre")] 
        public bool IsRegistered { get; set; }

        public int ID { get; set; }

        public string Gender { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Highest level of Education")]
        public int HighestLevelOfEducation { get; set; }

        public string Location { get; set; }

        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; }

        public string UserID { get; set; }

        public string Summary
        {
            get
            {
                return string.Format("{0}, {1}, {2}", Name, Gender, DateOfBirth.ToShortDateString());
            }
        }
    }
}
