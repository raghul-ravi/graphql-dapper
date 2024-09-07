using GraphQL.Types;
using GraphQLDapperService.GraphQL.Types;
using GraphQLDapperService.Repository;

namespace GraphQLDapperService.GraphQL.Queries;

public class EmployeeQuery : ObjectGraphType
{
    public EmployeeQuery(IEmployeeRepository employeeRepository)
    {
        Field<ListGraphType<EmployeeType>>("employees")
            .Description("Returns a list of employees")
            .ResolveAsync(async context =>
            {
                var employees = await employeeRepository.GetEmployeesAsync();
                return employees;
            });
    }
}
