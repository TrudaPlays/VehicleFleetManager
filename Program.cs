namespace VehicleFleetManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Fleet fleet = new Fleet();


            Console.WriteLine("=== Vehicle Fleet Manager ===\n");

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
                        Console.WriteLine("Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a number 1–6.");
                        break;
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("=== Vehicle Fleet Manager ===\n");
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("1. Add Vehicle by Make, Model, Year and Mileage");
            Console.WriteLine("2. Remove Vehicle by Make (Corolla, Escape, etc)");
            Console.WriteLine("3. Display Fleet");
            Console.WriteLine("4. Show Average Mileage of entire Fleet");
            Console.WriteLine("5. Service all Vehicles");
            Console.WriteLine("6. Exit");
            Console.Write("Choose: ");
        }

        static void AddVehicle(Fleet fleet)
        {
            Console.Write("Enter Make: ");
            string make = Console.ReadLine()?.Trim() ?? "Unknown";

            Console.Write("Enter Model: ");
            string model = Console.ReadLine()?.Trim() ?? "Unknown";

            Console.Write("Enter Year: ");
            if (!int.TryParse(Console.ReadLine(), out int year) || year < 1900 || year > DateTime.Now.Year + 2)
            {
                Console.WriteLine("Invalid year. Vehicle not added.");
                return;
            }

            Console.Write("Enter Mileage: ");
            if (!double.TryParse(Console.ReadLine(), out double mileage) || mileage < 0)
            {
                Console.WriteLine("Invalid mileage. Vehicle not added.");
                return;
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