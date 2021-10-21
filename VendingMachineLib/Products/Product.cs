using System;

namespace VendingMachineLib.Products
{
    public abstract class Product
    {
        private int _price;
        private string _productName;
        private string _info;
        public string Info
        {
            get => _info;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("This product has no info.");
                }

                _info = value;
            }
        }
        public string ProductName
        {
            get => _productName;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentException("Product name cannot be null or empty.");
                _productName = value;
            }
        }
        
        public int Price 
        { 
            get => _price;
            set
            {
                if (value <= 0) throw new ArgumentException("Price must have a value above Zero.");
                _price = value;
            } 
        }

        protected Product(string productName, int price , string info)
        {
            ProductName = productName;
            Price = price;
            Info = info;
        }
        
        public string Examine()
        {
            return $"Price: {Price} Info: {Info}";
        }

        public abstract string Use();
    }
}