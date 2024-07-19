using TranscriberApi.DataBased.Context;
using TranscriberApi.DataBased.Models;
using Dapper;

namespace TranscriberApi.DataBased.Services
{
    public class ClientRequestService
    {
        private readonly ClientsContext _context;

        public ClientRequestService(ClientsContext context)
        {
            _context = context;
        }

        public async Task<ClientRequestModel> GetClientByPhoneNumberAsync(string phoneNumber, DateTime start, DateTime end)
        {
            using var connection = _context.CreateConnection();
            var query = @"
                SELECT * FROM Cube_MoneyRent_Orchard_Cabinets_Collection_CollectionCalling cc
                INNER JOIN Cube_MoneyRent_Orchard_RequestForm_ContactDataRecord cdr ON cdr.Id = cc.RentRequestId
                WHERE cdr.MobilePhone = @phoneNumber AND 
	            cc.DateTime BETWEEN @startDate AND @endDate"; 

            var parameters = new { PhoneNumber = phoneNumber };
            var result = await connection.QuerySingleOrDefaultAsync<ClientRequestModel>(query, parameters);

            return result!;
        }
    }
}
