using System.Collections.Generic;
using System.Threading.Tasks;
using Family_Web_API.Models;

namespace Family_Web_API.Data
{
    public interface IFamilyService
    {

        Task AddFamilyAsync(Family family);
     //   void RemoveFamilyAsync(Family family);
        Task RemoveFamilyAsync(string streetName, int houseNumber);
        void AddAdultToFamily(Family family, Adult adult);
        void AddChildToFamily(Family family, Child child);
        void AddPetToFamily(Family family, Pet pet);
        void RemoveAdultFromFamilyAsync(Adult adult);
        void RemoveChildFromFamilyAsync(Child child);
        void RemovePetFromFamilyAsync(Pet pet);
        Task<IList<Family>> GetFamiliesAsync();
        Task<IList<Family>>  GetFamilyAsync(string street, int number);
    }
}