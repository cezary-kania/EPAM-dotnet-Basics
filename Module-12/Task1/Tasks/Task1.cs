using System;
using System.Collections.Generic;
using System.Linq;
using Task1.DoNotChange;

namespace Task1.Tasks;

public class Task1
{
    /// <summary>
    ///  Finds all customers with the sum of all orders that exceed a certain value. 
    /// </summary>
    /// <param name="customers"></param>
    /// <param name="limit"></param>
    /// <returns>Collection of customers</returns>
    public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
    {
        ArgumentNullException.ThrowIfNull(customers, nameof(customers));
        return limit < 0 ? customers : customers.Where(c => c.Orders.Sum(o => o.Total) > limit);
    }
}