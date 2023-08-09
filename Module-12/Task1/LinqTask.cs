using System;
using System.Collections.Generic;
using System.Linq;
using Task1.DoNotChange;

namespace Task1;

public static class LinqTask
{
    public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
    {
        ArgumentNullException.ThrowIfNull(customers, nameof(customers));
        return limit < 0 ? customers : customers.Where(c => c.Orders.Length > limit);
    }

    public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2(
        IEnumerable<Customer> customers,
        IEnumerable<Supplier> suppliers
    )
    {
        ArgumentNullException.ThrowIfNull(customers, nameof(customers));
        ArgumentNullException.ThrowIfNull(suppliers, nameof(suppliers));
        return customers.Select(customer => (customer, suppliers
            .Where(supplier => supplier.City.Equals(customer.City, StringComparison.OrdinalIgnoreCase)
                               && supplier.Country.Equals(customer.Country, StringComparison.OrdinalIgnoreCase))));
    }

    public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2UsingGroup(
        IEnumerable<Customer> customers,
        IEnumerable<Supplier> suppliers
    )
    {
        ArgumentNullException.ThrowIfNull(customers, nameof(customers));
        ArgumentNullException.ThrowIfNull(suppliers, nameof(suppliers));
        return customers.GroupJoin(suppliers,
            customer => new { customer.City, customer.Country },
            supplier => new { supplier.City, supplier.Country },
            (customer, supplierList) => (customer, supplierList));
    }

    public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
    {
        ArgumentNullException.ThrowIfNull(customers, nameof(customers));
        return customers.Where(c => c.Orders.Any(x => x.Total > limit));
    }

    public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
        IEnumerable<Customer> customers
    )
    {
        ArgumentNullException.ThrowIfNull(customers, nameof(customers));
        return customers.Where(customer => customer.Orders.Length > 0)
            .Select(customer => (customer, customer.Orders.Min(order => order.OrderDate)));
    }

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

    public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
    {
        ArgumentNullException.ThrowIfNull(customers, nameof(customers));
        return customers.Where(customer => !customer.PostalCode.All(char.IsDigit)
                                           || string.IsNullOrEmpty(customer.Region)
                                           || !(customer.Phone.Contains('(') && customer.Phone.Contains(')'))
        );
    }

    public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
    {
        /* example of Linq7result

         category - Beverages
            UnitsInStock - 39
                price - 18.0000
                price - 19.0000
            UnitsInStock - 17
                price - 18.0000
                price - 19.0000
         */
        ArgumentNullException.ThrowIfNull(products, nameof(products));
        return products.GroupBy(x => x.Category)
            .Select(categoryGroup => new Linq7CategoryGroup
            {
                Category = categoryGroup.Key,
                UnitsInStockGroup = categoryGroup.GroupBy(product => product.UnitsInStock)
                    .Select(stockGroup => new Linq7UnitsInStockGroup()
                    {
                        UnitsInStock = stockGroup.Key,
                        Prices = stockGroup
                            .OrderBy(product => product.UnitPrice)
                            .Select(product => product.UnitPrice)
                            .ToList()
                    })
                    .OrderByDescending(stockGroup => 
                        stockGroup.Prices.Select(p => p * stockGroup.UnitsInStock).Sum())
                    .ToList()
            })
            .ToList();
    }

    public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
        IEnumerable<Product> products,
        decimal cheap,
        decimal middle,
        decimal expensive
    )
    {
        ArgumentNullException.ThrowIfNull(products, nameof(products));
        return products.GroupBy(product => product.UnitPrice switch
            {
                var p when p <= cheap => cheap,
                var p when p <= middle => middle,
                _ => expensive
            })
            .Select(group => (group.Key, (IEnumerable<Product>) group));
    }

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

    public static string Linq10(IEnumerable<Supplier> suppliers)
    {
        ArgumentNullException.ThrowIfNull(suppliers, nameof(suppliers));
        var countries = suppliers
            .Select(supplier => supplier.Country)
            .Distinct()
            .OrderBy(country => country.Length)
            .ThenBy(country => country);
        return string.Concat(countries);
    }
}