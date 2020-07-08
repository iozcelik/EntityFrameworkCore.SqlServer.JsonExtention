using System;
using System.Collections.Generic;

namespace EntityFrameworkCore.SqlServer.JsonExtention {
    public class Customer {
        public int Id { get; set; }
        public string Name { get; set; }
        public Company Company { get; set; }
        public Dictionary<string,object> ContactDetail { get; set; }
        public List<string> MenuItems { get; set; }
        public List<int> LuckyNumbers { get; set; }
    }

    public class Company {
        public string Name { get; set; }
        public DateTime? FoundDate { get; set; }
        public List<Branch> Branches { get; set; }
    }

    public class Branch {
        public string Name { get; set; }
        public string City { get; set; }
        public int? Code { get; set; }
    }
}
