using System.Text.Json.Serialization;

namespace BJCWeb.Models
{
    public class User
    {
        public User()
        {
            Name = String.Empty;
            PhoneNumber = String.Empty;
            Email = String.Empty;
            Role = String.Empty;
            Password = String.Empty;
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
        public string PhoneNumber { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("isregistered")]
        public bool IsRegistered { get; set; }

        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("dateofbirth")]
        public DateTime DateOfBirth { get; set; }

        [JsonPropertyName("highestlevelofeducation")]
        public int HighestLevelOfEducation { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }

        [JsonPropertyName("paymenttype")]
        public string PaymentType { get; set; }
    }
}
