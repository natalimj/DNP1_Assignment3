using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Family_Web_API.Models;


namespace Family_Web_API.Data
{
    public class FamilyService :IFamilyService
    {
        
        private string familiesFile = "families.json";
        
        public IList<Family> Families { get;  set; }
        
        public FamilyService()
        {

            if (!File.Exists(familiesFile))
            {
                Families = new[]
                {
                    new Family
                    {
                        StreetName = "Street1",
                        HouseNumber = 1
                    }
                }.ToList();
                WriteFamiliesToFile();
            }
            else
            {
                string content = File.ReadAllText(familiesFile);
                Families = JsonSerializer.Deserialize<List<Family>>(content);
            }

        }

        
        private void WriteFamiliesToFile()
        {
            string familiesAsJson = JsonSerializer.Serialize(Families,new JsonSerializerOptions{WriteIndented = true});
            File.WriteAllText(familiesFile, familiesAsJson);
         
        }

        //Get
        
        public async Task<IList<Family>> GetFamiliesAsync()
        {
            List<Family> tmp=new List<Family>(Families);
            return tmp;
        }

        public async Task<IList<Family>> GetFamilyAsync(string street, int number)
        {
            IList<Family> families=Families.Where(f => f.StreetName.Equals(street) && f.HouseNumber == number).ToList();
            return families;
        }


        public async Task AddFamilyAsync(Family family)
        {
            
            Families.Add(family);
            WriteFamiliesToFile();
        }

        public async Task RemoveFamilyAsync(string streetName, int houseNumber)
        {
            Family family = Families.Where(f => f.StreetName.Equals(streetName) && f.HouseNumber == houseNumber).ToList()[0];
            Families.Remove(family);
            WriteFamiliesToFile();
        }

        public void AddAdultToFamily(Family family,Adult adult)
            {
                family.Adults.Add(adult);
                WriteFamiliesToFile();
            }
        
        
        public void AddChildToFamily(Family family,Child child)
        {
            family.Children.Add(child);
                WriteFamiliesToFile();
        }
        
        public void AddPetToFamily(Family family, Pet pet)
        {
            family.Pets.Add(pet);
            WriteFamiliesToFile();
        }

        public void RemoveAdultFromFamilyAsync(Adult adult)
        {
            foreach (var family in Families)
            {
                foreach (var ad in family.Adults.ToList())
                {
                    if (adult.Id == ad.Id)
                    {
                        family.Adults.Remove(adult);
                    }
                }
            }
            WriteFamiliesToFile();
        }

        public void RemoveChildFromFamilyAsync(Child child)
        {
            foreach (var family in Families)
            {
                foreach (var ch in family.Children.ToList())
                {
                    if (child.Id == ch.Id)
                    {
                        family.Children.Remove(child);
                    }
                }
            }
            
            WriteFamiliesToFile();
        }

        public void RemovePetFromFamilyAsync(Pet pet)
        {
            foreach (var family in Families)
            {
                foreach (var pt in family.Pets.ToList())
                {
                    if (pet.Id == pt.Id)
                    {
                        family.Pets.Remove(pet);
                    }
                }
            }
            WriteFamiliesToFile();
        }
    }
}