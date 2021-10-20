namespace VendingMachineLib.Products
{
    public class Drink : Product
    {
        public Drink(string productName, int price , string info) : base(productName, price, info)
        {
        }

        public override string Use()
        {
            return $"You took a sip from {ProductName}.";
        }
    }
}