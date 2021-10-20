namespace VendingMachineLib.Products
{
    public class Food : Product
    {
        public Food(string productName, int price , string info) : base(productName, price, info)
        {
        }

        public override string Examine()
        {
            return "You can eat this.";
        }

        public override string Use()
        {
            return $"You just ate {ProductName}";
        }
    }
}