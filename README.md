# C# + SQL Server + SOAP + Win Forms

This is a product application on 3 separate tiers, Data Tier, Business Logic Tier and Presentation Tier. The database server client program is a 
web service, which functions as a server of methods that can be accessed remotely over the internet by client applications.  The web service is 
exposed by the Windows web server, IIS. Any client program will be able to use the methods exposed by the web service, which implement the logic of 
the application. This product application's database contains 3 tables, one for products, one for clients, linked by the code of product and one table for comments. A client makes a request for a product or more which is stored in the database, can update or delete the request and recieve a greeting message. The client can make a rating for each product and can write a message.
