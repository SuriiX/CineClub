var dir = "http://localhost:52814/api/";
var oTabla = $("#tablaDatos").DataTable();

jQuery(function () {
    // Carga el menú
    $("#dvMenu").load("../Paginas/Menu.html");
    llenarComboEjemplar();
    llenarComboAlquiler();
    actualizarUsuario();
    // Registrar los botones para responder al evento click
    $("#btnAgre").on("click", function () {
        mensajeInfo("");
        let codigo = $("#txtCodigo").val();
        let cantidad = $("#txtCantidad").val();

        if (cantidad.trim() == "" || codigo.trim() == "" || parseInt(codigo, 10) <= 0 || parseInt(cantidad, 10) <= 0) {
            mensajeError("Debe ingresar todos los campos");
            $("#txtCodigo").focus();
            return;
        } else {
            let rpta = window.confirm("¿Estás seguro de agregar el alquiler: " + codigo + "?");
            if (rpta) {
                ejecutarComando("POST");
            } else {
                mensajeInfo("Acción de agregar cancelada");
            }
        }
    });

    $("#btnModi").on("click", function () {
        let codigo = $("#txtCodigo").val();
        let cantidad = $("#txtCantidad").val();

        if (cantidad.trim() == "" || codigo.trim() == "" || parseInt(codigo, 10) <= 0 || parseInt(cantidad, 10) <= 0) {
            mensajeError("Debe ingresar todos los campos");
            $("#txtCodigo").focus();
            return;
        } else {
            let rpta = window.confirm("¿Estás seguro de modificar el alquiler: " + codigo + "?");
            if (rpta) {
                ejecutarComando("PUT");
            } else {
                mensajeInfo("Acción de modificar cancelada");
            }
        }
    });

    $("#btnEliminar").on("click", async function () {
        let filaSeleccionada = $("#tablaDatos tbody tr.selected");
        let nombre = $("#txtNombre").val();
        if (filaSeleccionada.length === 0) {
            mensajeError("Debe seleccionar una fila para eliminar.");
            return;
        }

        // Obtener el identificador único de la fila (Código)
        let codigo = filaSeleccionada.find("td:eq(1)").text();

        // Confirmar la eliminación
        let confirmacion = window.confirm("¿Está seguro de eliminar el registro con Código: " + codigo + "?");
        if (!confirmacion) {
            mensajeInfo("Acción de eliminación cancelada.");
            return;
        }

        // Llamar a la función de eliminación
        await eliminarRegistro(codigo, filaSeleccionada);
        mensajeInfo("Ha sido eliminado con exito " + codigo);
    });

    $("#btnBusc").on("click", function () {
        mensajeInfo("");
        Consultar();
    });

    $("#btnCanc").on("click", function () {
        mensajeInfo("");
        limpiarFormulario();
    });


    $("#tablaDatos tbody").on("click", 'tr', function (evento) {
        // Levanta el evento click sobre la tabla
        if ($(this).hasClass('selected')) {
            evento.preventDefault();
            $(this).removeClass('selected');
        } else {
            oTabla.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
            editarFila($(this).closest('tr'), evento);
        }
    });

});
function actualizarUsuario() {
    let usuario = localStorage.getItem('usuario');
    if (usuario) {
        $("#usuarioNombre").text(`Usuario: ${usuario}`);
    } else {
        $("#usuarioNombre").text("Usuario: No logueado");
    }
}
function mensajeError(texto) {
    $("#dvMensaje").removeClass("alert alert-success");
    $("#dvMensaje").addClass("alert alert-danger");
    $("#dvMensaje").html(texto);
}

function mensajeInfo(texto) {
    $("#dvMensaje").removeClass("alert alert-success");
    $("#dvMensaje").addClass("alert alert-info");
    $("#dvMensaje").html(texto);
}

function mensajeOk(texto) {
    $("#dvMensaje").removeClass("alert alert-success");
    $("#dvMensaje").addClass("alert alert-success");
    $("#dvMensaje").html(texto);
}

function editarFila(datosFila, evt) {
    evt.preventDefault();

    datosFila.addClass("selected-row");
    let codigo = datosFila.find("td:eq(1)").text();
    let cantidad = datosFila.find("td:eq(2)").text();
    let fechaInicio = datosFila.find("td:eq(3)").text().split("T")[0];
    let fechaFinal = datosFila.find("td:eq(4)").text().split("T")[0];
    let valorAlquiler = datosFila.find("td:eq(5)").text();
    let ejemplar = datosFila.find("td:eq(6)").text(); // Aquí está el texto que necesitas
    let cliente = datosFila.find("td:eq(7)").text();

    $("#txtCodigo").val(codigo);
    $("#txtCantidad").val(cantidad);
    $("#txtfechaInicio").val(fechaInicio);
    $("#txtfechaFinal").val(fechaFinal);
    $("#txtValorAlquiler").val(valorAlquiler);

    // Buscar el value correspondiente al texto de ejemplar
    $("#cboEjemplar option").each(function () {
        if ($(this).text() === ejemplar) {
            $("#cboEjemplar").val($(this).val()); // Asigna el value al combo
        }
    });
    $("#cboAlquiler option").each(function () {
        if ($(this).text() === cliente) {
            $("#cboAlquiler").val($(this).val()); // Asigna el value al combo
        }
    });

    

    mensajeOk("Datos cargados en el formulario.");
}


