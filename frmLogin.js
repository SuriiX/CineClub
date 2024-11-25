const apiUrl = "http://localhost:52814/api/usuario";

$(document).ready(function () {
    $("#btnIngresar").on("click", function () {
        Ingresar();
    });

    $("#btnLimpiar").on("click", function () {
        Limpiar();
    });
});

function mensajeError(texto) {
    $("#error-message").html(`<div style="color: red;">${texto}</div>`);
}

function mensajeOk(texto) {
    $("#error-message").html(`<div style="color: green;">${texto}</div>`);
}

async function Ingresar() {
    let usu = $("#username").val();
    let cla = $("#password").val();

    if (!usu.trim() || !cla.trim()) {
        mensajeError("Debe completar todos los campos.");
        return;
    }

    try {
        let response = await fetch("http://localhost:52814/api/usuario", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ clave: usu, contrasena: cla }),
        });

        if (!response.ok) {
            mensajeError(`Error HTTP: ${response.status} - ${response.statusText}`);
            return;
        }

        let resultado = await response.json();

        if (resultado === 0) {
            // Guardamos los valores de usuario y contraseña en localStorage
            localStorage.setItem('usuario', usu);
            localStorage.setItem('contrasena', cla);

            window.location.href = "frmInicio.html";
        } else if (resultado === 1) {
            mensajeError("Usuario o contraseña incorrectos.");
        } else {
            mensajeError("Error interno del servidor.");
        }
    } catch (error) {
        console.error("Error de conexión o de fetch:", error);
        mensajeError(`Error de conexión: ${error.message}`);
    }
}

function Limpiar() {
    $("#username").val("");
    $("#password").val("");
    $("#error-message").html("Limpio");
}
