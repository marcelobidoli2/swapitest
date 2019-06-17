using System;
using System.Collections.Generic;
using System.Linq;

namespace kneat.swapi.Domain {
    public class Starship {
        private readonly int HOURS_IN_DAY = 24;
        private readonly int HOURS_IN_WEEK = 168;
        private readonly int HOURS_IN_MOUNT = 720;
        private readonly int HOURS_IN_YEAR = 8760;

        public Starship(string name, int mGLT, string consumables) {
            this.Name = name;
            this.MGLT = mGLT;
            this.Consumables = consumables;
            this.ValidationNotifications = new List<string>();
            Validate();
        }

        /// <summary>
        /// Gets or sets the starship name.
        /// </summary>
        /// <value>The starship name.</value>
        public string Name { get; private set; }
        /// <summary>
        /// Gets or sets the Mega Lightyears.
        /// </summary>
        /// <value>The Mega Lightyears.</value>
        public int MGLT { get; private set; }
        /// <summary>
        /// Gets or sets the consumables period.
        /// </summary>
        /// <value>The consumables period.</value>
        public string Consumables { get; private set; }
        public bool IsValid => this.ValidationNotifications.Count == 0;
        public IList<string> ValidationNotifications { get; private set; }

        /// <summary>
        /// Gets consumable amount as hours.
        /// </summary>
        /// <value>The consumables in hours.</value>
        private int ConsumablesInHours {
            get {
                var amount = Int32.Parse(this.Consumables.Split(' ').First());
                var period = this.Consumables.Split(' ').Last().Trim().ToLower();
                switch (period) {
                    case "day":
                    case "days":
                        {
                            return amount * HOURS_IN_DAY;
                        }
                    case "week":
                    case "weeks":
                        {
                            return amount * HOURS_IN_WEEK;
                        }
                    case "month":
                    case "months":
                        {
                            return amount * HOURS_IN_MOUNT;
                        }
                    case "year":
                    case "years":
                        {
                            return amount * HOURS_IN_YEAR;
                        }
                    default:
                        {
                            return 0;
                        }
                }
            }
        }

        /// <summary>
        /// Calculates the amont of stops the starship needs givin a distance in Mega Lightyears.
        /// </summary>
        /// <param name="mGLT">The distance in Mega Lightyears to bem calculated.</param>
        /// <returns>Number of stoppes needed to traval the requested Mega Lightyears parameter.</returns>
        public int CalculateStopsRequired(int mGLT) {
            var consumables = this.ConsumablesInHours;
            return (mGLT / (consumables * this.MGLT));
        }

        private void Validate() {
            if(String.IsNullOrEmpty(Name)) { this.ValidationNotifications.Add("Name is required."); }
            if(MGLT < 0) { this.ValidationNotifications.Add("MGLT must be gratter then 0."); }
            if(this.Consumables.Contains("day") == false 
                && this.Consumables.Contains("week") == false
                && this.Consumables.Contains("month") == false
                && this.Consumables.Contains("year") == false) { this.ValidationNotifications.Add("Consumables must be a valid day, week, month or year."); }
        }
    }
}