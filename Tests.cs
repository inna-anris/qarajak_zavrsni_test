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
            this.NavigateTo("http://test5.qa.rs/");
            var wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(20));
            wait.Until(c => c.FindElement(By.XPath("//a[@href='/register']")));
            this.DoWait(1);
            IWebElement registerNew = this.FindElement(By.XPath("//a[@href='/register']"));
            registerNew.Click();
            this.DoWait(1);
            IWebElement ime = this.FindElement(By.Name("ime"));
            this.SendKeys("Ivana", false, ime);
            this.DoWait(1);
            IWebElement prezime = this.FindElement(By.Name("prezime"));
            this.SendKeys("Anris", false, prezime);
            this.DoWait(1);
            IWebElement email = this.FindElement(By.Name("email"));
            this.SendKeys("example@blabla.com", false, email);
            this.DoWait(1);
            IWebElement korisnicko = this.FindElement(By.Name("korisnicko"));
            this.SendKeys("anris", false, korisnicko);
            this.DoWait(1);
            IWebElement lozinka = this.FindElement(By.Id("password"));
            this.SendKeys("12345", false, lozinka);
            this.DoWait(1);
            IWebElement lozinkaPonovo = this.FindElement(By.Id("passwordAgain"));
            this.SendKeys("12345", false, lozinkaPonovo);
            this.DoWait(2);
            IWebElement registruj = this.FindElement(By.Name("register"));
            registruj.Click();
            this.DoWait(1);
            IWebElement uspeh = this.FindElement(By.XPath("//div[@class='alert alert-success']"));
            Assert.True(uspeh.Displayed);
            this.DoWait(3);
        }

        [Test]
        public void Zadatak2()
        {
            this.NavigateTo("http://test5.qa.rs/");
            IWebElement login = this.FindElement(By.XPath("//a[@href='/login']"));
            login.Click();
            this.DoWait(1);
            IWebElement korisnicko = this.FindElement(By.Name("username"));
            this.SendKeys("anris", false, korisnicko);
            this.DoWait(1);
            IWebElement lozinka = this.FindElement(By.Name("password"));
            this.SendKeys("12345", false, lozinka);
            this.DoWait(1);
            IWebElement uloguj = this.FindElement(By.Name("login"));
            uloguj.Click();
            this.DoWait(1);
            IWebElement uspeh = this.FindElement(By.XPath("//h2[contains(text(),'Welcome back, Ivana')]"));
            Assert.True(uspeh.Displayed);
            this.DoWait(3);
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
