﻿using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using Universalis.Application.Views;
using Universalis.DbAccess.MarketBoard;
using Universalis.DbAccess.Queries.MarketBoard;
using Universalis.GameData;

namespace Universalis.Application.Controllers.V1
{
    [ApiController]
    [Route("api/tax-rates")]
    public class TaxRatesController : WorldDcControllerBase
    {
        private readonly ITaxRatesDbAccess _taxRatesDb;

        public TaxRatesController(IGameDataProvider gameData, ITaxRatesDbAccess taxRatesDb) : base(gameData)
        {
            _taxRatesDb = taxRatesDb;
        }

        /// <summary>
        /// Retrieves the current tax rate data for the specified world. This data is provided by the Retainer Vocate in each major city.
        /// </summary>
        /// <param name="world">The world or to retrieve data for. This may be an ID or a name.</param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">Data retrieved successfully.</response>
        /// <response code="404">The world requested is invalid.</response>
        [HttpGet]
        [ProducesResponseType(typeof(TaxRatesView), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get([FromQuery, BindRequired] string world, CancellationToken cancellationToken = default)
        {
            if (!TryGetWorldDc(world, out var worldDc))
            {
                return NotFound();
            }

            if (!worldDc.IsWorld)
            {
                return NotFound();
            }

            var taxRates = await _taxRatesDb.Retrieve(new TaxRatesQuery { WorldId = worldDc.WorldId }, cancellationToken);
            if (taxRates == null)
            {
                return Ok(new TaxRatesView());
            }

            return Ok(new TaxRatesView
            {
                LimsaLominsa = taxRates.LimsaLominsa,
                Gridania = taxRates.Gridania,
                Uldah = taxRates.Uldah,
                Ishgard = taxRates.Ishgard,
                Kugane = taxRates.Kugane,
                Crystarium = taxRates.Crystarium,
                OldSharlayan = taxRates.OldSharlayan ?? 0,
            });
        }
    }
}
