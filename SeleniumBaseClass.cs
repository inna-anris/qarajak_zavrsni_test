using System;
using System.Collections.ObjectModel;
using System.Threading;
using OpenQA.Selenium;

namespace ZavrsniTestIvana
{
    class SeleniumBaseClass
    {
        IWebDriver driver;
        private uint _wait = 0;
        private IWebElement _element = null;

        /*
         * Class constructor
         */
        public SeleniumBaseClass()
        {
            //this._file = new FileManagement
            {
                //FileName = @"C:\Kurs\SeleniumLog.txt"
            };
        }

        /*
         * Getters and setters
         */
        public IWebDriver Driver
        {
            get { return this.driver; }
            set { this.driver = value; }
        }

        public uint Wait
        {
            get { return this._wait; }
            set
            {
                this._wait = value * 1000;
            }
        }

        /*
         * Public methods
         */
        public void Close()
        {
            this.driver.Close();
        }

        public void NavigateTo(string URL)
        {
            this.driver.Navigate().GoToUrl(URL);
            if (this._wait > 0)
                this.DoWait();
        }

        public void DoWait(uint TimeToWait = 0)
        {
            uint WaitFor = 0;
            if (TimeToWait > 0)
            {
                WaitFor = TimeToWait * 1000;
            }
            else
            {
                WaitFor = this._wait;
            }

            Thread.Sleep(Convert.ToInt32(WaitFor));
        }

        public void SendKeys(string KeysToSend, bool SendEnter = true, IWebElement Element = null)
        {
            IWebElement el = null;
            if (Element == null)
            {
                if (this._element == null)
                {
                    //this._file.Store($"SendKeys: Element not defined, can't send keys.");
                    throw new Exception("SendKeys: No element defined, can't send keys.");
                }
                else
                {
                    el = this._element;
                }
            }
            else
            {
                el = Element;
            }

            el.SendKeys(KeysToSend);
            //            if (this._wait > 0)
            //                this.DoWait();
            if (SendEnter)
                el.SendKeys(Keys.Enter);
        }

        public IWebElement FindElement(By Selector, IWebElement FindWithin = null)
        {
            IWebElement Element = null;
            try
            {
                if (FindWithin == null)
                {
                    Element = this.driver.FindElement(Selector);
                }
                else
                {
                    Element = FindWithin.FindElement(Selector);
                }
                this._element = Element;
            }
            catch (NoSuchElementException)
            {
                //this._file.Store($"FindElement: Element [{Selector.ToString()}] not found");
            }
            catch (Exception e)
            {
                throw e;
            }
            return Element;
        }

        public ReadOnlyCollection<IWebElement> FindElements(By Selector, IWebElement FindWithin = null)
        {
            ReadOnlyCollection<IWebElement> Elements = null;
            try
            {
                if (FindWithin == null)
                {
                    Elements = this.driver.FindElements(Selector);
                }
                else
                {
                    Elements = FindWithin.FindElements(Selector);
                }

            }
            catch (NoSuchElementException)
            {
                //this._file.Store($"FindElements: Element [{Selector.ToString()}] not found");
            }
            catch (Exception e)
            {
                throw e;
            }
            return Elements;
        }

    }
}
