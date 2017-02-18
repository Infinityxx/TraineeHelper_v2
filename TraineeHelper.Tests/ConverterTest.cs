using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TraineeHelper.Logic.Converters;
using TraineeHelper.ViewModels;
using TraineeHelper.Models;

namespace TraineeHelper.Tests
{
    [TestClass]
    public class ConverterTest
    {
        [TestMethod]
        public void TestUserConverter()
        {
            UserContext source = new UserContext { Id = "1", Email = "a@gmail.com", Password = "123456", UserName = "abc" };
            User result = UserConverter.ConvertToUser(source, true);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Password, source.Password);

            source = new UserContext { };
            result = UserConverter.ConvertToUser(source, true);

            Assert.IsNotNull(result);
            //Assert.AreEqual(result.Password, source.Password);

        }
    }
}
