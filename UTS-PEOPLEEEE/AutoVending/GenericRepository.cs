using AutoVending.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AutoVending
{
    public class GenericRepository<T> where T : IIdentifiable
    {
        private List<T> items = new List<T>();

        public void Tambah(T item)
        {
            Debug.Assert(item != null, "Item yang ditambahkan tidak boleh null.");
            items.Add(item);
        }

        public List<T> LihatSemua()
        {
            Debug.Assert(items != null, "List item tidak boleh null saat dilihat.");
            return items;
        }

        public T? Detil(string id)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(id), "ID tidak boleh kosong atau null.");
            return items.FirstOrDefault(i => (i as dynamic).Id == id);
        }

        public void Hapus(string id)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(id), "ID tidak boleh kosong atau null.");
            int awal = items.Count;
            items.RemoveAll(i => (i as dynamic).Id == id);
            Debug.Assert(items.Count < awal, "Item dengan ID tersebut tidak ditemukan atau tidak terhapus.");
        }

        public void Clear()
        {
            items.Clear();
            Debug.Assert(items.Count == 0, "List item harus kosong setelah di-clear.");
        }

        public decimal HitungTotal(Func<T, decimal> kalkulasi)
        {
            Debug.Assert(kalkulasi != null, "Fungsi kalkulasi tidak boleh null.");
            return items.Sum(kalkulasi);
        }
    }
}
