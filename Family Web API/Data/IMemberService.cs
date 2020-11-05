using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Family_Web_API.Models;

namespace Family_Web_API.Data
{
    public interface IMemberService
    {
        Task<IList<Adult>> GetAdultsAsync();
        Task<IList<Child>> GetChildrenAsync();
        Task<IList<Pet>> GetPetsAsync();
        
        Task<Adult> AddAdultAsync(Adult adult);
        Task<Child> AddChildAsync(Child child);
        Task<Pet> AddPetAsync(Pet pet);

        Task<Pet> RemovePetAsync(int id);
        Task<Child> RemoveChildAsync(int id);
        Task<Adult> RemoveAdultAsync(int id);
        Task RemoveAllFamilyMembersAsync(Family family);
       







    }
}