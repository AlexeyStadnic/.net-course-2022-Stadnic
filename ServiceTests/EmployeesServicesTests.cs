using Models;
using Services.Exceptions;
using Services;
using Xunit;

namespace ServiceTests
{
    public class EmployeesServicesTests
    {
        [Fact]
        public void YongAgeExceptionTest()
        {
            // Arrange
            var employee = new Employee();
            employee.Birthday = new DateTime(2016, 2, 9);
            var employeeStorage = new EmployeeStorage();
            var employeeService = new EmployeeService(employeeStorage);

            // Act/Assert            
            Assert.Throws<YoungAgeException>(() => employeeService.AddEmployee(employee));            
        }


        [Fact]
        public void NoPassportExceptionTest()
        {
            // Arrange
            var employee = new Employee();
            var employeeStorage = new EmployeeStorage();
            var employeeService = new EmployeeService(employeeStorage);

            // Act/Assert            
            Assert.Throws<NoPassportException>(() => employeeService.AddEmployee(employee));
        }

        [Fact]
        public void AddPositiveTest()
        {
            // Arrange      

            var employeeStorage = new EmployeeStorage();
            var employeeService = new EmployeeService(employeeStorage);
            TestDataGenerator testDataGenerator = new TestDataGenerator();
            List<Employee> employees = testDataGenerator.GenerateThousandEmployees();

            // Act
            foreach (Employee employee in employees)
            {                
                employeeService.AddEmployee(employee);
            }

            // Assert
            if (employeeStorage._employees.Count != 1000)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void SelectEmployeePositiveTest()
        {
            // Arrange      

            var employeeStorage = new EmployeeStorage();
            var employeeService = new EmployeeService(employeeStorage);
            TestDataGenerator testDataGenerator = new TestDataGenerator();
            List<Employee> employees = testDataGenerator.GenerateThousandEmployees();

            foreach (Employee employee in employees)
            {
                employeeService.AddEmployee(employee);
            }

            // Act
            DateTime youngEmployeeBirthday = employeeService.GetEmployees().Max(e => e.Birthday);
            var youngEmployee = employeeService.GetEmployees().FirstOrDefault(e => e.Birthday.Equals(youngEmployeeBirthday));


            DateTime oldEmployeeBirthday = employeeService.GetEmployees().Min(e => e.Birthday);
            var oldEmployee = employeeService.GetEmployees().FirstOrDefault(e => e.Birthday.Equals(oldEmployeeBirthday));

            double averageAgeEmployee = employeeService.GetEmployees().Average(e => (DateTime.Now.Year - e.Birthday.Year));

            // Assert
            if (averageAgeEmployee > 18)
            {
                Assert.True(true);
            }
            else
            {
                Assert.True(false);
            }
        }
    }
}
