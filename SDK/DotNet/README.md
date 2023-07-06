# Uloq

The Uloq SDK allows developers to 

* Generate Uloq QR Codes
* Request Authorization for a specific key
* Get the responses of authorizations

## Get an API key and secret
In order to get an api key, you need to register your application at [Uloq Auth](https://auth.uloq.io) and register a developer account.
From there you will need to navigate to the Uloq section and create a Uloq access key pair.

## Creating the Connection object
The SDK connection object is created as follows:
```
Uloq.SDK.Models.ConnectionModel.CreateConnection(apiKey, apiSecret, (boolean) Use Sandbox)
```
This will create a connection object for you which will be used when connecting to the Uloq services.

## QR Code Generation
```
var connection = Models.ConnectionModel.CreateConnection("test", "test", true);
QRGenerator qrGenerator = new QRGenerator(connection);
QRCodeResponse? output = await qrGenerator.GenerateQRCode(
	new QRCodeRequest() { 
		Category = "Test", 
		ActionTitle = "Sign Test", 
		ActionMessage = "Test Message", 
		Metadata = "Test Metadata", 
		PublicKey = "", 
		RequestType = QRCodeRequest.RequestTypeEnum.Sign 
	});
```