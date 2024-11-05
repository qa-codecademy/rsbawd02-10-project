using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lamazon.Domain.Entities;

public class Order : BaseEntity
{
    public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public bool IsActive { get; set; }
    public decimal TotalPrice { get; set; }

    // Shipping Details
    public string? ShippingUserFullName { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public string? PhoneNumber { get; set; }
}
