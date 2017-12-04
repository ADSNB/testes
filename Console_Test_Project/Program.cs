using System;
using Console_Test_Project.Interfaces;
using Console_Test_Project.Classes;
using System.Data;

namespace Console_Test_Project
{
    class Program
    {
        static void Main(string[] args)
        {


            //var dt = null;
            //var dt = new Class();
            //IObject dt = new Class();
            var dt = new DataTable();
            //decimal? dt = null;
            //DataView dt = new DataView();

            dt.NewRow();

            var result = dt.NewRow();

            if (dt == null)
                Console.WriteLine("É nulo");
            else
                Console.WriteLine("É diferente de nulo.");

            Console.WriteLine(dt.ToString());
            Console.ReadKey();

            /*
            if (!dt)
                Console.WriteLine("Não é nulo");
            
            
            DateTime dat = new DateTime();
            DateTime? n = null;
            var dt = Data(n);

            Console.WriteLine(dt.ToString());
            Console.ReadKey();*/
        }

        public static DateTime Data(DateTime data)
        {
            return data;
        }
    }
}