using CleanArchitecture.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Emplooyes 
{
    public sealed class Employee  : Entity
    {

        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string FullName => string.Join(" ", FirstName, LastName);
        public DateOnly BirthOfDate { get; set; }
        public decimal Salary { get; set; }
        public PersonelInformation PersonelInformation { get; set; } = default!;
        public Address Address { get; set; } = default!;

        

    }

}
