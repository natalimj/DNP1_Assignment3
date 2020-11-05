using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Family_Web_API.Models
{
    public class Adult : Person {
        
      //  [JsonPropertyName("jobTitle")]
        public string JobTitle { get; set; }

        public override string ToString() {
            return JsonSerializer.Serialize(this);
        }

        public void Update(Adult toUpdate) {
            JobTitle = toUpdate.JobTitle;
            base.Update(toUpdate);
        }

    }
    
    public class ValidJobTitle : ValidationAttribute {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            List<string> valid = new[]
            {
                "Teacher", "Engineer", "Garbage Collector", "Shepherd", "Pilot", "Police Officer", "Pirate", "Fireman", "Astronaut",
                "Captain", "Soldier", "Pizza Chef", "Chef", "Ninja", "Doctor", "Janitor", "Factory Worker",
                "Chauffeur", "Waitress", "Nurse", "Chemist", "Caretaker", "Gardener", "Mascot", "Athlete", "Unemployed", "None"
            }.ToList();
            if (value != null &&valid.Contains(value.ToString().ToLower())) {
                return ValidationResult.Success; }
            return new ValidationResult("Please select a job title");
        }
    }
}