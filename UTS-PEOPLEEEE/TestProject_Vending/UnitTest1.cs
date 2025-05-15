using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using AutoVending;
using AutoVending.Models;
using UTS_PEOPLEEEE;

namespace TestProject_Vending
{
    public class UnitTesting
    {
        // ===============================
        // GenericRepository Tests
        // ===============================

        [Fact]
        public void GenericRepo_Tambah()
        {
            var repo = new GenericRepository<Product>();
            var product = new Product { Id = "P01", Name = "Air", Price = 5000, Quantity = 2 };

            repo.Tambah(product);

            Assert.Single(repo.LihatSemua());
            Assert.Equal("P01", repo.LihatSemua().First().Id);
        }

        [Fact]
        public void GenericRepo_Detil()
        {
            var repo = new GenericRepository<Product>();
            var product = new Product { Id = "P02", Name = "Teh", Price = 7000, Quantity = 3 };
            repo.Tambah(product);

            var result = repo.Detil("P02");

            Assert.NotNull(result);
            Assert.Equal("Teh", result.Name);
        }

        [Fact]
        public void GenericRepo_Hapus()
        {
            var repo = new GenericRepository<Product>();
            repo.Tambah(new Product { Id = "P03", Name = "Kopi", Price = 8000, Quantity = 1 });

            repo.Hapus("P03");

            Assert.Empty(repo.LihatSemua());
        }

        [Fact]
        public void GenericRepo_Clear()
        {
            var repo = new GenericRepository<Product>();
            repo.Tambah(new Product { Id = "P01", Name = "A", Price = 1000, Quantity = 1 });
            repo.Tambah(new Product { Id = "P02", Name = "B", Price = 2000, Quantity = 1 });

            repo.Clear();

            Assert.Empty(repo.LihatSemua());
        }

        [Fact]
        public void GenericRepo_HitungTotal()
        {
            var repo = new GenericRepository<Product>();
            repo.Tambah(new Product { Id = "P01", Name = "A", Price = 1000, Quantity = 2 });
            repo.Tambah(new Product { Id = "P02", Name = "B", Price = 1500, Quantity = 3 });

            var total = repo.HitungTotal(p => p.Price * p.Quantity);

            Assert.Equal(1000 * 2 + 1500 * 3, total);
        }

        // ===============================
        // VendingManager Tests
        // ===============================

        [Fact]
        public void VendingManager_TambahProduk()
        {
            var manager = new VendingManager();
            manager.TambahProduk(new Product { Id = "P01", Name = "A", Price = 1000, Quantity = 1 });

            Assert.Single(manager.LihatSemuaProduk());
        }

        [Fact]
        public void VendingManager_DetilProduk()
        {
            var manager = new VendingManager();
            manager.TambahProduk(new Product { Id = "P02", Name = "B", Price = 2000, Quantity = 1 });

            var product = manager.DetilProduk("P02");

            Assert.NotNull(product);
            Assert.Equal("B", product.Name);
        }

        [Fact]
        public void VendingManager_DeleteProduk()
        {
            var manager = new VendingManager();
            manager.TambahProduk(new Product { Id = "P03", Name = "C", Price = 3000, Quantity = 1 });

            manager.DeleteProduk("P03");

            Assert.Empty(manager.LihatSemuaProduk());
        }

        [Fact]
        public void VendingManager_CheckoutProduk()
        {
            var manager = new VendingManager();
            manager.TambahProduk(new Product { Id = "P04", Name = "D", Price = 4000, Quantity = 1 });

            manager.CheckoutProduk();

            Assert.Empty(manager.LihatSemuaProduk());
        }

        [Fact]
        public void VendingManager_HitungTotal()
        {
            var manager = new VendingManager();
            manager.TambahProduk(new Product { Id = "P05", Name = "E", Price = 5000, Quantity = 2 });
            manager.TambahProduk(new Product { Id = "P06", Name = "F", Price = 1000, Quantity = 3 });

            var total = manager.HitungTotal();

            Assert.Equal(5000 * 2 + 1000 * 3, total);
        }

        // ===============================
        // LanguageConfig Tests
        // ===============================

        [Fact]
        public void LanguageConfig_GetMessage()
        {
            var config = new LanguageConfig
            {
                Language = "ID",
                Messages = new Dictionary<string, Dictionary<string, string>>
                {
                    {
                        "ID", new Dictionary<string, string>
                        {
                            { "welcome", "Selamat datang" }
                        }
                    }
                }
            };

            var msg = config.GetMessage("welcome");

            Assert.Equal("Selamat datang", msg);
        }

        [Fact]
        public void RuntimeLanguage_Load()
        {
            var tempPath = Path.GetTempFileName();
            var json = """
            {
                "Language": "EN",
                "messages": {
                    "EN": {
                        "welcome": "Welcome"
                    }
                }
            }
            """;
            File.WriteAllText(tempPath, json);

            var config = RuntimeLanguage.Load(tempPath);

            Assert.Equal("EN", config.Language);
            Assert.Equal("Welcome", config.GetMessage("welcome"));

            File.Delete(tempPath);
        }

        // ===============================
        // Product & Transaction Tests
        // ===============================

        [Fact]
        public void Product_AddProduct()
        {
            var product = new Product { Id = "X1", Name = "Test", Price = 1000, Quantity = 2 };

            Assert.Equal("X1", product.Id);
            Assert.Equal("Test", product.Name);
            Assert.Equal(1000, product.Price);
            Assert.Equal(2, product.Quantity);
        }

        [Fact]
        public void Transaction_SimpanData()
        {
            var now = DateTime.Now;
            var transaction = new Transaction
            {
                ProductId = "P01",
                ProductName = "Air",
                Quantity = 2,
                TotalPrice = 10000,
                Timestamp = now
            };

            Assert.Equal("P01", transaction.ProductId);
            Assert.Equal("Air", transaction.ProductName);
            Assert.Equal(2, transaction.Quantity);
            Assert.Equal(10000, transaction.TotalPrice);
            Assert.Equal(now, transaction.Timestamp);
        }
    }
}
