using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carvana
{
    using System;
    using System.Collections.Generic;

    public class History
    {
        private List<string> damageHistory; // List of past damages
        private List<string> previousRentals; // List of past rentals

        public History()
        {
            this.damageHistory = new List<string>();
            this.previousRentals = new List<string>();
        }

        public List<string> getDamageHistory() { return damageHistory; }
        public List<string> getPreviousRentals() { return previousRentals; }

        public void addDamage(string damage)
        {
            damageHistory.Add(damage);
        }

        public void addRental(string rental)
        {
            previousRentals.Add(rental);
        }

        public override string ToString()
        {
            string damages = damageHistory.Count > 0 ? string.Join(", ", damageHistory) : "None";
            string rentals = previousRentals.Count > 0 ? string.Join(", ", previousRentals) : "None";

            return $"Damage History: {damages}\nPrevious Rentals: {rentals}";
        }
    }
}

