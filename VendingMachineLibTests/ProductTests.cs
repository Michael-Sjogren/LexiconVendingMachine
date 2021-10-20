using System;
using VendingMachineLib.Products;
using Xunit;

namespace VendingMachineLibTests
{
    public class ProductTests
    {
        [Fact]
        public void Construct_ProductWithEmptyName_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new Food("", 10));
        }
        
        [Fact]
        public void Construct_ProductWithInvalidPrice_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new Drink("Pepsi", -90));
            Assert.Throws<ArgumentException>(() => new Food("Beef Jerky", 0));

        }
    }
}