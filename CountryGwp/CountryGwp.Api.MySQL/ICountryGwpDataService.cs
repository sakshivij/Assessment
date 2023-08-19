using CountryGwp.Api.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CountryGwp.Api.MySQL
{
    public interface ICountryGwpDataService
    {
        Task<string> GetAverageGwp(CountryGwpIn inputData, CancellationToken cancellationToken = default);
    }
}
