using System;

namespace VendingMachineLib.Products
{
    public abstract class Product
    {
        private int _price;
        private string _productName;
        public string ProductName
        {
            get => _productName;
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentException("Product name cannot be null or empty.");
                _productName = value;
            }
        }

        public string ProductInfo { get; set; }
        public int Price 
        { 
            get => _price;
            set
            {
                if (value <= 0) throw new ArgumentException("Price must have a value above Zero.");
                _price = value;
            } 
        }

        protected Product(string productName, int price)
        {
            ProductName = productName;
            Price = price;
        }
        
        public virtual string Examine()
        {
            return $"Name: {ProductName} Price: {Price}";
        }

        public abstract string Use();
    }
}