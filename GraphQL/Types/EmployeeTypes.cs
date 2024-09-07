using GraphQL.Types;
using GraphQLDapperService.Models;

namespace GraphQLDapperService.GraphQL.Types;

public class EmployeeType : ObjectGraphType<Employee>
{
    public EmployeeType()
    {
        Field(x => x.BusinessEntityID).Description("The ID of the Employee.");
        Field(x => x.JobTitle).Description("The Job Title of the Employee.");
        Field(x => x.HireDate).Description("The Hire Date of the Employee.");
    }
}