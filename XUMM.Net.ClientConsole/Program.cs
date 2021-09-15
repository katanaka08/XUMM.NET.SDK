﻿using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace XUMM.Net.ClientConsole
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            //The XUMM API can be called using an API Key and API Secret, which can be obtained from the xumm Developer Dashboard.
            var credentials = new XummApiCredentials(Settings.ApiKey, Settings.ApiSecret);

            // Xumm client options with default endpoint is used here.
            var options = new XummClientOptions(credentials);

            using var client = new XummClient(options);

            await CallAndWriteResponseAsync(client.Misc.PingAsync);
            await CallAndWriteResponseAsync(client.Misc.CuratedAssetsAsync);
            await CallAndWriteResponseAsync(() => client.Misc.GetTransactionAsync(Settings.TxHash));
            Console.ReadKey();
        }

        private static async Task CallAndWriteResponseAsync<T>(Func<Task<T>> task)
        {
            var start = DateTime.UtcNow;
            var result = await task();

            Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true }));
            Console.WriteLine($"Response time: {Math.Round((DateTime.UtcNow - start).TotalMilliseconds)}ms.");
        }
    }
}
