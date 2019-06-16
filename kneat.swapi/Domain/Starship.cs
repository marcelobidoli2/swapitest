namespace kneat.swapi.Domain
{
    public class Starship
    {
        public Starship(string name, int stopsRequired)
        {
            this.Name = name;
            this.StopsRequired = stopsRequired;
        }
        
        public string Name { get; private set; }
        public int StopsRequired { get; private set; }
    }
}