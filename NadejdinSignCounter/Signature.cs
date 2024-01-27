using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NadejdinSignCounter
{
    internal class Signature
    {
        public string region;
        public int signCount;

        public Signature(string region, int signCount)
        {
            this.region = region;
            this.signCount = signCount;
        }
    }

    public class SignatureParser
    {

        protected IWebDriver? webDriver;
        protected IWebDriver GetWebDriver()
        {
            if (webDriver == null)
            {
                ChromeOptions chromeOptions = new ChromeOptions();
                chromeOptions.AddArgument("--disable-infobars");
                chromeOptions.AddArgument("--start-maximized");
                chromeOptions.AddArgument("--ignore-certificate-errors");
                //chromeOptions.AddArgument("--headless");
                chromeOptions.AddArgument("--incognito");

                #if Linux
                    webDriver = new ChromeDriver("/usr/bin/", chromeOptions);
                #elif Windows
                    webDriver = new ChromeDriver(chromeOptions);
                #endif

                webDriver.Manage().Cookies.DeleteAllCookies();
            }
            return webDriver;
        }

        internal List<Signature> Collector()
        {
            GetWebDriver().Url = "https://nadezhdin2024.ru/signatures";

            List<Signature> signaturesList = new List<Signature>();

            while (true)
            {
                try
                {
                    string regions = GetWebDriver().FindElement(By.XPath($"/html/body/section[1]/div/div[{signaturesList.Count + 1}]/div[1]/h3")).GetAttribute("textContent");
                    string signatures = GetWebDriver().FindElement(By.XPath($"/html/body/section[1]/div/div[{signaturesList.Count + 1}]/div[2]/div/div[2]/div/span")).GetAttribute("textContent");

                    signatures = Regex.Match(signatures, "[0-9]+").Value;

                    signaturesList.Add(new Signature(regions, int.Parse(signatures)));
                }
                catch
                {
                    break;
                }
            }
            GetWebDriver().Close();

            return signaturesList;
        }
    }
}
