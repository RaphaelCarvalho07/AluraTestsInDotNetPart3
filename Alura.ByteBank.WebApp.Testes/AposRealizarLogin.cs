using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Alura.ByteBank.WebApp.Testes
{
    public class AposRealizarLogin
    {
        [Fact]
        public void AposRealizarLoginVerificaSeExisteOpcaoAgenciaMenu()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            driver.Navigate().GoToUrl("https://localhost:44309/UsuarioApps/Login");
            var login = driver.FindElement(By.Id("Email")); // Selecionar elementos no HTML
            var senha = driver.FindElement(By.Id("Senha")); // Selecionar elementos no HTML
            var btnLogar = driver.FindElement(By.Id("btn-logar")); // Selecionar elementos no HTML

            login.SendKeys("andre@email.com");
            senha.SendKeys("senha01");

            // Act - faz o login
            btnLogar.Click();

            // Assert
            Assert.Contains("Agência", driver.PageSource);
            driver.Close();
        }

        [Fact]
        public void TentaRealizarLoginSemPreencherCampos()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            driver.Navigate().GoToUrl("https://localhost:44309/UsuarioApps/Login");
            var login = driver.FindElement(By.Id("Email")); // Selecionar elementos no HTML
            var senha = driver.FindElement(By.Id("Senha")); // Selecionar elementos no HTML
            var btnLogar = driver.FindElement(By.Id("btn-logar")); // Selecionar elementos no HTML

            // login.SendKeys("andre@email.com");
            // senha.SendKeys("senha01");

            // Act - faz o login
            btnLogar.Click();

            // Assert
            Assert.Contains("The Email field is required.", driver.PageSource);
            Assert.Contains("The Senha field is required.", driver.PageSource);
            driver.Close();
        }

        [Fact]
        public void TentaRealiarLoginComSenhaInvalida()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            driver.Navigate().GoToUrl("https://localhost:44309/UsuarioApps/Login");
            var login = driver.FindElement(By.Id("Email")); // Selecionar elementos no HTML
            var senha = driver.FindElement(By.Id("Senha")); // Selecionar elementos no HTML
            var btnLogar = driver.FindElement(By.Id("btn-logar")); // Selecionar elementos no HTML

            login.SendKeys("andre@email.com");
            senha.SendKeys("senha010"); // Senha inválida

            // Act - faz o login
            btnLogar.Click();

            // Assert
            Assert.Contains("Login", driver.PageSource);
            driver.Close();
        }

        [Fact]
        public void TentaAcessarPaginaSemEstarLogado()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            driver.Navigate().GoToUrl("https://localhost:44309/Agencia/index");
            // Act

            // Assert
            Assert.Contains("401", driver.PageSource);
            driver.Close();
        }

        [Fact]
        public void AcessaPaginaSemEstarLogadoVerificaURL()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            // Act
            driver.Navigate().GoToUrl("https://localhost:44309/Agencia/index");

            // Assert
            Assert.Contains("https://localhost:44309/Agencia/index", driver.Url);
            Assert.Contains("401", driver.PageSource);
            driver.Close();
        }

        [Fact]
        public void TentaAcessarPaginaDeContaCorrenteSemEstarLogado()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            // Act
            driver.Navigate().GoToUrl("https://localhost:44309/ContaCorrentes/Index");

            // Assert
            Assert.Contains("https://localhost:44309/ContaCorrentes/Index", driver.Url);
            Assert.Contains("401", driver.PageSource);
            driver.Close();
        }
    }
}
