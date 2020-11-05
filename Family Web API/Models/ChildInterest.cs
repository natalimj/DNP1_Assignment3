using System.Text.Json.Serialization;


namespace Family_Web_API.Models {
public class ChildInterest {
   
  //  [JsonPropertyName("childId")]
    public int ChildId { get; set; }
    [JsonIgnore]
    public Child Child { get; set; }
 //   [JsonPropertyName("interestId")]
    public string InterestId { get; set; }
    [JsonIgnore]
    public Interest Interest { get; set; }

    /*
    public override bool Equals(object? obj) {
        ChildInterest ci = ((ChildInterest) obj);
        if (ci.Child.Equals(Child) && ci.Interest.Equals(Interest)) return true;
        return base.Equals(obj);
    }
*/
   
}
}