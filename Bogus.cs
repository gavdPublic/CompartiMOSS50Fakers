using Bogus;
using ConsoleTables;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;

namespace Fakers
{
    class Bogus
    {
        public static void GeneradorParqueadero()
        {
            // Las reglas del juego
            Faker<Automovil> generadorDeAutos = new Faker<Automovil>()
                .RuleFor(ag => ag.PropietarioNombre, bog => bog.Name.FirstName())
                .RuleFor(ag => ag.PropietarioApellido, bog => bog.Name.LastName())
                .RuleFor(ag => ag.PropietarioEmail, (bog, ag) => bog.Internet.
                                        Email(ag.PropietarioNombre, ag.PropietarioApellido))
                .RuleFor(ag => ag.AutoTipo, bog => bog.PickRandom(TipoAuto.TodosLosAutos))
                .RuleFor(ag => ag.AutoFabricante, bog => bog.Vehicle.Manufacturer())
                .RuleFor(ag => ag.PrecioMensual, bog => bog.Random.Number(100, 200))
                .RuleFor(ag => ag.CuentaGarage, bog => bog.Finance.Account());

            // Genere una lista con los objetos
            List<Automovil> autosEnParqueadero = generadorDeAutos.Generate(10);

            // Prepare una tabla para mostrar los datos de la lista
            ConsoleTable myTabla = new ConsoleTable("Nombre", "Apellido", "Email", "Tipo",
                                            "Comentario", "Marca", "Mensualidad", "Cuenta");

            // Llene la lista con los datos
            foreach (Automovil unAuto in autosEnParqueadero)
            {
                myTabla.AddRow(unAuto.PropietarioNombre,
                    unAuto.PropietarioApellido,
                    unAuto.PropietarioEmail,
                    unAuto.AutoTipo.Tipo,
                    unAuto.AutoTipo.Descripcion,
                    unAuto.AutoFabricante,
                    unAuto.PrecioMensual,
                    unAuto.CuentaGarage);
            }

            // Muestre la lista por pantalla
            myTabla.Write();

            // Cree un archivo JSON y otro csv con los objetos generados
            File.WriteAllText(@"C:\Temporary\myAutos.json", JsonConvert.SerializeObject(autosEnParqueadero, Formatting.Indented));
            SaveToCsv<Automovil>(autosEnParqueadero, @"C:\Temporary\myAutos.csv");
        }

        private static void SaveToCsv<T>(List<T> reportData, string path)
        {
            var lines = new List<string>();
            IEnumerable<PropertyDescriptor> props = TypeDescriptor.GetProperties(typeof(T)).OfType<PropertyDescriptor>();
            var header = string.Join(",", props.ToList().Select(x => x.Name));
            lines.Add(header);
            var valueLines = reportData.Select(row => string.Join(",", header.Split(',').Select(a => row.GetType().GetProperty(a).GetValue(row, null))));
            lines.AddRange(valueLines);
            File.WriteAllLines(path, lines.ToArray());
        }
    }

    public class Automovil
    {
        public string PropietarioNombre { get; set; }
        public string PropietarioApellido { get; set; }
        public string PropietarioEmail { get; set; }
        public TipoAuto AutoTipo { get; set; }
        public string AutoFabricante { get; set; }
        public int PrecioMensual { get; set; }
        public string CuentaGarage { get; set; }
    }

    public class TipoAuto
    {
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public TipoTransamision Transmision { get; set; }

        public static TipoAuto Automovil = new TipoAuto() { Tipo = "Automovil", Descripcion = "Con cuatro ruedas", Transmision = TipoTransamision.Automatica };
        public static TipoAuto Camioneta = new TipoAuto() { Tipo = "Camioneta", Descripcion = "Muy grande", Transmision = TipoTransamision.Manual };
        public static TipoAuto AutoViejo = new TipoAuto() { Tipo = "Auto Viejo", Descripcion = "Austin", Transmision = TipoTransamision.Manual };
        public static TipoAuto Electrico = new TipoAuto() { Tipo = "Electrico", Descripcion = "Solo anda 400 km", Transmision = TipoTransamision.NoTiene };

        public static List<TipoAuto> TodosLosAutos = new List<TipoAuto>
        {
            Automovil,
            Camioneta,
            AutoViejo,
            Electrico
        };
    }

    public enum TipoTransamision
    {
        Automatica,
        Manual,
        NoTiene
    }
}
