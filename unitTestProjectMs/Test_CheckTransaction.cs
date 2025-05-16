
using UTS_Evi;

namespace unitTestProjectMs
{
    [TestClass]
    public sealed class Test_CheckTransaction
    {
        UTS_Evi.CheckTransaction CT = new UTS_Evi.CheckTransaction();

        [TestMethod]
        public void ShowProductBuyTesting()
        {
            Assert.AreEqual(35000, CT.ShowProductBuy([1, 2]));
            Assert.AreEqual(0, CT.ShowProductBuy([50]));
            Assert.AreEqual(15000, CT.ShowProductBuy([1, 50]));
        }

        [TestMethod]
        public void ReturnProductTesting()
        {
            Object[][] cek = new object[][]
            {
                new object[]
                {
                    "Lays", "Pringles", "Cheetos", "Taro",
                                "Roma", "Khong Guan", "Oops! Cookies",
                                "Oreo", "BelVita", "Marie Regal",
                                "Garuda", "Mamasuka", "ABC",
                                "Cap Matahari", "Tanggo", "Lays",
                                "Richeese", "Richeese Roll", "Chiki",
                                "Coca-Cola", "Pepsi", "Sprite", "Fanta",
                                "Teh Botol Sosro", "Lipton", "Sariwangi",
                                "Minute Maid", "Tropicana", "Mizone",
                                "Red Bull", "Monster Energy", "Pocari Sweat",
                                "Indomilk", "Frisian Flag", "Ovaltine",
                                "Nescafe", "Starbucks", "Kopi Kapal Api"
                },
                new object[]
                {
                    15000, 20000, 18000, 12000,
                            10000, 5000, 25000, 30000,
                            25000, 22000, 12000, 15000,
                            8000, 7000, 15000, 18000,
                            12000, 10000, 13000, 25000,
                            15000, 12000, 10000, 13000,
                            16000, 18000, 25000, 30000,
                            22000, 25000, 30000, 25000,
                            22000, 15000, 35000, 25000,5000,54000
                }
            };

            Object[][] tes = CT.Product();

            for(int i = 0; i < cek.Length; i++)
            {
                CollectionAssert.AreEqual(tes[i], cek[i]);
            }
        }
    }
}
