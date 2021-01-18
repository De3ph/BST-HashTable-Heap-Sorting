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

        static List<Müşteri> müşteriEkle(int müşteriSayısı)
        {
            List<Müşteri> müşteriler = new List<Müşteri>();

            for (int i = 0; i < müşteriSayısı; i++)
            {
                string saat = rastgele.Next(00, 23) + ":" + rastgele.Next(0, 59);
                Müşteri temp = new Müşteri(rastgele.Next(1,20), saat);
                müşteriler.Add(temp);
            }
            return müşteriler;
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
        }
        class BinarySTree
        {

            Node Root;
            public Node GetRoot()
            {
                return Root;
            }
            int müşteriSayaç = 0;

            public BinarySTree()
            {
                Root = null;
            }

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
                if (root == null)
                {
                    return;
                }
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
                if (root == null)
                {
                    return;
                }
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
        }

        static void Main(string[] args)
        {
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
    }
}
