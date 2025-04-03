using CleanArchitecture.Domain.Emplooyes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace CleanArchitecture.Application.Employees
{
    public sealed record EmployeeGetQuery(
    Guid Id) : IRequest<Result<Employee>>;

    internal sealed class EmployeeGetQueryHandler(
        IEmployeeRepository employeeRepository) : IRequestHandler<EmployeeGetQuery, Result<Employee>>
    {
        public async Task<Result<Employee>> Handle(EmployeeGetQuery request, CancellationToken cancellationToken)
        {
            var employee = await employeeRepository.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (employee is null)
            {
                return Result<Employee>.Failure("Personel is not found");
            }

            return employee;
        }
    }
}
