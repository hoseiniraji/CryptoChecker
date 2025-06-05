# Answers to technical questions
---

## How much time did you spend on this task?

It took me about 6 hours to design structure, develop the backend application and write unit tests.


## If you had more time, what improvements or additions would you make?

If I had more time, I would use a more robust UI framework like React.js instead of vanilla JavaScript to improve scalability and maintainability.

I would also add a feature to automatically fetch data at regular intervals and display the updated data to users via SignalR.

Currently, the ExchangeService class maintains a local cache for about five minutes to avoid fetching data on every request. It would be beneficial to use a more persistent storage mechanism, such as a database or another form of data store.

## What is the most useful feature recently added to your favorite programming language?

There have been a large number of new features recently added to C# and the .NET framework. Most of these improvements focus on enhancing code readability and making code easier to write and understand. 
For example, primary constructors can now be used in any class, which helps make code more concise and readable.

you can now change your syntax from 

```
public Class User {

   public User(string name) {
	Name = name;
   }
   .
   .
   .
}
```

to this one: 

```
public Class User(string name) {
	.
	.
	.
}
```


## How do you identify and diagnose a performance issue in a production environment?

In most cases, monitoring and logging tools help identify and trace performance issues such as request timeouts, query delays or timeouts, network errors, or memory leaks. 
Once the issue is located, we can analyze its root cause and develop a plan to resolve it accordingly.


## What’s the last technical book you read or technical conference you attended?

In my previous job, I frequently read technical documentation and lectures related to smart home protocols, specifically the Z-Wave protocol.
One of the most interesting aspects of Z-Wave was its implementation of secure data transmission, particularly the protocol’s security layer for encrypted communication.


## What’s your opinion about this technical test?

I believe the main task was to call external APIs, process and combine the results, and then return the response to the client.

This test offers a good opportunity to demonstrate how a developer structures a project and applies clean code principles.

Additionally, there is a caching mechanism for both currency exchange rates and cryptocurrency prices to reduce the number of requests sent to remote APIs.

This strategy helps improve response times and minimizes API calls, which is important due to the limitations of free-tier plans.

## Please describe yourself using JSON format.

```
{
	"fullName": "Hosein Iraji Moazzami",
	"gender": "male"
	"birthdate": "1991-04-19",
	"email" : "iraji.work@gmail.com",
	"phoneNumber" :"09354575708",
	"social" : [
		{
			"handle" : "Linkedin",
			"url" : "https://www.linkedin.com/in/hosein-iraji-59613311b/"
		}
	],
	"expertise": "Dotnet web developer",
	"skills" : ["C#" , "Asp.net Core" , "SQL" , "SignalR" , "Websocket" , "RESTFull API" , "Html", "Css" , "Js" , "ReactJs" , "ReactNative"]
}
```