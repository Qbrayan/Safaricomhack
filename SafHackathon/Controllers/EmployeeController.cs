using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SafHackathon.Domain.Models;
using SafHackathon.Domain.Services;

namespace SafHackathon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public  EmployeeController(IEmployeeService cardService)
        {
            _employeeService = cardService;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Employee> employees = new List<Employee>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://dummy.restapiexample.com/api/v1/employees"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    employees = JsonConvert.DeserializeObject<List<Employee>>(apiResponse);
                }
            }
            string json = JsonConvert.SerializeObject(employees);

            System.IO.File.WriteAllText(@"C:\employees.json", json);
            return Ok();
        }

        // GET: api/Employee/Select
        [HttpGet("Select")]
        public async Task<IActionResult> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://dummy.restapiexample.com/api/v1/employees"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    employees = JsonConvert.DeserializeObject<List<Employee>>(apiResponse);
                }
            }
            var list = await _employeeService.GetSelect(employees);

            if (list.Count() == 0)
            {
                return NotFound();
            }

            return Ok(list);
        }


  

        // POST: api/Employee
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Employee input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            HttpClient client = new HttpClient();
            StringContent queryString = new StringContent(input.ToString());
            var response = await client.PostAsync("http://dummy.restapiexample.com/api/v1/create",
                    queryString);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            
            System.IO.File.WriteAllText(@"C:\employee.txt", responseBody);
            return Ok();
        }
        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Employee value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            HttpClient client = new HttpClient();
            StringContent queryString = new StringContent(value.ToString());
            var response = await client.PutAsync("http://dummy.restapiexample.com/api/v1/update/" + id.ToString() +"/",
                    queryString);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            Employee employee = new Employee();
            employee = JsonConvert.DeserializeObject<Employee>(responseBody);
            var list = await _employeeService.Update(employee);

            if (list == null)
            {
                return NotFound();
            }

            return Ok(list);

        }

    }
}
