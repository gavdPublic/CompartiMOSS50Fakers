using ConsoleTables;
using Faker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakers
{
    class FakerNet
    {
        public static void GeneradorUsuarios(int NumeroDeUsuarios)
        {
            List<Usuario> misUsuarios = new List<Usuario>();

            for (int unUsuario = 1; unUsuario <= NumeroDeUsuarios; unUsuario++)
            {
                Usuario myUsuario = new Usuario();

                myUsuario.Nombre = Faker.Name.FullName(NameFormats.WithPrefix);
                myUsuario.Ciudad = $"{Faker.Address.Country()}, {Faker.Address.City()}";
                myUsuario.Direccion = $"{Faker.Address.StreetAddress()}, {Faker.Address.ZipCode()}";
                myUsuario.Bio = String.Join(" ", Faker.Lorem.Words(2));
                myUsuario.EstadoCivil = Faker.Enum.Random<EstadoCivil>();
                myUsuario.Compannia = Faker.Internet.DomainName();
                myUsuario.Email = Faker.Internet.Email(myUsuario.Nombre);
                myUsuario.Edad = Faker.RandomNumber.Next(40, 18);

                misUsuarios.Add(myUsuario);
            }

            // Prepare una tabla para mostrar los datos de la lista
            ConsoleTable myTabla = new ConsoleTable("Nombre", "Ciudad", "Direccion", "Bio",
                                            "Estado Civil", "Compania", "Email", "Edad");

            // Llene la lista con los datos
            foreach (Usuario unUsuario in misUsuarios)
            {
                myTabla.AddRow(unUsuario.Nombre,
                    unUsuario.Ciudad,
                    unUsuario.Direccion,
                    unUsuario.Bio,
                    unUsuario.EstadoCivil,
                    unUsuario.Compannia,
                    unUsuario.Email,
                    unUsuario.Edad);
            }

            // Muestre la lista por pantalla
            myTabla.Write();
        }
    }

    public class Usuario
    {
        public string Nombre { get; set; }
        public string Ciudad { get; set; }
        public string Direccion { get; set; }
        public string Bio { get; set; }
        public EstadoCivil EstadoCivil { get; set; }
        public string Compannia { get; set; }
        public string Email { get; set; }
        public int Edad { get; set; }

    }

    public enum EstadoCivil
    {
        Soldero,
        Casado,
        Viudo,
        Desconocido
    }
}
