using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TestProject
{
    [TestFixture]
    public class Tests
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }
        [Test]
        public void Test_That_SpecialPrice_TwoTimes_less_Subtotal()
        {
            driver.Url = "https://www.1stopbedrooms.com/";
            driver.FindElement(By.Id("search")).SendKeys("99TBM96CSSVK");  
            driver.FindElement(By.CssSelector("#header-search button[type='submit']")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("product-price-171156")));
            string specialPrice = driver.FindElement(By.Id("product-price-171156")).Text;
           
            driver.FindElement(By.CssSelector("div.add-to-cart span.qty-plus")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("button[onclick=\"productAddToCartForm.submit(this)\"]")).Click();
            string subtotal = driver.FindElement(By.CssSelector("#shopping-cart-table > thead > tr > th > div > div.right > div > span")).Text;
           
            Assert.AreEqual(Converter(subtotal),Converter(specialPrice)*2);
        }

        public int Converter(string s)
        {
            string s1 = s.Replace(",", "");
            string s2 = s1.Replace(".", "");
            string s3 = s2.Replace("$", "");
            int spp = Int32.Parse(s3);
            return spp;
        }

        [TearDown]
        public void CloseApp()
        {
            driver.Quit();
            driver = null;
        }
    }
}
