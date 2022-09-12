using Models;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services;
public class EmployeeStorage : IStorage
{
    public readonly List<Employee> _employees = new List<Employee>();
    public void Add(Employee employee)
    {        
        _employees.Add(employee);
    }        

    public void Add()
    {
        throw new NotImplementedException();
    }

    public void Delete()
    {
        throw new NotImplementedException();
    }

    public void Update()
    {
        throw new NotImplementedException();
    }
}