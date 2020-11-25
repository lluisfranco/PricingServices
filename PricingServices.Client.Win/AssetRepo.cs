using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PricingServices.Client.Win
{

    public class AssetRepo : BaseRepository
    {
        public AssetRepo() : base(null, null)
        {

        }

        public AssetRepo(
           string connectionString, SynchronizationContext syncContext) :
            base(connectionString, syncContext)
        { }

        public async Task<List<SecurityDTO>> GetSecuritiesAsync()
        {
            using (var con = CreateConnection())
            {
                var sql = @"DECLARE @Valuedate DATE =  
                                (SELECT MAX(ValueDate) FROM assets.Photos)
                            SELECT 
                              Q.AssetId,
                              q.AssetCode,
                              Q.AssetName,
                              Q.Ticker,
                              Q.AssetTypeId,
                              Q.AssetTypeName,
                              Q.IsActive
                            FROM assets.GetQuotesList(@Valuedate) Q
                            WHERE Q.QuoteIntroducedAt = 1 ";
                var commandDefinition = CreateCommand(sql);
                return await con.QueryCmdAsync<SecurityDTO>(commandDefinition);
            }
        }

    }

}
