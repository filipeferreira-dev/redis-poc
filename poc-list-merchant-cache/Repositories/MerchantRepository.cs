using Dapper;
using Dapper.Contrib.Extensions;
using pocListMerchantCache.Model.Entities;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace pocListMerchantCache.Repositories
{
    public class MerchantRepository
    {
        private DbConnection CreateConnection()
        {
            return new SqlConnection("Data Source=.;Initial Catalog=POC;Persist Security Info=True;User ID=sa;password=<YourStrong@Passw0rd>");
        }

        public async Task AddAsync(Merchant merchant)
        {
            using (var connection = CreateConnection())
            {
                await connection.OpenAsync();
                var identity = await connection.InsertAsync(merchant);
            }
        }

        public async Task<IEnumerable<int>> GetAllKeysAsync()
        {
            const string query = "select id from merchant";

            using (var connection = CreateConnection())
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<int>(query);
            }
        }

        public async Task<Merchant> GetAsync(int id)
        {
            const string query = "select id, [Name], Document, CreatedAt from merchant where id = @id";

            using (var connection = CreateConnection())
            {
                await connection.OpenAsync();
                return await connection.QuerySingleOrDefaultAsync<Merchant>(query, new { id });
            }
        }

        public async Task<bool> UpdateAsync(Merchant merchant)
        {
            using (var connection = CreateConnection())
            {
                await connection.OpenAsync();
                return await connection.UpdateAsync(merchant);
            }
        }
    }
}
