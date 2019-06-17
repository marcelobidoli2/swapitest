using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using kneat.swapi.Domain;
using kneat.swapi.Domain.Models;
using Newtonsoft.Json;

namespace kneat.swapi.Infra {
    public class SWApiClient {
        private readonly HttpClient _client;

        public SWApiClient() {
            _client = new HttpClient();
        }

        public async Task<IEnumerable<Starship>> GetAllStarships() {
            var swapiStarships = new List<Result>();
            var hasNext = true;
            var pageCounter = 1;

            while (hasNext) {
                var swapiResult = await _client.GetStringAsync($"https://swapi.co/api/starships/?page={pageCounter}");

                var a = JsonConvert.DeserializeObject<Starships>(swapiResult);
                swapiStarships.AddRange(a.results);

                if (String.IsNullOrEmpty(a.next)) { break; }
                pageCounter++;
            }

            return ConvertToStarship(swapiStarships);
        }

        private IEnumerable<Starship> ConvertToStarship(List<Result> swapiStarships)
        {
            return swapiStarships
                .Where(x => x.MGLT.Trim().ToLower() != "unknown"
                    && x.consumables.Trim().ToLower() != "unknown")
                .Select(x => new Starship(x.name, Int32.Parse(x.MGLT), x.consumables));
        }
    }
}