using MatchHut.Helpers;
using System;

namespace MatchHut.Tools
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting MatchHut tools...");

            Console.WriteLine(string.Format("Super Admin Pass: {0}", SecurityHelper.HashPassword("admin123")));
        }
    }
}
