using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using kneat.swapi.Domain;
using kneat.swapi.Infra.Models;
using Newtonsoft.Json;

namespace kneat.swapi.Infra {
    public class SWApiClient {
        private readonly HttpClient _client;

        /// <summary>
        /// SWAPI client constructor.
        /// </summary>
        public SWApiClient() {
            _client = new HttpClient();
        }

        /// <summary>
        /// Gets all starships from the SWAPI.co starships endpoint.
        /// </summary>
        /// <returns>All starships from SWAPI.co, converted to domain model.</returns>
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

        /// <summary>
        /// Converts starships from the SWAPI.co to starships from the domain model.
        /// </summary>
        /// <param name="swapiStarships">Enumerable contaning SWAPI.co starships endpoint.</param>
        /// <returns>Enumerable starships domain model.</returns>
        private IEnumerable<Starship> ConvertToStarship(IEnumerable<Result> swapiStarships) {
            return swapiStarships
                .Where(x => x.MGLT.Trim().ToLower() != "unknown" &&
                    x.consumables.Trim().ToLower() != "unknown")
                .Select(x => new Starship(x.name, Int32.Parse(x.MGLT), x.consumables));
        }
    }
}