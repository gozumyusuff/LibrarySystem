using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Kitap
{
    public string Baslik { get; set; }
    public string Yazar { get; set; }
    public string ISBN { get; set; }
    public int ToplamKopya { get; set; }
    public int KullanilabilirKopya { get; set; }

    public Kitap(string baslik, string yazar, string isbn, int toplamKopya)
    {
        Baslik = baslik;
        Yazar = yazar;
        ISBN = isbn;
        ToplamKopya = toplamKopya;
        KullanilabilirKopya = toplamKopya;
    }
}


class Kutuphane
{
    private List<Kitap> kitaplar = new List<Kitap>();

    // Kitap ekleme
    public void KitapEkle(string baslik, string yazar, string isbn, int toplamKopya)
    {
        // ISBN numarasının benzersiz olup olmadığını kontrol et
        if (ISBNBenzersizMi(isbn))
        {
            Kitap yeniKitap = new Kitap(baslik, yazar, isbn, toplamKopya);
            kitaplar.Add(yeniKitap);
            Console.WriteLine($"{yeniKitap.Baslik} adlı kitap kütüphaneye eklendi.");
        }
        else
        {
            Console.WriteLine("Hata: ISBN numarası her kitap için farklı olmalı.");
        }
    }

    // ISBN numarasının benzersiz olup olmadığını kontrol et
    private bool ISBNBenzersizMi(string isbn)
    {
        foreach (var kitap in kitaplar)
        {
            if (kitap.ISBN == isbn)
            {
                return false;
            }
        }
        return true;
    }

    // Tüm kitapları görüntüleme
    public void TumKitaplariGoruntule()
    {
        Console.WriteLine("Tüm kitaplar:");
        foreach (var kitap in kitaplar)
        {
            Console.WriteLine($"Başlık: {kitap.Baslik}, Yazar: {kitap.Yazar}, ISBN: {kitap.ISBN}, Toplam Kopya: {kitap.ToplamKopya}, Kullanılabilir Kopya: {kitap.KullanilabilirKopya}");
        }
    }

    // Kitap arama
    public void KitapAra(string anahtarKelime)
    {
        Console.WriteLine($"\"{anahtarKelime}\" ile eşleşen kitaplar:");
        foreach (var kitap in kitaplar)
        {
            if (kitap.Baslik.Contains(anahtarKelime) || kitap.Yazar.Contains(anahtarKelime))
            {
                Console.WriteLine($"Başlık: {kitap.Baslik}, Yazar: {kitap.Yazar}, ISBN: {kitap.ISBN}, Toplam Kopya: {kitap.ToplamKopya}, Kullanılabilir Kopya: {kitap.KullanilabilirKopya}");
            }
        }
    }

    // Kitap ödünç alma
    public void KitapOduncAl(string isbn)
    {
        foreach (var kitap in kitaplar)
        {
            if (kitap.ISBN == isbn)
            {
                if (kitap.KullanilabilirKopya > 0)
                {
                    kitap.KullanilabilirKopya--;
                    Console.WriteLine($"{kitap.Baslik} adlı kitabı ödünç aldınız.");
                    return;
                }
                else
                {
                    Console.WriteLine("Üzgünüz, bu kitap şu anda mevcut değil.");
                    return;
                }
            }
        }
        Console.WriteLine("Kitap bulunamadı.");
    }

    // Kitap iade etme
    public void KitapIadeEt(string isbn)
    {
        foreach (var kitap in kitaplar)
        {
            if (kitap.ISBN == isbn)
            {
                if (kitap.KullanilabilirKopya < kitap.ToplamKopya)
                {
                    kitap.KullanilabilirKopya++;
                    Console.WriteLine($"{kitap.Baslik} adlı kitabı iade ettiniz.");
                    return;
                }
                else
                {
                    Console.WriteLine("Bu kitap zaten iade edilmiş veya kütüphaneye ait değil.");
                    return;
                }
            }
        }
        Console.WriteLine("Kitap bulunamadı.");
    }

    class Program
    {
        static void Main(string[] args)
        {
            Kutuphane kutuphane = new Kutuphane();

            // Default kitapları ekleyelim
            kutuphane.KitapEkle("Suç ve Ceza", "Fyodor Dostoyevski", "1234567", 5);
            kutuphane.KitapEkle("Satranç", "Stefan Zweig", "2341523", 3);
            kutuphane.KitapEkle("Sefiller", "Victor Hugo", "65784932", 4);

            

            //Kitap eklemek için bir döngü oluşturalım
            while (true)
            {
                Console.WriteLine("\nKütüphane Yönetim Sistemi");
                Console.WriteLine("1. Tüm kitapları görüntüle");
                Console.WriteLine("2. Kitap ara");
                Console.WriteLine("3. Kitap ödünç al");
                Console.WriteLine("4. Kitap iade et");
                Console.WriteLine("5. Yeni kitap ekle");
                Console.WriteLine("6. Çıkış");
                Console.Write("Seçiminiz: ");

                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        kutuphane.TumKitaplariGoruntule();
                        break;
                    case "2":
                        Console.Write("Aramak istediğiniz kitap veya yazar adını girin: ");
                        string anahtarKelime = Console.ReadLine();
                        kutuphane.KitapAra(anahtarKelime);
                        break;
                    case "3":
                        Console.Write("Ödünç almak istediğiniz kitabın ISBN'sini girin: ");
                        string oduncAlISBN = Console.ReadLine();
                        kutuphane.KitapOduncAl(oduncAlISBN);
                        break;
                    case "4":
                        Console.Write("İade etmek istediğiniz kitabın ISBN'sini girin: ");
                        string iadeISBN = Console.ReadLine();
                        kutuphane.KitapIadeEt(iadeISBN);
                        break;
                    case "5":
                        Console.Write("Başlık: ");
                        string baslik = Console.ReadLine();
                        Console.Write("Yazar: ");
                        string yazar = Console.ReadLine();
                        Console.Write("ISBN: ");
                        string isbn = Console.ReadLine();
                        Console.Write("Toplam Kopya: ");
                        int toplamKopya = int.Parse(Console.ReadLine());
                        kutuphane.KitapEkle(baslik, yazar, isbn, toplamKopya);
                        break;
                    case "6":
                        Console.WriteLine("Programdan çıkılıyor...");
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                        break;
                }
            }
        }
    }
}