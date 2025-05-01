using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace UTS_Evi
{
    public class CheckTransaction
    {
        enum Status { Idle, Order, Payment, Done };
        Status status = Status.Idle;


        public void UI()
        {
            String Buy = "0";
            List<int> Products = [1, 2];
            Console.WriteLine("=====================EVI FITRIYA========================");



            //Automata
            while (status != Status.Done)
            {
                switch (status)
                {
                    case Status.Idle:
                        Buy = "0";
                        Products = null;
                        Console.WriteLine("=====================idle========================");
                        Buy = Idle();
                        break;
                    case Status.Order:
                        Console.WriteLine("===================Order==============");
                        Products = Order(Buy);
                        break;
                    case Status.Payment:
                        Console.WriteLine("==================Payment===========");
                        Payyment(Products);
                        break;
                }
            }
            Console.WriteLine("Application Done");
        }

        public String Idle()
        {
            Console.WriteLine("Silahkan Pilih Product yg ingin dibeli");
            int count = 0;
            foreach (var item in Product()[0])
            {
                //Console.WriteLine((count+1)+". "+item+"\t Tes");
                Console.WriteLine($"{count + 1}. {item,-40}");
                count += 1;
            }

            Console.WriteLine("untuk membeli lebih dari 1 pisahkan dengan ',', contoh 1,2");
            Console.Write("input number:");
            String input = Console.ReadLine();
            if (input == "END" || input == "end" || input == "End")
            {
                status = Status.Done;
                return input;
            }
            else
            {
                status = Status.Order;
                return input;
            }
        }
        public Object[][] Product()
        {
            String[] Product = {
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
        };
            int[] Price = {
                            15000, 20000, 18000, 12000,
                            10000, 5000, 25000, 30000,
                            25000, 22000, 12000, 15000,
                            8000, 7000, 15000, 18000,
                            12000, 10000, 13000, 25000,
                            15000, 12000, 10000, 13000,
                            16000, 18000, 25000, 30000,
                            22000, 25000, 30000, 25000,
                            22000, 15000, 35000, 25000,5000,54000
        };
            return new Object[][] { Product, Price.Cast<Object>().ToArray() };

        }


        public List<int> Order(String inputan)
        {
            Console.WriteLine("Product yang anda beli");
            List<int> Product = new List<int>();

            if (inputan.Contains(","))
            {
                {
                    String[] a = inputan.Split(',');
                    foreach (var item in a)
                        if (int.TryParse(item, out int result))
                        {
                            Product.Add(result);
                        }
                }
            }
            else
            {
                if (int.TryParse(inputan, out int result))
                {
                    Product.Add(result);
                }
                else
                {
                    Console.WriteLine("Invalid input");
                    status = Status.Idle;
                    return null;
                }
            }
            ShowProductBuy(Product);

            Console.Write("Apakah Barang yang akan dibeli sudah sesuai? (y/n) ");
            String input = Console.ReadLine();
            if (input == "y" || input == "Y")
            {

                status = Status.Payment;
                return Product;
            }
            else
            {
                status = Status.Idle;
                return null;
            }
        }

        public int ShowProductBuy(List<int> inputan)
        {
            Console.WriteLine("Detail Produk: ");
            Object[][] product = Product();
            String[] Name = (String[])product[0];
            int[] harga = product[1].Cast<int>().ToArray();

            int sum = 0;
            int count = 0;
            Console.WriteLine(inputan[0]);
            foreach (var item in inputan)
            {
                if (item < Name.Length && item > 0)
                {
                    Console.WriteLine((count + 1) + ". " + Name[item] + " - " + harga[item]);
                    count += 1;
                    sum += harga[item];
                }
            }

            Console.WriteLine("Total: " + sum);
            if (sum == 0)
            {
                Console.WriteLine("Tidak ada product dengan Nomor Tersebut");
                status = Status.Idle;
            }

            return sum;

        }

        public void Payyment(List<int> Products)
        {
            ShowProductBuy(Products);
            Console.WriteLine("=========================================");
            Console.WriteLine("|              QRIS PAYMENT             |");
            Console.WriteLine("|            SCAN THE QR CODE           |");
            Console.WriteLine("=========================================");
            Thread.Sleep(1000);
            Console.WriteLine("\n\n Pembayaranmu telah diterima, silahkan ambil produknya di bawah");
            Console.WriteLine("Terimakasih");
            Thread.Sleep(1000);
            Console.WriteLine("Dalam 5 detik Halaman ini akan di reset");
            Thread.Sleep(5000);
            status = Status.Idle;
        }

    }

}
