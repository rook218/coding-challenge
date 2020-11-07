using challenge.Models;
using challenge.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger<CompensationService> _logger;

        public CompensationService(ILogger<CompensationService> logger, ICompensationRepository compensationRepository)
        {
            _compensationRepository = compensationRepository;
            _logger = logger;
        }

        /// <summary>
        /// Passes the Compensation object to the repository and saves the repository changes to the database.
        /// </summary>
        /// <returns>
        /// Returns the passed Compensation object
        /// </returns>
        public Compensation Create(Compensation compensation)
        {

            if (compensation != null)
            {
                _compensationRepository.Add(compensation);
                _compensationRepository.SaveAsync().Wait();
            }

            return compensation;
        }

        /// <summary>
        /// Gets all Compensations in the data layer for the specified employee ID
        /// </summary>
        /// <param name="id">The Employee's unique identifier</param>
        /// <returns>
        /// Returns <c>IEnumerable<Compensation></c> containing all compensation objects for the given employee ID.
        /// </returns>
        public IEnumerable<Compensation> GetById(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                return _compensationRepository.GetById(id);
            }

            return null;
        }
    }
}
