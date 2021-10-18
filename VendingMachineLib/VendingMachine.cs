using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachineLib
{
    public class VendingMachine : IVending
    {
        // uint because there is no negative values for currency.
        private readonly uint[] _denominations;
        private CurrencyType _currency;
        private List<Product> _products;

        public uint[] Denominations => _denominations;
        public CurrencyType Currency => _currency;
        
        public VendingMachine(CurrencyType currency, uint[] denominations)
        {
            if (denominations == null || denominations.Length == 0 || denominations.Contains(0U))
            {
                throw new ArgumentException("Denominations must have atleast 1 element in the array. values must be above zero.");
            }
            
            _currency = currency;
            // remove duplicates
            _denominations = denominations.Distinct().ToArray();
            _products = new List<Product>();
        }

        public uint[] CalculateChange(int price, uint[] moneyInserted)
        {
            if (moneyInserted == null || moneyInserted.Length == 0) return Array.Empty<uint>();
            
            var change = new List<uint>();
            var i = _denominations.Length - 1;
            var totalInserted = moneyInserted.Aggregate((x,y) => x+y);
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
                difference -= (int)_denominations[i];
            }
            change.Sort();
            return change.ToArray();
        }

        public Product PurchaseProduct(uint productId)
        {
            return null;
        }

        public bool SelectProduct(uint productId)
        {
            return false;
        }

        public void InsertMoney(int money)
        {
            if (!Denominations.Contains((uint)money))
            {
                throw new ArgumentException("Invalid denomination value entered.");
            }
            
        }

        public void EndTransaction()
        {
            
        }
    }
}