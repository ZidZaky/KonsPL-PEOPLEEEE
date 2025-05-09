using System;
using System.Collections.Generic;
using System.IO;
using UTS_PEOPLEEEE;
using Xunit;

namespace TestProject
{
    public class UnitTests
    {
        private readonly string currencyFilePath = "currency_config.json";
        private readonly string operationalFilePath = "operational_config.json";

        [Fact]
        public void LoadCurrencyConfig_FromFile_ShouldSucceed()
        {
            Assert.True(File.Exists(currencyFilePath), $"File tidak ditemukan: {Path.GetFullPath(currencyFilePath)}");

            var config = CurrencyConfig<Currency>.Load(currencyFilePath);

            Assert.NotNull(config);
            Assert.False(string.IsNullOrEmpty(config.DefaultCurrency));
            Assert.NotEmpty(config.Currencies);
        }

        [Fact]
        public void LoadOperationalConfig_FromFile_ShouldSucceed()
        {
            Assert.True(File.Exists(operationalFilePath), $"File tidak ditemukan: {Path.GetFullPath(operationalFilePath)}");

            var config = JamOperationalConfig.Load(operationalFilePath);

            Assert.NotNull(config);
            Assert.False(string.IsNullOrEmpty(config.DefaultMode));
            Assert.NotEmpty(config.Modes);
        }

        [Fact]
        public void PaymentHandler_ShouldConvertCurrency()
        {
            var config = CurrencyConfig<Currency>.Load(currencyFilePath);
            var handler = new PaymentHandler<Currency>(config.Currencies);

            double amount = 1000000;

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                handler.Pay("USD", amount);

                var output = sw.ToString();
                Assert.Contains("USD", output, StringComparison.OrdinalIgnoreCase);
                Assert.Contains("$", output); // simbol USD
            }
        }

        [Fact]
        public void PaymentHandler_ShouldHandleInvalidCode()
        {
            var config = CurrencyConfig<Currency>.Load(currencyFilePath);
            var handler = new PaymentHandler<Currency>(config.Currencies);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                handler.Pay("INVALID_CODE", 1000);

                var output = sw.ToString();
                Assert.Contains("tidak ditemukan", output, StringComparison.OrdinalIgnoreCase);
            }
        }

        [Fact]
        public void OperationalConfig_ShouldContainDefaultMode()
        {
            var config = JamOperationalConfig.Load(operationalFilePath);

            Assert.Contains(config.DefaultMode, config.Modes.Keys);
        }
    }
}
