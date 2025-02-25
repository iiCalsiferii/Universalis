﻿using System;
using System.Linq;
using Universalis.GameData;

namespace Universalis.Application.Common
{
    public class WorldDc
    {
        public bool IsWorld { get; private init; }

        public uint WorldId { get; private init; }

        public string WorldName { get; private init; }

        public bool IsDc { get; private init; }

        public string DcName { get; private init; }

        /// <summary>
        /// Parses out a <see cref="WorldDc"/> from a string containing either a world name, a world ID, or a DC name.
        /// </summary>
        /// <param name="worldOrDc">The input string.</param>
        /// <param name="gameData">A game data provider.</param>
        /// <param name="worldDc">A <see cref="WorldDc"/> object with either the world or the DC populated.</param>
        /// <returns>Whether or not parsing succeeded.</returns>
        public static bool TryParse(string worldOrDc, IGameDataProvider gameData, out WorldDc worldDc)
        {
            worldDc = null;

            if (worldOrDc == null)
            {
                return false;
            }

            string worldName = null;
            string dcName = null;
            var worldIdParsed = uint.TryParse(worldOrDc, out var worldId);
            if (!worldIdParsed)
            {
                var cleanWorldOrDc = char.ToUpperInvariant(worldOrDc[0]) + worldOrDc[1..].ToLowerInvariant();

                // Effectively does nothing if the input doesn't refer to a Chinese world or DC
                cleanWorldOrDc = ChineseServers.RomanizedToHanzi(cleanWorldOrDc);

                worldIdParsed = gameData.AvailableWorldsReversed().TryGetValue(cleanWorldOrDc, out worldId);
                
                if (!worldIdParsed)
                {
                    if (!gameData.DataCenters().Select(dc => dc.Name).Contains(cleanWorldOrDc))
                    {
                        return false;
                    }

                    dcName = cleanWorldOrDc;
                }
                else
                {
                    worldName = cleanWorldOrDc;
                }
            }
            else if (!gameData.AvailableWorlds().TryGetValue(worldId, out worldName))
            {
                return false;
            }

            worldDc = new WorldDc
            {
                IsWorld = worldIdParsed,
                WorldId = worldId,
                WorldName = worldName,
                IsDc = dcName != null,
                DcName = dcName,
            };

            return true;
        }
    }
}