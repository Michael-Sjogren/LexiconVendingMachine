namespace VendingMachineLib
{
    public class Drink : Product
    {
        public Drink(string productName, int price) : base(productName, price)
        {
        }

        public override string Use()
        {
            return "You can drink me!";
        }
    }
}