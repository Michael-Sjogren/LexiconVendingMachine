using VendingMachineLib.Products;

namespace VendingMachineLib
{
    public interface IVending
    {
        public string ShowAllProducts();
        public bool InsertMoney(int money);
        public int[] CalculateChange(int price, int[] moneyInserted);
        public Product Purchase(int productIndex);
        public int[] EndTransaction();
        public bool AddProduct(Product p);
    }
}