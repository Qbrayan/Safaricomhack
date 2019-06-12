using SafHackathon.Domain.Models;
using SafHackathon.Domain.Repositories;
using SafHackathon.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafHackathon.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> GetSelect(List<Employee> e)
        {
            return await _employeeRepository.GetSelect( e);
        }

        public async Task<Employee> Update(Employee e)
        {
            return await _employeeRepository.Update(e);
        }
    }
}
