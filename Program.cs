using System;

namespace Fakers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Resultados de Bogus");
            Bogus.GeneradorParqueadero();
            Console.WriteLine("");
            Console.WriteLine("Resultados de Faker.NET");
            FakerNet.GeneradorUsuarios(5);

            Console.Read();
        }
    }
}
