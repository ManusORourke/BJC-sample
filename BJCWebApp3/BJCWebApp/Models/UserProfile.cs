using System.Text.Json.Serialization;
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
            DateOfBirth = DateTime.Now;
            HighestLevelOfEducation = 0;
            Location = String.Empty;
            PaymentType = String.Empty;
        }
        
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("phonenumber")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("isregistered")]
        public bool IsRegistered { get; set; }

        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("dateofbirth")]
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }

        [JsonPropertyName("highestlevelofeducation")]
        [Display(Name = "Highest level of Education")]
        public int HighestLevelOfEducation { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }

        [JsonPropertyName("paymenttype")]
        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; }
    }
}
