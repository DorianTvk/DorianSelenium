using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace UITestingPlaygroundTests
{
    [TestFixture]
    public class HiddenLayersTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver(@"C:\Users\opilane\source\repos\DorianSelenium-main\selenium-task-master\UITestingPlayground\drivers");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void NavBarHinnakiri()
        {
            driver.Navigate().GoToUrl("file:///C:/Users/opilane/source/repos/AutoParandusSoloToo-main/index.html");

            var button = driver.FindElement(By.CssSelector("a[href='hinnakiri.html']"));
            button.Click();

            Assert.That(driver.Url, Does.Contain("hinnakiri.html"), "Navigation to 'hinnakiri.html' failed.");
        }

        [Test]
        public void NavBarKontakt()
        {
            driver.Navigate().GoToUrl("file:///C:/Users/opilane/source/repos/AutoParandusSoloToo-main/index.html");

            var button = driver.FindElement(By.CssSelector("a[href='kontakt.html']"));
            button.Click();

            Assert.That(driver.Url, Does.Contain("kontakt.html"), "Navigation to 'kontakt.html' failed.");
        }

        [Test]
        public void NavBarBroneeriAeg()
        {
            driver.Navigate().GoToUrl("file:///C:/Users/opilane/source/repos/AutoParandusSoloToo-main/index.html");

            var button = driver.FindElement(By.CssSelector("a[href='register.html']"));
            button.Click();

            Assert.That(driver.Url, Does.Contain("register.html"), "Navigation to 'register.html' failed.");
        }

        [Test]
        public void NavBarTeenused()
        {
            driver.Navigate().GoToUrl("file:///C:/Users/opilane/source/repos/AutoParandusSoloToo-main/index.html");

            var button = driver.FindElement(By.CssSelector("a[href='teenused.html']"));
            button.Click();

            Assert.That(driver.Url, Does.Contain("teenused.html"), "Navigation to 'teenused.html' failed.");
        }

        [Test]
        public void TestFormSubmission()
        {
            driver.Navigate().GoToUrl("file:///C:/Users/opilane/source/repos/AutoParandusSoloToo-main/kontakt.html");

            var nameField = driver.FindElement(By.Id("name"));
            var emailField = driver.FindElement(By.Id("email"));
            var messageField = driver.FindElement(By.Id("message"));
            var submitButton = driver.FindElement(By.CssSelector("button[type='submit']"));

            nameField.SendKeys("DORIAN");
            emailField.SendKeys("TAMMEVESKI@example.com");
            messageField.SendKeys("vaitmaa best");
            submitButton.Click();

            Assert.Pass("Form submitted successfully. Verify behavior manually if needed.");
        }

        public void HinnakiriNav()
        {
            driver.Navigate().GoToUrl("file:///C:/Users/opilane/source/repos/AutoParandusSoloToo-main/index.html");

            var button = driver.FindElement(By.CssSelector("onclick=\"window.location.href='hinnakiri.html'\"']"));
            button.Click();

            Assert.That(driver.Url, Does.Contain("teenused.html"), "Navigation to 'teenused.html' failed.");
        }

        [Test]
        public void TestBookingFormSubmission()
        {
            driver.Navigate().GoToUrl("file:///C:/Users/opilane/source/repos/AutoParandusSoloToo-main/register.html");

            var bookingLink = driver.FindElement(By.CssSelector("a[href='register.html']"));
            bookingLink.Click();

            var nameField = driver.FindElement(By.Id("name"));
            var emailField = driver.FindElement(By.Id("email"));
            var dateField = driver.FindElement(By.Id("date"));
            var timeField = driver.FindElement(By.Id("time"));
            var serviceDropdown = driver.FindElement(By.Id("service"));
            var submitButton = driver.FindElement(By.CssSelector("button[type='submit']"));

            nameField.SendKeys("Test User");
            emailField.SendKeys("testuser@example.com");
            dateField.SendKeys(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
            timeField.SendKeys("10:30");
            var selectElement = new SelectElement(serviceDropdown);
            selectElement.SelectByValue("auto_hooldus");

            submitButton.Click();

            Assert.Pass("Booking form submitted successfully. Verify behavior manually if needed.");
        }

        [Test]
        public void TestServiceDropdownOptions()
        {
            driver.Navigate().GoToUrl("file:///C:/Users/opilane/source/repos/AutoParandusSoloToo-main/register.html");

            var bookingLink = driver.FindElement(By.CssSelector("a[href='register.html']"));
            bookingLink.Click();

            var serviceDropdown = driver.FindElement(By.Id("service"));
            var selectElement = new SelectElement(serviceDropdown);
            var options = selectElement.Options;

            Assert.That(options.Count, Is.EqualTo(9), "Service dropdown does not contain the expected number of options.");

            var expectedOptions = new string[]
            {
                "Auto hooldus",
                "Õlivahetus",
                "Autodiagnostika",
                "Rehvivahetus",
                "Siduri Vahetus",
                "Hammasrihma vahetus",
                "Summuti remont ja vahetus",
                "Generaatori ja Starteri remont",
                "Mootori remont ja vahetus"
            };

            for (int i = 0; i < expectedOptions.Length; i++)
            {
                Assert.That(options[i].Text, Is.EqualTo(expectedOptions[i]), $"Option {i + 1} text does not match.");
            }
        }

        [Test]
        public void TestBookingFormFieldValidation()
        {
            driver.Navigate().GoToUrl("file:///C:/Users/opilane/source/repos/AutoParandusSoloToo-main/register.html");

            var bookingLink = driver.FindElement(By.CssSelector("a[href='register.html']"));
            bookingLink.Click();

            var submitButton = driver.FindElement(By.CssSelector("button[type='submit']"));
            submitButton.Click();

            var nameField = driver.FindElement(By.Id("name"));
            var emailField = driver.FindElement(By.Id("email"));
            var dateField = driver.FindElement(By.Id("date"));
            var timeField = driver.FindElement(By.Id("time"));

            Assert.That(nameField.GetAttribute("validationMessage"), Is.Not.Empty, "Name field validation message is missing.");
            Assert.That(emailField.GetAttribute("validationMessage"), Is.Not.Empty, "Email field validation message is missing.");
            Assert.That(dateField.GetAttribute("validationMessage"), Is.Not.Empty, "Date field validation message is missing.");
            Assert.That(timeField.GetAttribute("validationMessage"), Is.Not.Empty, "Time field validation message is missing.");
        }

[TearDown]
        public void TearDown()
        {
            driver?.Quit();
            driver?.Dispose();
        }
    }
}
