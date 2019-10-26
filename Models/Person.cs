using System;
using System.Collections.Generic;

namespace cs_api.Models {
    public partial class Person {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Hobbies { get; set; }
        public int? AddressId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Address Address { get; set; }
    }
}