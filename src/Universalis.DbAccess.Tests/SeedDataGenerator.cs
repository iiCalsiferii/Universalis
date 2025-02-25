﻿using System;
using System.Collections.Generic;
using System.Linq;
using Universalis.Entities;
using Universalis.Entities.MarketBoard;
using Universalis.Entities.Uploads;

namespace Universalis.DbAccess.Tests
{
    public static class SeedDataGenerator
    {
        public static CurrentlyShown MakeCurrentlyShown(uint worldId, uint itemId, long? lastUploadTime = null)
        {
            var rand = new Random();
            return new CurrentlyShown
            {
                WorldId = worldId,
                ItemId = itemId,
                LastUploadTimeUnixMilliseconds = lastUploadTime ?? (uint)DateTimeOffset.Now.ToUnixTimeMilliseconds(),
                Listings = Enumerable.Range(0, 100)
                    .Select(i => new Listing
                    {
                        ListingIdInternal = "FB",
                        Hq = rand.NextDouble() > 0.5,
                        OnMannequin = rand.NextDouble() > 0.5,
                        Materia = new List<Materia>(),
                        PricePerUnit = (uint)rand.Next(100, 60000),
                        Quantity = (uint)rand.Next(1, 999),
                        DyeId = (byte)rand.Next(0, 255),
                        CreatorIdInternal = "54565458626446136552",
                        CreatorName = "Bingus Bongus",
                        LastReviewTimeUnixSeconds = (uint)DateTimeOffset.Now.ToUnixTimeSeconds() - (uint)rand.Next(0, 360000),
                        RetainerIdInternal = "54565458626446136554",
                        RetainerName = "xpotato",
                        RetainerCityIdInternal = 0xA,
                        SellerIdInternal = "54565458626446136553",
                        UploadApplicationName = "test runner",
                    })
                    .ToList(),
                RecentHistory = Enumerable.Range(0, 100)
                    .Select(i => new Sale
                    {
                        Hq = rand.NextDouble() > 0.5,
                        PricePerUnit = (uint)rand.Next(100, 60000),
                        Quantity = (uint)rand.Next(1, 999),
                        BuyerName = "Someone Someone",
                        TimestampUnixSeconds = (uint)DateTimeOffset.Now.ToUnixTimeSeconds() - (uint)rand.Next(0, 80000),
                        UploadApplicationName = "test runner",
                    })
                    .ToList(),
                UploaderIdHash = "2A",
            };
        }

        public static History MakeHistory(uint worldId, uint itemId, long? lastUploadTime = null)
        {
            var rand = new Random();
            return new History
            {
                WorldId = worldId,
                ItemId = itemId,
                LastUploadTimeUnixMilliseconds = lastUploadTime ?? (uint)DateTimeOffset.Now.ToUnixTimeMilliseconds(),
                Sales = Enumerable.Range(0, 100)
                    .Select(i => new MinimizedSale
                    {
                        Hq = rand.NextDouble() > 0.5,
                        PricePerUnit = (uint)rand.Next(100, 60000),
                        Quantity = (uint)rand.Next(1, 999),
                        SaleTimeUnixSeconds = (uint)DateTimeOffset.Now.ToUnixTimeSeconds() - (uint)rand.Next(0, 80000),
                        UploaderIdHash = "2A",
                    })
                    .ToList(),
            };
        }

        public static TaxRates MakeTaxRates(uint worldId)
        {
            return new()
            {
                WorldId = worldId,
                UploaderIdHash = "",
                UploadApplicationName = "test runner",
                LimsaLominsa = 3,
                Gridania = 3,
                Uldah = 3,
                Ishgard = 0,
                Kugane = 0,
                Crystarium = 5,
            };
        }

        public static FlaggedUploader MakeFlaggedUploader()
        {
            return new() { UploaderIdHash = "afffff" };
        }

        public static TrustedSource MakeTrustedSource()
        {
            return new()
            {
                ApiKeySha512 = "aefe32ee",
                Name = "test runner",
                UploadCount = 0,
            };
        }
    }
}