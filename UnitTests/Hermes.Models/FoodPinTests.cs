//using Hermes.Models;
//using NUnit.Framework;

//namespace UnitTests.Hermes.ModelsTests
//{
//    [TestFixture]
//    public class FoodPinTests
//    {
//        private FoodAndWaterPin _foodPin;
//        string RName = "Food and Water";

//        [SetUp]
//        public void SetupBeforeEachTest() => _foodPin = new FoodAndWaterPin();

//        [Test]
//        public void PinConstructor1()
//        {
//            //Arrange

//            //Act
//            var Resource = _foodPin.Resource;
//            var Lat = _foodPin.Location.Latitude;
//            var Lon = _foodPin.Location.Longitude;
//            double Available = _foodPin.Availability;

//            //Assert
//            Assert.AreEqual(RName, Resource);
//            Assert.IsNull(Lat);
//            Assert.IsNull(Lon);
//        }

//        [Test]
//        public void PinConstructor2()
//        {
//            //Arrange
//            _foodPin = new FoodAndWaterPin(1.0, 1.0);

//            //Act
//            var Resource = _foodPin.Resource;
//            var Lat = _foodPin.Location.Latitude;
//            var Lon = _foodPin.Location.Longitude;
//            double Available = _foodPin.Availability;
//            var expectedLat = 1.0;
//            var expectedLon = 1.0;

//            //Assert
//            Assert.AreEqual(RName, Resource);
//            Assert.AreEqual(expectedLat, Lat);
//            Assert.AreEqual(expectedLon, Lon);
//            Assert.AreEqual(0.0, Available);
//        }

//    }
//}
