# labs
Labs y Ejercicios
&nbsp;
&nbsp;
&nbsp;
### 29-Mar-2022 ***Sopra.Labs.ConsoleApp1***
* Calcular letra DNI
* Tabla de multiplicar
* Secuencia numérica
* Calcular valores
&nbsp;
---
&nbsp;
### 30-Mar-2022 ***Sopra.Labs.ConsoleApp2***
Consultas básicas de las colecciones del objeto DataList con LINQ
&nbsp;
* Clientes nacidos entre 1980 y 1990
* Clientes mayores de 25 años
* Producto con el precio más alto
* Precio medio de todos los productos
* Productos con un precio inferior a la media
&nbsp;
---
&nbsp;
### 31-Mar-2022 ***Sopra.Labs.ConsoleApp3***
Consultas básicas utilizando EntityFramerworkCore y LINQ
* Listado de Clientes que residen en USA
* Listado de Proveedores (Suppliers) de Berlin
* Listado de Empleados con identificadores 3, 5 y 8
* Listado de Productos con stock mayor de cero
* Listado de Productos con stock mayor de cero de los proveedores con identificadores 1, 3 y 5
* Listado de Productos con precio mayor de 20 y menor 90
* Listado de Pedidos entre 01/01/1997 y 15/07/1997
* Listado de Pedidos registrados por los empleados con identificador 1, 3, 4 y 8 en 1997
* Listado de Pedidos de abril de 1996
* Listado de Pedidos del realizado los dia uno de cada mes del año 1998
* Listado de Clientes que no tiene fax
* Listado de los 10 productos más baratos
* Listado de los 10 productos más caros con stock
* Listado de Cliente de UK y nombre de empresa que comienza por B
* Listado de Productos de identificador de categoria 3 y 5
* Importe total del stock
* Listado de Pedidos de los clientes de Argentina
&nbsp;
---
&nbsp;
### 01-Abr-2022 ***Sopra.Labs.ConsoleApp4***
* Listado de empleados mayores que su jefe (ReportsTo es id Jefe)
* Listado de productos: Nombre del Producto, Stock, Valor del Stock
* Listado de Empleados, nombre, apellidos, número de pedidos gestionado en 1997
* Tiempo medio en días para la preparación un pedido
* INCLUDE -> Listado de Empleados, nombre, apellidos, número de pedidos gestionado en 1997
* INCLUDE -> Productos de la categoría Condiments y Seafood 
* INCLUDE -> Listado de pedidos de los clientes de USA
* INTERSECT -> Listado de IDs Clientes que ha pedido el producto 57 y el producto 72 en el año 1997
* INTERSECT -> Listado de Clientes que ha pedido el producto 57 y el producto 72 en el año 1997, id y nombre de empresa
* GROUPBY -> Listado de clientes agrupados por país
* GROUPBY -> Listado de pedidos con su importe total
&nbsp;
---
&nbsp;
### 04-Abr-2022 y 05-Abr-2022 ***Sopra.Labs.ConsoleApp5***
* Geo-localizar direcciones IP
* Consultar información códigos postales
* Consultar autobuses/paradas EMT
* Consultar plazas parkings de Madrid
&nbsp;
---
&nbsp;
### del 29-Abr al 01-May ***EJERCICIOS EXTRA FIN DE SEMANA***
* Listado de unidades vendidas de cada producto ordenado por número total de unidad
* SELECT ProductID, SUM(Quantity) AS Quantity FROM dbo.[Order Details] GROUP BY ProductID ORDER BY Quantity
---
* Importe facturado por cada producto ordenado por producto
* SELECT ProductID, SUM(Quantity * UnitPrice) AS Quantity FROM dbo.[Order Details] GROUP BY ProductID ORDER BY ProductID
---
* En la tabla Orders tenemos los gastos de envio en el campo ***Freight***.
* Listado de Pedidos agrupado por Empleado, con número de pedidos, importe total factura en concepto de gastos de envio
---
* En la tabla Orders tenemos el identificador de la empresa de transportes ***ShipVia***.
* Número de pedidos enviado por cada empresa de transporte.
---
* Listado de pedido enviados por la empresa 3 que incluya el OrderID y número de lineas de pedido (registros en Orders_Details)
