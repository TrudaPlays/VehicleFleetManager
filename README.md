How organizing code into objects makes my code easier to read and expand:
Organizing the code into objects that each contain their own private data makes the code easier to debug, read and expand upon by allowing me to know where everything
is going in the program. In my FleetManager, I have a class Fleet that kept track of all the different objects called by the user on the console: different cars with specified
make, model, year and mileage. I had a class Vehicle that had all of the code necessary to call those different objects into being. The program.cs file contained some static methods
that made use of my public methods in both my Vehicle and Fleet classes. By setting it up this way, I have allowed the user to create a huge amount of vehicles each with their own
specifications that are then added as objects and stored in the fleet. The user can modify those vehicles by way of adding more mileage, and the Vehicle class keeps track of the
amount of mileage and figures out if it needs to be serviced again if the 10,000 mileage mark has been reached. With adding xUnit tests, I was able to thoroughly go through my code and 
figure out what worked and what didn't and why what didn't work didn't work, and hunt down any bugs before they gave me trouble in the actual console app itself. This way I saved
myself a lot of headaches by fixing the problems before I even knew they existed!
