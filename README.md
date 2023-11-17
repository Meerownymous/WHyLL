# WHyLL
True object-oriented, declarative, DTO-free HTTP library. Make requests and responses using immutable objects.

Currently under development, without an alpha release.

## HTTP is simple, so is this library.
You send a message to the server, and get a message back. The message, sent and received, is text and always in this shape:

```
First Line // Saying what you want (request) or what you got back (response)
Header * n // N Headers which have a name and a value, controls things

Body       //Can contain anything from Text to Downloads
```

This is how the library is designed. There is a message, which you can refine by first line, headers, and a body.
Then, you render the message, using one of the available renderings. 
Most likely you render it to a response message, and then you render the response message to what you need, for example an XML document.


## Setup a request
This is how you setup a request message:

```csharp

var request = new Get(new Uri("http://www.enhanced-calm.com"), new Version(1, 1));

```

Then, you can refine the headers:

```csharp

var request = 
	new Get(new Uri("http://www.enhanced-calm.com/newest-postings"), new Version(1, 1))
		.Refine(new Header("Content-Type", "application/json"))
```

And the body:

```csharp

var request = 
	new POST(new Uri("http://www.enhanced-calm.com/upload/"), new Version(1, 1))
		.Refine(new Header("Content-Type", "application/json"))
		.Refine(new FileStream("~/my-jsons/important-posting.json", FileMode.Read));
```

## Render the response

This is how you render a request to a response:

```csharp
var response =
	await
		new GET(new Uri("http://www.enhanced-calm.com/newest-postings"), new Version(1, 1))
			.Refine(new Header("Content-Type", "application/json"))
			.Render(new AspNetResponse());
```

And from there on, you can render the response to what you need. For example, json content:

```csharp

var bodyText =
	await 
		new GET(new Uri("http://www.enhanced-calm.com/newest-postings"), new Version(1, 1))
			.Refine(new Header("Content-Type", "application/json"))
			.Render(new AspNetResponse())
			.Render(new BodyAsText());
```