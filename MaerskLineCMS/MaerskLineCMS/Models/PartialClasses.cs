using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaerskLineCMS.Models
{
    [MetadataType(typeof(adminMetadata))]
    public partial class admin
    {
    }

    [MetadataType(typeof(agentMetadata))]
    public partial class agent
    {
    }

    [MetadataType(typeof(scheduleMetadata))]
    public partial class schedule
    {
    }

    [MetadataType(typeof(customerMetadata))]
    public partial class customer
    {
    }

    [MetadataType(typeof(scheduleBookingMetadata))]
    public partial class scheduleBooking
    {
    }
    [MetadataType(typeof(deliveryInvoicesMetadata))]
    public partial class deliveryInvoice
    {
    }
    [MetadataType(typeof(itemMetadata))]
    public partial class item
    {
    }
}