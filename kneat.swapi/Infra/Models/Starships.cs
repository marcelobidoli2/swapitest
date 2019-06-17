using System.Collections.Generic;

namespace kneat.swapi.Infra.Models {
    public class Starships {
        public int count { get; set; }
        public string next { get; set; }
        public object previous { get; set; }
        public List<Result> results { get; set; }
    }
}