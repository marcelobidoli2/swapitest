using System;
using System.Collections.Generic;
using System.Net.Http;
using kneat.swapi.Domain.Models;
using kneat.swapi.Infra;
using Newtonsoft.Json;

namespace kneat.swapi {
    class Program {
        static void Main(string[] args) {
            
            var swApiClient = new SWApiClient();
            var starships = swApiClient.GetAllStarships().Result;

            var distance = 1000000;

            foreach(var starship in starships) {
                Console.WriteLine($"{starship.Name}: {starship.CalculateStopsRequired(distance)}");
            }

            Console.ReadLine();
        }
    }
}