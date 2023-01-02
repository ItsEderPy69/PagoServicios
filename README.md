# PagoServicios
APLICACIÓN MÓVIL DE PAGO DE SERVICIOS.
Backend realizada Bajo el Framework .Net 6.0
Se utiliza Entity Framework Core 7.0.1
Se utiliza PostGreSQL 14.4

Con el BackUp se posee un Usuario por defecto "matiasbm002@gmail.com", password "123456"
Los controladores no poseen Autoriazacion debido a falta de tiempo.

Se puede acceder al swagger a través de http://localhost:5000/swagger/index.html
Se encuentra un BackUp en la raiz de la rama "Main", se encuentra una publicación comprimida en el mismo lugar.
Se modifica el String de Conexión a la base de datos desde el archivo appsettings.json
Al iniciar el Sistema se programo una inserción de cuentas pendientes de cobro por cada Usuario y por cada servicio para facilitar las pruebas.
Se puede importar el archivo http://localhost:5000/swagger/v1/swagger.json para realizar las pruebas desde postman o simplemente utilizar el swagger.
Estructura del Proyecto Agregado en el main

