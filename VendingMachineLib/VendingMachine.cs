using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VendingMachineLib.Products;

namespace VendingMachineLib
{
    public class VendingMachine : IVending
    {
        private readonly int[] _denominations;
        private List<int> _insertedMoney;
        private CurrencyType _currency;
        private List<Product> _products;
        public int TotalMoneyInserted => _insertedMoney.Sum();
        public int[] Denominations => _denominations;
        public int[] Change { get; private set; }

        public CurrencyType Currency => _currency;
        
        public VendingMachine(CurrencyType currency, int[] denominations)
        {
            if (denominations == null || denominations.Length == 0 || denominations.Any((e) => e <= 0))
            {
                throw new ArgumentException("Denominations must have at least 1 element in the array. values must be above zero.");
            }
            
            _currency = currency;
            // remove duplicates
            _denominations = denominations.Distinct().ToArray();
            Array.Sort(_denominations);
            _products = new List<Product>();
            _insertedMoney = new List<int>();
        }
        public bool AddProduct(Product p)
        {
            if (_products.Exists((e) => e.ProductName.Equals(p.ProductName)))
            {
                return false;
            }
            _products.Add(p);
            return false;
        }
        
        public void FillVendorWithProducts()
        {
            _products = new List<Product>()
            {
                new Drink("Coca Cola Zero",10),
                new Drink("Fanta",10),
                new Drink("Sprite",10),
                new Food("Sandwich",25),
                new Food("Apple",12),
                new Food("Berries",12),
                new Toy("Fidget Spinner",200),
                new Toy("JoJo",100),
                new Toy("Remote Controlled Toy Car", 500)
            };
            
            _products = _products.OrderBy( (e) => e.Price ).ToList();
        }

        public int GetProductIndexByName(string productName)
        {
            for (var i = 0; i < _products.Count; i++)
            {
                var product = _products[i];
                if (product.ProductName.Equals(productName)) return i;
            }
            return -1;
        }
        
        /**
         * Returns array of change to simulate you receiving bills and coins.
         * 
         * if you have inserted 500 SEK and you buy something for 100 SEK
         * you will receive 4x 100 bills as change: int[]{100,100,100,100}.
         *
         */
        public int[] CalculateChange(int price, int[] moneyInserted)
        {
            // no money inserted
            if (moneyInserted == null || moneyInserted.Length == 0)
            {
                throw new ArgumentException("No money inserted.");
            }
            var totalInserted = moneyInserted.Aggregate((x,y) => x+y);
            // cant afford
            if (totalInserted < price)
            {
                throw new ArgumentException("You do not have enough money.");
            }
            
            var change = new List<int>();
            var i = _denominations.Length - 1;
            
            var difference = Math.Abs(price - totalInserted);
            while (difference > 0)
            {
                if (difference < _denominations[i])
                {
                    i--;
                    continue;
                }
         
                if (i <= 0) i = 0;
                
                change.Add(_denominations[i]);
                difference -= _denominations[i];
            }
            change.Sort();
            return change.ToArray();
        }

 
        // purchase selected product
        public Product Purchase(int productIndex)
        {
            // index out of range
            if (productIndex < 0 || productIndex >= _products.Count)
            {
                throw new ArgumentException("The selected product does not exist.");
            }

            var product = _products[productIndex];
            // can't afford
            if (TotalMoneyInserted < product.Price)
            {
                throw new ApplicationException("You do not have enough money inserted to purchase this item.");
            }
            Change = CalculateChange(product.Price, _insertedMoney.ToArray());
            return product;
        }
        
        // returns a string that displays all products and their price
        public string ShowAllProducts()
        {
            var builder = new StringBuilder();
            for (var i = 0; i < _products.Count; i++)
            {
                var p = _products[i];
                builder.Append($"{i,-3}");
                builder.Append($"{p.ProductName,-35}");
                builder.Append($"{p.Price,5}");
                builder.Append($"{_currency,4}");
                builder.AppendLine();
            }
            return builder.ToString();
        }
        
        public bool InsertMoney(int money)
        {
            if (money <= 0)
            {
                throw new ArgumentException("Money cant be zero or less than zero.");
            }
            if (!Denominations.Contains(money))
            {
                throw new ArgumentException("Invalid denomination value entered.");
            }
            _insertedMoney.Add(money);
            return true;
        }
        
        public int[] EndTransaction()
        {
            var change = Change;
            Change = Array.Empty<int>();
            _insertedMoney.Clear();
            return change;
        }

    }
}