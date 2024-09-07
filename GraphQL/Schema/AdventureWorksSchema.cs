using GraphQLDapperService.GraphQL.Queries;
namespace GraphQLDapperService.GraphQL.Schema;

public class AdventureWorksSchema : global::GraphQL.Types.Schema
{
    public AdventureWorksSchema(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        Query = serviceProvider.GetRequiredService<EmployeeQuery>();
    }
}
