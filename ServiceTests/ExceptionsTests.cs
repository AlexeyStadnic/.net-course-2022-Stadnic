using Xunit;
using Models;
using Services;
using Services.Exceptions;

namespace ServiceTests;

public class ExceptionsTests
{
    [Fact]
    public void YongAgeExceptionTest()
    {
        // Arrange
        var client = new Client();
        client.Birthday = new DateTime(2016, 2,9);
        var clientService = new ClientService();

        var employee = new Employee();
        employee.Birthday = new DateTime(2016,2,9);
        var employeeService = new EmployeeService();
        
        // Act/Assert
        Assert.Throws<YongAgeException>(() => clientService.AddClient(client));
        Assert.Throws<YongAgeException>(() => employeeService.AddEmployee(employee));
    }
    
    [Fact]
    public void NoPassportExceptionTest()
    {
        // Arrange
        var client = new Client();
        //client.Passport = 14714;
        ClientService clientService = new ClientService();
        
        var employee = new Employee();
        //employee.Passport = 14714;
        var employeeService = new EmployeeService();
        
        // Act/Assert
        Assert.Throws<NoPassportException>(() => clientService.AddClient(client));
        Assert.Throws<NoPassportException>(() => employeeService.AddEmployee(employee));
    }
}