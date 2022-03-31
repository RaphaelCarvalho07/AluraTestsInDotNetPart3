using Alura.ByteBank.WebApp.Testes.PageObjects;
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
using Xunit.Abstractions;

namespace Alura.ByteBank.WebApp.Testes
{
    public class AposRealizarLogin
    {
        private IWebDriver driver;
        public ITestOutputHelper SaidaConsoleTeste;

        public AposRealizarLogin(ITestOutputHelper _saidaConsoleTeste)
        {
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            SaidaConsoleTeste = _saidaConsoleTeste;
        }

        [Fact]
        public void AposRealizarLoginVerificaSeExisteOpcaoAgenciaMenu()
        {
            // Arrange
            var loginPO = new LoginPO(driver);
            loginPO.Navegar("https://localhost:44309/UsuarioApps/Login");

            // Act
            loginPO.PreencherCampos("andre@email.com", "senha01");
            loginPO.btnClick();

            // Assert
            Assert.Contains("Agência", driver.PageSource);
            driver.Close();
        }

        [Fact]
        public void TentaRealizarLoginSemPreencherCampos()
        {
            // Arrange
            var loginPO = new LoginPO(driver);
            loginPO.Navegar("https://localhost:44309/UsuarioApps/Login");

            // Act
            loginPO.PreencherCampos("", "");
            loginPO.btnClick();

            // Assert
            Assert.Contains("The Email field is required.", driver.PageSource);
            Assert.Contains("The Senha field is required.", driver.PageSource);
            driver.Close();
        }

        [Fact]
        public void TentaRealiarLoginComSenhaInvalida()
        {
            // Arrange
            var loginPO = new LoginPO(driver);
            loginPO.Navegar("https://localhost:44309/UsuarioApps/Login");

            // Act
            loginPO.PreencherCampos("andre@email.com", "senha010");
            loginPO.btnClick();

            // Assert
            Assert.Contains("Login", driver.PageSource);
            driver.Close();
        }

        [Fact]
        public void TentaAcessarPaginaSemEstarLogado()
        {
            // Arrange
            // IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

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
            // IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

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
            // IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            // Act
            driver.Navigate().GoToUrl("https://localhost:44309/ContaCorrentes/Index");

            // Assert
            Assert.Contains("https://localhost:44309/ContaCorrentes/Index", driver.Url);
            Assert.Contains("401", driver.PageSource);
            driver.Close();
        }

        [Fact]
        public void RealizarLoginAcessaMenuECadastraCliente()
        {
            // Arrange
            var loginPO = new LoginPO(driver);
            loginPO.Navegar("https://localhost:44309/UsuarioApps/Login");

            // Act
            loginPO.PreencherCampos("andre@email.com", "senha01");
            loginPO.btnClick();

            driver.FindElement(By.LinkText("Cliente")).Click();

            driver.FindElement(By.LinkText("Adicionar Cliente")).Click();

            driver.FindElement(By.Name("Identificador")).Click();
            driver.FindElement(By.Name("Identificador")).SendKeys("2df71922-ca7d-4d43-b142-0767b32f822a");
            driver.FindElement(By.Name("CPF")).Click();
            driver.FindElement(By.Name("CPF")).SendKeys("69981034096");
            driver.FindElement(By.Name("Nome")).Click();
            driver.FindElement(By.Name("Nome")).SendKeys("Aegon Tagaryen");
            driver.FindElement(By.Name("Profissao")).Click();
            driver.FindElement(By.Name("Profissao")).SendKeys("Cientista");

            // Act
            driver.FindElement(By.CssSelector(".btn-primary")).Click();
            driver.FindElement(By.LinkText("Home")).Click();

            // Assert
            Assert.Contains("Logout", driver.PageSource);
            driver.Close();
        }

        [Fact]
        public void RealizarLoginAcessaListagemDeContas()
        {
            // Arrange
            var loginPO = new LoginPO(driver);
            loginPO.Navegar("https://localhost:44309/UsuarioApps/Login");

            // Act
            loginPO.PreencherCampos("andre@email.com", "senha01");
            loginPO.btnClick();

            // Act - Conta Corrente
            driver.FindElement(By.Id("contacorrente")).Click();

            IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.TagName("a"));

            var elemento = (from webElemento in elements
                            where webElemento.Text.Contains("Detalhes")
                            select webElemento).First();

            elemento.Click();

            // Assert
            Assert.Contains("Voltar", driver.PageSource);
            driver.Close();
        }

        [Fact]
        public void RealizarLoginAcessaListagemDeContasUsandoHomePO()
        {
            // Arrange
            var LoginPO = new LoginPO(driver);
            LoginPO.Navegar("https://localhost:44309/UsuarioApps/Login");

            // Act
            LoginPO.PreencherCampos("andre@email.com", "senha01");
            LoginPO.btnClick();

            var homePO = new HomePO(driver);
            homePO.LinkContaCorrenteClick();

            // Assert
            Assert.Contains("Adicionar Conta-Corrente", driver.PageSource);
            driver.Close();
        }
    }
}
