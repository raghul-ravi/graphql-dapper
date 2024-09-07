using System.Data;
using Dapper;
using GraphQLDapperService.Models;

namespace GraphQLDapperService.Repository;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetEmployeesAsync();
}

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IDbConnection _dbConnection;

    public EmployeeRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<Employee>> GetEmployeesAsync()
    {
        var sql = "SELECT BusinessEntityID, JobTitle, HireDate FROM HumanResources.Employee";
        return await _dbConnection.QueryAsync<Employee>(sql);
    }
}