# XmlFeedConsumer

Practical assignment from UltraPlay for the role of Junior .Net Developer. The application requests the http://vitalbet.net/sportxml feed every 60 seconds and stores only the new entities in the database and updates the existing ones if a change is present. Only matches which have odds and will start in the next 24 hours are displayed. Data on the page are be updated every 60 seconds.

## Build status

[![Build status](https://ci.appveyor.com/api/projects/status/x71wm2k7pgjr777k?svg=true)](https://ci.appveyor.com/project/itplamen/xmlfeedconsumer)

## Technologies

* ASP.NET MVC 5 with Razor view engine - [link](https://github.com/itplamen/XmlFeedConsumer/blob/master/XmlFeedConsumer/Web/XmlFeedConsumer.Web/Views/Matches/Index.cshtml)
* ASP.NET SignalR - [link](https://github.com/itplamen/XmlFeedConsumer/blob/master/XmlFeedConsumer/Web/XmlFeedConsumer.Web/Hubs/DataHub.cs)
* Entity Framework Code First - [link](https://github.com/itplamen/XmlFeedConsumer/blob/master/XmlFeedConsumer/Data/XmlFeedConsumer.Data.Models/Match.cs)
* Repository pattern - [link](https://github.com/itplamen/XmlFeedConsumer/blob/master/XmlFeedConsumer/Data/XmlFeedConsumer.Data.Common/DbRepository%7BT%7D.cs)

## Libraries

* Ninject Dependency Injector - [link](https://github.com/ninject)
* Automapper, object-object mapper - [link](https://github.com/AutoMapper/AutoMapper)
* Moq, mocking framework - [link](https://github.com/moq/moq4)
* EntityFramework.Extended - [link](https://github.com/loresoft/EntityFramework.Extended)
* Bytes2you.Validation - [link](https://github.com/veskokolev/Bytes2you.Validation)
* NUnit - [link](https://github.com/nunit)
* TestStack.FluentMVCTesting - [link](https://github.com/TestStack/TestStack.FluentMVCTesting) 
* MvcRouteTester.Mvc5 - [link](https://github.com/AnthonySteele/MvcRouteTester)