namespace VendingMachineLib.Products
{
    public class Toy : Product
    {
        public Toy(string productName, int price , string info) : base(productName, price, info)
        {
        }

        public override string Use()
        {
            return "You are playing with this toy!";
        }
    }
}