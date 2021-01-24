using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProje
{
    class Program
    {
        public static Random rastgele = new Random();
        // Random için bir sınıf metodu veya fonksiyon yazılınca sürekli aynı değeri üretiyordu.
        // O yüzden en başta sadece bir tane nesnesini oluşturdum.

        class Durak
        {
            public string DurakAdı;
            public int BoşPark;
            public int TandemBisiklet;
            public int NormalBisiklet;
            public Durak(string durakAdı, int boşPark, int tandemBisiklet, int normalBisiklet)
            {
                DurakAdı = durakAdı;
                BoşPark = boşPark;
                TandemBisiklet = tandemBisiklet;
                NormalBisiklet = normalBisiklet;
            }

            public string toString()
            {
                return "Durak adı: " + DurakAdı + ", boş park: " + BoşPark + ", tandem bisiklet: " + TandemBisiklet + ", normal bisiklet: " + NormalBisiklet;
            }
        }
        class Müşteri
        {
            int ID;
            string kiralamaSaati;

            public Müşteri(int ID, string kiralamaSaati)
            {
                this.ID = ID;
                this.kiralamaSaati = kiralamaSaati;
            }

            public int GetID()
            {
                return ID;
            }
            public string GetKiralamaSaati()
            {
                return kiralamaSaati;
            }


            public string toString()
            {
                return "Müşteri ID: " + ID + ", müşterinin bisiklet kiralama saati: " + kiralamaSaati;
            }
        }
        class Node
        {
            Durak data;
            Node leftChild;
            Node rightChild;
            List<Müşteri> müşteriler;

            public Node(Durak data)
            {
                this.data = data;
                leftChild = null;
                rightChild = null;
            }
            public void SetLeft(Node less)
            {
                this.leftChild = less;
            }
            public void SetRight(Node more)
            {
                this.rightChild = more;
            }
            public Node GetLeft()
            {
                return this.leftChild;
            }
            public Node GetRight()
            {
                return this.rightChild;
            }
            public Durak GetData()
            {
                return this.data;
            }
            public void SetData(Durak durak)
            {
                this.data = durak;
            }
            public void SetMüşteriler(List<Müşteri> liste)
            {
                müşteriler = liste;
            }
            public List<Müşteri> GetMüşteriListesi() { return this.müşteriler; }

            public void AddMüşteri(Müşteri müşteri)
            {
                müşteriler.Add(müşteri);
            }
        }
        class BinarySTree
        {

            Node Root;
            int müşteriSayaç = 0;

            public BinarySTree()
            {
                Root = null;
            }
            public Node GetRoot() { return Root; }
            public void Add(Durak durak)
            {
                if (Root == null)
                {
                    Root = new Node(durak);
                    int MüşteriSayısı;
                    if (Root.GetData().NormalBisiklet < 10)
                    {
                        MüşteriSayısı = rastgele.Next(1, Root.GetData().NormalBisiklet);
                    }
                    else { MüşteriSayısı = rastgele.Next(1,10); }

                    Root.SetMüşteriler(müşteriEkle(MüşteriSayısı));
                    Root.GetData().NormalBisiklet -= MüşteriSayısı;
                    Root.GetData().BoşPark += MüşteriSayısı;
                }

                else
                {
                    int position = 0;
                    Node ParentNode = Root;
                    Node currentNode = ParentNode;

                    while (!(currentNode is null))
                    {
                        if (durak.DurakAdı.CompareTo(currentNode.GetData().DurakAdı) < 0)
                        {
                            ParentNode = currentNode;
                            currentNode = currentNode.GetLeft();
                            position = -1;
                        }
                        else if (durak.DurakAdı.CompareTo(currentNode.GetData().DurakAdı) > 0)
                        {
                            ParentNode = currentNode;
                            currentNode = currentNode.GetRight();
                            position = +1;
                        }
                        else
                        {
                            Console.WriteLine("Durak zaten var");
                            break;
                        }
                    }

                    if (position < 0)
                    {
                        Node temp = new Node(durak);
                        int MüşteriSayısı;
                        if (temp.GetData().NormalBisiklet < 10)
                        {
                            MüşteriSayısı = rastgele.Next(1,temp.GetData().NormalBisiklet);
                        }
                        else
                        {
                            MüşteriSayısı = rastgele.Next(1, 10);
                        }
                        temp.SetMüşteriler(müşteriEkle(MüşteriSayısı));
                        temp.GetData().NormalBisiklet -= MüşteriSayısı;
                        temp.GetData().BoşPark += MüşteriSayısı;
                        müşteriSayaç += temp.GetMüşteriListesi().Count;
                        ParentNode.SetLeft(temp);
                    }
                    else if (position > 0)
                    {
                        Node temp = new Node(durak);
                        int MüşteriSayısı;
                        if (temp.GetData().NormalBisiklet < 10)
                        {
                            MüşteriSayısı = rastgele.Next(1, temp.GetData().NormalBisiklet);
                        }
                        else
                        {
                            MüşteriSayısı = rastgele.Next(1, 10);
                        }
                        temp.SetMüşteriler(müşteriEkle(MüşteriSayısı));
                        temp.GetData().NormalBisiklet -= MüşteriSayısı;
                        temp.GetData().BoşPark += MüşteriSayısı;
                        müşteriSayaç += temp.GetMüşteriListesi().Count;
                        ParentNode.SetRight(temp);
                    }

                }
            }
            public int Derinlik(Node root)
            {
                if (root == null)
                    return 0;

                int solDerinlik = Derinlik(root.GetLeft());

                int sağDerinlik = Derinlik(root.GetRight());

                if (solDerinlik > sağDerinlik)
                    return (solDerinlik + 1);
                else
                    return (sağDerinlik + 1);
            }
            public void MüşteriSayısı()
            {
                Console.WriteLine(" ");
                Console.WriteLine("Toplam bisiklet kiralayan müşteri sayısı : " + müşteriSayaç);
                Console.WriteLine("************************");
            }
            public void BilgiYaz(Node root)
            {
                if (root == null){return;}
                else
                {
                    Console.WriteLine(root.GetData().toString());
                    foreach (var item in root.GetMüşteriListesi())
                    {
                        Console.WriteLine(item.toString());
                    }
                    Console.WriteLine("------------------------------");
                    Console.WriteLine("------------------------------");
                    BilgiYaz(root.GetLeft());
                    BilgiYaz(root.GetRight());
                }
            }
            public void IdBul(int ID , Node root)
            {
                if (root == null) { return; }
                else
                {
                    List<Müşteri> liste= root.GetMüşteriListesi();
                    foreach (var item in liste)
                    {
                        if (item.GetID() == ID)
                        {
                            Console.WriteLine("İstasyon ismi : " + root.GetData().DurakAdı + ", bisiklet kiralama saati : " + item.GetKiralamaSaati());
                        }
                    }
                    IdBul(ID, root.GetLeft());
                    IdBul(ID, root.GetRight());
                }
            }
            public void BisikletKirala(string İstasyonİsmi, int ID, Node root)
            {
                if (root == null) { return; }
                if (root.GetData().DurakAdı == İstasyonİsmi)
                {
                    if (root.GetData().NormalBisiklet < 1)
                    {
                        Console.WriteLine("İstasyonda normal bisiklet yok, kiralama işlemi yapılamaz.");
                    }
                    else
                    {
                        string kiralamaSaati = rastgele.Next(00, 23) + ":" + rastgele.Next(0, 59);
                        Müşteri tempMüşteri = new Müşteri(ID, kiralamaSaati);
                        root.GetData().NormalBisiklet -= 1;
                        root.GetData().BoşPark += 1;
                        root.AddMüşteri(tempMüşteri);
                    }
                    return;
                }
                else
                {
                    BisikletKirala(İstasyonİsmi, ID, root.GetLeft());
                    BisikletKirala(İstasyonİsmi, ID, root.GetRight());
                }

            }

        }
        class HashTablo
        {
            Durak[] HashTable;
            int boyut;

            public HashTablo(int boyut)
            {
                HashTable = new Durak[boyut];
                this.boyut = boyut;
            }

            int HashKoduOluştur(string key)
            {
                int temp = 0;
                foreach (var harf in key)
                {
                    temp += Convert.ToInt32(harf);
                }

                return temp % boyut;
            }

            public void Add(Durak durak)
            {
                int hashKodu = HashKoduOluştur(durak.DurakAdı);

                switch (HashTable[hashKodu] == null)
                {
                    case true:
                        if (HashTable[hashKodu] != null)
                        {
                            goto case false;
                        }
                        HashTable[hashKodu] = durak;
                        break;
                    case false:
                        hashKodu += 1;
                        goto case true;
                }
            }

            public void Update(Durak durak)
            {
                int hashKodu = HashKoduOluştur(durak.DurakAdı);

                switch (HashTable[hashKodu].DurakAdı == durak.DurakAdı)
                {
                    case true:
                        // eğer elemanların tutulduğu dizide yeri dolu ise bir sonraki boş yere ataması lazım.
                        // bu if bloğu asıl yeri dolu ise boş yer bulana kadar diziyi gezdiriyor.

                        if (HashTable[hashKodu].DurakAdı != durak.DurakAdı)
                        {
                            goto case false;
                        }

                        HashTable[hashKodu].NormalBisiklet += 5;
                        break;
                    case false:
                        hashKodu += 1;
                        goto case true;
                }

            }
        }

        class Heap
        {
            private Node[] nodes;
            int sıra = 0;
            public Heap(int boyut)
            {
                nodes = new Node[boyut];
            }
            public void updateInsert(int index)
            {
                int parent = (index - 1) / 2;
                Node bottom = nodes[index];
                while (index > 0 &&
                nodes[parent].GetData().NormalBisiklet < bottom.GetData().NormalBisiklet)
                {
                    nodes[index] = nodes[parent]; // aşşağı at
                    index = parent; // indeksi arttır
                    parent = (parent - 1) / 2; // bir üst ebeveyni seç
                } 
                nodes[index] = bottom;
            }
            public void insert(Durak durak)
            {
                Node ekle = new Node(durak);
                nodes[sıra] = ekle;
                updateInsert(sıra++);
            }
            public void updateRemove(int index)
            {
                int largerChild;
                Node top = nodes[index]; 
                while (index < sıra / 2) 
                {
                    int leftChild = 2 * index + 1;
                    int rightChild = leftChild + 1;
                    
                    if (rightChild < sıra && 
                    nodes[leftChild].GetData().NormalBisiklet <
                    nodes[rightChild].GetData().NormalBisiklet)
                            largerChild = rightChild;
                    else
                        largerChild = leftChild;
                    if (top.GetData().NormalBisiklet >= nodes[largerChild].GetData().NormalBisiklet)
                        break;
                    nodes[index] = nodes[largerChild];
                    index = largerChild; 
                } 
                nodes[index] = top; 
            }
            public void remove() 
            { 
                Node root = nodes[0];
                nodes[0] = nodes[--sıra];
                nodes[sıra] = null;
                updateRemove(0); 
            }  
            public void Max3()
            {
                Node[] max3 = new Node[nodes.Length];
                for (int i = 0; i < nodes.Length; i++)
                {
                    max3[i] = nodes[i];
                }
                for (int i = 0; i <3; i++)
                {
                    Node root = max3[0];
                    Console.WriteLine(root.GetData().toString());
                    max3[0] = max3[--sıra];
                    updateRemove(0);
                }
            }
            
        }

        static void Main(string[] args)
        {
            // CompareTo metodunda ASCII tablosu ile karşılaştırma yapıldığı için durak isimlerinde Türkçe karakter kullanmadım.
            string[] duraklarString =
                {"Inciralti,28,2,10",
                "Sahilevleri,8,1,11",
                "Dogal Yasam Parki,17,1,6",
                "Bostanli Iskele,7,0,5",
                "Bornova Metro,4,3,8",
                "Kus Cenneti,30,6,4",
                "Yunuslar,4,8,8",
                "Karatas,5,7,12",
                "Susuz Dede,10,11,13"
                };

            Durak[] Duraklar = DurakYap(duraklarString);
            /*
            BinarySTree durakAğacı = new BinarySTree();

            for (int i = 0; i < Duraklar.Length; i++)
            {
                durakAğacı.Add(Duraklar[i]);
            }
            durakAğacı.MüşteriSayısı();
            durakAğacı.BilgiYaz(durakAğacı.GetRoot());

            Console.WriteLine("Aranacak müşterinin ID bilgisini girin...");
            int ID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("****************************");
            durakAğacı.IdBul(ID,durakAğacı.GetRoot());

            Console.WriteLine("Bisikletin kiralanacağı istasyon ismini yazın (Türkçe karakter kullanmadan):");
            string istasyonAdı = Console.ReadLine();
            Console.WriteLine("Bisiklet kiralayacak müşterinin ID sini yazın : ");
            int kiralamaID = Convert.ToInt32(Console.ReadLine());
            durakAğacı.BisikletKirala(istasyonAdı, kiralamaID, durakAğacı.GetRoot());

            //kodun doğru çalıştığını kontrol etmek için debug ile bakmak yerine ID bilgisini 20 üstü girip "durakAğacı.IdBul(ID,durakAğacı.GetRoot())" kodunu çalıştırarak bakılabilirs
            

            /////////////////////////////

            HashTablo tablo = new HashTablo(Duraklar.Length);

            foreach (var item in Duraklar)
            {
                tablo.Add(item);
            }

            foreach (var item in Duraklar)
            {
                if (item.BoşPark > 5)
                {
                    tablo.Update(item);
                }
            }
            */

            Heap heap = new Heap(Duraklar.Length);
            for (int i = 0; i < Duraklar.Length; i++)
            {
                heap.insert(Duraklar[i]);
            }

            /*
            int[] InsertionArray = { 25, 77, 9, 3, 225, 10, 0 , 167};
            InsertionSort(InsertionArray);
            for (int i = 0; i < InsertionArray.Length; i++)
            {
                Console.WriteLine("Arrayin " + (i + 1) + ". elemanı = " + InsertionArray[i]);
            }
            */

            Console.ReadLine();
        }

        static Durak[] DurakYap(string[] durakBilgileri)
        {
            Durak[] Duraklar = new Durak[durakBilgileri.Length];

            for (int i = 0; i < durakBilgileri.Length; i++)
            {
                string[] bilgiler = durakBilgileri[i].Split(',');
                Durak temp = new Durak(bilgiler[0], Convert.ToInt32(bilgiler[1]), Convert.ToInt32(bilgiler[2]), Convert.ToInt32(bilgiler[3]));
                Duraklar[i] = temp;
            }

            return Duraklar;
        }
        static List<Müşteri> müşteriEkle(int müşteriSayısı)
        {
            List<Müşteri> müşteriler = new List<Müşteri>();

            for (int i = 0; i < müşteriSayısı; i++)
            {
                string saat = rastgele.Next(00, 23) + ":" + rastgele.Next(0, 59);
                Müşteri temp = new Müşteri(rastgele.Next(1, 20), saat);
                müşteriler.Add(temp);
            }
            return müşteriler;
        }
        static int[] InsertionSort(int[] array) // metodun parametresi array tipi verileri kabul ediyor 
        {
            for (int i = 1; i < array.Length; i++) // dıştaki for döngüsü array in 2. elemanından son elemanına kadar gitmesini sağlıyor, i ye kadar olan kısım sıralı kısım
            {
                for (int j = i - 1; j != -1; j--) // içteki for döngüsü ise sıralanmış array kısmı ile dıştaki for ile seçilen elemanın karşılaştırılmasını sağlıyor.
                {
                    if (array[i] < array[j])
                    // eğer array deki sağda olan eleman solda olandan daha küçükse (array[i]<array[j]) bunların değerlerini değiştiriyor.
                    // ve eğer koşul doğruysa soldaki diğer elemanlarla da karşılaştırmak için bir index sola gidiyor (i--)
                    {
                        int temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                        i--;
                    }
                }
            }
            return array;
        }
        static void RadixSort(int[] array)
        {
            string[] newArray = new string[array.Length];
            string a = array.Max().ToString();
            int tekrarSayısı = a.Length;


            for (int i = 0; i < array.Length; i++)
            {
                newArray[i] = array[i].ToString().Insert(0,"0");
            }

        }   
    }
}
