using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VehicleFleetManager;

namespace VehicleFleetManager
{
    /// <summary>
    /// Manages a collection of vehicles in the company fleet.
    /// </summary>
    public class Fleet
    {
        private readonly List<Vehicle> _vehicles = new List<Vehicle>();

        /// <summary>
        /// Adds a vehicle to the fleet.
        /// </summary>
        /// <param name="v">The vehicle to add</param>
        /// <exception cref="ArgumentNullException">Thrown if vehicle is null</exception>
        public void AddVehicle(Vehicle v)
        {
            if (v == null)
            {
                throw new ArgumentNullException(nameof(v));
            }

            _vehicles.Add(v);
        }

        /// <summary>
        /// Removes the first vehicle that matches the given model (case-insensitive).
        /// </summary>
        /// <param name="model">The model name to search for</param>
        /// <returns>true if a vehicle was removed, false if no match was found</returns>
        public bool RemoveVehicle(string model)
        {
            if (string.IsNullOrWhiteSpace(model))
            {
                return false;
            }

            var vehicleToRemove = _vehicles
                .FirstOrDefault(v => v.Model.Equals(model.Trim(), StringComparison.OrdinalIgnoreCase));

            if (vehicleToRemove != null)
            {
                return _vehicles.Remove(vehicleToRemove);
            }

            return false;
        }

        /// <summary>
        /// Calculates the average mileage of all vehicles in the fleet.
        /// </summary>
        /// <returns>The average mileage, or 0 if the fleet is empty</returns>
        public double GetAverageMileage()
        {
            if (_vehicles.Count == 0)
            {
                return 0.0;
            }

            return _vehicles.Average(v => v.Mileage);
        }

        /// <summary>
        /// Prints a summary of every vehicle in the fleet to the console.
        /// </summary>
        public void DisplayAllVehicles()
        {
            if (_vehicles.Count == 0)
            {
                Console.WriteLine("The fleet is empty.");
                return;
            }

            Console.WriteLine($"Fleet contains {_vehicles.Count} vehicle(s):\n");

            int index = 1;
            foreach (var vehicle in _vehicles)
            {
                Console.WriteLine($"[{index}]");
                Console.WriteLine(vehicle.GetSummary());
                Console.WriteLine(new string('-', 40));
                index++;
            }
        }

        /// <summary>
        /// Performs service on every vehicle that currently needs it.
        /// </summary>
        /// <returns>The number of vehicles that were serviced</returns>
        public int ServiceAllDue()
        {
            int servicedCount = 0;

            foreach (var vehicle in _vehicles)
            {
                if (vehicle.NeedsService())
                {
                    vehicle.PerformService();
                    servicedCount++;
                }
            }

            return servicedCount;
        }

        // Optional: convenience properties / methods

        public int Count => _vehicles.Count;

        public IReadOnlyList<Vehicle> Vehicles => _vehicles.AsReadOnly();

        public void Clear()
        {
            _vehicles.Clear();
        }
    }
}
