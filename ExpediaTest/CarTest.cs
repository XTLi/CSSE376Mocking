using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Expedia;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestClass]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[TestInitialize]
		public void TestInitialize()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[TestMethod]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [TestMethod]
        public void TestThatCarDoesGetCorrectCarLocation()
        {
            IDatabase mockDB = mocks.StrictMock<IDatabase>();
            string currentLocation = "Terre Haute";

            Expect.Call(mockDB.getCarLocation(376)).Return(currentLocation);

            mocks.ReplayAll();

            Car target = new Car(10);
            target.Database = mockDB;

            string result = target.getCarLocation(376);
            Assert.AreEqual(currentLocation, result);
            mocks.VerifyAll();
        }

        [TestMethod]
        public void TestThatCarDoesMileageFromDatabase()
        {
            IDatabase mockDB = mocks.StrictMock<IDatabase>();
            Int32 expectedMiles = 1000;

            Expect.Call(mockDB.Miles).PropertyBehavior();
            mocks.ReplayAll();

            mockDB.Miles = expectedMiles;
            Car target = new Car(10);
            target.Database = mockDB;
            Assert.AreEqual(expectedMiles, target.Mileage);
            mocks.VerifyAll();

        }

        //task 9 demonstrate object mother
        [TestMethod]
        public void TestThatCarDoesGetCorrectCarLocationUsingObjectMother()
        {
            IDatabase mockDB = mocks.StrictMock<IDatabase>();
            string currentLocation = "Chicago";

            Expect.Call(mockDB.getCarLocation(376)).Return(currentLocation);

            mocks.ReplayAll();

            var target = ObjectMother.BMW();
            target.Database = mockDB;
            string result = target.getCarLocation(376);
            Assert.AreEqual(currentLocation, result);
            mocks.VerifyAll();
        }
	}
}
