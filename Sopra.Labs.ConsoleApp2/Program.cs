using System;
using System.Linq;

namespace Sopra.Labs.ConsoleApp2
{
    internal class Program
    {
        /// <summary>
        /// Inicio del programa
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Ejercicios();
        }

        /// <summary>
        /// Ejercicios realizados el 30/03/2022
        /// </summary>
        static void Ejercicios()
        {
            /////////////////////////////////////////////////////////////////////////////////
            // Clientes nacidos entre 1980 y 1990
            /////////////////////////////////////////////////////////////////////////////////

            var clientes1 = DataLists.ListaClientes
                .Where(x => x.FechaNac.Year >= 1980 && x.FechaNac.Year < 1990)
                .ToList();

            Console.WriteLine("CLIENTES: nacidos entre 1980 y 1990");
            Console.WriteLine("==================================================================");
            foreach (var cliente in clientes1) Console.WriteLine($"{cliente.Nombre}");
            Console.WriteLine("");


            /////////////////////////////////////////////////////////////////////////////////
            // Clientes mayores de 25 años
            /////////////////////////////////////////////////////////////////////////////////

            var clientes2 = DataLists.ListaClientes
                .Where(x => x.FechaNac.AddYears(25) <= DateTime.Now)
                .ToList();

            var clientes2b = DataLists.ListaClientes
                .Where(x => DateTime.Now.Subtract(x.FechaNac).Days / 365 > 25)
                .ToList();

            var clientes2c = DataLists.ListaClientes
                .Where(x => DateTime.Now.Year - x.FechaNac.Year > 25)
                .ToList();

            Console.WriteLine("CLIENTES: mayores de 25 años");
            Console.WriteLine("==================================================================");
            foreach (var cliente in clientes2) Console.WriteLine($"{cliente.Nombre}");
            Console.WriteLine("");


            /////////////////////////////////////////////////////////////////////////////////
            // Producto con el precio más alto
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT MAX(Precio) FROM ListaProductos

            var precioMax = DataLists.ListaProductos
                .Max(r => r.Precio);

            Console.WriteLine($"Precio Máximo: {precioMax.ToString("N2")}" + Environment.NewLine);

            // SELECT TOP(1) * FROM ListaProductos WHERE Precio = 12.54

            var producto = DataLists.ListaProductos
                .Where(r => r.Precio == precioMax)
                .FirstOrDefault();

            Console.WriteLine($"Precio máximo: {producto.Descripcion} - {producto.Precio.ToString("N2")}" + Environment.NewLine);

            // SELECT * FROM ListaProductos WHERE Precio = (SELECT MAX(Precio) FROM ListaProductos)

            var productos = DataLists.ListaProductos
                .Where(r => r.Precio == DataLists.ListaProductos.Max(r => r.Precio))
                .ToList();

            // SELECT * FROM ListaProductos WHERE Precio = 12.54

            var productos2 = DataLists.ListaProductos
                .Where(r => r.Precio == precioMax)
                .ToList();

            Console.WriteLine($"{productos.Count} productos con precio máximo" + Environment.NewLine);


            /////////////////////////////////////////////////////////////////////////////////
            // Precio medio de todos los productos
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT AVG(Precio) FROM ListaProductos

            var precioMedio = DataLists.ListaProductos
                .Select(r => r.Precio)
                .Average();

            var precioMedio2 = DataLists.ListaProductos
                .Average(r => r.Precio);

            Console.WriteLine($"Precio Medio: {precioMedio.ToString("N2")}" + Environment.NewLine);


            /////////////////////////////////////////////////////////////////////////////////
            // Productos con un precio inferior a la media
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM ListaProductos WHERE Precio = (SELECT AVG(Precio) FROM ListaProductos)

            var productos3 = DataLists.ListaProductos
               .Where(r => r.Precio < DataLists.ListaProductos.Average(r => r.Precio))
               .ToList();

            // SELECT * FROM ListaProductos WHERE Precio = 2.54

            var productos4 = DataLists.ListaProductos
               .Where(r => r.Precio == precioMedio)
               .ToList();

            Console.WriteLine($"PRODUCTOS: inferiores a {DataLists.ListaProductos.Average(r => r.Precio).ToString("N2")}");
            Console.WriteLine("==================================================================");
            foreach (var item in productos3) Console.WriteLine($"{item.Descripcion} {item.Precio.ToString("N2")}");
            Console.WriteLine("");
        }
    }
}
