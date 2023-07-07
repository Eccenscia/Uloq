Certainly! Here's the complete document for the Uloq integration:

# Uloq Integration Documentation

Welcome to the Uloq integration documentation. This guide will help you understand how to integrate Uloq into your application using the Uloq SDK.

## Table of Contents

- [Introduction](#introduction)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Getting Started](#getting-started)
  - [Creating an Authorization Request](#creating-authorization-request)
  - [Creating an Authorization Request with Fields](#creating-authorization-request-with-fields)
  - [Getting Authorization Response](#getting-authorization-response)
  - [Testing Response Timeout](#testing-response-timeout)
- [Sample Code](#sample-code)
- [Contributing](#contributing)
- [License](#license)

## Introduction<a name="introduction"></a>

Uloq is a powerful service that provides authorization and QR code generation capabilities for your application. With Uloq, you can easily implement secure authorization flows and generate QR codes for various purposes.

This integration documentation focuses on the Uloq SDK, which provides a convenient way to interact with the Uloq service in your .NET applications. You'll learn how to create authorization requests, retrieve authorization responses, and generate QR codes using the Uloq SDK.

## Prerequisites<a name="prerequisites"></a>

Before you begin integrating Uloq into your application, make sure you have the following prerequisites:

- .NET Core or .NET Framework installed on your development machine.
- Uloq API key and related credentials.
- Access to the Uloq service endpoints.

## Installation<a name="installation"></a>

To install the Uloq SDK in your application, you can use either NuGet Package Manager or the .NET CLI.

### NuGet Package Manager

1. Open the NuGet Package Manager in Visual Studio.
2. Search for `Uloq.SDK` in the NuGet package repository.
3. Select the `Uloq.SDK` package from the search results.
4. Choose the desired version of the package and click the **Install** button.

### .NET CLI

1. Open a command prompt or terminal.
2. Navigate to your project directory.
3. Run the following command:

   ```shell
   dotnet add package Uloq.SDK
   ```

## Getting Started<a name="getting-started"></a>

To get started with Uloq integration, follow the steps below:

1. Obtain your Uloq API key and related credentials from the Uloq service provider.
2. Install the Uloq SDK in your .NET application using the installation instructions provided above.
3. Create an instance of the `UloqAuthorizationClient` class using your API key and credentials.
4. Use the methods provided by the `UloqAuthorizationClient` class to interact with the Uloq service.

### Creating an Authorization Request<a name="creating-authorization-request"></a>

To create an authorization request, use the `CreateAuthorizationRequest` method of the `UloqAuthorizationClient` class. This method allows you to create an authorization request with various parameters, such as action message, action title, category, expiry date, key identifier, metadata, and notification identifier.

Example:

```csharp
using Uloq.SDK.Authorizations;

// Create an instance of the UloqAuthorizationClient class
UloqAuthorizationClient authorizationClient = new UloqAuthorizationClient("<your-api-key>", "<your-credentials>");

// Create an authorization request
AuthorizationRequest request = new AuthorizationRequest
{
    ActionMessage = "Test Message",
    ActionTitle = "Test Title",
    Category = "Test Category",
    ExpiryDateUTC = DateTime.UtcNow.ToString(),
    KeyIdentifier = "<insert your uloq key identifier>",
    Metadata = "Test Metadata",
    NotificationIdentifier = Guid.NewGuid().ToString()
};

// Send the authorization request
bool authorizationCreated = await authorizationClient.CreateAuthorizationRequest(request);

```

### Creating an Authorization Request with Fields<a name="creating-authorization-request-with-fields"></a>

If you prefer to create an authorization request by providing individual fields, you can use the `CreateAuthorizationRequest` method with the corresponding parameters. This approach offers flexibility in constructing the authorization request.

Example:

```csharp
using Uloq.SDK.Authorizations;

// Create an instance of the UloqAuthorizationClient class
UloqAuthorizationClient authorizationClient = new UloqAuthorizationClient("<your-api-key>", "<your-credentials>");

// Create an authorization request with individual fields
bool authorizationCreated = await authorizationClient.CreateAuthorizationRequest(
    "<insert your uloq key identifier>",
    Guid.NewGuid().ToString(),
    DateTime.UtcNow.AddMinutes(1),
    "Test Category",
    "Test Title",
    "Test Message",
    "Test Metadata");

```

### Getting Authorization Response<a name="getting-authorization-response"></a>

To retrieve an authorization response, use the `GetAuthorizationResponse` method of the `UloqAuthorizationClient` class. This method allows you to get the authorization response based on the notification identifier.

Example:

```csharp
using Uloq.SDK.Authorizations;

// Create an instance of the UloqAuthorizationClient class
UloqAuthorizationClient authorizationClient = new UloqAuthorizationClient("<your-api-key>", "<your-credentials>");

// Get the authorization response for a notification identifier
NotificationDetailsResponse response = await authorizationClient.GetAuthorizationResponse("<notification-identifier>");

```

### Testing Response Timeout<a name="testing-response-timeout"></a>

If you want to test the response timeout when waiting for an authorization response, you can use the `RunAuthorizationResponseTask` method of the `UloqAuthorizationClient` class. This method allows you to run a task that waits for the authorization response within a specified timeout period.

Example:

```csharp
using Uloq.SDK.Authorizations;

// Create an instance of the UloqAuthorizationClient class
UloqAuthorizationClient authorizationClient = new UloqAuthorizationClient("<your-api-key>", "<your-credentials>");

// Set up the necessary parameters
NotificationDetailsRequest detailsRequest = new NotificationDetailsRequest();
TimeSpan interval = TimeSpan.FromSeconds(1);
TimeSpan timeout = TimeSpan.FromSeconds(10);

// Run the authorization response task
Task<NotificationDetailsResponse> task = authorizationClient.RunAuthorizationResponseTask(detailsRequest, interval, timeout);

// Wait for the task to complete
NotificationDetailsResponse response = await task;

```

In this example, the `RunAuthorizationResponseTask` method starts a task that waits for the authorization response within the given timeout period. If the response is received within the timeout, the task completes successfully.

## Sample Code<a name="sample-code"></a>

For more sample code and usage examples, please refer to the provided test classes in the Uloq SDK repository.

## Contributing<a name="contributing"></a>

Contributions to the Uloq SDK are welcome! If you find any issues or have suggestions for improvement, please submit an issue or pull request on the [Uloq GitHub repository](https://github.com/Eccenscia/Uloq).

## License<a name="license"></a>
The **Uloq.SDK** is released under the [MIT License](https://opensource.org/licenses/MIT).