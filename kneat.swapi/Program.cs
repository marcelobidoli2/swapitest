using System;
using kneat.swapi.Infra;

namespace kneat.swapi {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("===== Welcome to the SWAPI =====");

            Console.Write("Please input the number of Mega Lightyears: ");
            var distanceInput = Console.ReadLine();
            var distance = 0;

            if (Int32.TryParse(distanceInput, out distance) == false) {
                Console.WriteLine("Please input a valid distance in Mega Lightyears...");
                Environment.Exit(0);
            }

            var swApiClient = new SWApiClient();
            var starships = swApiClient.GetAllStarships().Result;

            foreach (var starship in starships) {
                Console.WriteLine($"{starship.Name}: {starship.CalculateStopsRequired(distance)}");
            }

            Console.ReadLine();
        }
    }
}