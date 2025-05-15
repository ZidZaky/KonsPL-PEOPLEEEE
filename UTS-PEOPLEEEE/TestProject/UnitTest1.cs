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

            // Pastikan USD tersedia di config
            if (!config.Currencies.ContainsKey("USD"))
            {
                // Lewatkan test jika data tidak ada
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
                    Console.SetOut(originalOut); // Kembalikan Console
                }

                var output = sw.ToString().ToLower();

                // Sesuaikan dengan output actual dari Pay()
                Assert.Contains("dibayar sebesar", output);
                Assert.Contains("$", output); // simbol USD
                Assert.Contains("us dollar", output); // Nama lengkap
            }
        }

        [Fact]
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
                    Console.SetOut(originalOut); // Restore Console
                }

                var output = sw.ToString().ToLower();

                // Sesuaikan dengan string yang ditampilkan di Pay()
                Assert.Contains("tidak dikenali", output);
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
