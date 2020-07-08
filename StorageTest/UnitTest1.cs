using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StorageMagazine;

namespace StorageTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Quantity_should_return_true()
        {
            // Arrange
            
            bool expected = true;
            SharedSqlCommand sharedsqlcommand = new SharedSqlCommand();

            // Act
            
            bool actual =  sharedsqlcommand.QuantityValidation("10");

            // Assert
            
            Assert.AreEqual(expected, actual,  "Mimo poprawnej wartości zwraca false");
        }
        [TestMethod]
        public void Quantity_should_return_false()
        {
            // Arrange

            bool expected = false;
            SharedSqlCommand sharedsqlcommand = new SharedSqlCommand();

            // Act

            bool actual = sharedsqlcommand.QuantityValidation("test");

            // Assert

            Assert.AreEqual(expected, actual, "Mimo błędnej wartości zwraca true");
        }
        [TestMethod]
        public void Date_Time_should_return_true()
        {
            // Arrange

            bool expected = true;
            SharedSqlCommand sharedsqlcommand = new SharedSqlCommand();

            // Act

            bool actual = sharedsqlcommand.DateTimeValidation("20-09-2019");

            // Assert

            Assert.AreEqual(expected, actual, "Mimo poprawnej wartości zwraca false");
        }
        [TestMethod]
        public void Date_Time_should_return_false()
        {
            // Arrange

            bool expected = false;
            SharedSqlCommand sharedsqlcommand = new SharedSqlCommand();

            // Act

            bool actual = sharedsqlcommand.DateTimeValidation("test");

            // Assert

            Assert.AreEqual(expected, actual, "Mimo błędnej wartości zwraca true");
        }
    }
}
