using System;
using System.Collections.Generic;
using System.Linq;
using Task1.DoNotChange;

namespace Task1.Tasks;

public class Task7
{
    /// <summary>
    /// Calculate the average profitability of each city (average amount of orders per customer)
    /// and average rate (average number of orders per customer from each city). 
    /// </summary>
    /// <param name="customers"></param>
    public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
        IEnumerable<Customer> customers
    )
    {
        ArgumentNullException.ThrowIfNull(customers, nameof(customers));
        return customers
            .GroupBy(customer => customer.City)
            .Select(cityGroup => (
                cityGroup.Key,
                (int) Math.Round(cityGroup.Average(customer => customer.Orders.Sum(order => order.Total))),
                (int) cityGroup.Average(customer => customer.Orders.Length)))
            .ToList();
    }
}