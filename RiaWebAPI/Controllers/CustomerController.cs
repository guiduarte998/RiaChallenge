using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using RiaWebAPI.Model;

namespace RiaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private static List<Customer> customers = new List<Customer>();

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return customers;
        }

        [HttpPost]
        public IActionResult Post([FromBody] List<Customer> newCustomers)
        {
            foreach (var customer in newCustomers)
            {
                // Validate customer fields
                if (string.IsNullOrEmpty(customer.FirstName) ||
                    string.IsNullOrEmpty(customer.LastName) ||
                    customer.Age <= 18 ||
                    customers.Any(c => c.Id == customer.Id))
                {
                    return BadRequest("Invalid customer data.");
                }

                // Find insert position
                int insertIndex = customers.FindIndex(c =>
                    string.Compare(c.LastName, customer.LastName) > 0 ||
                    (c.LastName == customer.LastName && string.Compare(c.FirstName, customer.FirstName) > 0));

                if (insertIndex < 0)
                {
                    customers.Add(customer);
                }
                else
                {
                    customers.Insert(insertIndex, customer);
                }
            }

            // Persist the customers list
            // This could be to a file or database. For simplicity, we'll just keep it in memory here.

            return Ok();
        }
    }


}





