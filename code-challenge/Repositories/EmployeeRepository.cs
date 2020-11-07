using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using challenge.Data;

namespace challenge.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeRepository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _employeeContext.Employees.Add(employee);
            return employee;
        }

        public Employee GetById(string id)
        {
            return _employeeContext.Employees.SingleOrDefault(e => e.EmployeeId == id);
        }

        /// <summary>
        /// Gets all direct reports 
        /// </summary>
        /// <param name="id">The Employee's unique identifier</param>
        /// <returns>
        /// Returns ReportingStructure with nested direct reports
        /// </returns>
        public ReportingStructure GetEmployeeReportingStructure(string id)
        {
            ReportingStructure rs = new ReportingStructure();
            Employee rootEmployee = _employeeContext.Employees.Include(e => e.DirectReports).SingleOrDefault(e => e.EmployeeId == id);
            // Adds the direct reports recursively, and increments the counter
            AppendDirectReportsRecursively(rootEmployee, rs);
            rs.Employee = rootEmployee;
            rs.EmployeeId = rootEmployee.EmployeeId;

            // the recursive function will add one for all employees, this removes one for the root employee her/himself
            rs.NumberOfReports--;

            return rs;
        }
        /// <summary>
        /// Nests all the given <paramref name="rootEmployee"/>'s direct reports and continues recursively 
        /// until an employee in the tree has no direct reports. Also updates the Reporting Structure's NumberOfReports property as it goes.
        /// </summary>
        /// <param name="rootEmployee">The top-level employee that will get its direct reports nested in its DirectReports property</param>
        /// <param name="rs">The reporting structure object that will get its count updated</param>
        /// <returns>
        /// void, updates the references passed to it
        /// </returns>
        public void AppendDirectReportsRecursively(Employee rootEmployee, ReportingStructure rs)
        {
            rootEmployee.DirectReports = _employeeContext.Employees.Include(e => e.DirectReports).SingleOrDefault(e => e.EmployeeId == rootEmployee.EmployeeId).DirectReports;
            if (rootEmployee.DirectReports.Count > 0)
            {
                foreach (Employee directReport in rootEmployee.DirectReports)
                {
                    AppendDirectReportsRecursively(directReport, rs);
                }
            }
            rs.NumberOfReports++;
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }
    }
}
