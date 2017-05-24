using System;
using System.Threading;
using System.Windows.Forms;

namespace zdarzenia
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MyArray tab = new MyArray();
            EventListener eventListener = new EventListener(tab);
            for (int i = 0; i < 100; i++)
            {
                ParameterizedThreadStart parameterizedThreadStart = new ParameterizedThreadStart(tab.Add);
                Thread myThread = new Thread(parameterizedThreadStart);
                myThread.Start(myThread.ManagedThreadId);
            }
            for (int i = 0; i < 100; i++)
            {
                ParameterizedThreadStart parameterizedThreadStart2 = new ParameterizedThreadStart(tab.AddNotBlocking);
                Thread myThread2 = new Thread(parameterizedThreadStart2);
                myThread2.Start(myThread2.ManagedThreadId);
            }
            /*tab.Add(1);
            tab.Add(2);
            tab.Add(5);
            tab.Add(54);
            tab.Add(523);
            tab.Add(235);
            tab.Add(615);
            tab.Add(235);
            tab.Add(615);
            tab.Add(35);
            tab.Add(65);
            tab.Add(125);
            tab[634] = 43;
            try
            {
                Console.WriteLine(tab[635]);
            }
            catch(IndexOutOfRangeException)
            {
                Console.WriteLine("Błąd: Zły indeks ");
            }
            Console.WriteLine(tab[632]);
            Console.WriteLine(tab[32]);
            Console.WriteLine(tab[156]);
            Console.WriteLine(tab[9]);
            tab.Add(23);
            Console.WriteLine(tab[635]);*/
        }



    }
}
