using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Emplooyes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Employees
{
    public sealed record EmployeeGetAllQuery() : IRequest<IQueryable<EmployeeGetAllQueryResponse>>;

    public sealed class EmployeeGetAllQueryResponse : EntityDto
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateOnly BirthOfDate { get; set; }
        public decimal Salary { get; set; }
        public string IdentityNumber { get; set; } = default!;
    }

    internal sealed class EmployeeGetAllQueryHandler(
        IEmployeeRepository employeeRepository) : IRequestHandler<EmployeeGetAllQuery, IQueryable<EmployeeGetAllQueryResponse>>
    {
        public Task<IQueryable<EmployeeGetAllQueryResponse>> Handle(EmployeeGetAllQuery request, CancellationToken cancellationToken)
        {
            var response = employeeRepository.GetAll().
                Select(s => new EmployeeGetAllQueryResponse
                {
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    BirthOfDate = s.BirthOfDate,
                    Salary = s.Salary,
                    IdentityNumber = s.PersonelInformation.IdentityNumber,
                    CreateAt = s.CreateAt,
                    DeleteAt = s.DeleteAt,
                    UpdateAt = s.UpdateAt,
                    Id = s.Id,
                    IsDeleted = s.IsDeleted

                }).AsQueryable();

            return Task.FromResult(response);
        }
    }
}
