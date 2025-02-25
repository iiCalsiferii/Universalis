﻿using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Universalis.DbAccess.Queries.MarketBoard;
using Universalis.Entities.Uploads;

namespace Universalis.DbAccess.Uploads
{
    public class RecentlyUpdatedItemsDbAccess : DbAccessService<RecentlyUpdatedItems, RecentlyUpdatedItemsQuery>, IRecentlyUpdatedItemsDbAccess
    {
        public static readonly int MaxItems = 200;

        public RecentlyUpdatedItemsDbAccess(IMongoClient client) : base(client, Constants.DatabaseName, "extraData") { }

        public RecentlyUpdatedItemsDbAccess(IMongoClient client, string databaseName) : base(client, databaseName, "content") { }

        public async Task Push(uint itemId, CancellationToken cancellationToken = default)
        {
            var query = new RecentlyUpdatedItemsQuery();
            var existing = await Retrieve(query, cancellationToken);

            if (existing == null)
            {
                await Create(new RecentlyUpdatedItems
                {
                    Items = new List<uint> { itemId },
                }, cancellationToken);
                return;
            }

            var newItems = existing.Items;
            var existingIndex = newItems.IndexOf(itemId);
            if (existingIndex != -1)
            {
                newItems.RemoveAt(existingIndex);
                newItems.Insert(0, itemId);
            }
            else
            {
                newItems.Insert(0, itemId);
                newItems = newItems.Take(MaxItems).ToList();
            }

            var updateBuilder = Builders<RecentlyUpdatedItems>.Update;
            var update = updateBuilder.Set(o => o.Items, newItems);
            await Collection.UpdateOneAsync(query.ToFilterDefinition(), update, cancellationToken: cancellationToken);
        }
    }
}