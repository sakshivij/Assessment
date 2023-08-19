using CountryGwp.Api.Domain;
using CountryGwp.Api.MySQL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace CountryGwp.Api.Web.Controllers
{
    /// <summary>
    /// controller.
    /// </summary>
    [ApiController]
    [Route("server/api/gwp/avg")]
    public class CountryGwpController : ControllerBase
    {
        private readonly ILogger<CountryGwpController> _logger;
        private readonly ICountryGwpDataService _countryGwpDataService;

        /// <summary>
        /// constructors.
        /// </summary>
        public CountryGwpController(ILogger<CountryGwpController> logger, ICountryGwpDataService countryGwpDataService)
        {
            _logger = logger;
            _countryGwpDataService = countryGwpDataService;
        }

        /// <summary>
        /// Retrieve average gross written premium for a country and specific line of businesses.
        /// </summary>
        /// <param name="inputRequest">Input Request</param>
        /// <returns>Average amounts for LOBs</returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PostAsync([FromBody] CountryGwpIn inputRequest)
        {
            if (string.IsNullOrEmpty(inputRequest.country))
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Country name can't be empty in the input");
            }

            try
            {
                var result = await _countryGwpDataService.GetAverageGwp(inputRequest).ConfigureAwait(false);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
