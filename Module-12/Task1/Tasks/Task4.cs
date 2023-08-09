using System;
using System.Collections.Generic;
using System.Linq;
using Task1.DoNotChange;

namespace Task1.Tasks;

public class Task4
{
    /// <summary>
    /// Select the clients which either have:
    /// a. non-digit postal code
    /// b. undefined region
    /// c. operator code in the phone is not specified (does not contain parentheses) 
    /// </summary>
    /// <param name="customers"></param>
    public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
    {
        ArgumentNullException.ThrowIfNull(customers, nameof(customers));
        return customers.Where(customer => !customer.PostalCode.All(char.IsDigit)
                                           || string.IsNullOrEmpty(customer.Region)
                                           || !(customer.Phone.Contains('(') && customer.Phone.Contains(')'))
        );
    }
}