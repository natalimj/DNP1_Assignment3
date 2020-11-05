using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Family_Web_API.Models
{
public class Pet {
   // [JsonPropertyName("id")]
    public int Id { get; set; }
    [Required]
    
   // [JsonPropertyName("species")]
    public string Species { get; set; }
    [Required]
    
   // [JsonPropertyName("name")]
    public string Name { get; set; }
    [Required]
 //   [JsonPropertyName("age")]
    public int Age { get; set; }
}
}