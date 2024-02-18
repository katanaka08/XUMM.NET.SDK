# XUMM.NET.SDK [![XUMM.NET.SDK](https://github.com/XRPL-Labs/XUMM.NET.SDK/actions/workflows/dotnet.yml/badge.svg)](https://github.com/XRPL-Labs/XUMM.NET.SDK/actions/workflows/dotnet.yml)
Interact with the XUMM SDK from .NET / C# environments.

The original repository is https://github.com/XRPL-Labs/XUMM.NET.SDK.

## Install XUMM.NET.SDK in Unity

1. Build XUMM.NET.SDK. (such as Build Selected projects)
2. Copy the built net48 folder to the Plugins folder of your Unity project.
3. In Unity, you can request a payload using the following sample code.
```csharp
xummSdk = new XummSdk(your.apiKey,your.apiSeclet);
var payload = new XummPayloadTransaction(XummTransactionType.SignIn).ToXummPostJsonPayload();
payload.CustomMeta = new XummPayloadCustomMeta { Instruction = "Test payload created with the XUMM.NET SDK." };
var payloaded = await xummSdk.Payload.CreateAsync(payload);
if (payloaded != null)
{
    // At this point you can get the QR code image URL for payload from payloaded.Refs.QrPng
    // UnityWebRequestTexture.GetTexture(payloaded.Refs.QrPng)

    // Wait for signature by Xumm app
    await xummSdk.Payload.SubscribeAsync(payloaded, Subscription_EventArgs, cancellationToken);
}
```



## caution
This modification project is incomplete.

The build for platforms such as Android passes, but a problem occurs in the DI constructor and it does not work.

Currently, it has only been confirmed to work with Unity Editor or Standalone builds.

If you can fix the constructor error in the dependency injection class in AddXummNetClients() in XummNetStartup.cs, you might be able to run it on Android and iOS platforms.