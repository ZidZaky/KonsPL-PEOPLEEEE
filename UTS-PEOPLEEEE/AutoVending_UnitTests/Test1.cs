using AutoVending;
using System.Transactions;
using UTS_PEOPLEEEE;
using AutoVending.Models;

namespace AutoVending_UnitTests
{
    [TestClass]
    public sealed class Test1
    {
        [TestClass]
        public class GenericRepositoryTests
        {
            [TestMethod]
            public void GenericRepo_Tambah()
            {
                var repo = new GenericRepository<Product>();
                var product = new Product { Id = "P01", Name = "Air", Price = 5000, Quantity = 2 };

                repo.Tambah(product);

                Assert.AreEqual(1, repo.LihatSemua().Count);
                Assert.AreEqual("P01", repo.LihatSemua().First().Id);
            }

            [TestMethod]
            public void GenericRepo_Detil()
            {
                var repo = new GenericRepository<Product>();
                var product = new Product { Id = "P02", Name = "Teh", Price = 7000, Quantity = 3 };
                repo.Tambah(product);

                var result = repo.Detil("P02");

                Assert.IsNotNull(result);
                Assert.AreEqual("Teh", result.Name);
            }

            [TestMethod]
            public void GenericRepo_Hapus()
            {
                var repo = new GenericRepository<Product>();
                repo.Tambah(new Product { Id = "P03", Name = "Kopi", Price = 8000, Quantity = 1 });

                repo.Hapus("P03");

                Assert.AreEqual(0, repo.LihatSemua().Count);
            }

            [TestMethod]
            public void GenericRepo_Clear()
            {
                var repo = new GenericRepository<Product>();
                repo.Tambah(new Product { Id = "P01", Name = "A", Price = 1000, Quantity = 1 });
                repo.Tambah(new Product { Id = "P02", Name = "B", Price = 2000, Quantity = 1 });

                repo.Clear();

                Assert.AreEqual(0, repo.LihatSemua().Count);
            }

            [TestMethod]
            public void GenericRepo_HitungTotal()
            {
                var repo = new GenericRepository<Product>();
                repo.Tambah(new Product { Id = "P01", Name = "A", Price = 1000, Quantity = 2 });
                repo.Tambah(new Product { Id = "P02", Name = "B", Price = 1500, Quantity = 3 });

                var total = repo.HitungTotal(p => p.Price * p.Quantity);

                Assert.AreEqual(1000 * 2 + 1500 * 3, total);
            }
        }

        [TestClass]
        public class VendingManagerTests
        {
            [TestMethod]
            public void VendingManager_TambahProduk()
            {
                var manager = new VendingManager();
                manager.TambahProduk(new Product { Id = "P01", Name = "A", Price = 1000, Quantity = 1 });

                Assert.AreEqual(1, manager.LihatSemuaProduk().Count);
            }

            [TestMethod]
            public void VendingManager_DetilProduk()
            {
                var manager = new VendingManager();
                manager.TambahProduk(new Product { Id = "P02", Name = "B", Price = 2000, Quantity = 1 });

                var product = manager.DetilProduk("P02");

                Assert.IsNotNull(product);
                Assert.AreEqual("B", product.Name);
            }

            [TestMethod]
            public void VendingManager_DeleteProduk()
            {
                var manager = new VendingManager();
                manager.TambahProduk(new Product { Id = "P03", Name = "C", Price = 3000, Quantity = 1 });

                manager.DeleteProduk("P03");

                Assert.AreEqual(0, manager.LihatSemuaProduk().Count);
            }

            [TestMethod]
            public void VendingManager_CheckoutProduk()
            {
                var manager = new VendingManager();
                manager.TambahProduk(new Product { Id = "P04", Name = "D", Price = 4000, Quantity = 1 });

                manager.CheckoutProduk();

                Assert.AreEqual(0, manager.LihatSemuaProduk().Count);
            }

            [TestMethod]
            public void VendingManager_HitungTotal()
            {
                var manager = new VendingManager();
                manager.TambahProduk(new Product { Id = "P05", Name = "E", Price = 5000, Quantity = 2 });
                manager.TambahProduk(new Product { Id = "P06", Name = "F", Price = 1000, Quantity = 3 });

                var total = manager.HitungTotal();

                Assert.AreEqual(5000 * 2 + 1000 * 3, total);
            }
        }

        [TestClass]
        public class LanguageConfigTests
        {
            [TestMethod]
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

                Assert.AreEqual("Selamat datang", msg);
            }

            [TestMethod]
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

                Assert.AreEqual("EN", config.Language);
                Assert.AreEqual("Welcome", config.GetMessage("welcome"));

                File.Delete(tempPath);
            }
        }

        [TestClass]
        public class ProductTransactionTests
        {
            [TestMethod]
            public void Product_AddProduct()
            {
                var product = new Product { Id = "X1", Name = "Test", Price = 1000, Quantity = 2 };

                Assert.AreEqual("X1", product.Id);
                Assert.AreEqual("Test", product.Name);
                Assert.AreEqual(1000, product.Price);
                Assert.AreEqual(2, product.Quantity);
            }

            [TestMethod]
            public void Transaction_SimpanData()
            {
                var now = DateTime.Now;
                var transaction = new AutoVending.Models.Transaction
                {
                    ProductId = "P01",
                    ProductName = "Air",
                    Quantity = 2,
                    TotalPrice = 10000,
                    Timestamp = now
                };

                Assert.AreEqual("P01", transaction.ProductId);
                Assert.AreEqual("Air", transaction.ProductName);
                Assert.AreEqual(2, transaction.Quantity);
                Assert.AreEqual(10000, transaction.TotalPrice);
                Assert.AreEqual(now, transaction.Timestamp);
            }
        }
    }
}
