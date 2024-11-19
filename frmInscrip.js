var dir = "http://localhost:65376/api/";
var oTabla = $("#tablaDatos").DataTable();
var vrInsc = 250000;
//fecha la cree yo 
const d = new Date().getTime();
const date = new Intl.DateTimeFormat("en-US", {
    year: "numeric",
    month: "2-digit",
    day: "2-digit"
}).format(d);

// //fecha de Delio, decides si usas la de delio o la mia 
//var f = new Date();
//  //                Día/Mes/Año,    como el mes emoieza en 0 toca sumarle 1
//var fecha = f.getDate() + "/" + (f.getMonth() + 1) + "/" + f.getFullYear();

jQuery(function () {
    //Carga el menú
    $("#dvMenu").load("../Paginas/Menu.html");

    //Registrar los botones para responder al evento click
    $("#btnAgre").on("click", function () {
        //alert("Agregar");
        let cod = $("#txtCodigo").val();
        if (cod == undefined || cod.trim() == " " || parseInt(cod, 10) <= 0) {
            grabarEncabezadoInsc();
        } 
        else {
            grabarDetalleIncs();
        }

    });
    $("#btnModi").on("click", function () {
        alert("Modificar");
        Modificar();
    });
    $("#btnBusc").on("click", function () {
        alert("Buscar");
        //Consultar();
    });
    $("#btnCanc").on("click", function () {
        alert("Cancelar");
        //Cancelar();
    });
    $("#btnImpr").on("click", function () {
        alert("Impresión");
        //Imprimir();
    });
    //fecha mia
    $("#lbFechaAct").val(date);
    //fecha de delio
   /* $("#lbFechaAct").val(fecha);*/

    llenarComboEvento();
    llenarComboBanda();

});  //Del: jQuery



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
async function llenarComboEvento() {
    let url = dir + "evento";
    let rpta = await llenarComboGral(url, "#cboEvento");
}

async function llenarComboBanda() {
    let url = dir + "banda";
    let rpta = await llenarComboGral(url, "#cboBanda");
    if (rpta == "Termino")
        llenarComboArtista();
}
async function llenarComboArtista(idArt) {
    let idB =$("#cboBanda").val();
    let url = dir + "BandArt?dato=" + idB + "&comando=1";
    let rpta = await llenarComboGral(url, "#cboBandArt");
    if (rpta == "Termino")
        ConsultarInstrum();
}
async function llenarTabla() {
    let cod = $("#txtCodigo").val();
    let url = dir + "detInsc?dato="+ cod + "&comando=1"
    let rpta = await llenarTablaGral(url, "#tablaDatos");
    if (rpta == "Termino")
        ConsultarInstrum();
}
async function ConsultarInstrum() {
    mensajeInfo("")
    try {
        let idArt = $("#cboBandArt").val();
        console.log(idArt);
        if (idArt == undefined || idArt.trim() == "" || parseInt(idArt, 10) <= 0) {
            mensajeError("Error, no selecciono el artista");
            $("#cboBandArt").focus();
            return;
        }
        const datosOut = await fetch(dir + "BandArt?dato=" + idArt + "&comando=2",
            {
                method: "GET",
                mode: "cors",
                headers: {
                    "content-type": "application/json",
                }
            }
        );
        const datosIn = await datosOut.json();
        if (datosIn == "") {
            mensajeInfo("No se encontro instrumento para el artista");
            return;
        }
        console.log(datosIn);
        $("#txtInstrum").val(datosIn[0].Nombre);

    } catch (error) {
        mensajeError("Error" + error);

    }
}
async function Consultar() {
    try {
        limpiar();
        mensajeInfo("");
        $("txtCodigo").prop('disabled', false);
        let cod = $("txtCodigo").val();
        if (cod == undefined || cod.trim() == " " || parseInt(cod, 10) <= 0) {
            $("txtCodigo").val()= 

        }
    } catch (e) {

    }
}
async function Modificar() {






}
