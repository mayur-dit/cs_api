using cs_api.Models;
using cs_api.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cs_api.Repository {
    public class PersonRepository : IPersonRepository {
        MainContext db;

        public PersonRepository(MainContext _db) {
            db = _db;
        }

        public async Task<List<Address>> GetAddresses() {
            if (db != null) return await db.Address.ToListAsync();
            return null;
        }

        public async Task<List<PersonViewModel>> GetPersons() {
            if (db != null) {
                return await (from p in db.Person
                    from c in db.Address
                    where p.AddressId == c.Id
                    select new PersonViewModel {
                        PersonId = p.PersonId,
                        Name = p.Name,
                        Hobbies = p.Hobbies,
                        AddressId = p.AddressId,
                        AddressTitle = c.Title,
                        CreatedDate = p.CreatedDate
                    }).ToListAsync();
            }

            return null;
        }

        public async Task<PersonViewModel> GetPerson(int? personId) {
            if (db != null) {
                return await (from p in db.Person
                    from c in db.Address
                    where p.PersonId == personId
                    select new PersonViewModel {
                        PersonId = p.PersonId,
                        Name = p.Name,
                        Hobbies = p.Hobbies,
                        AddressId = p.AddressId,
                        AddressTitle = c.Title,
                        CreatedDate = p.CreatedDate
                    }).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<int> AddPerson(Person person) {
            if (db != null) {
                await db.Person.AddAsync(person);
                await db.SaveChangesAsync();
                return person.PersonId;
            }

            return 0;
        }

        public async Task<int> DeletePerson(int? personId) {
            int result = 0;
            if (db != null) {
                var person = await db.Person.FirstOrDefaultAsync(x => x.PersonId == personId); // Find the person for specific person id
                if (person != null) {
                    db.Person.Remove(person); // Delete that person
                    result = await db.SaveChangesAsync(); // Commit the transaction
                }

                return result;
            }

            return result;
        }


        public async Task UpdatePerson(Person person) {
            if (db != null) {
                db.Person.Update(person); //Delete that person
                await db.SaveChangesAsync(); //Commit the transaction
            }
        }
    }
}