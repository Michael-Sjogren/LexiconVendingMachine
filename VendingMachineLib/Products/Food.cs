namespace VendingMachineLib
{
    public class Food : Product
    {
        public Food(string productName, int price) : base(productName, price)
        {
        }

        public override string Use()
        {
            return "You can eat me!";
        }
    }
}