async function eliminarRegistro(codigo) {
    if (!codigo) {
        mensajeError("Debe proporcionar un código válido para eliminar.");
        return;
    }

    if (!window.confirm(`¿Estás seguro de eliminar el registro con Código ${codigo}?`)) {
        mensajeInfo("Eliminación cancelada.");
        return;
    }

    try {
        // Enviar solicitud DELETE con el código de la película
        let response = await fetch(dir + "alquilar/" + codigo, {
            method: "DELETE",
            headers: {
                "Content-Type": "application/json",
            },
        });

        const resultado = await response.text(); // Leer respuesta como texto
        if (response.ok) {
            // Eliminar la fila visualmente si la respuesta es exitosa
            $(`#tablaDatos td:contains(${codigo})`).closest('tr').remove();
            mensajeOk(resultado); // Mostrar mensaje de éxito desde el servidor
        } else {
            mensajeError("Error al eliminar: " + resultado);
        }
    } catch (error) {
        mensajeError("Error al conectar con el servidor: " + error.message);
    }
}

async function llenarTabla() {
    let rpta = await llenarTablaGral(dir + "alquilar?id=0&comando=1", "#tablaDatos");
}

async function llenarComboEjemplar() {
    let url = dir + "ejemplar?id=0&comando=1";
    let rpta = await llenarComboGral(url, "#cboEjemplar");
}

async function llenarComboAlquiler() {
    let url = dir + "alquiler?id=0&comando=1";
    let rpta = await llenarComboGral(url, "#cboAlquiler");
}

async function Consultar() {
    mensajeInfo("Se va a consultar");
    try {
        let codigo = $("#txtCodigo").val();
        if (codigo == undefined || codigo.trim() == "") {
            llenarTabla();
            mensajeInfo("Error, no existe el codigo");
            return;
        }

        const datosOut = await fetch(dir + "alquilar?id=" + codigo + "&comando=2", {
            method: "GET",
            mode: "cors",
            headers: {
                "content-type": "application/json",
            }
        });
        const datosIn = await datosOut.json();
        if (datosIn == "") {
            mensajeInfo("No se encontró película con el código: " + codigo);
            return;
        }
        $("#txtCodigo").val(datosIn[0].Codigo);
        $("#txtCantidad").val(datosIn[0].Cantidad);
        $("#txtValorAlquiler").val(datosIn[0].ValorAlquiler);
        $("#txtfechaInicio").val(datosIn[0].FechaInicio.split("T")[0]);
        $("#txtfechaFinal").val(datosIn[0].FechaFinal.split("T")[0]);
        $("#cboEjemplar").val(datosIn[0].Id_Ejemplar);
        $("#cboAlquiler").val(datosIn[0].Id_Alquiler);
    } catch (error) {
        mensajeError("Error: " + error);
    }
}

async function ejecutarComando(accion) {
    let codigo = $("#txtCodigo").val();
    let cantidad = $("#txtCantidad").val();
    let valorAlquiler = $("#txtValorAlquiler").val();
    let fechaInicio = $("#txtfechaInicio").val();
    let fechaFinal = $("#txtfechaFinal").val();
    let idEjemplar = $("#cboEjemplar").val();
    let idAlquiler = $("#cboAlquiler").val();

    if (!codigo || !cantidad || !valorAlquiler || !fechaInicio || !fechaFinal || !idEjemplar || !idAlquiler) {
        mensajeError("Por favor, completa todos los campos.");
        return;
    }

    if (!validarFecha(fechaInicio,fechaFinal)) {
        return; 
    }

    let datosOut = {
        Codigo: parseInt(codigo, 10),
        Cantidad: parseInt(cantidad, 10),
        Fecha_Inicio: fechaInicio,
        Fecha_Fin: fechaFinal,
        Vlr_Alquiler: parseFloat(valorAlquiler),
        Id_PeliculaEjem: parseInt(idEjemplar, 10),
        Id_Alquiler: parseInt(idAlquiler, 10),
    };

    try {
        const response = await fetch(dir + "alquilar", {
            method: accion,
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(datosOut),
        });

        if (!response.ok) {
            throw new Error(`Error ${response.status}: ${response.statusText}`);
        }

        const Respuesta = await response.json();
        Consultar(); 
        llenarTabla();
        mensajeOk(Respuesta);
    } catch (error) {
        mensajeError("Error: " + error.message);
    }
}

 function validarFecha(fechaInicio,fechaFinal) {
     const fechaInicioIngresada = new Date(fechaInicio); // Convierte la fecha de inicio a un objeto Date
     const fechaFinalIngresada = new Date(fechaFinal);   // Convierte la fecha final a un objeto Date
     const fechaActual = new Date();
     const fechaMinima = new Date("1985-01-01");
     if (isNaN(fechaInicioIngresada.getTime()) || isNaN(fechaFinalIngresada.getTime())) {
         mensajeError("Por favor, ingresa fechas válidas.");
         return false;
     }

     // Validar que la fecha de inicio esté dentro del rango permitido
     if (fechaInicioIngresada < fechaMinima || fechaInicioIngresada > fechaActual) {
         mensajeError("La fecha de inicio debe estar entre 1985 y la fecha actual.");
         return false;
     }

     // Validar que la fecha final esté dentro del rango permitido
     if (fechaFinalIngresada < fechaMinima || fechaFinalIngresada > fechaActual) {
         mensajeError("La fecha final debe estar entre 1985 y la fecha actual.");
         return false;
     }

     // Validar que la fecha de inicio sea menor o igual a la fecha final
     if (fechaInicioIngresada > fechaFinalIngresada) {
         mensajeError("La fecha de inicio no puede ser mayor a la fecha final.");
         return false;
     }

     mensajeInfo("Fechas válidas.");
     return true;
}

