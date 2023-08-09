using System;
using System.Collections.Generic;
using System.Linq;
using Task1.DoNotChange;

namespace Task1.Tasks;

public class Task8
{
    /// <summary>
    /// Build a string of unique supplier country names, sorted first by length and then by country.
    /// </summary>
    /// <param name="suppliers"></param>
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