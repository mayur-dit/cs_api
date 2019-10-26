using System;
using System.Collections.Generic;

namespace cs_api.Models {
    public partial class Address {
        public Address() {
            Person = new HashSet<Person>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }

        public ICollection<Person> Person { get; set; }
    }
}