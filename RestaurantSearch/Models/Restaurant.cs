using System;
namespace RestaurantSearch.Models;

public class Restaurant
{
    public int restaurantId { get; set; }
    public string RestaurantName { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string FoodType { get; set; }
    public string Phone { get; set; }
    public string LogoUrl { get; set; }

    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public object MenuItems { get; set; }
}

