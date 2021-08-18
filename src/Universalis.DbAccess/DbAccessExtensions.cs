﻿using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Universalis.DbAccess.MarketBoard;
using Universalis.DbAccess.Uploads;

namespace Universalis.DbAccess
{
    public static class DbAccessExtensions
    {
        public static void AddDbAccessServices(this IServiceCollection sc)
        {
            ThreadPool.GetMaxThreads(out var workerThreads, out var completionPortThreads);
            sc.AddSingleton<IMongoClient>(new MongoClient(new MongoClientSettings
            {
                MaxConnectionPoolSize = workerThreads + completionPortThreads,
            }));

            sc.AddSingleton<IConnectionThrottlingPipeline, ConnectionThrottlingPipeline>();

            sc.AddTransient<ICurrentlyShownDbAccess, CurrentlyShownDbAccess>();
            sc.AddTransient<IHistoryDbAccess, HistoryDbAccess>();
            sc.AddTransient<IContentDbAccess, ContentDbAccess>();
            sc.AddTransient<ITaxRatesDbAccess, TaxRatesDbAccess>();
            sc.AddTransient<ITrustedSourceDbAccess, TrustedSourceDbAccess>();
            sc.AddTransient<IFlaggedUploaderDbAccess, FlaggedUploaderDbAccess>();
            sc.AddTransient<IWorldUploadCountDbAccess, WorldUploadCountDbAccess>();
            sc.AddTransient<IRecentlyUpdatedItemsDbAccess, RecentlyUpdatedItemsDbAccess>();
            sc.AddTransient<IUploadCountHistoryDbAccess, UploadCountHistoryDbAccess>();
        }
    }
}