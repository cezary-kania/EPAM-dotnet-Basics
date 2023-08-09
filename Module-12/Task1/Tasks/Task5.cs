using System;
using System.Collections.Generic;
using System.Linq;
using Task1.DoNotChange;

namespace Task1.Tasks;

public class Task5
{
    /// <summary>
    /// Group the products by category, then by availability in stock with ordering by cost. 
    /// </summary>
    /// <param name="products"></param>
    public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
    {
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
}