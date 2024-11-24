var dir = "http://localhost:52814/api/";
var oTabla = $("#tablaDatos").DataTable();

jQuery(function () {
    // Carga el menú
    $("#dvMenu").load("../Paginas/Menu.html");
    llenarComboPais();
    llenarComboProduc();
    llenarComboDirector();
    llenarComboEmpleado();
    // Registrar los botones para responder al evento click
    $("#btnAgre").on("click", function () {
        mensajeInfo("");
        let codigo = $("#txtCodigo").val();
        let nombre = $("#txtNombre").val();
        let fechaEstreno = $("#txtFechaEstreno").val();
        let productora = $("#cboProductora").val();
        let nacionalidad = $("#cboPais").val();
        let director = $("#cboDirector").val();
        let empleado = $("#cboEmpleado").val();

        if (nombre.trim() == "" || codigo.trim() == "" || parseInt(codigo, 10) <= 0) {
            mensajeError("Debe ingresar todos los campos");
            $("#txtCodigo").focus();
            return;
        } else {
            let rpta = window.confirm("¿Estás seguro de agregar la película: " + nombre + "?");
            if (rpta) {
                ejecutarComando("POST");
            } else {
                mensajeInfo("Acción de agregar cancelada");
            }
        }
    });

    $("#btnModi").on("click", function () {
        mensajeInfo();
        let codigo = $("#txtCodigo").val();
        let nombre = $("#txtNombre").val();
        let fechaEstreno = $("#txtFechaEstreno").val();
        let productora = $("#cboProductora").val();
        let nacionalidad = $("#cboPais").val();
        let director = $("#cboDirector").val();
        let empleado = $("#cboEmpleado").val();

        if (nombre.trim() == "" || codigo.trim() == "" || parseInt(codigo, 10) <= 0) {
            mensajeError("Debe ingresar todos los campos");
            $("#txtCodigo").focus();
            return;
        } else {
            let rpta = window.confirm("¿Estás seguro de modificar la película: " + nombre + "?");
            if (rpta) {
                ejecutarComando("PUT");
            } else {
                mensajeInfo("Acción de modificar cancelada");
            }
        }
    });
    $("#btnEliminar").on("click", async function () {
        // Verificar si hay una fila seleccionada
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
        mensajeInfo("Ha sido eliminado con exito " + nombre);
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
    let nombre = datosFila.find("td:eq(2)").text();
    let fechaEstreno = datosFila.find("td:eq(3)").text().split("T")[0];
    let productora = datosFila.find("td:eq(4)").text();
    let nacionalidad = datosFila.find("td:eq(5)").text();
    let director = datosFila.find("td:eq(6)").text();

    $("#txtCodigo").val(codigo);
    $("#txtNombre").val(nombre);
    $("#txtFechaEstreno").val(fechaEstreno);
    $("#txtProductora").val(productora);
    $("#txtPais").val(nacionalidad);
    $("#txtDirector").val(director);

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
        let response = await fetch(dir + "pelicula/" + codigo, {
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
    let rpta = await llenarTablaGral(dir + "pelicula?id=0&comando=1", "#tablaDatos");
}
async function llenarComboPais() {
    let url = dir + "pais";
    let rpta = await llenarComboGral(url, "#cboPais");
}

async function llenarComboProduc() {
    let url = dir + "productora?id=0&comando=1";
    let rpta = await llenarComboGral(url, "#cboProductora");
}
async function llenarComboDirector() {
    let url = dir + "director?id=0&comando=1";
    let rpta = await llenarComboGral(url, "#cboDirector");
}
async function llenarComboEmpleado() {
    let url = dir + "empleado?id=0&comando=1";
    let rpta = await llenarComboGral(url, "#cboEmpleado");
}
//async function llenarComboDpto() {
    //let url = dir + "dpto";
    //let rpta = await llenarComboGral(url, "#cboDpto");
   // if (rpta == "Termino")
        //llenarComboCiudad();
//}


//async function llenarComboCiudad(idCiu) {
    //let idDpto = $("#cboDpto").val();
    //let url = dir + "ciudad?idDpto=" + idDpto;
    //let rpta = await llenarComboGral(url, "#cboCiudad");
    //if (idCiu != undefined && rpta == "Termino")
        //$("#cboCiudad").val(idCiu);
//}
async function Consultar() {
    mensajeInfo("Se va a consultar");
    try {
        let codigo = $("#txtCodigo").val();
        if (codigo == undefined || codigo.trim() == "") {
            llenarTabla();
            mensajeInfo("Error, no existe el codigo");
            return;
        }

        const datosOut = await fetch(dir + "pelicula?id=" + codigo + "&comando=2", {
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
        $("#txtNombre").val(datosIn[0].Nombre);
        $("#txtFechaEstreno").val(datosIn[0].FechaEstreno.split("T")[0]);
        $("#cboProductora").val(datosIn[0].Id_Productora);
        $("#cboPais").val(datosIn[0].Id_Nacionalidad);
        $("#cboDirector").val(datosIn[0].Id_Director);
        $("#cboEmpleado").val(datosIn[0].Id_Empleado);
    } catch (error) {
        mensajeError("Error: " + error);
    }
}

async function ejecutarComando(accion) {

    let codigo = $("#txtCodigo").val();
    let nombre = $("#txtNombre").val();
    let fechaEstreno = $("#txtFechaEstreno").val();
    let idProductora = $("#cboProductora").val(); 
    let idPais = $("#cboPais").val(); 
    let idDirector = $("#cboDirector").val(); 
    let idEmpleado = $("#cboEmpleado").val(); 

    if (!codigo || !nombre || !fechaEstreno || !idProductora || !idPais || !idDirector || !idEmpleado) {
        mensajeError("Por favor, completa todos los campos.");
        return;
    }
    let datosOut = {
        Codigo: parseInt(codigo, 10),
        Nombre: nombre.trim(),
        Fecha_Estreno: fechaEstreno,
        Id_Productora: parseInt(idProductora, 10),
        Id_Pais: parseInt(idPais, 10),
        Id_Director: parseInt(idDirector, 10),
        Id_Empleado: parseInt(idEmpleado, 10),
    };

    try {
        const response = await fetch(dir + "pelicula", {
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
        Consultar(); // Refrescar datos en pantalla
        llenarTabla();
        mensajeOk(Respuesta);
    } catch (error) {
        mensajeError("Error: " + error.message);
    }
}

function limpiarFormulario() {
    $("#txtCodigo").val("");
    $("#txtNombre").val("");
    $("#txtFechaEstreno").val("");
    mensajeInfo("Formulario limpio.");
}
