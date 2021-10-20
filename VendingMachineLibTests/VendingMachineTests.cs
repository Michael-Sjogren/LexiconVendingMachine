using System;
using System.Linq;
using VendingMachineLib.Products;
using VendingMachineLib;

using Xunit;

namespace VendingMachineLibTests
{
    public class VendingMachineTests : IDisposable
    {
        private VendingMachine _vendor;

        public VendingMachineTests()
        {
            _vendor = new VendingMachine(CurrencyType.SEK, new int []{1,5,20,50,100,500,1000});
            _vendor.FillVendorWithProducts();
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
                () => new VendingMachine(CurrencyType.SEK, Array.Empty<int>())
            );
        }

        [Fact]
        public void Construct_BelowOrEqualZeroInDenominations_ThrowsException()
        {
            // array with zeroes
            Assert.Throws<ArgumentException>(
                () => new VendingMachine(CurrencyType.GBP, new int[]{0,1 , -1})
            );
        }
        
        [Fact]
        public void Construct_DuplicateDenominations_ReturnsUniqueValues()
        {
            var dupes = new int[] { 1  , 100 , 100 ,500 , 5 };
            var expected = new int[] {1, 5, 100, 500};
            _vendor = new VendingMachine(CurrencyType.SEK, dupes);
            var actual = _vendor.Denominations;
            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Purchase_AbsentProduct_ThrowsException()
        {
            _vendor.InsertMoney(500);
            Assert.Throws<ArgumentException>( () => _vendor.Purchase(-1));
            Assert.Throws<ArgumentException>( () => _vendor.Purchase(100));

        }
        
        [Fact]
        public void Purchase_Successfull_ReturnsProduct()
        {
            _vendor.InsertMoney(500);
            var product = new Drink("Socker Dryck", 20);
            const int expectedChangeSum = 500 - 20;
            _vendor.AddProduct(product);
            var productIndex = _vendor.GetProductIndexByName(product.ProductName);
            var actual = _vendor.Purchase(productIndex);
            var change = _vendor.EndTransaction();
            Assert.Equal(expectedChangeSum,change.Sum());
            Assert.Equal(product , actual);
            
        }

        [Fact]
        public void InsertMoney_AbsentInDenominations_ThrowsException()
        {
            _vendor = new VendingMachine(CurrencyType.SEK, new int []{1,5,20,50,100,500,1000});
            const int insertedValue = 25;
            Assert.Throws<ArgumentException>(() => _vendor.InsertMoney(insertedValue));
        }
        [Fact]
        public void CalculateChange_NegativeValues_ThrowsException()
        {
            const int price = 200;
            int[] insertedMoney = { -100 };
            Assert.Throws<ArgumentException>( (() => _vendor.CalculateChange(price, insertedMoney)));
        }
        
        [Fact]
        public void CalculateChange_InsertedIsLowerThanPrice_ThrowsException()
        {
            const int price = 200;
            int[] insertedMoney = { 100 };
            Assert.Throws<ArgumentException>( (() => _vendor.CalculateChange(price, insertedMoney)));
        }

        
        [Theory]
        [InlineData(3,new int[]{5},new int[]{1,1})]
        [InlineData(200,new int[]{500},new int[]{100,100,100})]
        [InlineData(1,new int[]{5},new int[]{1,1,1,1})]
        public void CalculateChange_ReturnsCorrectAmount(int price, int[] insertedMoney, int[] expected)
        {
            var actual = _vendor.CalculateChange(price, insertedMoney);
            Assert.Equal(expected,actual);
        }
    }
}