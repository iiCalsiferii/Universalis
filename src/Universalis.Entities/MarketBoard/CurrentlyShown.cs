﻿using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Universalis.Entities.MarketBoard
{
    public class CurrentlyShown
    {
        [BsonElement("itemID")]
        public uint ItemId { get; init; }

        [BsonElement("worldID")]
        public uint WorldId { get; init; }

        [BsonElement("lastUploadTime")]
        public uint LastUploadTimeUnixMilliseconds { get; init; }

        [BsonElement("uploaderID")]
        public string UploaderIdHash { get; init; }

        [BsonElement("listings")]
        public List<Listing> Listings { get; set; }

        [BsonElement("recentHistory")]
        public List<Sale> RecentHistory { get; set; }
    }
}