using System;
using System.Collections.Generic;
using System.Linq;
using Task1.DoNotChange;

namespace Task1.Tasks;

public class Task3
{
    /// <summary>
    /// Repeat the previous query (Select the clients, including the date of their first order)
    /// but order the result by year, month, turnover (descending) and customer name. 
    /// </summary>
    /// <param name="customers"></param>
    public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
        IEnumerable<Customer> customers
    )
    {
        ArgumentNullException.ThrowIfNull(customers, nameof(customers));
        return customers.Where(customer => customer.Orders.Length > 0)
            .Select(customer => (customer, customer.Orders.Min(order => order.OrderDate)))
            .OrderBy(customerWithOrder => customerWithOrder.Item2.Year)
            .ThenBy(customerWithOrder => customerWithOrder.Item2.Month)
            .ThenByDescending(customerWithOrder => customerWithOrder.customer.Orders.Sum(order => order.Total))
            .ThenBy(customerWithOrder => customerWithOrder.customer.CompanyName)
            .ToList();
    }
}