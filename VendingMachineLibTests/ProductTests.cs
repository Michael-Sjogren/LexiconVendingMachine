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
            Assert.Throws<ArgumentException>(() => new Food("", 10 , ""));
        }
        
        [Fact]
        public void Construct_ProductWithEmptyInfo_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new Toy("Toy Car", 100 , ""));
        }
        
        [Fact]
        public void Construct_ProductWithInvalidPrice_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new Drink("Pepsi", -90 , "info"));
            Assert.Throws<ArgumentException>(() => new Food("Beef Jerky", 0 , "info"));

        }
    }
}