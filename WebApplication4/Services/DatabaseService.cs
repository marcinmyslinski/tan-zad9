using System.Collections.Generic;
using System.Linq;
using WebApplication4.Models;
using WebApplication4.Models.DTOs.Responses;

namespace WebApplication4.Services
{
    public interface IDbService
    {
        public IEnumerable<GetClientStatistiscsResponseDto> GetReport();
    }

    public class DatabaseService : IDbService
    {
        private readonly pd3809Context _context;

        public DatabaseService(pd3809Context context)
        {
            _context = context;
        }

        public IEnumerable<dynamic> GetReport()
        {
            return _context.Clients
                           .Select(c => new
                           {

                           });
        }

        IEnumerable<GetClientStatistiscsResponseDto> IDbService.GetReport()
        {
            throw new System.NotImplementedException();
        }
    }
}
