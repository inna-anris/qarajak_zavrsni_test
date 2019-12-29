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
            // Na primer cekamo register dugme za svaki slucaj
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

        [Test]
        public void Zadatak3()
        {
            // Uloguj se
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

            // Ako login nije uspesan nema svrhe nastaviti sa kupovinom
            IWebElement uspehLogin = this.FindElement(By.XPath("//h2[contains(text(),'Welcome back, Ivana')]"));
            Assert.True(uspehLogin.Displayed);
            this.DoWait(1);

            // 3x PRO u korpu
            IWebElement kolicina = this.FindElement(By.XPath("//h3[contains(text(),'pro')]/parent::div/following-sibling::div[1]//select"));
            var select = new SelectElement(kolicina);
            select.SelectByValue("3");
            this.DoWait(1);
            IWebElement order = this.FindElement(By.XPath("//h3[contains(text(),'pro')]/parent::div/following-sibling::div[1]//input[@type='submit']"));
            order.Click();

            // Nastavi kupovinu
            IWebElement nastavi = this.FindElement(By.XPath("//a[contains(text(), 'Continue shopping')]"));
            nastavi.Click();

            // 4x ENTERPRISE u korpu
            kolicina = this.FindElement(By.XPath("//h3[contains(text(),'enterprise')]/parent::div/following-sibling::div[1]//select"));
            select = new SelectElement(kolicina);
            select.SelectByValue("4");
            this.DoWait(1);
            order = this.FindElement(By.XPath("//h3[contains(text(),'enterprise')]/parent::div/following-sibling::div[1]//input[@type='submit']"));
            order.Click();

            // Nastavi kupovinu
            nastavi = this.FindElement(By.XPath("//a[contains(text(), 'Continue shopping')]"));
            nastavi.Click();

            // 9x STARTER u korpu
            kolicina = this.FindElement(By.XPath("//h3[contains(text(),'starter')]/parent::div/following-sibling::div[1]//select"));
            select = new SelectElement(kolicina);
            select.SelectByValue("9");
            this.DoWait(1);
            order = this.FindElement(By.XPath("//h3[contains(text(),'starter')]/parent::div/following-sibling::div[1]//input[@type='submit']"));
            order.Click();

            // Procitaj ocekivanu cenu
            int ocekivanaCena = Convert.ToInt32(this.FindElement(By.XPath("(//table//td)[last()]")).Text.Substring(8));

            // Checkout
            IWebElement checkout = this.FindElement(By.Name("checkout"));
            checkout.Click();

            // Verifikuj uspeh
            IWebElement uspehKupovina = this.FindElement(By.XPath("//h2[contains(text(), 'You have successfully placed your order')]"));
            Assert.True(uspehKupovina.Displayed);
            this.DoWait(1);

            // Verifikuj naplatu s kartice
            IWebElement uspehKartica = this.FindElement(By.XPath("//h3[contains(text(), 'Your credit card has been charged with the amount of')]"));
            Assert.True(uspehKartica.Displayed);
            this.DoWait(1);

            // Vrifikuj cenu
            var cena = uspehKartica.Text.Trim().Substring(54);
            int prikazanaCena = Convert.ToInt32(cena);
            Assert.AreEqual(ocekivanaCena, prikazanaCena);

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
