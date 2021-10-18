namespace VendingMachineLib
{
    public static class ProductIdSequencer
    {
        private static uint _nextProductId = 0;
        public static uint GetNextProductId()
        {
            return _nextProductId++;
        }

        public static void Reset()
        {
            _nextProductId = 0;
        }
    }
}