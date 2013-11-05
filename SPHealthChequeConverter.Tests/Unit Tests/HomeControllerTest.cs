using System;
using System.Web.Mvc;
using FakeItEasy;
using NUnit.Framework;
using SPHealthChequeConverter.Controllers;
using SPHealthChequeConverter.Helpers;
using SPHealthChequeConverter.Models;

namespace SPHealthChequeConverter.Tests
{
    
    [TestFixture]
    public class HomeControllerTest
    {
        private IChequeValueConverter _checkValueConverterFake;
        private HomeController _homeController;
        private static DateTime _currentTime;

        const double Amount = 100.22;
        private const string Name = "Test Name";
        const string ConvertResult = "One Hundred";

        [SetUp]
        public void Setup()
        {
            _checkValueConverterFake = A.Fake<IChequeValueConverter>();
            _currentTime = DateTime.UtcNow;
            _homeController = new HomeController(_checkValueConverterFake);
        }

        [Test]
        public void IndexReturnsOwnView()
        {
            var view = _homeController.Index() as ViewResult;
            Assert.NotNull(view);
            Assert.AreEqual(view.ViewName, string.Empty);               
        }

        [Test]
        public void IndexReturnsEmptyModel()
        {
            var view = _homeController.Index() as ViewResult;
            Assert.NotNull(view);
            Assert.AreEqual(view.Model, null);
        }

        [Test]
        public void CreateReturnsIndexViewWhenModelInvalid()
        {            
            var view = _homeController.Create(InvalidModel()) as ViewResult;            
            Assert.NotNull(view);
            Assert.AreEqual(view.ViewName, "Index");
        }

        [Test]
        public void ConvertNotCalledWhenInvalidModel()
        {            
            _homeController.Create(InvalidModel());
            A.CallTo(() => _checkValueConverterFake.Convert(A<double>.Ignored)).MustNotHaveHappened();
        }

        [Test]
        public void CreateCallsConertOnAmount()
        {            
            _homeController.Create(ValidModel);
            A.CallTo(() => _checkValueConverterFake.Convert(Amount)).MustHaveHappened();
        }

        [Test]
        public void CreateReturnsOwnViewWhenValid()
        {
            var view = _homeController.Create(ValidModel) as ViewResult;
            Assert.NotNull(view);
            Assert.AreEqual(string.Empty,view.ViewName);            
        }

        [Test]
        public void CreateReturnsCorrectModel()
        {
            
            A.CallTo(() => _checkValueConverterFake.Convert(Amount)).Returns(ConvertResult);
            var view = _homeController.Create(ValidModel) as ViewResult;

            Assert.NotNull(view);
            var model = view.Model as ChequeDisplayDetails;
            Assert.NotNull(model);
            Assert.AreEqual(ConvertResult, model.AmountInWords);
            Assert.AreEqual(Amount, model.Amount);
            Assert.AreEqual(Name, model.Name);
            Assert.AreEqual(_currentTime, model.Date);            
        }

        private static ChequeSubmitDetails ValidModel
        {
            get
            {
                _currentTime = DateTime.UtcNow;
                var details = new ChequeSubmitDetails
                {
                    Name = Name,
                    Date = _currentTime,
                    Amount = Amount
                };
                return details;
            }
        }

        private ChequeSubmitDetails InvalidModel()
        {
            var details = new ChequeSubmitDetails
            {
                Name = ""
            };

            //Cause ModelState.IsValid to False     
            _homeController.ModelState.AddModelError("key", "model is invalid");
            return details;
        }
    }
}