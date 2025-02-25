﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Universalis.Application.Views
{
    public class CheapestView
    {
        [JsonPropertyName("items")]
        public SortedDictionary<uint, ListingView> Items { get; init; }
    }
}