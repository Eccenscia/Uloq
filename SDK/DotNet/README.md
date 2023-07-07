# Uloq

Welcome to the Uloq repository!

- [Integration Documentation](#integration-documentation) - Learn how to integrate Uloq into your application using the .NET SDK.

## Table of Contents

- [Integration Documentation](#integration-documentation)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Usage](#usage)
  - [Creating Authorization Request](#creating-authorization-request)
  - [Getting Authorization Response](#getting-authorization-response)
- [Contributing](#contributing)
- [License](#license)

## Integration Documentation<a name="integration-documentation"></a>

This documentation provides guidance on how to integrate Uloq into your application using the Uloq .NET SDK.

### Prerequisites<a name="prerequisites"></a>

Before you start integrating Uloq into your application, ensure that you have the following prerequisites:

- .NET SDK installed on your development machine
- Uloq API key and credentials

### Installation<a name="installation"></a>

To install the Uloq .NET SDK in your project, you can use the NuGet package manager in Visual Studio. Follow these steps:

1. Open your project in Visual Studio.
2. Right-click on your project in the Solution Explorer and select "Manage NuGet Packages".
3. In the NuGet Package Manager, search for "Uloq.SDK".
4. Click on the "Uloq.SDK" package in the search results.
5. Select the desired version of the package and click "Install" to add it to your project.

Alternatively, you can install the Uloq .NET SDK using the .NET CLI. Open a command prompt and navigate to your project directory. Run the following command:

```shell
dotnet add package Uloq.SDK
```

### Usage<a name="usage"></a>

To use the Uloq SDK in your application, follow these steps:

1. Import the necessary namespaces in your code:

```csharp
using Uloq.SDK.Authorizations;
using Uloq.SDK.Eccenscia.Services.Models.UloqRequestor;
```

2. Create an instance of the `AuthorizationRequestor` class:

```csharp
var authorizationRequestor = new AuthorizationRequestor(Models.ConnectionModel.CreateConnection("test", "test", true));
```

3. Use the methods provided by the `AuthorizationRequestor` class to perform authorization-related actions.

### Creating Authorization Request<a name="creating-authorization-request"></a>

The `CreateAuthorizationRequest` method allows you to create an authorization request using the Uloq.SDK. The following example demonstrates how to create an authorization request with a model:

```csharp
string keyIdentifier = "<insert your uloq key identifier>";
string notificationIdentifier = Guid.NewGuid().ToString();

var request = new AuthorizationRequest
{
    ActionMessage = "Test Message",
    ActionTitle = "Test Title",
    Category = "Test Category",
    ExpiryDateUTC = DateTime.UtcNow.ToString(),
    KeyIdentifier = keyIdentifier,
    Metadata = "Test Metadata",
    NotificationIdentifier = notificationIdentifier
};

bool authorizationCreated = await authorizationRequestor.CreateAuthorization(request);

// Assert the authorization creation status and perform necessary actions
```

The `CreateAuthorizationRequestWithFields` method demonstrates how to create an authorization request with individual fields:

```csharp
string keyIdentifier = "<insert your uloq key identifier>";
string notificationIdentifier = Guid.NewGuid().ToString();

var request = new AuthorizationRequest
{
    ActionMessage = "Test Message",
    ActionTitle = "Test Title",
    Category = "Test Category",
    ExpiryDateUTC = DateTime.UtcNow.ToString(),
    KeyIdentifier = keyIdentifier,
    Metadata = "Test Metadata",
    NotificationIdentifier = notificationIdentifier
};

bool authorizationCreated = await authorizationRequestor.CreateAuthorization(
    request.KeyIdentifier,
    request.NotificationIdentifier,
    DateTime.UtcNow.AddMinutes(1),
    request.Category,
    request.ActionTitle,
    request.ActionMessage,
    request.Metadata);

// Assert the authorization creation status and perform necessary actions
```

### Getting Authorization Response<a name="getting-authorization-response"></a>

The `GetRequestResponse` method allows you to retrieve an authorization response using the Uloq.SDK. It waits for a response for a specified timeout period. The following example demonstrates how to get an authorization response:

```csharp
var response = await authorizationRequestor.GetAuthorizationResponse(new NotificationDetailsRequest(notificationIdentifier));

int counter = 0;
while (response == null && counter < 30)
{
    Thread.Sleep(1000);
    counter++;
    response = await authorizationRequestor.GetAuthorizationResponse(new NotificationDetailsRequest(notificationIdentifier));
}

if (response != null)
{
    Assert.True(response.Status == NotificationDetailsResponse.StatusEnum.Approved || response.Status == NotificationDetailsResponse.StatusEnum.Declined, "Status is pending");
    Assert.True(response.Signature != null, "Signature is not null");
    Assert.True(response.KeyIdentifier == keyIdentifier, "Key identifier is correct");
}
```

The `RunAuthorizationResponseTask_ShouldReturnResponseWithinTimeoutPeriod` method demonstrates how to run the authorization response task with a specified timeout period:

```csharp
// Arrange
var notificationDetailsRequest = new NotificationDetailsRequest();
var interval = TimeSpan.FromSeconds(1);
var timeout = TimeSpan.FromSeconds(10);

// Act
var task = authorizationRequestor.RunAuthorizationResponseTask(notificationDetailsRequest, interval, timeout);
var response = await task;

if (response != null)
    Assert.NotNull(response);
```

Please note that the `Thread.Sleep` method in the `GetRequestResponse` method is used for demonstration purposes and should be replaced with a more suitable approach in a production environment.

## Contributing<a name="contributing"></a>

Contributions to the Uloq project are welcome! If you find any issues or have suggestions for improvement, please submit an issue or pull request on the GitHub repository.

## License<a name="license"></a>

Uloq is licensed under the MIT License. See the [LICENSE](./LICENSE) file for more details.