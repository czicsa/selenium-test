using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTest.Tests
{
    [TestClass]
    public class LocalTest
    {
        [TestMethod]
        public void LocalInputTest()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--window-size=1920,1080");
            chromeOptions.AddArgument("--headless");
            var webDriver = new RemoteWebDriver(new Uri("http://localhost:4444/"), chromeOptions);
            webDriver.Navigate().GoToUrl("file:///C:/git/selenium-test/index.html");

            var inputText = "@éáőöüú<#&@{}>asd@";

            var element = webDriver.FindElement(By.XPath($"//*[@data-e2e-testing = 'problematic.input']"));
            element.SendKeys(inputText);

            var screenshot1 = (webDriver as ITakesScreenshot).GetScreenshot();
            screenshot1.SaveAsFile("C:/git/selenium-test/screenshot.png");

            var text = webDriver.FindElement(By.XPath($"//*[@data-e2e-testing = 'problematic.input']")).GetAttribute("value");
            Assert.AreEqual(inputText, text);
        }
    }
}
