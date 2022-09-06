using Models;
using Services.Exceptions;
using Services;
using Xunit;

namespace ServiceTests
{
    public class EmployeesExceptionsTests
    {
        [Fact]
        public void YongAgeExceptionTest()
        {
            // Arrange
            var employee = new Employee();
            employee.Birthday = new DateTime(2016, 2, 9);
            var employeeService = new EmployeeService();

            // Act/Assert            
            Assert.Throws<YongAgeException>(() => employeeService.AddEmployee(employee));
        }

        [Fact]
        public void NoPassportExceptionTest()
        {
            // Arrange
            var employee = new Employee();
            var employeeService = new EmployeeService();

            // Act/Assert            
            Assert.Throws<NoPassportException>(() => employeeService.AddEmployee(employee));
        }
    }
}
