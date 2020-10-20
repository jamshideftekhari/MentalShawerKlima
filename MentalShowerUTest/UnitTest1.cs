using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApiMentalShowerIndoor;
using ApiMentalShowerIndoor.Controllers;
using System.Collections.Generic;
using ModelLib.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MentalShowerUTest
{
    /// <summary>
    /// Ikke relevant UnitTest Eftersom der ikke længere bruges statisk lister
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        private IndoorClimateController _indoorClimateController;
        private ActionResult<SensorDataModel> sensorTest;

        [TestInitialize]
        public void BeforeTest()
        {
            DbContextOptions<FanContext> options = new DbContextOptionsBuilder<FanContext>()
                .UseInMemoryDatabase(databaseName: "FanList")
                .Options;
            _indoorClimateController = new IndoorClimateController(new FanContext(options)); 
        }

        [TestMethod]
        public async Task TestGetAllMethod()
        {
            Console.WriteLine(_indoorClimateController.ToString());
            sensorTest = await _indoorClimateController.PostSensorDataModel(new SensorDataModel{ SensorID = 5, RoomID = "d-9", Temperature = 29.9f, Humidity = 55, CO2 = 55, Pressure = 55 });
            Assert.AreEqual(5, sensorTest.Value.SensorID);
            sensorTest = await _indoorClimateController.PostSensorDataModel(new SensorDataModel{ SensorID = 4, RoomID = "d-8", Temperature = 12.7f, Humidity = 50, CO2 = 50, Pressure = 50 });
            Assert.AreEqual(4, sensorTest.Value.SensorID);
            sensorTest = await _indoorClimateController.PostSensorDataModel(new SensorDataModel{ SensorID = 3, RoomID = "d-7", Temperature = 15.4f, Humidity = 45, CO2 = 45, Pressure = 45 });
            Assert.AreEqual(3, sensorTest.Value.SensorID);
            int actual = (await _indoorClimateController.Get()).Value.Count();
            int expected = 3;
            Assert.AreEqual(expected, actual);
            await _indoorClimateController.DeleteSensorDataModel(5);
            await _indoorClimateController.DeleteSensorDataModel(4);
            await _indoorClimateController.DeleteSensorDataModel(3);
            actual = (await _indoorClimateController.Get()).Value.Count();
            expected = 0;
            Assert.AreEqual(expected, actual);
            await Task.Delay(1);
        }
    }
}
