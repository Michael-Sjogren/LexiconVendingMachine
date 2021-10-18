namespace VendingMachineLib
{
    public interface IVending
    {
        
        public bool SelectProduct(uint productId);
        void InsertMoney(int money);
        public uint[] CalculateChange(int price, uint[] moneyInserted);
        Product PurchaseProduct(uint productId);
        void EndTransaction();
        
    }
}