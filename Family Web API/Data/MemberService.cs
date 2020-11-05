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
    public class MemberService : IMemberService
    {
        private string adultsFile = "adults.json";
        private string childrenFile = "children.json";
        private string petsFile = "pets.json";
        private string childInterestFile = "childinterest.json";
        

        public IList<Adult> Adults { get; private set; }
        public IList<Child> Children { get; private set; }
        
        public IList<Pet> Pets { get; private set; }
        public IList<ChildInterest> ChildInterests { get;  set; }
        
        public MemberService()
        {
         
            if (!File.Exists(adultsFile))
            {
                WriteAdultsToFile();
            }
            else
            {
                string content = File.ReadAllText(adultsFile);
                Adults = JsonSerializer.Deserialize<List<Adult>>(content);
            }

            if (!File.Exists(childrenFile))
            {
                Children = new[]
                {
                    new Child
                    {
                        Id = 1,
                        FirstName= "Peter",
                        LastName= "Smith",
                        HairColor= "Leverpostej",
                        EyeColor="Brown",
                        Age= 5,
                        Weight= 26,
                        Height= 120, 
                        Sex= "M",
                       
                    }
                }.ToList();
                
                WriteChildrenToFile();
                
            }
            else
            {
                string content2 = File.ReadAllText(childrenFile);
                Children = JsonSerializer.Deserialize<List<Child>>(content2);
            }
            
            if (!File.Exists(petsFile))
            {
                Pets=new[]{
                    new Pet
                    {
                        Id = 1,
                        Species = "Cat",
                        Name = "Cutie",
                        Age= 1
                    }
                }.ToList();
                WritePetsToFile();
            }
            else
            {
                string content3 = File.ReadAllText(petsFile);
                Pets = JsonSerializer.Deserialize<List<Pet>>(content3);
            }

            if (!File.Exists(childInterestFile))
            {
                ChildInterests=new[]{
                new ChildInterest()
                {
                   ChildId=1,
                   InterestId = "Lego"
                   
                }
            }.ToList();
                
                WriteChildInterestToFile();
            }
            else
            {
                string content4 = File.ReadAllText(childInterestFile);
                ChildInterests = JsonSerializer.Deserialize<List<ChildInterest>>(content4);
            }
            
        }
        private void WriteAdultsToFile()
        {
            string adultsAsJson = JsonSerializer.Serialize(Adults,new JsonSerializerOptions{WriteIndented = true});
            File.WriteAllText(adultsFile, adultsAsJson);
        }
        private void WriteChildrenToFile()
        {
            string childrenAsJson = JsonSerializer.Serialize(Children,new JsonSerializerOptions{WriteIndented = true});
            File.WriteAllText(childrenFile, childrenAsJson);
        }

        private void WritePetsToFile()
        {
            string petsAsJson = JsonSerializer.Serialize(Pets,new JsonSerializerOptions{WriteIndented = true});
            File.WriteAllText(petsFile, petsAsJson);
            
        }
        
        private void WriteChildInterestToFile()
        {
            string childInterestsAsJson = JsonSerializer.Serialize(ChildInterests,new JsonSerializerOptions{WriteIndented = true});
            File.WriteAllText(childInterestFile, childInterestsAsJson);
        }
        
        public void RemoveAdult(Adult adult)
        {
            
            Adults.Remove(adult);
            WriteAdultsToFile();
        }
       
        public void AddChildInterest(Child child, String interestType)
        {
            
            ChildInterest childInterest=new ChildInterest();
            childInterest.Child = child;
            childInterest.ChildId = child.Id;
            // childInterest.Interest = interest;
            childInterest.InterestId= interestType;
            ChildInterests.Add(childInterest);
            child.ChildInterests.Add(childInterest);
            
            WriteChildrenToFile();
            WriteChildInterestToFile();
            
        }

        public void AddChildPet(Child child, Pet pet)
        {   
           
          //  AddPet(pet);
            child.Pets.Add(pet);
            WriteChildrenToFile();
        }
        
        
        public async Task<IList<Adult>> GetAdultsAsync()
        {
            List<Adult> tmp=new List<Adult>(Adults);
            return tmp;
        }

        public async Task<IList<Child>> GetChildrenAsync()
        {
            List<Child> tmp=new List<Child>(Children);
            return tmp;
        }

        public async Task<IList<Pet>> GetPetsAsync()
        {
            List<Pet> tmp=new List<Pet>(Pets);
            return tmp;
        }

        public async Task<Adult> AddAdultAsync(Adult adult)
        {
            int max = Adults.Max(adult => adult.Id);
            adult.Id = (++max);
            Adults.Add(adult);
            WriteAdultsToFile();
            return adult;
        }

        public async Task<Child> AddChildAsync(Child child)
        {
            int max = Children.Max(child => child.Id);
            child.Id = (++max);
            Children.Add(child);
            WriteChildrenToFile();
            return child;
        }

        public async Task<Pet> AddPetAsync(Pet pet)
        {
            int max = Pets.Max(pet => pet.Id);
            pet.Id = (++max);
            Pets.Add(pet);
            WritePetsToFile();
            return pet;
        }

        public async Task<Pet> RemovePetAsync(int id)
        {
            foreach (var pet in Pets)
            {
                if (pet.Id == id)
                {
                    Pets.Remove(pet);
                    WritePetsToFile();
                    return pet;
                }
            }
            return null;
        }

        public async Task<Child> RemoveChildAsync(int id)
        {
            Child child = Children.Where(c => c.Id == id).ToList()[0];
            Children.Remove(child);
            
            foreach (var interest in ChildInterests.ToList())
            {
                if (child.Id == interest.ChildId)
                {
                    ChildInterests.Remove(interest);
                }
            }
            foreach (var pet in child.Pets.ToList())
            {
                Pets.Remove(pet);
            }
            
            Children.Remove(child);
            WriteChildrenToFile();
            WriteChildInterestToFile();
            WritePetsToFile();
            return child;
        }

        public async Task<Adult> RemoveAdultAsync(int id)
        {
            Adult adult = Adults.Where(a => a.Id == id).ToList()[0];
            Adults.Remove(adult);
            return adult;
        }

        public async Task RemoveAllFamilyMembersAsync(Family family)
        {
            foreach (var adult in family.Adults)
            {
                RemoveAdultAsync(adult.Id);
            }

            foreach (var child in family.Children)
            {
                RemoveChildAsync(child.Id);
            }

            foreach (var pet in family.Pets)
            {
                RemovePetAsync(pet.Id);
            }
        }
    }
}