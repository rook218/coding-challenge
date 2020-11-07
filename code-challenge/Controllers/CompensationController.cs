using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;

namespace challenge.Controllers
{
    [Route("api/compensation")]
    public class CompensationController : Controller
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }
        /// <summary>
        /// Expects JSON in the following format:
        /// <code>
        /// {
        ///     "employee": {
        ///         "employeeId": "16a596ae-edd3-4847-99fe-c4518e82c86f"
        ///     },
        ///     "salary": 90000,
        ///     "effectiveDate": "2020-03-25T14:56"
        /// }
        ///</code>
        /// </summary>
        /// <returns>
        /// Returns the compensation object (without its unique ID), with the related Employee
        /// </returns>
        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {

            _logger.LogDebug($"Received compensation create request for '{compensation.Employee.FirstName} {compensation.Employee.LastName}' with salary of ${compensation.Salary}");

            _compensationService.Create(compensation);

            return CreatedAtRoute("getCompensationByEmployee", new { id = compensation.CompensationId }, compensation);
        }

        /// <summary>
        /// Returns all Compensations by <paramref name="id"/> (Employee's Id)
        /// </summary>
        /// <param name="id">The Employee's unique identifier</param>
        /// <returns>
        /// Returns the compensation object (without its unique ID), with the related Employee nested
        /// </returns>
        [HttpGet("{id}", Name = "getCompensationByEmployee")]
        public IActionResult GetCompensationByEmployee(String id)
        {
            _logger.LogDebug($"Received compensation get request for employee ID '{id}'");

            var compensation = _compensationService.GetById(id);

            if (compensation == null)
                return NotFound();

            return Ok(compensation);
        }
    }
}
