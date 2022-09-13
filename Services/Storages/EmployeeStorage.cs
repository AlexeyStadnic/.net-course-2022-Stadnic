using Models;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Storages;

namespace Services;
public class EmployeeStorage : IEmployeeStorage
{    
    public List<Employee> Data { get; }

    public EmployeeStorage()
    {
        Data = new List<Employee>();
    }
    public void Add(Employee employee)
    {        
        Data.Add(employee);
    }        

    public void Add()
    {
        throw new NotImplementedException();
    }

    public void Delete(Employee employee)
    {
        throw new NotImplementedException();
    }

    public void Update(Employee employee)
    {
        throw new NotImplementedException();
    }
}