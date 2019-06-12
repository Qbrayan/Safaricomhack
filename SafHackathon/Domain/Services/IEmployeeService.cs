using SafHackathon.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafHackathon.Domain.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetSelect(List<Employee> e);
        Task<Employee> Update(Employee e);
    }
}
