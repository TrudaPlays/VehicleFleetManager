namespace VehicleFleetManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Fleet fleet = new Fleet();


            while (true)
            {
                DisplayMenu();
                string choice = Console.ReadLine()?.Trim() ?? "";

                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        AddVehicle(fleet);
                        break;

                    case "2":
                        RemoveVehicle(fleet);
                        break;

                    case "3":
                        fleet.DisplayAllVehicles();
                        break;

                    case "4":
                        ShowAverageMileage(fleet);
                        break;

                    case "5":
                        ServiceDueVehicles(fleet);
                        break;

                    case "6":
                        AddMileageToVehicle(fleet);
                        break;

                    case "7":
                        Console.WriteLine("Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a number 1–7.");
                        break;
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        static void AddMileageToVehicle(Fleet fleet)
        {
            if (fleet.Count == 0)
            {
                Console.WriteLine("The fleet is empty. No vehicles to update.");
                return;
            }

            // Show current vehicles so user knows what models exist
            Console.WriteLine("\nCurrent vehicles in fleet:");
            fleet.DisplayAllVehicles();

            string model;
            Vehicle targetVehicle = null;

            // Loop until the program finds a matching vehicle or the user cancels
            while (true)
            {
                Console.Write("\nEnter the Model of the vehicle to add mileage to (or 'cancel' to go back): ");
                model = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(model))
                {
                    Console.WriteLine("Model cannot be empty. Try again.");
                    continue;
                }

                if (model.Equals("cancel", StringComparison.OrdinalIgnoreCase) ||
                    model.Equals("c", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Operation cancelled.");
                    return;
                }

                // Find vehicle by model (case-insensitive, first match)
                targetVehicle = fleet.Vehicles
                    .FirstOrDefault(v => v.Model.Equals(model, StringComparison.OrdinalIgnoreCase));

                if (targetVehicle != null)
                {
                    Console.WriteLine($"Found vehicle: {targetVehicle.Year} {targetVehicle.Make} {targetVehicle.Model} ({targetVehicle.Mileage:N1} miles)");
                    break;
                }

                Console.WriteLine($"No vehicle found with model '{model}'. Please try again.");
            }

            // Now ask for mileage amount – loop until valid
            double milesToAdd;
            while (true)
            {
                Console.Write("Enter miles to add: ");
                string input = Console.ReadLine()?.Trim();

                if (double.TryParse(input, out milesToAdd) && milesToAdd >= 0)
                {
                    break;
                }

                Console.WriteLine("Please enter a valid non-negative number.");
            }

            // Actually add the mileage
            targetVehicle.AddMileage(milesToAdd);

            Console.WriteLine($"Success! Added {milesToAdd:N1} miles.");
            Console.WriteLine($"New mileage for {targetVehicle.Year} {targetVehicle.Make} {targetVehicle.Model}: {targetVehicle.Mileage:N1} miles");
        }

        static void DisplayMenu() //displays a menu for the user
        {
            Console.WriteLine("=== Vehicle Fleet Manager ===\n");
            Console.WriteLine("1. Add Vehicle by Make, Model, Year and Mileage");
            Console.WriteLine("2. Remove Vehicle by Make (Corolla, Escape, etc)");
            Console.WriteLine("3. Display Fleet");
            Console.WriteLine("4. Show Average Mileage of entire Fleet");
            Console.WriteLine("5. Service all Vehicles");
            Console.WriteLine("6. Add mileage to a vehicle");
            Console.WriteLine("7. Exit");
            Console.Write("Choose: ");
        }

        static void AddVehicle(Fleet fleet)
        {
            Console.Write("Enter Make: ");
            string make = Console.ReadLine()?.Trim() ?? "Unknown";

            Console.Write("Enter Model: ");
            string model = Console.ReadLine()?.Trim() ?? "Unknown";

            int year;
            while (true) //loops until a valid year is inputted
            {
                Console.Write("Enter Year: ");
                string yearInput = Console.ReadLine()?.Trim();

                if (int.TryParse(yearInput, out year) && year >= 1900 && year <= DateTime.Now.Year + 2)
                {
                    break; // valid → exits the loop
                }

                Console.WriteLine("Invalid year. Please enter a number between 1900 and " +
                                  (DateTime.Now.Year + 2) + ".");
            }

            double mileage;
            while (true) //loops until a valid mileage is inputted
            {
                Console.Write("Enter Mileage: ");
                string mileageInput = Console.ReadLine()?.Trim();

                if (double.TryParse(mileageInput, out mileage) && mileage >= 0)
                {
                    break; // valid → exit the loop
                }

                Console.WriteLine("Invalid mileage. Please enter a non-negative number.");
            }

            var vehicle = new Vehicle(make, model, year, mileage);
            fleet.AddVehicle(vehicle);
            Console.WriteLine("Vehicle added successfully!");
        }

        static void RemoveVehicle(Fleet fleet)
        {
            if (fleet.Count == 0)
            {
                Console.WriteLine("The fleet is empty. Nothing to remove.");
                return;
            }

            Console.Write("Enter Model to remove: ");
            string model = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(model))
            {
                Console.WriteLine("Model cannot be empty.");
                return;
            }

            bool removed = fleet.RemoveVehicle(model);

            if (removed)
            {
                Console.WriteLine($"Vehicle with model '{model}' removed.");
            }
            else
            {
                Console.WriteLine($"No vehicle found with model '{model}'.");
            }
        }

        static void ShowAverageMileage(Fleet fleet)
        {
            if (fleet.Count == 0)
            {
                Console.WriteLine("The fleet is empty. No average mileage available.");
                return;
            }

            double avg = fleet.GetAverageMileage();
            Console.WriteLine($"Average mileage of the fleet: {avg:N1} miles");
        }

        static void ServiceDueVehicles(Fleet fleet)
        {
            if (fleet.Count == 0)
            {
                Console.WriteLine("The fleet is empty. No vehicles to service.");
                return;
            }

            int serviced = fleet.ServiceAllDue();

            if (serviced == 0)
            {
                Console.WriteLine("No vehicles currently need service.");
            }
            else
            {
                Console.WriteLine($"{serviced} vehicle{(serviced == 1 ? "" : "s")} serviced.");
            }
        }
    }
}