using System;
using System.Collections.Generic;
using System.Linq;
using Task1.DoNotChange;

namespace Task1.Tasks;

public class Task2
{
    /// <summary>
    ///  Select the clients, including the date of their first order. 
    /// </summary>
    /// <param name="customers"></param>
    /// <returns>Customer with date of first order</returns>
    public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
        IEnumerable<Customer> customers
    )
    {
        ArgumentNullException.ThrowIfNull(customers, nameof(customers));
        return customers.Where(customer => customer.Orders.Length > 0)
            .Select(customer => (customer, customer.Orders.Min(order => order.OrderDate)));
    }
}