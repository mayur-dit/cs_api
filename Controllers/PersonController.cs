using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cs_api.Models;
using cs_api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace cs_api.Controllers {
    [Route("api/person/")]
    [ApiController]
    public class PersonController : ControllerBase {
        IPersonRepository _personRepository;

        public PersonController(IPersonRepository personRepository) {
            _personRepository = personRepository;
        }

        [HttpGet]
        [Route("GetAddresses")]
        public async Task<IActionResult> GetAddresses() {
            try {
                var addresses = await _personRepository.GetAddresses();
                if (addresses == null) return NotFound();
                return Ok(addresses);
            }
            catch (Exception) {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetPersons")]
        public async Task<IActionResult> GetPersons() {
            try {
                var persons = await _personRepository.GetPersons();
                if (persons == null) return NotFound();
                return Ok(persons);
            }
            catch (Exception) {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetPerson")]
        public async Task<IActionResult> GetPerson(int? personId) {
            if (personId == null) return BadRequest();
            try {
                var person = await _personRepository.GetPerson(personId);
                if (person == null) return NotFound();
                return Ok(person);
            }
            catch (Exception) {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddPerson")]
        public async Task<IActionResult> AddPerson([FromBody] Person model) {
            if (ModelState.IsValid) {
                try {
                    var personId = await _personRepository.AddPerson(model);
                    if (personId > 0) return Ok(personId);
                    else return NotFound();
                }
                catch (Exception) {
                    return BadRequest();
                }
            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("DeletePerson")]
        public async Task<IActionResult> DeletePerson(int? personId) {
            int result = 0;
            if (personId == null) return BadRequest();
            try {
                result = await _personRepository.DeletePerson(personId);
                if (result == 0) return NotFound();
                return Ok();
            }
            catch (Exception) {
                return BadRequest();
            }
        }


        [HttpPost]
        [Route("UpdatePerson")]
        public async Task<IActionResult> UpdatePerson([FromBody] Person model) {
            if (ModelState.IsValid) {
                try {
                    await _personRepository.UpdatePerson(model);
                    return Ok();
                }
                catch (Exception ex) {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException") {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }
    }
}