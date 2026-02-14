using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleFleetManager
{
    /// <summary>
    /// Represents a vehicle in the company fleet.
    /// </summary>
    public class Vehicle
    {
        // Private backing fields
        private string _make;
        private string _model;
        private int _year;
        private double _mileage;
        private double _lastServiceMileage;

        // Public properties
        public string Make
        {
            get => _make;
            set => _make = value?.Trim() ?? "Unknown";
        }

        public string Model
        {
            get => _model;
            set => _model = value?.Trim() ?? "Unknown";
        }

        public int Year
        {
            get => _year;
            set
            {
                // Reasonable validation for vehicle year
                if (value >= 1900 && value <= DateTime.Now.Year + 1)
                    _year = value;
                else
                    _year = DateTime.Now.Year;
            }
        }

        public double Mileage
        {
            get => _mileage;
            private set => _mileage = value >= 0 ? value : 0;
        }

        public double LastServiceMileage
        {
            get => _lastServiceMileage;
            private set => _lastServiceMileage = value >= 0 ? value : 0;
        }

        // Default constructor
        public Vehicle()
        {
            _make = "Unknown";
            _model = "Unknown";
            _year = DateTime.Now.Year;
            _mileage = 0.0;
            _lastServiceMileage = 0.0;
        }

        // Overloaded constructor
        public Vehicle(string make, string model, int year, double initialMileage)
        {
            Make = make;
            Model = model;
            Year = year;
            Mileage = initialMileage >= 0 ? initialMileage : 0.0;
            LastServiceMileage = 0.0;  // New vehicles start with service at 0 miles
        }

        /// <summary>
        /// Adds miles to the vehicle's current mileage.
        /// Negative values are ignored.
        /// </summary>
        /// <param name="miles">Miles to add</param>
        public void AddMileage(double miles)
        {
            if (miles > 0)
            {
                Mileage += miles;
            }
        }

        /// <summary>
        /// Determines whether the vehicle needs service.
        /// Service is needed if more than 10,000 miles have been driven since last service.
        /// </summary>
        /// <returns>true if service is due, false otherwise</returns>
        public bool NeedsService()
        {
            const double SERVICE_INTERVAL = 10000.0;
            return (Mileage - LastServiceMileage) >= SERVICE_INTERVAL;
        }

        /// <summary>
        /// Records that a service has been performed by setting LastServiceMileage
        /// to the current mileage.
        /// </summary>
        public void PerformService()
        {
            LastServiceMileage = Mileage;
        }

        /// <summary>
        /// Returns a formatted summary of the vehicle's information.
        /// </summary>
        /// <returns>A string containing make, model, year, mileage, and service status</returns>
        // In Vehicle.cs → update GetSummary()
        public string GetSummary()
        {
            string serviceStatus = NeedsService() ? "NEEDS SERVICE" : "Service up to date";

            // Use invariant culture for consistent number formatting (dot separator)
            var ci = System.Globalization.CultureInfo.InvariantCulture;

            return $"Vehicle: {Year} {Make} {Model}\n" +
                   $"Mileage: {Mileage.ToString("N1", ci)} miles\n" +
                   $"Last service at: {LastServiceMileage.ToString("N1", ci)} miles\n" +
                   $"Status: {serviceStatus}";
        }

        // Optional: override ToString for easier debugging/logging
        public override string ToString()
        {
            return $"{Year} {Make} {Model} ({Mileage:N1} mi)";
        }
    }
}