$(document).ready(function () {
    actualizarUsuario();

    // 2. Marcar la última opción seleccionada como activa
    let ultimaOpcion = localStorage.getItem('menuSeleccionado');
    if (ultimaOpcion) {
        $(`[href="${ultimaOpcion}"]`).parent().addClass('active');
    }

    // 3. Manejar la selección del menú
    $(".navbar-nav a").on("click", function () {
        $(".navbar-nav .nav-item").removeClass("active"); // Quitar 'active' de todas
        $(this).parent().addClass("active");             // Agregar 'active' al seleccionado
        let opcionSeleccionada = $(this).attr("href");
        localStorage.setItem('menuSeleccionado', opcionSeleccionada); // Guardar selección
    });
});

// Función para actualizar el nombre del usuario en la interfaz
function actualizarUsuario() {
    let usuario = localStorage.getItem('usuario'); 
    if (usuario) {
        $("#usuarioNombre").text(`Usuario: ${usuario}`); 
    } else {
        $("#usuarioNombre").text("Usuario: Menu"); 
    }
}
