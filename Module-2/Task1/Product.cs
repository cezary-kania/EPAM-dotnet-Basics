﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace Task1
{
    public class Product : IEquatable<Product>
    {
        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; set; }

        public double Price { get; set; }

        public bool Equals(Product other)
        {
            if (other is null)
            {
                return false;
            }

            return Name == other.Name && Price == other.Price;
        }
    }
}
