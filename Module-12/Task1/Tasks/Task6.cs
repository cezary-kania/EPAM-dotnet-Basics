using System;
using System.Collections.Generic;
using System.Linq;
using Task1.DoNotChange;

namespace Task1.Tasks;

public class Task6
{
    /// <summary>
    /// Group the products by “cheap”, “average” and “expensive” following the rules:
    /// a. From 0 to cheap inclusive
    /// b. From cheap exclusive to average inclusive
    /// c. From average exclusive to expensive inclusive 
    /// </summary>
    /// <param name="products"></param>
    /// <param name="cheap"></param>
    /// <param name="middle"></param>
    /// <param name="expensive"></param>
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
}