using Models;
using Services;
using Xunit;

namespace ServiceTests;

public class EquivalenceTests
{
    [Fact]
    public void GetHashCodeNecessityPositivTest()
    {
        // Arrange
        TestDataGenerator testDataGenerator = new TestDataGenerator();    
        List<Client> clients = testDataGenerator.GenerateThousandClients();
        Dictionary<Client, List<Account>> dictionary = testDataGenerator.GenerateDictionaryClients(clients);
        
        Client newClient = new Client();
        newClient.Name = clients[0].Name;
        newClient.Passport = clients[0].Passport;   
        newClient.Phone = clients[0].Phone;
        newClient.Birthday = clients[0].Birthday;

        // Act
        List<Account> newAccounts = dictionary[newClient];
        
        //Assert
        Assert.True(true);
    }    
    
    [Fact]
    public void GetHashCodeNecessityPositivEmployeeTest()
    {
        // Arrange
        TestDataGenerator testDataGenerator = new TestDataGenerator();
        List<Employee> employees = testDataGenerator.GenerateThousandEmployees();

        //Act
        Employee newEmployee = new Employee();
        newEmployee.Name = employees[0].Name;
        newEmployee.Passport = employees[0].Passport;   
        newEmployee.Phone = employees[0].Phone;
        newEmployee.Birthday = employees[0].Birthday;
        newEmployee.Contract = employees[0].Contract;
        newEmployee.Salary = employees[0].Salary;

        //Assert
        Assert.Equal(newEmployee,employees[0]);
    }    
}