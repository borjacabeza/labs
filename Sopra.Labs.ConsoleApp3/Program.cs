using System;
using System.Linq;
using Sopra.Labs.Database;

namespace Sopra.Labs.ConsoleApp3
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
        /// Ejercicios realizados el 31/03/2022
        /// </summary>
        static void Ejercicios()
        {
            var context = new Northwind();

            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Clientes que residen en USA
            /////////////////////////////////////////////////////////////////////////////////
            
            // SELECT * FROM dbo.Customers WHERE Country = 'USA'

            var r1 = context.Customers
                .Where(r => r.Country == "USA")
                .ToList();


            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Proveedores (Suppliers) de Berlin
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Suppliers WHERE Country = 'Berlin'

            var r2 = context.Suppliers
                .Where(r => r.City == "Berlin")
                .ToList();


            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Empleados con identificadores 3, 5 y 8
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Employees WHERE EmployeeID IN (3, 5, 8)

            int[] employeesIds = new int[] { 3, 5, 8 };

            var r3a = context.Employees
                .Where(r => employeesIds.Contains(r.EmployeeID))
                .ToList();

            // SELECT * FROM dbo.Employees WHERE EmployeeID IN (3, 5, 8)

            var r3b = context.Employees
                .Where(r => new int[] { 3, 5, 8 }.Contains(r.EmployeeID))
                .ToList();

            // SELECT * FROM dbo.Employees WHERE EmployeeID = 3 OR EmployeeID = 5 OR EmployeeID = 8 

            var r3c = context.Employees
                .Where(r => r.EmployeeID == 3 || r.EmployeeID == 5 || r.EmployeeID == 8)
                .ToList();


            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Productos con stock mayor de cero
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Products WHERE UnitsInStock > 0

            var r4 = context.Products
                .Where(r => r.UnitsInStock > 0)
                .ToList();


            /////////////////////////////////////////////////////////////////////////////////////////////////
            // Listado de Productos con stock mayor de cero de los proveedores con identificadores 1, 3 y 5
            /////////////////////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Products WHERE SupplierID IN (1, 3, 5) 

            int?[] suppliersIds = new int?[] { 1, 3, 5 };

            var r5 = context.Products
                .Where(r => suppliersIds.Contains(r.SupplierID) && r.UnitsInStock > 0)
                .ToList();


            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Productos con precio mayor de 20 y menor 90
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Products WHERE UnitPrice > 20 AND UnitPrice < 90

            var r6 = context.Products
                .Where(r => r.UnitPrice > 20 && r.UnitPrice < 90)
                .ToList();


            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Pedidos entre 01/01/1997 y 15/07/1997
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * dbo.Orders WHERE OrderDate >= '1997/01/01' AND OrderDate <= '1997/09/15'

            var r7 = context.Orders
                .Where(r => r.OrderDate >= new DateTime(1997, 1, 1) && r.OrderDate <= new DateTime(1997, 7, 15))
                .ToList();


            /////////////////////////////////////////////////////////////////////////////////////////////////
            // Listado de Pedidos registrados por los empleados con identificador 1, 3, 4 y 8 en 1997
            /////////////////////////////////////////////////////////////////////////////////////////////////

            // SELECT * dbo.Orders WHERE YEAR(OrderDate) = 1997 AND EmployeeID IN (1, 3, 4, 8)

            int?[] employeesIds2 = new int?[] { 1, 3, 4, 8 };

            var r8 = context.Orders
                .Where(r => r.OrderDate.Value.Year == 1997 && employeesIds2.Contains(r.EmployeeID))
                .ToList();


            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Pedidos de abril de 1996
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * dbo.Orders WHERE YEAR(OrderDate) = 1996 AND MONTH(OrderDate) = 4

            var r9 = context.Orders
                .Where(r => r.OrderDate.Value.Year == 1996 && r.OrderDate.Value.Month == 4)
                .ToList();


            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Pedidos del realizado los dia uno de cada mes del año 1998
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * dbo.Orders WHERE YEAR(OrderDate) = 1998 AND MONTH(OrderDate) = 1

            var r10 = context.Orders
                .Where(r => r.OrderDate.Value.Year == 1998 && r.OrderDate.Value.Day == 1)
                .ToList();

            var r10b = context.Orders
                .Where(r =>
                    (r.OrderDate.HasValue ? r.OrderDate.Value.Year : -1) == 1998 &&
                    (r.OrderDate.HasValue ? r.OrderDate.Value.Day : -1) == 1)
                .ToList();


            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Clientes que no tiene fax
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Customers WHERE Fax = NULL

            var r11 = context.Customers
                .Where(r => r.Fax == null)
                .ToList();


            /////////////////////////////////////////////////////////////////////////////////
            // Listado de los 10 productos más baratos
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT TOP(10) * FROM dbo.Products ORDER BY UnitPrice

            var r12 = context.Products
                .OrderBy(r => r.UnitPrice)
                .Take(10)
                .ToList();


            /////////////////////////////////////////////////////////////////////////////////
            // Listado de los 10 productos más caros con stock
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT TOP(10) * FROM dbo.Products ORDER BY UnitPrice DESC

            var r13 = context.Products
                .Where(r => r.UnitsInStock > 0)
                .OrderByDescending(r => r.UnitPrice)
                .Take(10)
                .ToList();


            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Cliente de UK y nombre de empresa que comienza por B
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Customers WHERE CompanyName LIKE 'B%' AND Country = 'Uk'

            var r14 = context.Customers
                .Where(r => r.CompanyName.StartsWith("B") && r.Country == "Uk")
                .ToList();


            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Productos de identificador de categoria 3 y 5
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT TOP(10) * FROM dbo.Products WHERE CategoryID IN (3, 5)

            var r15 = context.Products
                .Where(r => new int?[] { 3, 5 }.Contains(r.CategoryID))
                .ToList();


            /////////////////////////////////////////////////////////////////////////////////
            // Importe total del stock
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT SUM(UnitInStock * UnitPrice) FROM Products

            var r16 = context.Products
                .Sum(r => r.UnitsInStock * r.UnitPrice);


            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Pedidos de los clientes de Argentina
            /////////////////////////////////////////////////////////////////////////////////            

            // SELECT CustomerID FROM dbo.Customers WHERE Country = 'Argentina'

            var argentinaIds = context.Customers
                    .Where(s => s.Country == "Argentina")
                    .Select(s => s.CustomerID)
                    .ToList();

            // SELECT * FROM dbo.Orders WHERE CustomerID IN ('CACTU', 'OCEAN', 'RANCH')

            var r17 = context.Orders
                .Where(r => argentinaIds.Contains(r.CustomerID))
                .ToList();


            // SELECT * FROM dbo.Orders WHERE CustomerID IN (SELECT CustomerID FROM dbo.Customers WHERE Country = 'Argentina')

            var r17a = context.Orders
                .Where(r =>
                    context.Customers
                        .Where(s => s.Country == "Argentina")
                        .Select(s => s.CustomerID)
                        .ToList()
                .Contains(r.CustomerID))
                .ToList();
        }
    }
}
