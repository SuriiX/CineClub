$(document).ready(function () {
    // Carga el menú
    $("#dvMenu").load("../Paginas/Menu.html", function () {
        // Una vez cargado el menú, actualiza el nombre de usuario
        actualizarUsuario();
    });
});


function actualizarUsuario() {
    let usuario = localStorage.getItem('usuario'); 
    if (usuario) {
        $("#usuarioNombre").text(`Usuario: ${usuario}`); 
    } else {
        $("#usuarioNombre").text("Usuario: Lagado"); 
    }
}