﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Universalis.GameData;

namespace Universalis.Application.Controllers.V1
{
    [ApiController]
    [Route("api/marketable")]
    public class MarketableController : ControllerBase
    {
        private readonly IGameDataProvider _gameData;

        public MarketableController(IGameDataProvider gameData)
        {
            _gameData = gameData;
        }

        /// <summary>
        /// Returns the set of marketable item IDs.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<uint>), 200)]
        public IEnumerable<uint> Get()
        {
            return _gameData.MarketableItemIds();
        }
    }
}
