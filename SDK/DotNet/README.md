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
using Uloq.SDK.Eccenscia.Services.Models.UloqRequestor;
using Uloq.SDK.QR;

public class QR
{
    [Fact(DisplayName = "Generate Signing QR Code")]
    public async void GenerateSigningQR()
    {
        // Create a connection model
        var connection = Models.ConnectionModel.CreateConnection("test", "test", true);

        // Create an instance of QRGenerator
        QRGenerator qrGenerator = new QRGenerator(connection);

        // Create a QRCodeRequest object
        var qrCodeRequest = new QRCodeRequest()
        {
            Category = "Test",
            ActionTitle = "Sign Test",
            ActionMessage = "Test Message",
            Metadata = "Test Metadata",
            PublicKey = "",
            RequestType = QRCodeRequest.RequestTypeEnum.Sign
        };

        // Generate the QR code
        QRCodeResponse? output = await qrGenerator.GenerateQRCode(qrCodeRequest);

        // Assert the output
        Assert.True(output != null, "No output received");
        if (output != null)
        {
            Assert.True(!String.IsNullOrEmpty(output.Image), "Image is not empty");
            Assert.True(!String.IsNullOrEmpty(output.Url), "URL is not empty");
            Assert.True(!String.IsNullOrEmpty(output.NotificationIdentifier), "Notification Identifier is not empty");
        }
    }
}
```

## Generating Key Exchange QR Code<a name="generating-key-exchange-qr-code"></a>
The following example demonstrates how to generate a key exchange QR code using the **Uloq.SDK**:

```csharp
using Uloq.SDK.Eccenscia.Services.Models.UloqRequestor;
using Uloq.SDK.QR;

public class QR
{
    [Fact(DisplayName = "Generate Key Exchange QR Code")]
    public async void GenerateKeyExchangeQR()
    {
        // Create a connection model
        var connection = Models.ConnectionModel.CreateConnection("test", "test", true);

        // Create an instance of QRGenerator


QRGenerator qrGenerator = new QRGenerator(connection);

        // Create a QRCodeRequest object
        var qrCodeRequest = new QRCodeRequest()
        {
            Category = "Test",
            ActionTitle = "Key Exchange Test",
            ActionMessage = "Test Message",
            Metadata = "Test Metadata",
            PublicKey = "",
            RequestType = QRCodeRequest.RequestTypeEnum.KeyExchange
        };

        // Generate the QR code
        QRCodeResponse? output = await qrGenerator.GenerateQRCode(qrCodeRequest);

        // Assert the output
        Assert.True(output != null, "No output received");
        if (output != null)
        {
            Assert.True(!String.IsNullOrEmpty(output.Image), "Image is not empty");
            Assert.True(!String.IsNullOrEmpty(output.Url), "URL is not empty");
            Assert.True(!String.IsNullOrEmpty(output.NotificationIdentifier), "Notification Identifier is not empty");
        }
    }
}
```

## Creating Authorization Request<a name="creating-authorization-request"></a>
The following example demonstrates how to create an authorization request using the **Uloq.SDK**:

```csharp
using Uloq.SDK.Authorizations;
using Uloq.SDK.Eccenscia.Services.Models.UloqRequestor;

public class Authorization
{
    private string _keyIdentifier = "<insert your uloq key identifier>";
    private string _notificationIdentifier = Guid.NewGuid().ToString();

    [Fact(DisplayName = "Create Authorization with Model")]
    public async void CreateAuthorizationRequest()
    {
        // Create an AuthorizationRequest object
        var request = new AuthorizationRequest
        {
            ActionMessage = "Test Message",
            ActionTitle = "Test Title",
            Category = "Test Category",
            ExpiryDateUTC = DateTime.UtcNow.ToString(),
            KeyIdentifier = _keyIdentifier,
            Metadata = "Test Metadata",
            NotificationIdentifier = _notificationIdentifier
        };

        // Create an instance of AuthorizationRequestor
        AuthorizationRequestor authorizationRequestor = new AuthorizationRequestor(Models.ConnectionModel.CreateConnection("test", "test", true));

        // Create the authorization request
        Assert.True(await authorizationRequestor.CreateAuthorization(request), "Authorization requested");
    }

    [Fact(DisplayName = "Create Authorization with fields")]
    public async void CreateAuthorizationRequestWithFields()
    {
        // Create an AuthorizationRequest object
        var request = new AuthorizationRequest
        {
            ActionMessage = "Test Message",
            ActionTitle = "Test Title",
            Category = "Test Category",
            ExpiryDateUTC = DateTime.UtcNow.ToString(),
            KeyIdentifier = _keyIdentifier,
            Metadata = "Test Metadata",
            NotificationIdentifier = _notificationIdentifier
        };

        // Create an instance of AuthorizationRequestor
        AuthorizationRequestor authorizationRequestor = new AuthorizationRequestor(Models.ConnectionModel.CreateConnection("test", "test", true));

        // Create the authorization request
        Assert.True(
            await authorizationRequestor.CreateAuthorization(
                request.KeyIdentifier,
                request.NotificationIdentifier,
                DateTime.UtcNow.AddMinutes(1),
                request.Category,
                request.ActionTitle,
                request.ActionMessage,
                request.Metadata),
            "Authorization requested");
    }
}
```

## Getting Authorization Response<a name="getting-authorization-response"></a>
The following example demonstrates how to get an authorization response using the **Uloq.SDK**:

```csharp
using Uloq.SDK.Authorizations;
using Uloq.SDK.Eccenscia.Services.Models.UloqRequestor;

public class Authorization
{
    private string _keyIdentifier = "<insert your uloq key identifier>";
    private string _notificationIdentifier = Guid.NewGuid().ToString();

    [Fact(DisplayName = "Get an authorization response")]
    public async void GetRequestResponse()
    {
        // Create an instance of AuthorizationRequestor
       

AuthorizationRequestor authorizationRequestor = new AuthorizationRequestor(Models.ConnectionModel.CreateConnection("test", "test", true));

        // Get the authorization response
        var response = await authorizationRequestor.GetAuthorizationResponse(new NotificationDetailsRequest(_notificationIdentifier));

        // Retry getting the response for up to 30 seconds
        int counter = 0;
        while (response == null && counter < 30)
        {
            Thread.Sleep(1000);
            counter++;
            response = await authorizationRequestor.GetAuthorizationResponse(new NotificationDetailsRequest(_notificationIdentifier));
        }

        // Assert the response
        if (response != null)
        {
            Assert.True(response.Status == NotificationDetailsResponse.StatusEnum.Approved || response.Status == NotificationDetailsResponse.StatusEnum.Declined, "Status is pending");
            Assert.True(response.Signature != null, "Signature is not null");
            Assert.True(response.KeyIdentifier == _keyIdentifier, "Key identifier is correct");
        }
    }
}
```

## Sample Code<a name="sample-code"></a>
For more sample code and usage examples, please refer to the following test classes in the **Uloq.SDK.Test** namespace:
- `QR` class for generating QR codes
- `Authorization` class for handling authorizations

## Contributing<a name="contributing"></a>
Contributions to the **Uloq.SDK** are welcome! If you find any issues or have suggestions for improvement, please submit an issue or pull request on the GitHub repository.

## License<a name="license"></a>
The **Uloq.SDK** is released under the [MIT License](https://opensource.org/licenses/MIT).