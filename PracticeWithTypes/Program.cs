using Models;
using BankService;

class Program
{
    static void Main()
    {

        DateTime birthday = new DateTime(1986, 2, 9);        
        Employee human = new Employee();
        human.Name = "Алексей";
        human.Passport = 14714;
        human.Birthday = birthday;
        human.Phone = "077881886";

        UpdateContractEmployee(human); // меняет контракт, используя неправильный подход
        Console.WriteLine(human.Contract);        

        human.Contract = UpdateContractEmployeeCorrect(human.Name, human.Passport, human.Phone, human.Birthday); // меняет контракт, используя правильный подход
        Console.WriteLine(human.Contract);


        Currency currency = new Currency();
        currency.Code = 978;
        currency.Name = "EUR";
        
        UpdateCurrency(currency); // не меняет свойства валюты
        Console.WriteLine("Изначально используется валюта EUR, пробуем поменять. Результат " + currency.Name);
        
        currency = UpdateCurrencyCorrect(currency); // меняет свойства валюты
        Console.WriteLine("Изначально используется валюта EUR, пробуем поменять. Результат " + currency.Name);

        // рассчет зарплаты владельцев банка
        Bank bank = new Bank();
        Console.WriteLine("Зарплата владельцев банка равна: ");
        human.Salary = bank.PayrollForBankOwners(20, 10, 3);
        Console.WriteLine(human.Salary);
        
        // Превращение клиента в сотрудника 
        Client client = new Client();
        client.Name = "Ольга";
        Employee employee = bank.ConversionClientToEmployee(client);
        employee.Salary = 500;
        Console.WriteLine("Зарплата нового сотрудника, по имени " + 
                          employee.Name + ", составляет " + employee.Salary + " " + currency.Name);
        
        // неправильный подход к обновлению контракта сотрудника       
        void UpdateContractEmployee(Employee employee)
        {
            employee.Contract = "Здравствуйте, дорогой " + employee.Name + ", " + employee.Birthday.Year + 
                                " года рождения. С Вами заключен новый контракт, в котором указаны " + 
                                "номер паспорта " + employee.Passport + " и номер телефона " +
                                employee.Phone + ". Проверьте пожалуйста правильность данных.";
        }


        // правильный подход к обновлению контракта сотрудника
        string UpdateContractEmployeeCorrect(string name, int passport, string phone, DateTime birthday)
        {
            return "Здравствуйте, дорогой " + name + ", " + birthday.Year + 
                   " года рождения. С Вами заключен новый контракт, в котором указаны " + 
                   "номер паспорта " + passport + " и номер телефона " +
                   phone + ". Проверьте пожалуйста правильность данных.";
        }

        // не меняет свойства валюты
        void UpdateCurrency(Currency currency)
        {
            currency.Code = 840;
            currency.Name = "USD";
        }
        
        // меняет свойства валюты
        Currency UpdateCurrencyCorrect(Currency currency)
        {
            currency.Code = 840;
            currency.Name = "USD";
            return currency;
        }
    }
}
