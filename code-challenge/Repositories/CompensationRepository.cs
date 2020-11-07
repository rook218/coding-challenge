using challenge.Data;
using challenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRepository(ILogger<ICompensationRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }
        ///<summary>
        /// Adds compensation to the DB context with the foreign key, but without the employee.
        ///</summary>
        /// <param name="compensation">The compensation object to add to the context.</param>
        ///<returns>
        /// The compensation object that was added, with its associated employee object nested.
        ///</returns>
        public Compensation Add(Compensation compensation)
        {
            // nullifies the employee to avoid adding a duplicate in the Employee context
            compensation.EmployeeId = compensation.Employee.EmployeeId;
            compensation.Employee = null;
            _employeeContext.Compensations.Add(compensation);

            // re-adds the employee to the compensation class by employee ID before returning
            compensation.Employee = _employeeContext.Employees.SingleOrDefault(e => e.EmployeeId == compensation.EmployeeId);
            return compensation;

        }

        ///<summary>
        /// Gets the collection of Compensation objects, retrieved by the Employee's ID number, 
        /// and ordered descending from most recent change to oldest change.
        ///</summary>
        /// <param name="id">The employee's ID number</param>
        ///<returns>
        /// <c>IEnumerable<Compensation></c> of all the employee's Compensation entries, ordered from newest to oldest.
        ///</returns>
        public IEnumerable<Compensation> GetById(string id)
        {
            return _employeeContext.Compensations
                .Include("Employee")
                .Where(c => c.EmployeeId == id)
                .OrderByDescending(c => c.EffectiveDate);
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }
    }
}
