using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Sopra.Labs.Database;

namespace Sopra.Labs.ConsoleApp4
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
        /// Ejercicios realizados el 01/04/2022
        /// </summary>
        static void Ejercicios()
        {
            var context = new Northwind();

            /////////////////////////////////////////////////////////////////////////////////
            // Listado de empleados mayores que su jefe (ReportsTo es id Jefe)
            /////////////////////////////////////////////////////////////////////////////////

            //  SELECT r.EmployeeID, r.FirstName, r.LastName, r.BirthDate, r.ReportsTo FROM dbo.Employees r WHERE r.BirthDate < (SELECT s.BirthDate FROM dbo.Employees s WHERE s.EmployeeID = r.ReportsTo)

            var r1 = context.Employees
                .Where(r => r.BirthDate < context.Employees.Where(s => s.EmployeeID == r.ReportsTo).Select(s => s.BirthDate).FirstOrDefault())
                .ToList();

            foreach (var item in r1) Console.WriteLine($"{item.FirstName} {item.LastName}");

            /////////////////////////////////////////////////////////////////////////////////
            // Listado de productos: Nombre del Producto, Stock, Valor del Stock
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT ProductName, UnitsInStock, (r.UnitPrice * r.UnitsInStock) AS Total FROM dbo.Products

            var r2 = context.Products
                .Select(r => new { r.ProductName, r.UnitsInStock, Total = r.UnitPrice * r.UnitsInStock });

            foreach (var item in r2)
                Console.WriteLine($"{item.ProductName.PadRight(35)} {item.UnitsInStock.ToString().PadLeft(5)} unidades {item.Total.ToString().PadLeft(11)}");



            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Empleados, nombre, apellidos, número de pedidos gestionado en 1997
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT r.FirstName, r.LastName, (SELECT COUNT(*) FROM dbo.Orders s WHERE YEAR(s.OrderDate) = 1997 AND s.EmployeeID = r.EmployeeID) AS Orders FROM dbo.Employees r

            var r3 = context.Employees
                .Select(r => new
                {
                    r.FirstName,
                    r.LastName,
                    Orders = context.Orders
                        .Count(s => s.EmployeeID == r.EmployeeID && s.OrderDate.Value.Year == 1997)
                }).ToList();

            foreach (var item in r3)
            {
                Console.Write($"{item.FirstName} {item.LastName}".PadRight(25));
                Console.WriteLine($"{item.Orders.ToString().PadLeft(5)} pedidos");
            }


            /////////////////////////////////////////////////////////////////////////////////
            // Tiempo medio en días para la preparación un pedido
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT AVG(ShippedDate - OrderDate) FROM dbo.Orders

            var r4a = context.Orders
                .Where(r => r.ShippedDate != null && r.OrderDate != null)
                .AsEnumerable()
                .Average(r => (r.ShippedDate.Value - r.OrderDate.Value).Days);

            var r4b = context.Orders
                .Where(r => r.ShippedDate != null && r.OrderDate != null)
                .AsEnumerable()
                .Average(r => (r.ShippedDate - r.OrderDate).Value.Days);

            var r4d = context.Orders
                .Where(r => r.ShippedDate != null && r.OrderDate != null)
                .AsEnumerable()
                .Average(r => r.ShippedDate.Value.Subtract(r.OrderDate.Value).Days);

            Console.WriteLine($"Tiempo medio de Preparación: {r4a.ToString("N2")}");



            /********************************************
             *  Sentecias de LINQ que utilizan INCLUDE  * 
             ********************************************/

            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Empleados, nombre, apellidos, número de pedidos gestionado en 1997
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT r.FirstName, r.LastName, (SELECT COUNT(*) FROM dbo.Orders s WHERE YEAR(s.OrderDate) = 1997 AND s.EmployeeID = r.EmployeeID) AS Orders FROM dbo.Employees r

            var i1 = context.Employees
                .Include(r => r.Orders)
                .Select(r => new { r.FirstName, r.LastName, r.Orders.Count })
                .ToList();


            /////////////////////////////////////////////////////////////////////////////////
            // Productos de la categoría Condiments y Seafood
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Products WHERE CategoryID IN (SELECT CategoryID FROM dbo.Categories WHERE CategoryName IN('Condiments', 'Seafood'))

            var i2 = context.Products
                .Include(r => r.Category)
                .Where(r => new string[] { "Condiments", "Seafood" }.Contains(r.Category.CategoryName))
                .ToList();


            /////////////////////////////////////////////////////////////////////////////////
            // Listado de pedidos de los clientes de USA
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Orders WHERE CustomerID IN (SELECT CustomerID FROM dbo.Customers WHERE Country = 'USA')

            var i3 = context.Orders
                .Include(r => r.Customer)
                .Where(r => r.Customer.Country == "USA")
                .ToList();



            /**********************************************
             *  Sentecias de LINQ que utilizan INTERSECT  * 
             **********************************************/

            /////////////////////////////////////////////////////////////////////////////////
            // Listado de IDs Clientes que ha pedido el producto 57 y el producto 72 en el año 1997
            /////////////////////////////////////////////////////////////////////////////////

            var c1 = context.Order_Details
                .Include(r => r.Order)
                .Where(r => r.ProductID == 57)
                .Select(r => r.Order.CustomerID)
                .ToList();

            var c2 = context.Order_Details
                .Include(r => r.Order)
                .Where(r => r.ProductID == 72 && r.Order.OrderDate.Value.Year == 1997)
                .Select(r => r.Order.CustomerID)
                .ToList();

            var customers = c1.Intersect(c2);

            foreach (var id in customers) Console.WriteLine($"{id}");
            Console.ReadKey();

            /////////////////////////////////////////////////////////////////////////////////

            // SELECT CustomerID FROM dbo.Orders WHERE OrderID IN (SELECT OrderID FROM [Order Details] WHERE ProductID = 57) INTERSECT SELECT CustomerID FROM dbo.Orders WHERE OrderID IN (SELECT OrderID FROM [Order Details] WHERE ProductID = 72 AND YEAR(OrderDate) = 1997)

            customers = context.Order_Details
                .Include(r => r.Order)
                .Where(r => r.ProductID == 57)
                .Select(r => r.Order.CustomerID)
                .ToList()
                .Intersect(context.Order_Details
                    .Include(s => s.Order)
                    .Where(s => s.ProductID == 72 && s.Order.OrderDate.Value.Year == 1997)
                    .Select(s => s.Order.CustomerID)
                    .ToList());

            foreach (var id in customers) Console.Write($"{id}  ");
            Console.WriteLine(Environment.NewLine);
            Console.ReadKey();


            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Clientes que ha pedido el producto 57 y el producto 72 en el año 1997, id y nombre de empresa
            /////////////////////////////////////////////////////////////////////////////////

            var customers2 = context.Order_Details
                .Include(r => r.Order)
                .Include(r => r.Order.Customer)
                .Where(r => r.ProductID == 57)
                .Select(r => new { r.Order.CustomerID, r.Order.Customer.CompanyName })
                .ToList()
                .Intersect(context.Order_Details
                    .Include(s => s.Order)
                    .Include(s => s.Order.Customer)
                    .Where(s => s.ProductID == 72 && s.Order.OrderDate.Value.Year == 1997)
                    .Select(s => new { s.Order.CustomerID, s.Order.Customer.CompanyName })
                    .ToList());

            foreach (var id in customers2) Console.WriteLine($"{id.CustomerID} {id.CompanyName}");



            /********************************************
             *  Sentecias de LINQ que utilizan GROUPBY  * 
             ********************************************/

            /////////////////////////////////////////////////////////////////////////////////
            // Listado de clientes agrupados por país
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT Country, COUNT(*) FROM dbo.Customers GROUP BY Country

            var customers3 = context.Customers
                .AsEnumerable()
                .GroupBy(g => g.Country)
                .Select(g => g)
                .ToList();

            foreach (var group in customers3)
            {
                Console.WriteLine($"==========================================");
                Console.WriteLine($" Clientes de {group.Key}: {group.Count()}");
                Console.WriteLine($"==========================================");

                foreach (var customer in group) Console.WriteLine($"{customer.CustomerID} {customer.CompanyName}");
            }


            /////////////////////////////////////////////////////////////////////////////////
            // Listado de pedidos con su importe total
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT OrderID, SUM(UnitPrice * Quantity) FROM dbo.[Order Details] GROUP BY OrderID

            var orders = context.Order_Details
                .AsEnumerable()
                .GroupBy(r => r.OrderID)
                .Select(r => new { r.Key, TotalPrice = r.Sum(s => s.UnitPrice * s.Quantity) })
                .ToList();

            foreach (var group in orders)
            {
                Console.Write($" Pedido Número: {group.Key.ToString().PadLeft(5)}");
                Console.WriteLine($" Importe: {group.TotalPrice.ToString().PadLeft(9)}");
            }
        }
    }
}
