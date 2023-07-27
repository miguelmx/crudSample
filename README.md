# crudSample

 Prueba de concepto utilizando las clases Autor y Libro para desmostrar la creacion de una api rest con algunos endpoints que incluya autenticacion basica utilizando Microsoft.AspNetCore.Authentication con un usuario estatico utilizando el decordor [Authorize] en los endpoints que lo requieran e informacion para generar documentacion para Swagger (incluido en el proyecto) para pruebas de los endpoints.

 * Cree una base de datos en Microsoft SQL Server en tu instancia local llamada crudSample
 * Crea un usuario de SQL llamado crudsample
 * Asigna el password SimplePassword123 a ese usuario
 * Da acceso como db_owner a crusdample a la base de datos crudsample
 * Asignale la misma base de datos como predeterminada y lenguaje español
 * Clona esta solucion en Visual Studio usando la url https://github.com/miguelmx/crudSample.git
 * En la ventana de comandos ejecute "update-database", esto creara las instancias necesarias (Autor, Libro) en la base de datos

* Ejecuta la aplicacion

Para mas informacion de las entidades y los endpoints, la aplicacion contiene una instancia de Swagger que te proporcionara informacion de las entidades, endpoints, sus parametros y respuestas esperadas
