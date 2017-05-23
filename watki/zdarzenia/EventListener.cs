using System;

namespace zdarzenia
{
    class EventListener
    {
        private MyArray myArray;
        public EventListener(MyArray myArray)
        {
            this.myArray = myArray;
            myArray.sizeChanged += new MyEventHandler(TableChanged);
        }
        private void TableChanged(int actualSize)
        {
            Console.WriteLine("[!EVENT!]: Aktualny rozmiar tablicy: " + actualSize);
        }
        public void Detach()
        {
            myArray.sizeChanged -= new MyEventHandler(TableChanged);
            myArray = null;
        }
    }
}

