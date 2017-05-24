using System;
using System.Linq;
using System.Threading;

namespace zdarzenia
{

    public delegate void MyEventHandler(int actualSize);

    class MyArray
    {

        public event MyEventHandler sizeChanged;
        int[] tab;
        int countOfElements;
        readonly object locker = new object();


        protected virtual void OnChanged()
        {
            sizeChanged?.Invoke(tab.Length);
        }

        public MyArray()
        {
            countOfElements = 0;
            tab = new int[4];
        }



        public int this[int el]
        {
            get
            {
                Monitor.Enter(locker);
                try
                {
                    if (el < countOfElements)
                    {
                        return tab[el];
                    }
                    else
                    {
                        throw new IndexOutOfRangeException();
                    }
                }
                finally { Monitor.Exit(locker); }
            }
            set
            {
                Monitor.Enter(locker);
                try
                {
                    if (el >= countOfElements)
                    {
                        Console.WriteLine("Dodawanie elementu poza tablicą.");
                        growTab(el + 1);
                        countOfElements = el + 1;
                    }
                    tab[el] = value;
                }
                finally { Monitor.Exit(locker); }
            }
        }

        public void Add(object el)
        {
            Monitor.Enter(locker);
            var watch = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                int count = tab.Count();
                if (countOfElements >= count)
                {
                    growTab(2 * count);
                }
                tab[countOfElements] = (int)el;
                countOfElements++;
                Console.WriteLine("dodano liczbę " + el.ToString());
                Console.WriteLine("aktualna ilość danych w tablicy: " + countOfElements);
                OnChanged();
            }
            finally {
                Monitor.Exit(locker);
                watch.Stop();
                var time = watch.ElapsedMilliseconds;
                Console.WriteLine("INFO: czas wykonania watku wraz z oczekiwnaiem " + time + " ms");
            }

        }
        public void AddNotBlocking(object el)
        {
            if (!Monitor.TryEnter(locker))
            {
                Console.WriteLine("INFO: W tym momencie nie dodano elementu. Element pominiety: " + el.ToString());
                return;
            }
            try
            {
                int count = tab.Count();
                if (countOfElements >= count)
                {
                    growTab(2 * count);
                }
                tab[countOfElements] = (int)el;
                countOfElements++;
                Console.WriteLine("dodano liczbę " + el.ToString());
                Console.WriteLine("aktualna ilość danych w tablicy: " + countOfElements);
                OnChanged();
            }
            finally
            {
                Monitor.Exit(locker);
            }

        }

        private void growTab(int newSize)
        {
            Monitor.Enter(locker);
            try
            {
                Console.WriteLine("Powiększanie tablicy ...");
                int count = tab.Count();
                Console.WriteLine("Stary rozmiar: " + count);
                Array.Resize(ref tab, newSize);
                Console.WriteLine("Zwiększono rozmiar do: " + tab.Count());
                OnChanged();
            }
            finally { Monitor.Exit(locker); }
        }

        private void save(string path)
        {

        }

    }
}
