namespace VendingMachineLib
{
    public abstract class Product
    {
        private readonly uint _prodcutId;
        public uint ProductId => _prodcutId;
        public string ProductName { get; set; }
        public string ProductInfo { get; set; }
        public int Price { get; set; }

        protected Product(string productName, int price)
        {
            _prodcutId = ProductIdSequencer.GetNextProductId();
            ProductName = productName;
            Price = price;
        }
        
        public virtual string Examine()
        {
            return $"Name: {ProductName}";
        }

        public abstract string Use();
    }
}