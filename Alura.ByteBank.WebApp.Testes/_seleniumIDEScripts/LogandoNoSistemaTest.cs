/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using Xunit;
public class SuiteTests : IDisposable
{
    public IWebDriver driver { get; private set; }
    public IDictionary<String, Object> vars { get; private set; }
    public IJavaScriptExecutor js { get; private set; }
    public SuiteTests()
    {
        driver = new ChromeDriver();
        js = (IJavaScriptExecutor)driver;
        vars = new Dictionary<String, Object>();
    }
    public void Dispose()
    {
        driver.Quit();
    }
    [Fact]
    public void LogandoNoSistema()
    {
        driver.Navigate().GoToUrl("https://localhost:5001/");
        driver.Manage().Window.Size = new System.Drawing.Size(1382, 736);
        driver.FindElement(By.LinkText("Login")).Click();
        driver.FindElement(By.Id("Email")).Click();
        driver.FindElement(By.Id("Email")).SendKeys("andre@email.com");
        driver.FindElement(By.Id("Senha")).Click();
        driver.FindElement(By.Id("Senha")).SendKeys("senha01");
        driver.FindElement(By.Id("btn-logar")).Click();
        driver.FindElement(By.CssSelector(".btn")).Click();
        driver.Close();
    }
}
*/