# Integration Documentation

This document provides an overview and usage examples for integrating the **Uloq.SDK** into your application. The **Uloq.SDK** is a software development kit that enables seamless integration with the Uloq platform for generating QR codes and handling authorizations.

## Table of Contents
1. [Prerequisites](#prerequisites)
2. [Installation](#installation)
3. [Generating Signing QR Code](#generating-signing-qr-code)
4. [Generating Key Exchange QR Code](#generating-key-exchange-qr-code)
5. [Creating Authorization Request](#creating-authorization-request)
6. [Getting Authorization Response](#getting-authorization-response)
7. [Verifying Signature](#verifying-signature)
8. [Sample Code](#sample-code)
9. [Contributing](#contributing)
10. [License](#license)

## Prerequisites<a name="prerequisites"></a>
- .NET Core SDK (version X.X or higher)
- Uloq account with API access

## Installation<a name="installation"></a>
To use the **Uloq.SDK** in your project, follow these steps:

1. Open your project in Visual Studio.

2. Right-click on your project in the Solution Explorer and select "Manage NuGet Packages".

3. In the NuGet Package Manager, search for "Uloq.SDK".

4. Select the **Uloq.SDK** package from the search results.

5. Click on the "Install" button to install the package into your project.

6. Visual Studio will download and install the **Uloq.SDK** package and its dependencies.

7. Once the installation is complete, you can start using the **Uloq.SDK** in your code.

   ```csharp
   using Uloq.SDK.Eccenscia.Services.Models.UloqRequestor;
   using Uloq.SDK.QR;
   using Uloq.SDK.Authorizations;
   ```

## Generating Signing QR Code<a name="generating-signing-qr-code"></a>
The following example demonstrates how to generate a signing QR code using the **Uloq.SDK**:

```csharp
var connection = Models.ConnectionModel.CreateConnection("test", "test", true);
QRGenerator qrGenerator = new QRGenerator(connection);
QRCodeRequest qrCodeRequest = new QRCodeRequest()
{
    RequestType = QRCodeRequest.RequestTypeEnum.Sign,
    Category = "Test",
    ActionTitle = "Sign Test",
    ActionMessage = "Test Message",
    Metadata = "Test Metadata",
    PublicKey = ""
};
QRCodeResponse? output = await qrGenerator.GenerateQRCode(qrCodeRequest);

// Assert the output and perform necessary actions
```

## Generating Key Exchange QR Code<a name="generating-key-exchange-qr-code"></a>
The following example demonstrates how to generate a key exchange QR code using the **Uloq.SDK**:

```csharp
var connection = Models.ConnectionModel.CreateConnection("test", "test", true);
QRGenerator qrGenerator = new QRGenerator(connection);
QRCodeRequest qrCodeRequest = new QRCodeRequest()
{
    RequestType = QRCodeRequest.RequestTypeEnum.KeyExchange,
    Category = "Test",
    ActionTitle = "Key Exchange Test",
    ActionMessage = "Test Message",
    Metadata = "Test Metadata",
    PublicKey = ""
};
QRCodeResponse? output = await qrGenerator.GenerateQRCode(qrCodeRequest);

// Assert the output and perform necessary actions
```

## Creating Authorization Request<a name="creating-authorization-request"></a>
The following example demonstrates how to create an authorization request using the **Uloq.SDK**:

```csharp
string keyIdentifier = "<insert your uloq key identifier>";
string notificationIdentifier = Guid.NewGuid().ToString();

AuthorizationRequest request = new AuthorizationRequest
{
    KeyIdentifier = keyIdentifier,
    NotificationIdentifier = notificationIdentifier,
    ExpiryDateUTC = DateTime.UtcNow.ToString(),
    Category = "Test Category",
    ActionTitle = "Test Title",
    ActionMessage = "Test Message",
    Metadata = "Test Metadata"
};

AuthorizationRequestor authorizationRequestor = new AuthorizationRequestor(Models.ConnectionModel.CreateConnection("test", "test", true));
bool authorizationCreated = await authorizationRequestor.CreateAuthorization(request);

// Assert the authorization creation status and perform necessary actions
```

## Getting Authorization Response<a name="getting-authorization-response"></a>
The following example demonstrates how to get an authorization response using the **Uloq.SDK**:

```csharp
string keyIdentifier = "<insert your uloq key identifier>";
string notificationIdentifier = "<insert notification identifier>";

AuthorizationRequestor authorizationRequestor = new AuthorizationRequestor(Models.ConnectionModel.CreateConnection("test", "test", true));
NotificationDetailsRequest detailsRequest = new NotificationDetailsRequest(notificationIdentifier);
NotificationDetailsResponse response = await authorizationRequestor.GetAuthorizationResponse(detailsRequest);

// Retry getting the response if needed and perform necessary actions
```

## Verifying Signature<a name="verifying-signature"></a>
To verify the signature using Bouncy Castle, you can use the following code:

```csharp
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;

public async Task<bool> VerifySignature(byte[] signatureData, byte[] publicKey, byte[] signature)
{
    ISigner signer = SignerUtilities.GetSigner("SHA256withECDSA");

    ECPublicKeyParameters pubKey = (ECPublicKeyParameters)PublicKeyFactory.CreateKey(publicKey);
    signer.Init(false, pubKey);
    signer.BlockUpdate(signatureData, 0, signatureData

.Length);

    return await Task.FromResult(signer.VerifySignature(signature));
}

```

To verify the signature using the standard .NET libraries, you can use the following code:

```csharp
using System.Security.Cryptography;

public async Task<bool> VerifySignature(byte[] signatureData, byte[] publicKey, byte[] signature)
{
    using (ECDsa ecdsa = ECDsa.Create())
    {
        ECParameters ecParams = new ECParameters
        {
            Curve = ECCurve.NamedCurves.nistP256,
            Q = new ECPoint
            {
                X = publicKey.Take(publicKey.Length / 2).ToArray(),
                Y = publicKey.Skip(publicKey.Length / 2).ToArray()
            }
        };

        ecdsa.ImportParameters(ecParams);

        return await Task.FromResult(ecdsa.VerifyData(signatureData, signature, HashAlgorithmName.SHA256));
    }
}
```

## Sample Code<a name="sample-code"></a>
For more sample code and usage examples, please refer to the provided test classes.

## Contributing<a name="contributing"></a>
Contributions to the **Uloq.SDK** are welcome! If you find any issues or have suggestions for improvement, please submit an issue or pull request on the GitHub repository.

## License<a name="license"></a>
The **Uloq.SDK** is released under the [MIT License](https://opensource.org/licenses/MIT).