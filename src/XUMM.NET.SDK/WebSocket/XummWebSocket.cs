﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace XUMM.NET.SDK.WebSocket;

public class XummWebSocket : IXummWebSocket
{
    private readonly ILogger<XummWebSocket> _logger;
    private string _payloadUuid = default!;

    public XummWebSocket(ILogger<XummWebSocket> logger)
    {
        _logger = logger;
    }

    public async IAsyncEnumerable<string> SubscribeAsync(string payloadUuid,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _payloadUuid = payloadUuid;

        using var webSocket = new ClientWebSocket();
        await webSocket.ConnectAsync(new Uri($"wss://xumm.app/sign/{_payloadUuid}"), CancellationToken.None);

        if (webSocket.State == WebSocketState.Open)
        {
            _logger.LogInformation("Payload {0}: Subscription active (WebSocket opened).", _payloadUuid);

            var buffer = new ArraySegment<byte>(new byte[1024]);

            while (webSocket.State == WebSocketState.Open)
            {
                await using var ms = new MyMemoryStreamWrapper();
                WebSocketReceiveResult? result;

                try
                {
                    do
                    {
                        result = await webSocket.ReceiveAsync(buffer, cancellationToken);
                        ms.Write(buffer.Array!, buffer.Offset, result.Count);
                    } while (!result.EndOfMessage && !cancellationToken.IsCancellationRequested);
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("Payload {0}: Subscription ended (WebSocket closed).", _payloadUuid);
                    yield break;
                }

                ms.Seek(0, SeekOrigin.Begin);
                yield return Encoding.UTF8.GetString(buffer.Array!, 0, result.Count);
            }
        }
    }
}
public class MyMemoryStreamWrapper : IAsyncDisposable
{
    private readonly MemoryStream _memoryStream;

    public MyMemoryStreamWrapper()
    {
        _memoryStream = new MemoryStream();
    }

    // Implement IAsyncDisposable
    public async ValueTask DisposeAsync()
    {
        _memoryStream.Dispose();
    }

    public void Write(byte[] buffer, int offset, int count)
    {
        _memoryStream.Write(buffer, offset, count);
    }

    public long Seek(long offset, SeekOrigin origin)
    {
        return _memoryStream.Seek(offset, origin);
    }

    // Other methods and properties as needed
}
