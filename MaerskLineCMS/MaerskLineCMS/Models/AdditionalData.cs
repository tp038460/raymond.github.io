using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaerskLineCMS.Models
{
    public class scheduleData
    {
        public int scheduleID { get; set; }
        public string scheduleDetail { get; set; }
        public string scheduleDeparture { get; set; }
        public System.DateTime scheduleDepartureDate { get; set; }
        public System.TimeSpan scheduleDepartureTime { get; set; }
        public string scheduleArrival { get; set; }
        public System.DateTime scheduleArrivalDate { get; set; }
        public System.TimeSpan scheduleArrivalTime { get; set; }
        public int shipID { get; set; }
        public int adminID { get; set; }

        public string shipName { get; set; }
        public decimal shipTEU { get; set; }

        public decimal requiredTEU { get; set; }

    }

    public class scheduleDataWithBooking
    {
        public int scheduleID { get; set; }
        public string scheduleDetail { get; set; }
        public string scheduleDeparture { get; set; }
        public System.DateTime scheduleDepartureDate { get; set; }
        public System.TimeSpan scheduleDepartureTime { get; set; }
        public string scheduleArrival { get; set; }
        public System.DateTime scheduleArrivalDate { get; set; }
        public System.TimeSpan scheduleArrivalTime { get; set; }
        public int shipID { get; set; }
        public int adminID { get; set; }

        public string shipName { get; set; }
        public decimal shipTEU { get; set; }
        
        public decimal requiredTEUforship { get; set; }

        public int scheduleBookingID { get; set; }
        public int agentID { get; set; }
        public string status { get; set; }
        public decimal requiredTEU { get; set; }

    }
}