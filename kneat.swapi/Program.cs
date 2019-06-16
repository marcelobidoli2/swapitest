using System;
using System.Collections.Generic;
using System.Net.Http;
using kneat.swapi.Domain.Models;
using Newtonsoft.Json;

namespace kneat.swapi {
    class Program {
        static void Main(string[] args) {
            
            using(HttpClient client = new HttpClient())
            {
                var starShips = new List<Result>();
                var hasNext = true;
                var pageCounter = 1;
                while(hasNext) {
                    var swapiResult = client.GetStringAsync($"https://swapi.co/api/starships/?page={pageCounter}").Result;

                    var a = JsonConvert.DeserializeObject<Starships>(swapiResult);
                    starShips.AddRange(a.results);

                    if(String.IsNullOrEmpty(a.next)) { break; }
                    pageCounter++;
                }
            }
        }
    }
}