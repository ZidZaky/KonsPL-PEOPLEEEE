using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UTS_PEOPLEEEE;

namespace TestProject
{
    [TestClass]
    public class UnitTests
    {
        private readonly string currencyFilePath = "currency_config.json";
        private readonly string operationalFilePath = "operational_config.json";

        [TestMethod]
        public void LoadCurrencyConfig_FromFile_ShouldSucceed()
        {
            Assert.IsTrue(File.Exists(currencyFilePath), $"File tidak ditemukan: {Path.GetFullPath(currencyFilePath)}");

            var config = CurrencyConfig<Currency>.Load(currencyFilePath);

            Assert.IsNotNull(config);
            Assert.IsFalse(string.IsNullOrEmpty(config.DefaultCurrency));
            Assert.IsTrue(config.Currencies.Count > 0);
        }

        [TestMethod]
        public void LoadOperationalConfig_FromFile_ShouldSucceed()
        {
            Assert.IsTrue(File.Exists(operationalFilePath), $"File tidak ditemukan: {Path.GetFullPath(operationalFilePath)}");

            var config = JamOperationalConfig.Load(operationalFilePath);

            Assert.IsNotNull(config);
            Assert.IsFalse(string.IsNullOrEmpty(config.DefaultMode));
            Assert.IsTrue(config.Modes.Count > 0);
        }

        [TestMethod]
        public void PaymentHandler_ShouldConvertCurrency()
        {
            var config = CurrencyConfig<Currency>.Load(currencyFilePath);
            var handler = new PaymentHandler<Currency>(config.Currencies);

            if (!config.Currencies.ContainsKey("USD"))
            {
                Assert.Inconclusive("USD tidak tersedia dalam konfigurasi.");
                return;
            }

            double amount = 1000000;

            using (var sw = new StringWriter())
            {
                var originalOut = Console.Out;
                Console.SetOut(sw);

                try
                {
                    handler.Pay("USD", amount);
                }
                finally
                {
                    Console.SetOut(originalOut);
                }

                var output = sw.ToString().ToLower();

                StringAssert.Contains(output, "dibayar sebesar");
                StringAssert.Contains(output, "$");
                StringAssert.Contains(output, "us dollar");
            }
        }

        [TestMethod]
        public void PaymentHandler_ShouldHandleInvalidCode()
        {
            var config = CurrencyConfig<Currency>.Load(currencyFilePath);
            var handler = new PaymentHandler<Currency>(config.Currencies);

            using (var sw = new StringWriter())
            {
                var originalOut = Console.Out;
                Console.SetOut(sw);

                try
                {
                    handler.Pay("INVALID_CODE", 1000);
                }
                finally
                {
                    Console.SetOut(originalOut);
                }

                var output = sw.ToString().ToLower();

                StringAssert.Contains(output, "tidak dikenali");
            }
        }

        [TestMethod]
        public void OperationalConfig_ShouldContainDefaultMode()
        {
            var config = JamOperationalConfig.Load(operationalFilePath);

            Assert.IsTrue(config.Modes.ContainsKey(config.DefaultMode));
        }
    }
}
