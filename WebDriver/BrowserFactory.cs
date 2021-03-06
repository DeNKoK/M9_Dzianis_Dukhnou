using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;

namespace M8_Dzianis_Dukhnou.WebDriver
{
    public class BrowserFactory
    {

        public static IWebDriver GetDriver(BrowserType type, int timeOutSec)
        {
            IWebDriver driver = null;

            switch (type)
            {
                case BrowserType.Chrome:
                    {
                        var service = ChromeDriverService.CreateDefaultService();
                        var options = new ChromeOptions();
                        driver = new ChromeDriver(service, options, TimeSpan.FromSeconds(timeOutSec));
                        break;
                    }
                case BrowserType.RemoteChrome:
                    {
                        var options = new ChromeOptions();
                        options.PlatformName = "windows";
                        options.AddArguments
                            (
                            "enable-automation",
                            "--no-sandbox",
                            "--disable-infobars",
                            "--disable-dev-shm-usage",
                            "--disable-browser-side-navigation",
                            "-disable-gpu",
                            "--ignore-certificate-errors"
                            );
                        driver = new RemoteWebDriver(new Uri(Configuration.RemoteNode), options.ToCapabilities());
                        break;
                    }
                case BrowserType.Firefox:
                    {
                        var service = FirefoxDriverService.CreateDefaultService();
                        var options = new FirefoxOptions();
                        driver = new FirefoxDriver(service, options, TimeSpan.FromSeconds(timeOutSec));
                        break;
                    }
                case BrowserType.RemoteFirefox:
                    {
                        var capability = new DesiredCapabilities();
                        capability.SetCapability(CapabilityType.BrowserName, BrowserType.Firefox.ToString().ToLower());
                        capability.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Any));
                        driver = new RemoteWebDriver(new Uri(Configuration.RemoteNode), capability);
                        break;
                    }
            }

            return driver;
        }
    }
}