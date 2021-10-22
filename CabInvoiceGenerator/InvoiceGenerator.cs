using System;
using System.Collections.Generic;
using System.Text;

namespace CabInvoiceGenerator
{
    public class InvoiceGenerator
    {
        RideType rideType;
        private RideRepository rideRepository;
        private double MINUMUN_COST_PER_KM;
        private int COST_PER_TIME;
        private double MINIMUN_FARE;

        public InvoiceGenerator(RideType ridetype)
        {
            this.rideType = rideType;
            this.rideRepository = new RideRepository();
            try
            {
                if (rideType.Equals(RideType.PREMIUM))
                {
                    this.MINUMUN_COST_PER_KM = 15;
                    this.COST_PER_TIME = 2;
                    this.MINIMUN_FARE = 20;
                }
                else if (rideType.Equals(RideType.NORMAL))
                {
                    this.MINUMUN_COST_PER_KM = 10;
                    this.COST_PER_TIME = 1;
                    this.MINIMUN_FARE = 5;
                }
            }
            catch (CabeInvoiceException)
            {
                throw new CabeInvoiceException(CabeInvoiceException.ExceptionType.INVALID_RIDE_TYPE, "Invalid Ride Type");
            }
        }
        public double CalculateFare(double distance, int time)
        {
            double totalFare = 0;
            try
            {
                totalFare = distance * MINUMUN_COST_PER_KM + time * COST_PER_TIME;
            }
            catch (CabeInvoiceException)
            {
                if (rideType.Equals(null))
                {
                    throw new CabeInvoiceException(CabeInvoiceException.ExceptionType.INVALID_RIDE_TYPE, "Invalid Ride Type");
                }
                if(distance <= 0)
                {
                    throw new CabeInvoiceException(CabeInvoiceException.ExceptionType.INVALID_DISTANCE, "Invalid Distance");
                }
                if (distance < 0)
                {
                    throw new CabeInvoiceException(CabeInvoiceException.ExceptionType.INVALID_TIME, "Invalid Time");
                }
            }
            return Math.Max(totalFare, MINIMUN_FARE);
        }
        public InvoiceSummary CalculateFare(Ride[] rides)
        {
            double totalFare = 0;
            try
            {
                foreach(Ride ride in rides)
                {
                    totalFare += this.CalculateFare(ride.distance, ride.time);
                }
            }
            catch (CabeInvoiceException)
            {
                throw new CabeInvoiceException(CabeInvoiceException.ExceptionType.NULL_RIDES, "Rides are null");
            }
            return new InvoiceSummary(rides.Length, totalFare);
        }
        public void AddRides(string userId, Ride[] rides)
        {
            try
            {
                rideRepository.AddRide(userId, rides);
            }
            catch (CabeInvoiceException)
            {
                if(rides == null)
                {
                    throw new CabeInvoiceException(CabeInvoiceException.ExceptionType.NULL_RIDES, "Rides are null");
                }
            }
        }
        public InvoiceSummary GetInvoiceSummary(string userId)
        {
            try
            {
                return this.CalculateFare(rideRepository.GetRides(userId));
            }
            catch (CabeInvoiceException)
            {
                throw new CabeInvoiceException(CabeInvoiceException.ExceptionType.INVALID_USER_ID, "Invalid user id");
            }
        }
    }
}
