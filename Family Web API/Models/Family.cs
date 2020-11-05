using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Family_Web_API.Models 
{
public class Family {
    
  
    [Required]
    public string StreetName { get; set; }
    [Required]
  
    public int HouseNumber{ get; set; }
    
    public List<Adult> Adults { get; set; }
  
    public List<Child> Children{ get; set; }
 
    public List<Pet> Pets{ get; set; }
    
   /*
   public int Id { get; set; }
    public string FamilyName { get; set; }
    [Required]
  [JsonPropertyName("streetName")]
    public string StreetName { get; set; }
    [Required]
    [JsonPropertyName("houseNumber")]
    public int HouseNumber{ get; set; }
    [JsonPropertyName("adults")]
    public List<Adult> Adults { get; set; }
    [JsonPropertyName("children")]
    public List<Child> Children{ get; set; }
    [JsonPropertyName("pets")]
    public List<Pet> Pets{ get; set; }
*/
    public Family() {
        Adults = new List<Adult>();     
        Children=new List<Child>();
        Pets=new List<Pet>();
    }
    
}

}