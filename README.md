# WHyLL
True object-oriented, declarative, DTO-free HTTP-Inspired library (with HTTP support).
Make requests and render responses using only immutable objects.

## HTTP messaging is simple, so is this library.
You send a http message to the server, and get a message back.
The message, sent and received, is text and always in this shape:

```
First Line // Saying what you want (request) or what you got back (response)
Header * n // N Headers which have a name and a value, controls things

Body       //Can contain anything from Text to Downloads
```

This is how the library is designed. There is a message, which you can refine by first line, headers, and a body.
Then, you render the message, using one of the available renderings.
Most likely you render it to a response message, and then you render the response message to what you need, for example an XML document.

## Setup a message
This is how you setup a http GET message:

```csharp
var request = new Get(new Uri("http://www.enhance-your-calm.com"));

```

Then, you can refine the headers:

```csharp

var request = 
	new Get(new Uri("http://www.enhance-your-calm.com/newest-postings"))
		.Refine(new Header("Content-Type", "application/json"))
```

And the body:

```csharp

var request = 
	new POST(new Uri("http://www.enhance-your-calm.com/upload/"))
		.Refine(new Header("Content-Type", "application/json"))
		.Refine(new FileStream("~/my-jsons/important-posting.json", FileMode.Read));
```

## Render the response (asynchronously)

This is how you render a request to a response (using Asp.Net):

```csharp
var response =
	await
		new GET(new Uri("http://www.enhance-your-calm.com/newest-postings"))
			.Refine(new Header("Content-Type", "application/json"))
			.Render(new AspNetResponse());
```

And from there on, you can render the response to what you need. For example, json content:

```csharp
var bodyText =
	await 
		new GET(new Uri("http://www.enhance-your-calm.com/newest-postings"))
			.Refine(new Header("Content-Type", "application/json"))
			.Render(new AspNetResponse())
			.Render(new BodyAsText());
```

## Http 2.0 and 3.0
For using Http 2.0/3.0 in your requests, take the objects in the WHyLL.Request.Http2 or WHyLL.Request.Http3 namespaces.

# Advanced rendering
Use strategic rendering to build a control-flow in your app:

```csharp
var result =
	await 
		new GET(new Uri("http://www.enhance-your-calm.com/todo"))
			.Render(new AspNetResponse())
			.Render(
				new Switch<string>(
					new Case<string>(body =>
						new StreamReader(stream)
							.ReadToEnd()
							.Contains("freetime"),
						new Fixed("Over and Out")
					),
					new Case<string>(body => true, //default to this
						new Fixed(
							"Text in the body is: " + new StreamReader(stream).ReadToEnd()
						)
					)
			);
```
```