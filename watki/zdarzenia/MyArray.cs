using System;
using System.Linq;

namespace zdarzenia
{

    public delegate void MyEventHandler(int actualSize);

    class MyArray
    {

        public event MyEventHandler sizeChanged;
        
        protected virtual void OnChanged()
        {
            sizeChanged?.Invoke(tab.Length);
        }


        int[] tab;
        int countOfElements;

        public MyArray()
        {
            countOfElements = 0;
            tab = new int[4];
        }

        

        public int this[int el]
        {
            get
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
            set
            {
                if (el >= countOfElements)
                {
                    Console.WriteLine("Dodawanie elementu poza tablicą.");
                    growTab(el + 1);
                    countOfElements = el + 1;
                }
                tab[el] = value;
            }
        }

        public void Add(int el)
        {
            int count = tab.Count();
            if (countOfElements >= count)
            {
                growTab(2 * count);
            }
            tab[countOfElements] = el;
            countOfElements++;
            Console.WriteLine("dodano liczbę " + el);
            Console.WriteLine("aktualna ilość danych w tablicy: " + countOfElements);
            OnChanged();
        }

        private void growTab(int newSize)
        {
            Console.WriteLine("Powiększanie tablicy ...");
            int count = tab.Count();
            Console.WriteLine("Stary rozmiar: " + count);
            Array.Resize(ref tab, newSize);
            Console.WriteLine("Zwiększono rozmiar do: " + tab.Count());
            OnChanged();
        }
    }
}
