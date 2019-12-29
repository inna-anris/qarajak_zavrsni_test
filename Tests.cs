using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace ZavrsniTestIvana
{
    class Tests : SeleniumBaseClass
    {

        [Test]
        public void Zadatak1()
        {

        }

        [SetUp]
        public void SetUpTests()
        {
            this.Driver = new FirefoxDriver();
        }

        [TearDown]
        public void TearDownTests()
        {
            this.Close();
        }
    }
}
