// File: VendingManager.cs
using System.Collections.Generic;
using System.Linq;

namespace AutoVending
{
    public class VendingManager
    {
        private List<Product> products = new List<Product>();

        public void TambahProduk(Product product) => products.Add(product);

        public decimal HitungTotal() => products.Sum(p => p.Price * p.Quantity);

        public Product? DetilProduk(string id) => products.FirstOrDefault(p => p.Id == id);

        public List<Product> LihatSemuaProduk() => products;

        public void CheckoutProduk() => products.Clear();

        public void DeleteProduk(string id) =>
            products.RemoveAll(p => p.Id == id);
    }
}

