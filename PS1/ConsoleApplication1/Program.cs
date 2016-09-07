using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaEvaluatorTest
{
    class Program
    {
        public static int lookUp(string x)
        {
            return 4;
        }

        public static IEnumerable<int> Scramble(int lo, int hi)
        {
            if (lo <= hi)
            {
                int mid = (lo + hi) / 2;
                yield return mid;
                foreach (int n in Scramble(mid + 1, hi))
                {
                    yield return n;
                }
                foreach (int n in Scramble(lo, mid - 1))
                {
                    yield return n;
                }
            }
        }
        public static int F(ref int x, ref int y)
        {
            x = 2;
            return x + y;
        }
        public static int G()
        {
            int a = 7;
            return F(ref a, ref a);
        }
        public static void swap(ref int n, ref int m)
        {
            int temp = n;
            n = m;
            m = temp;
        }

        public static IEnumerable<int> g()
        {
            for (int i = 0; i < 4; i++)
            {
                yield return i;
                Console.WriteLine(8 - i);
            }
            Console.WriteLine(4);
        }

        public static void f()
        {
            foreach (int n in g())
            {
                Console.WriteLine(n);
                if (n > 1) break;
            }
            //Console.ReadLine();
        }

        static void Main(string[] args)
        {
            //int[] A = { 2, 9 };
            //Console.WriteLine("Before " + A[0] + " " + A[1]);
            //swap(ref A[0], ref A[1]);
            //Console.WriteLine(" After " + A[0] + " " + A[1]);
            //foreach (int n in Scramble(0, 6))
            //{
            //    Console.Write(n);
            //}
            //Delcare variable
            FormulaEvaluator.Evaluator.Lookup lok = lookUp;
            
             //String test2 = "2 *3";
            //string test3 = "2";
            //string test4 = "A2";
            //string test5 = "A5 + 8";
            //string test6 = "3-2";
            //string test7 = "8/2";
            //string test8 = "3*5 +8";
            //string test9 = "3+5*8";
            //string test10 = "(3+5)*8";
            //string test11 = "3*(8+5)";
            //string test12 = "3+(10+8)";
            //string test13 = "3+(12+3*2)";
            //string test14 = "3+5*2-(3*4+8)*2+2";
            //string test15 = "3 * 2 +";
            //string test16 = "3*(2+3";
            //string test17 = "3+(5+(9+(2+3)))";
            //string test18 = "A5 - A5 +A5 *A5/A5";
            string test19 = "20/0";
            //string test20 = "-5+3";
            //string test21 = "";

           //Test1, test expression "5 + 2"
            try
            {
               int test = FormulaEvaluator.Evaluator.Evaluate(test19, lok);
               Console.WriteLine(test + " First test successful");
                Console.Read();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("First test failed");
                Console.Read();
            }
        }
    }
}
