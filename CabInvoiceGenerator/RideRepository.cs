using System;
using System.Collections.Generic;
using System.Text;

namespace CabInvoiceGenerator
{
    public class RideRepository
    {
        Dictionary<string, List<Ride>> userRide = null;
        public RideRepository()
        {
            this.userRide = new Dictionary<string, List<Ride>>();
        }
        public void AddRide(string userId, Ride[] rides)
        {
            bool rideList = this.userRide.ContainsKey(userId);
            try
            {
                if (!rideList)
                {
                    List<Ride> list = new List<Ride>();
                    list.AddRange(rides);
                    this.userRide.Add(userId, list);
                }
            }
            catch (CabeInvoiceException)
            {
                throw new CabeInvoiceException(CabeInvoiceException.ExceptionType.NULL_RIDES, "Rides are null");
            }
        }
        public Ride[] GetRides(string userId)
        {
            try
            {
                return this.userRide[userId].ToArray();
            }
            catch (CabeInvoiceException)
            {
                throw new CabeInvoiceException(CabeInvoiceException.ExceptionType.INVALID_USER_ID, "Invalid userId");
            }
        }
    }
}
