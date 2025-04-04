﻿using CleanArchitecture.Application.Employees;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace WebApi.Controllers
{
    [Route("odata")]
    [ApiController]
    [EnableQuery]
    // [Authorize("Bearer")]    her sınıfa zorunlu yazsın dersen programcsde RequireAuthorization() ekle
    public class AppODataController(
        ISender sender) : ODataController
    {

        public static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new();
            builder.EnableLowerCamelCase();
            builder.EntitySet<EmployeeGetAllQueryResponse>("employees");
            return builder.GetEdmModel();
        }

        [HttpGet("employees")]
        public async Task<IQueryable<EmployeeGetAllQueryResponse>> GetAllEmployees(CancellationToken cancellationToken)
        {
            var response = await sender.Send(new EmployeeGetAllQuery(), cancellationToken);
            return response;    
        }


    }
}
