# Integration Documentation

This document provides an overview and usage examples for integrating the **Uloq.SDK** into your application. The **Uloq.SDK** is a software development kit that enables seamless integration with the Uloq platform for generating QR codes and handling authorizations.

## Table of Contents
1. [Prerequisites](#prerequisites)
2. [Installation](#installation)
3. [Generating Signing QR Code](#generating-signing-qr-code)
4. [Generating Key Exchange QR Code](#generating-key-exchange-qr-code)
5. [Creating Authorization Request](#creating-authorization-request)
6. [Getting Authorization Response](#getting-authorization-response)
7. [Sample Code](#sample-code)
8. [Contributing](#contributing)
9. [License](#license)

## Prerequisites<a name="prerequisites"></a>
- .NET Core SDK (version X.X or higher)
- Uloq account with API access

## Installation<a name="installation"></a>
To use the **Uloq.SDK** in your project, follow these steps:

1. Install the **Uloq.SDK** NuGet package by running the following command in the NuGet Package Manager Console:

   ```bash
   Install-Package Uloq.SDK
   ```

2. Import the necessary namespaces in your code files:

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
    Category = "Test",
    ActionTitle = "Sign Test",
    ActionMessage = "Test Message",
    Metadata = "Test Metadata",
    PublicKey = "",
    RequestType = QRCodeRequest.RequestTypeEnum.Sign
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
    Category = "Test",
    ActionTitle = "Key Exchange Test",
    ActionMessage = "Test Message",
    Metadata = "Test Metadata",
    PublicKey = "",
    RequestType = QRCodeRequest.RequestTypeEnum.KeyExchange
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
    ActionMessage = "Test Message",
    ActionTitle = "Test Title",
    Category = "Test Category",
    ExpiryDateUTC = DateTime.UtcNow.ToString(),
    KeyIdentifier = keyIdentifier,
    Metadata = "Test Metadata",
    NotificationIdentifier = notificationIdentifier
};

AuthorizationRequestor authorizationRequestor = new

AuthorizationRequestor(Models.ConnectionModel.CreateConnection("test", "test", true));
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

## Sample Code<a name="sample-code"></a>
For more sample code and usage examples, please refer to the provided test classes.

## Contributing<a name="contributing"></a>
Contributions to the **Uloq.SDK** are welcome! If you find any issues or have suggestions for improvement, please submit an issue or pull request on the GitHub repository.

## License<a name="license"></a>
The **Uloq.SDK** is released under the [MIT License](https://opensource.org/licenses/MIT).