﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IniciarSesion.aspx.cs" Inherits="SISTEMATICKET.IniciarSesion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Assets/Plugins/bootstrap.4.5.2/bootstrap.min.css" rel="stylesheet" />
    <link href="css/IniciarSesion/Styles.css" rel="stylesheet" />
    <link href="Assets/Plugins/Simple_Line_Icons/simple-line-icons.min.css" rel="stylesheet" />
    <link href="Assets/Plugins/bootstrap-icons-1.2.2/font/bootstrap-icons.css" rel="stylesheet" />
    <script src="Assets/Plugins/jquery/jquery.3.5.1.min.js"></script>
    <script src="Assets/Plugins/bootstrap.4.5.2/bootstrap.min.js"></script>

    
    

<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    
    <div class="area" >
            <ul class="circles">
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>

    <div class="registration-form">
        <form>
            <div class="form-icon">
                <span><i class="bi bi-person-fill"></i></span>
            </div>
            <div class="form-group">
                <input type="text" style="border-radius: 20px;background-color:#d0d0d0;border-color:#CA515C;box-shadow: 3px 3px 3px 3px"; class="form-control item" id="username" placeholder="Usuario" value="Admin"/>
            </div>
            <div class="form-group">
                <input type="password" style="border-radius: 20px;background-color:#d0d0d0;border-color:#CA515C;box-shadow: 3px 3px 3px 3px"; class="form-control item" id="password" placeholder="Contraseña" value="Admin123"/>
            </div>
            <div class="form-group">
                <button style="background-color:#CA515C" id="btnIniciarSesion" type="button" class="btn btn-block btn-danger create-account">Iniciar Sesión</button>
            </div>
        </form>
        <div class="social-media">
            <p>© Todos los derechos reservados</p>
        </div>
    </div>
  </div >
                </ul>




    <script src="Controlador/IniciarSesion/IniciarSesion.js"></script>
    <script src="Controlador/Utilidades.js"></script>
    <script src="Assets/Plugins/loadingoverlay/loadingoverlay.js"></script>

    <link href="Assets/Plugins/sweetalert2/sweetalert.css" rel="stylesheet" />
    <script src="Assets/Plugins/sweetalert2/sweetalert.js"></script>
</body>
</html>
