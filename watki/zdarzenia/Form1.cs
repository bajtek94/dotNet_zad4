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
            tab.save();
        }



    }
}
