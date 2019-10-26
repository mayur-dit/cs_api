using System;

namespace cs_api.ViewModel {
    public class PersonViewModel {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Hobbies { get; set; }
        public int? AddressId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string AddressTitle { get; set; }
    }
}