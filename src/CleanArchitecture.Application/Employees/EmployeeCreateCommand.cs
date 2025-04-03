using CleanArchitecture.Domain.Emplooyes;
using FluentValidation;
using GenericRepository;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace CleanArchitecture.Application.Employees
{
    public sealed record EmployeeCreateCommand(
        string FirstName,
        string LastName,
        DateOnly BirthOfDate,
        decimal Salary,
        PersonelInformation PersonelInformation,
        Address? Address,
        bool IsActive): IRequest<Result<string>>;  //taner saydam Hocanın result patterını - nuget pacjage kullanarak geriye string döndürdük

    public sealed class EmployeeCreateCommandValidator : AbstractValidator<EmployeeCreateCommand>
    {
        public EmployeeCreateCommandValidator()
        {
            RuleFor(x => x.FirstName).MinimumLength(3).WithMessage("Ad alanı en az 3 karakter olmalıdır");
            RuleFor(x => x.LastName).MinimumLength(3).WithMessage("Soyad alanı en az 3 karakter olmalıdır");
            RuleFor(x => x.PersonelInformation.IdentityNumber)
                .MinimumLength(11).WithMessage("Geçerli bir TC Numarası yazın")
                .MaximumLength(11).WithMessage("Geçerli bir TC Numarası yazın");
        }
    }
    internal sealed class EmployeeCreateCommandHandler(
        IEmployeeRepository employeeRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<EmployeeCreateCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(EmployeeCreateCommand request, CancellationToken cancellationToken)
        {
            var isEmployeeExists = await employeeRepository.AnyAsync(x => x.PersonelInformation.IdentityNumber == request.PersonelInformation.IdentityNumber, cancellationToken);

            if (isEmployeeExists) {
                return Result<string>.Failure("Employee already exists");
            }   

            Employee employee = request.Adapt<Employee>();  

            employeeRepository.Add(employee);   

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return "Employee Completed Successfully";
        }
    }

}
