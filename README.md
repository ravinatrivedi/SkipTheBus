# RideSharingApp
  * Technology - ASP.NET MVC
  * Database - SQL Server (Used LocalDB Instance ProjectV13. Can also use locally installed SQL Server by changing connectionString in WebConfig file)
  * Entity framework (code first approach)
  * ASP.NET Identity 
  * Partial Views
## Steps taken to complete the project:
  * Created an empty project with individual authentication. 
  * Created the required models, in this case Ride.cs, Booking.cs, Transaction.cs and created the related database using code first Entity framework.
  * Designed the views for Ride, Booking, Home. 
  * Implemented business logic and web security in controller. 
  * Tested against sample data. 
## Roles in the application:
  * Drivers
  * Passengers
## Functionality:
  * Only Users registered as ‘Driver’ can post a ride.
  * Drivers and Passenger can book rides.
  * Drivers cannot book their own ride.
  * Transactions are loaded as users book a ride. 
## Registered Users:
  * Username - rachel@gmail.com, Password - Rachel@123, Role - Driver
  * Username - charlie@gmail.com, Password - Charlie@123, Role - Passenger
