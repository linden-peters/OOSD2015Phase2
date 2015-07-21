using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Bookings
/// </summary>
public class Bookings
{
	
		//
		// TODO: Add constructor logic here
		//
        public Bookings() {}

        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public int TravelerCount { get; set; }
        public int CustomerId { get; set; }
        public int PackageId { get; set; }
        public string BookingNo { get; set; }
    
}