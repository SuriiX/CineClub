<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="webCinePelis.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pantalla Splash - CineClub</title>
    <style>
        body {
            margin: 0;
            padding: 0;
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
        }
        .splash-screen {
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            height: 25vh; /* Ocupa solo la mitad de la pantalla */
            background-color: #000;
            color: #f4f4f4;
            text-align: center;
            margin-top: 25vh; /* Centra verticalmente el splash en la pantalla */
            width: 100%;
            max-width: 100%;
        }
        .splash-screen-3-4 {
            height: 50vh; /* Ocupa la  mitad partes de la pantalla */
            margin-top: 12vh; /* Centra verticalmente el splash */
        }
        .logo {
            width: 250px; /* Ajusta el tamaño de tu logo */
            height: auto;
            margin-bottom: 20px;
        }
        .project-info {
            font-size: 20px;
            margin-bottom: 20px;
        }
        .integrantes {
            font-size: 18px;
            margin-bottom: 15px;
        }
        .fecha {
            font-size: 16px;
        }
    </style>
    <script>
        // Redirigir a frmLogin.html después de 3 segundos
        window.onload = function () {
            setTimeout(function () {
                window.location.href = "/paginas/frmLogin.html"
            }, 3000);
        };
    </script>
</head>
<body>
    <div class="splash-screen splash-screen-3-4">
        <!-- Logo: pon aquí la ruta de tu logo -->
        <img src="imagenes/descarga (27).jpeg" alt="Logo CineClub" class="logo"  />

        <h1>Bienvenido a CineClub</h1>

        <div class="project-info">
             <p><strong>Fecha:</strong> <span id="fecha"></span></p>
            <p><strong>Desarrolladores:</strong> Manuela Restrepo y Owen Pérez</p>
        </div>


    </div>

    <script>
        // Mostrar la fecha actual
        document.getElementById('fecha').innerHTML = new Date().toLocaleDateString();
    </script>
</body>
</html>
