namespace VendingMachineLib.Products
{
    public class Toy : Product
    {
        public Toy(string productName, int price) : base(productName, price)
        {
        }

        public override string Use()
        {
            return "You can play with this!";
        }
    }
}