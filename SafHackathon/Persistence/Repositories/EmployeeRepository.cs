using Microsoft.EntityFrameworkCore;
using SafHackathon.Domain.Models;
using SafHackathon.Domain.Repositories;
using SafHackathon.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafHackathon.Persistence.Repositories
{
    public class EmployeeRepository : BaseRepository,IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Employee>> GetSelect(List<Employee> e)
        {
            foreach(Employee emp in e)
            {
                if (emp.employee_age > 25 && emp.employee_salary > 1000)
                {
                    _context.employee.Add(emp);
                }
            }
            await _context.SaveChangesAsync();
            return await _context.employee
              .ToListAsync();
        }

        public async Task<Employee> Update(Employee e)
        {
            _context.employee
              .Where(x => x.Id.Equals(e.Id))
              .ToList().ForEach(x => { x.employee_age = e.employee_age; x.employee_name = e.employee_name;
                  x.employee_salary = e.employee_salary;x.profile_image = e.profile_image;
              });
            await _context.SaveChangesAsync();
            return e;
        }
    }
}
