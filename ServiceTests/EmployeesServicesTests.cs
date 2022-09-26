using ModelsDb;
using Models;
using Services.Exceptions;
using Services;
using Xunit;
using Services.Storages;

namespace ServiceTests
{
    public class EmployeesServicesTests
    {
        [Fact]
        public void YongAgeExceptionTest()
        {
            // Arrange
            var employee = new EmployeeDb();
            employee.Birthday = new DateTime(2016, 2, 9);
            var employeeStorage = new EmployeeStorage();
            var employeeService = new EmployeeService(employeeStorage);

            // Act/Assert            
            Assert.Throws<YoungAgeException>(() => employeeService.Add(employee));            
        }

        [Fact]
        public void NoPassportExceptionTest()
        {
            // Arrange
            var employee = new EmployeeDb();
            var employeeStorage = new EmployeeStorage();
            var employeeService = new EmployeeService(employeeStorage);

            // Act/Assert            
            Assert.Throws<NoPassportException>(() => employeeService.Add(employee));
        }

        [Fact]
        public void GetPositiveTest()
        {
            // Arrange      
            var employeeStorage = new EmployeeStorage();
            var employeeService = new EmployeeService(employeeStorage);

            // Act
            var employees = employeeStorage.Data.Employees.ToList();
            Guid id = employees[0].Id;
            var employee = employeeService.Get(id);

            // Assert       
            Assert.True(employee.Equals(employees[0]));

        }

        [Fact]
        public void AddPositiveTest()
        {
            // Arrange
            var employeeStorage = new EmployeeStorage();
            var employeeService = new EmployeeService(employeeStorage);
            TestDataGenerator testDataGenerator = new TestDataGenerator();
            List<Employee> employees = testDataGenerator.GenerateThousandEmployees();
            int i = 0;

            // Act
            foreach (Employee employee in employees)
            {
                var employeeDB = new EmployeeDb();
                employeeDB.Name = employee.Name;
                employeeDB.Phone = employee.Phone;
                employeeDB.Birthday = employee.Birthday;
                employeeDB.Birthday = DateTime.SpecifyKind(employeeDB.Birthday, DateTimeKind.Utc);
                employeeDB.Id = Guid.NewGuid();
                employeeDB.Bonus = employee.Bonus;
                employeeDB.Passport = employee.Passport;
                employeeDB.Contract = employee.Contract;
                employeeDB.Salary = employee.Salary;
                employeeService.Add(employeeDB);
                i++;
            }
            employeeStorage.Data.SaveChanges();

            // Assert            
            Assert.True(i == 1000);
        }

        [Fact]
        public void UpdatePositiveTest()
        {
            // Arrange      
            var employeeStorage = new EmployeeStorage();
            var employeeService = new EmployeeService(employeeStorage);
            var employees = employeeStorage.Data.Employees.ToList();
            Guid id = employees[0].Id;
            var employee = employeeService.Get(id);
            employee.Name = "Жорик";

            // Act
            employeeService.Update(employee);
            employeeStorage.Data.SaveChanges();

            // Assert            
            Assert.True(employeeService.Get(id).Name == employee.Name);
        }

        [Fact]
        public void DeletePositiveTest()
        {
            // Arrange      
            var employeeStorage = new EmployeeStorage();
            var employeeService = new EmployeeService(employeeStorage);
            var employees = employeeStorage.Data.Employees.ToList();
            Guid id = employees[0].Id;
            var employee = employeeService.Get(id);

            // Act
            employeeService.Delete(employee);
            employeeStorage.Data.SaveChanges();

            // Assert            
            Assert.True(employeeService.Get(id) == null);
        }

        [Fact]
        public void SelectEmployeePositiveTest()
        {
            // Arrange   
            var employeeStorage = new EmployeeStorage();
            var employeeService = new EmployeeService(employeeStorage);            
            
            var filter = new Filter();
            filter.Name = "Алексей";
            filter.DateFrom = DateTime.Today.AddYears(-60);
            filter.DateFrom = DateTime.SpecifyKind(filter.DateFrom, DateTimeKind.Utc);
            filter.DateBefore = DateTime.Today;
            filter.DateBefore = DateTime.SpecifyKind(filter.DateBefore, DateTimeKind.Utc);

            // Act
            DateTime youngEmployeeBirthday = employeeService.GetEmployees(filter).Max(e => e.Birthday);
            var youngEmployee = employeeService.GetEmployees(filter).
                FirstOrDefault(e => e.Birthday.Equals(youngEmployeeBirthday));

            DateTime oldEmployeeBirthday = employeeService.GetEmployees(filter).Min(e => e.Birthday);
            var oldEmployee = employeeService.GetEmployees(filter).
                FirstOrDefault(e => e.Birthday.Equals(oldEmployeeBirthday));

            double averageAgeEmployee = employeeService.GetEmployees(filter)
                .Average(e => (DateTime.Now.Year - e.Birthday.Year));

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
