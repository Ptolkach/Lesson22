using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Func<Task<int[]>, int> func2 = new Func<Task<int[]>, int>(SumArray);
            Task<int> task2 = task1.ContinueWith<int>(func2);

            Func<Task<int[]>, int> func3 = new Func<Task<int[]>, int>(MaxArray);
            Task<int> task3 = task1.ContinueWith<int>(func3);

            task1.Start();
            Console.ReadLine();

        }

        // в методах выводы на консоль, чтобы наглядно было видно, что они работают ))

        static int[] GetArray(object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = rnd.Next(0, 100);
            }
            foreach (int i in array)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
            return array;
        }

        static int SumArray(Task<int[]> task)
        {
            int[] array = task.Result;
            int sa = array.ToList().Sum();
            Console.WriteLine(sa);
            return sa;
            //return array.ToList().Sum();
        }
        static int MaxArray(Task<int[]> task)
        {
            int[] array = task.Result;
            int ma = array.ToList().Max();
            Console.WriteLine(ma);
            return ma;
            //return array.ToList().Max();
        }
    }
}
