using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReichBL;

namespace ReichUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Law law = new Law(2);
            List<Vote> list = law.Votes;
            Console.WriteLine("Beep");

            foreach(Vote vote in list)
            {
                Console.WriteLine(vote);
            }

            Console.ReadKey();
        }
    }
}
