*Integrantes del grupo: Lucila Dornes, Joaquina Aguilar, Milton Bustos, Cristian Acosta, Juan Bauer.

*Consideraciones:
-Para poder iniciar el sistema se debe clonar el repositorio: "https://github.com/JuanIBr85/DespedidaTP_UNLZ.git". Dentro del mismo, por motivos de seguridad, no se encuentran las claves de auntenticacion de Google (ClientId y Client Secret),
las mismas deberan ser cargadas en el archivo appsettings.json que se encuentra dentro de la carpera GestosEventos.WebUsuario, del repositorio. El formato a utilizar sera el siguiente:

      ClientId: "xxxxxxxxxxxxxxxxxxxxxxxxx.apps.googleusercontent.com",
      ClientSecret: "xxxxxxxxxxxxxxxxxxxxxxxxxxxx"

El proyecto esta conectado a una base de datos SQL Server en un servidor dentro de azure, por lo tanto para acceder se tendra que conectar utilizando las credenciales detalladas:

        Nombre de la base de datos = DespedidaDeSolteros-DB
          Servidor = 
            - Nombre del servidor = despedidadesolteros-server.database.windows.net
            - Inicio de sesión del administrador del servidor = Administrrador
            - Contraseña = Jimifloyd_22

Método de acceso a la base de datos: hemos utilizado Dapper para hacer peticiones a la base de datos.
Disposición de los datos a la web: se utilizo una API REST y Web App MVC





      
