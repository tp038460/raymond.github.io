using System;
using System.ComponentModel.DataAnnotations;

namespace MaerskLineCMS.Models
{
    public class adminMetadata
    {
        [Required(ErrorMessage = "Admin Username is required")]
        [Display(Name ="Admin Username")]
        public string adminUsername { get; set; }
        [Required(ErrorMessage = "Admin Password is required")]
        [Display(Name = "Admin Password")]
        public string adminPassword { get; set; }
        [Display(Name = "Admin Email")]
        public string adminEmail { get; set; }
        [Display(Name = "Admin Name")]
        public string adminName { get; set; }
        [Display(Name = "Admin IC")]
        public string adminIC { get; set; }
        [Display(Name = "Gender")]
        public Nullable<bool> adminGender { get; set; }
    }

    public class agentMetadata
    {
        [Required(ErrorMessage = "Agent Username is required")]
        [Display(Name = "Agent Username")]
        public string agentUsername { get; set; }
        [Required(ErrorMessage = "Agent Password is required")]
        [Display(Name = "Agent Password")]
        public string agentPassword { get; set; }
        [Display(Name = "Agent Email")]
        public string agentEmail { get; set; }
        [Display(Name = "Agent Name")]
        public string agentName { get; set; }
        [Display(Name = "Agent IC")]
        public string agentIC { get; set; }
        [Display(Name = "Gender")]
        public Nullable<bool> agentGender { get; set; }
    }

    public class scheduleMetadata
    {
        
        public int scheduleID { get; set; }
        [Required(ErrorMessage = "Departure is required")]
        [Display(Name = "Departure")]
        public string scheduleDeparture { get; set; }
        [Display(Name = "Departure Date")]
        [DisplayFormat(DataFormatString= "{0:dd/MM/yyyy}", ApplyFormatInEditMode= true)]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Departure Date is required")]
        public System.DateTime scheduleDepartureDate { get; set; }
        [Display(Name = "Departure Time")]
        [DisplayFormat(DataFormatString = "{0:t}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "Departure Time is required")]
        public System.TimeSpan scheduleDepartureTime { get; set; }
        [Required(ErrorMessage = "Arrival is required")]
        [Display(Name = "Arrival")]
        public string scheduleArrival { get; set; }
        [Display(Name = "Arrival Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Arrival Date is required")]
        public System.DateTime scheduleArrivalDate { get; set; }
        [Display(Name = "Arrival Time")]
        [DisplayFormat(DataFormatString = "{0:t}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "Arrival Time is required")]
        public System.TimeSpan scheduleArrivalTime { get; set; }
        [Display(Name = "Ship ID")]
        public int shipID { get; set; }
        public int adminID { get; set; }
    }


    public class customerMetadata
    {
        [Display(Name = "Customer ID")]
        public int customerID { get; set; }

        [Display(Name = "Customer Email")]
        [Required(ErrorMessage = "Email is required")]
        public string customerEmail { get; set; }

        [Display(Name = "Customer Name")]
        [Required(ErrorMessage = "Name is required")]
        public string customerName { get; set; }

        [Display(Name = "Customer IC")]
        [Required(ErrorMessage = "IC is required")]
        public string customerIC { get; set; }

        [Display(Name = "Customer Gender")]
        [Required(ErrorMessage = "Gender is required")]
        public Nullable<bool> customerGender { get; set; }

        [Display(Name = "Customer PhoneNo")]
        [Required(ErrorMessage = "PhoneNo is required")]
        public string customerPhoneNo { get; set; }

        [Display(Name = "Customer Address")]
        [Required(ErrorMessage = "Address is required")]
        public string customerAddress { get; set; }

        [Display(Name = "Customer Address Country")]
        [Required(ErrorMessage = "Address Country is required")]
        public string customerAddressCountry { get; set; }

        [Display(Name = "Agent ID")]
        public int agentID { get; set; }
    }

    public class scheduleBookingMetadata
    {
        [Display(Name = "Schedule Booking ID")]
        public int scheduleBookingID { get; set; }
        [Display(Name = "Schedule ID")]
        public int scheduleID { get; set; }
        [Display(Name = "Agent ID")]
        public int agentID { get; set; }
        [Display(Name = "Status")]
        public string status { get; set; }
        [Display(Name = "Required TEU")]
        public decimal requiredTEU { get; set; }
    }

    public class deliveryInvoicesMetadata
    {
        [Display(Name = "Invoice ID")]
        public int deliveryInvoiceID { get; set; }
        [Display(Name = "Schedule Booking ID")]
        public int scheduleBookingID { get; set; }
        [Display(Name = "Agent ID")]
        public int agentID { get; set; }
        [Display(Name = "Customer ID")]
        public int customerID { get; set; }
        [Display(Name = "Invoice TEU")]
        public decimal invoiceTEU { get; set; }
    }

    public class itemMetadata
    {
        [Display(Name = "Item ID")]
        public int itemID { get; set; }
        [Display(Name = "Item Name")]
        public string itemName { get; set; }
        [Display(Name = "Item Category")]
        public string itemCategory { get; set; }
        [Display(Name = "Item Volumn")]
        public decimal itemVolume { get; set; }
        [Display(Name = "Item Mass")]
        public decimal itemMass { get; set; }
        [Display(Name = "Delivery Invoice ID")]
        public int deliveryInvoiceID { get; set; }
    }
}