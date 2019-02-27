using Hermes.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class ItemTests
    {
        [TestMethod]
        public void ItemConstructor()
        {
            //Arrange
            Item i = new Item { Id ="Ball", Text = "Text", Description = "Red" };
            //Act

            //Assert
            Assert.AreEqual(i.Id, "Ball");
            Assert.AreEqual(i.Text, "Text");
            Assert.AreEqual(i.Description, "Red");
        }
    }
}
