using System;
using VendingMachineLib;
using Xunit;

namespace VendingMachineLibTests
{
    public class VendingMachineTests : IDisposable
    {
        private VendingMachine _vendor;

        public VendingMachineTests()
        {
            _vendor = new VendingMachine(CurrencyType.SEK, new uint []{1,5,20,50,100,500,1000});
        }
        public void Dispose() { }

        [Fact]
        public void Construct_NullDenominations_ThrowsException()
        {
            // null array
            Assert.Throws<ArgumentException>(
                () => new VendingMachine(CurrencyType.GBP, null)
            );
        }
        
        [Fact]
        public void Construct_EmptyDenominations_ThrowsException()
        {
            // empty array
            Assert.Throws<ArgumentException>(
                () => new VendingMachine(CurrencyType.SEK, Array.Empty<uint>())
            );
        }

        [Fact]
        public void Construct_ZeroesInDenominations_ThrowsException()
        {
            // array with zeroes
            Assert.Throws<ArgumentException>(
                () => new VendingMachine(CurrencyType.GBP, new uint[]{0,1})
            );
        }
        
        [Fact]
        public void Construct_DuplicateDenominations_ReturnsUniqueValues()
        {
            var dupes = new uint[] { 1 , 5 , 100 , 100 ,500 };
            var expected = new uint[] {1, 5, 100, 500};
            _vendor = new VendingMachine(CurrencyType.SEK, dupes);
            var actual = _vendor.Denominations;
            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PurchaseProduct_SuccessPurchase_ReturnsProduct()
        {
            var product = new Food("Noodles", 29);
            //var actual = _vendor.PurchaseProduct(product);

        }

        [Fact]
        public void InsertMoney_AbsentDenominationValue_ThrowsException()
        {
            const int insertedValue = 25;
            Action action = () => _vendor.InsertMoney(insertedValue);
            Assert.Throws<ArgumentException>(action);
        }
        [Theory]
        [InlineData(3,new uint[]{5},new uint[]{1,1})]
        [InlineData(200,new uint[]{500},new uint[]{100,100,100})]
        [InlineData(5,new uint[]{5},new uint[]{})]
        [InlineData(1,new uint[]{5},new uint[]{1,1,1,1})]
        [InlineData(1,new uint[]{},new uint[]{})]
        [InlineData(1,null,new uint[]{})]
        public void CalculateChange_ReturnsCorrectAmount(int price, uint[] insertedMoney, uint[] expected)
        {
            var actual = _vendor.CalculateChange(price, insertedMoney);
            Assert.Equal(expected,actual);
        }
    }
}