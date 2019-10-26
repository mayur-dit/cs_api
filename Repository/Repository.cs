using cs_api.Models;
using cs_api.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cs_api.Repository {
    public interface IPersonRepository {
        Task<List<Address>> GetAddresses();
        Task<List<PersonViewModel>> GetPersons();
        Task<PersonViewModel> GetPerson(int? personId);
        Task<int> AddPerson(Person person);
        Task<int> DeletePerson(int? personId);
        Task UpdatePerson(Person person);
    }
}