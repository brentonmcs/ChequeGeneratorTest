using System;
using System.Configuration;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using TechTalk.SpecFlow;

namespace SPHealthChequeConverter.Tests.SpecFlow
{
    [Binding]
    
    public class ChequeCreateSteps
    {
        private static IWebDriver _browser;
            
        [BeforeFeature]
        public static void BeforeFeautre()
        {
            _browser = new ChromeDriver();            
        }

        [Given(@"I am on ""(.*)"" page")]
        public void GivenIAmOnPage(string relativeUrl)
        {
            var rootUrl = new Uri(ConfigurationManager.AppSettings["RootUrl"]);
            var absoluteUrl = new Uri(rootUrl, relativeUrl);
            _browser.Navigate().GoToUrl(absoluteUrl);
        }

        [Given(@"I enter ""(.*)"" in ""(.*)""")]
        public void GivenIEnterIn(string text, string elementId)
        {
            _browser.FindElement(By.Id(elementId)).SendKeys(text);
        }
    
        
        [When(@"I Navigate To ""(.*)""")]
        public void WhenINavigateTo(string relativeUrl)
        {
            var rootUrl = new Uri(ConfigurationManager.AppSettings["RootUrl"]);
            var absoluteUrl = new Uri(rootUrl, relativeUrl);
            _browser.Navigate().GoToUrl(absoluteUrl);
        }

        [When(@"I Press ""(.*)""")]
        public void WhenIPress(string elementId)
        {
            IWebElement createButton = _browser.FindElement(By.Id(elementId));

            
                _browser.FindElement(By.CssSelector("h1")).Click();
            createButton.Click();
        }

        [Then(@"""(.*)"" field should be available")]
        public void ThenFieldShouldBeAvailable(string elementId)
        {
            Assert.NotNull(_browser.FindElement(By.Id(elementId)));
        }
        
        [Then(@"""(.*)"" Required Message Appears")]
        public void ThenRequiredMessageAppears(string p0)
        {
            var elements = _browser.FindElements(By.ClassName("field-validation-error"));
            Assert.NotNull(elements);
            var element = elements.First(x => x.GetAttribute("data-valmsg-for") == p0);
            Assert.NotNull(element);

            var span = element.FindElement(By.CssSelector("span"));
            Assert.NotNull(span);
            Assert.IsFalse(string.IsNullOrEmpty(span.Text));                                    
        }
       
        [Then(@"""(.*)"" Required Message Does Not Appear")]
        public void ThenRequiredMessageDoesNotAppear(string p0)
        {
            var elements = _browser.FindElements(By.ClassName("field-validation-error"));
            Assert.NotNull(elements);
            var element = elements.FirstOrDefault(x => x.GetAttribute("data-valmsg-for") == p0);
            Assert.IsNull(element);            
        }

        [Then(@"I am on ""(.*)"" page")]
        public void ThenIAmOnPage(string pageName)
        {
            Assert.IsTrue(_browser.Url.Contains(pageName));            
        }

        [Then(@"Date Fields Are Correct")]
        public void ThenDateFieldsAreCorrect()
        {
            var day1 = _browser.FindElement(By.Id("dateDay1"));
            Assert.NotNull(day1);
            Assert.AreEqual("3", day1.Text);

            var day2 = _browser.FindElement(By.Id("dateDay2"));
            Assert.NotNull(day2);
            Assert.AreEqual("0", day2.Text);

            var month1 = _browser.FindElement(By.Id("dateMonth1"));
            Assert.NotNull(month1);
            Assert.AreEqual("0", month1.Text);

            var month2 = _browser.FindElement(By.Id("dateMonth2"));
            Assert.NotNull(month2);
            Assert.AreEqual("6", month2.Text);

            var year1 = _browser.FindElement(By.Id("dateYear1"));
            Assert.NotNull(year1);
            Assert.AreEqual("2", year1.Text);

            var year2 = _browser.FindElement(By.Id("dateYear2"));
            Assert.NotNull(year2);
            Assert.AreEqual("0", year2.Text);

            var year3 = _browser.FindElement(By.Id("dateYear3"));
            Assert.NotNull(year3);
            Assert.AreEqual("1", year3.Text);

            var year4 = _browser.FindElement(By.Id("dateYear4"));
            Assert.NotNull(year4);
            Assert.AreEqual("4", year4.Text);
        }

        [Then(@"Name is ""(.*)""")]
        public void ThenNameIs(string p0)
        {
            var name = _browser.FindElement(By.Id("name"));
            Assert.NotNull(name);
            Assert.AreEqual(p0, name.Text);
        }
        [Then(@"Amount is ""(.*)""")]
        public void ThenAmountIs(Decimal p0)
        {
            var amount = _browser.FindElement(By.Id("amount"));
            Assert.NotNull(amount);
            Assert.AreEqual(p0.ToString(), amount.Text);
        }

        [Then(@"Amount In Words is ""(.*)""")]
        public void ThenAmountInWordsIs(string p0)
        {
            var amount = _browser.FindElement(By.Id("amounttext"));
            Assert.NotNull(amount);
            Assert.AreEqual(p0, amount.Text);                        
        }

        [AfterFeature]
        public static void TearDown()
        {
            _browser.Close();
            _browser.Dispose();
        }
    }
}
