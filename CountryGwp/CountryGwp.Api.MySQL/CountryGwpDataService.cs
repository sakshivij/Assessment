using CountryGwp.Api.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using SqlKata.Execution;
using SqlKata;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace CountryGwp.Api.MySQL
{
    public class CountryGwpDataService : ICountryGwpDataService
    {
        private readonly ILogger _logger;
        private readonly string _connectionString;
        private readonly MySqlCompiler _compiler = new MySqlCompiler();

        public CountryGwpDataService(ILogger<CountryGwpDataService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        
        public async Task<string> GetAverageGwp(CountryGwpIn inputData, CancellationToken cancellationToken = default)
        {
            IDictionary<string, double> averageGwp = new Dictionary<string, double>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                using (var db = new QueryFactory(connection, _compiler))
                {
                    var query = db.Query("COUNTRYGWP_Tbl").Where(nameof(CountryGwpDataModel.country), "=", inputData.country).WhereIn(nameof(CountryGwpDataModel.lineOfBusiness), inputData.lob);
                    var result = await query.GetAsync<CountryGwpDataModel>().ConfigureAwait(false);
                    foreach(var countryGwpDataObject in result)
                    {
                        double average = (countryGwpDataObject.Y2008 + countryGwpDataObject.Y2009 + countryGwpDataObject.Y2010 + countryGwpDataObject.Y2011 + countryGwpDataObject.Y2012 + countryGwpDataObject.Y2013 + countryGwpDataObject.Y2014 + countryGwpDataObject.Y2015) / 8;
                        averageGwp.Add(countryGwpDataObject.lineOfBusiness, average);
                    }

                    return JsonConvert.SerializeObject(averageGwp);
                }
            }
        }
    }
}
