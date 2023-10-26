function get(id) {
    return document.getElementById(id).value;
}

function Error1(texto = "Ocurrio un error") {
    Swal.fire({
        icon: 'error',
        title: 'Error',
        text: texto,
        focusCancel: true
    })
}

function Correcto(texto = "Se realizo correctamente") {
    Swal.fire({
        position: 'top-end',
        icon: 'success',
        title: texto,
        showConfirmButton: false,
        timer: 1500
    })
}

function Confirmacion(title = "Confirmacion", texto = "¿Desea guardar los cambios?",
    callback) {
    return Swal.fire({
        title: title,
        text: texto,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si',
        cancelButtonText: 'No'
    }).then((result) => {
        if (result.isConfirmed) {
            callback();
        }
    })
}

function Informacion(titulo = "Información Importante", contenido = "", contenidohtml = "") {
    if (contenido != "") {
        Swal.fire({
            icon: 'success',
            title: titulo,
            text: contenido,
            focusConfirm: true,
        })
    }
    else {
        Swal.fire({
            icon: 'success',
            title: titulo,
            html: contenidohtml,
            focusConfirm: true,
        })
    }
}


function set(id, valor) {
    document.getElementById(id).value = valor;
}

function setD(id, valor) {
    document.getElementById(id).style.display = valor;
}

function setN(id, valor) {
    document.getElementsByName(id)[0].value = valor;
}

function getN(id) {
    return document.getElementsByName(id)[0].value;
}

function setC(selector) {
    document.querySelector(selector).checked = true;
}

function setSRC(id, valor) {
    document.getElementsByName(id)[0].src = valor;
}

function setURL(url) {
    var raiz = document.getElementById("hdfOculto").value;
    var urlAbsoluta = window.location.protocol + "//" +
        window.location.host + raiz + url;
    return urlAbsoluta;
}

var combosLlenar = [];
var radioLimpiar = [];
var radioNames = [];
function LimpiarDatos(idFormulario, excepciones = []) {
    var elementos = document.querySelectorAll("#" + idFormulario + " [name]")
    //Limpiar todo
    for (var j = 0; j < radioNames.length; j++) {
        document.querySelectorAll("[name=" + radioNames[j] + "]").forEach(x => x.checked = false);
    }
    for (var j = 0; j < radioLimpiar.length; j++) {
        document.getElementById(radioLimpiar[j]).checked = true;
    }
    for (var i = 0; i < elementos.length; i++) {
        if (!excepciones.includes(elementos[i].name)) {
            elementos[i].value = "";
        }
    }
}

function ValidarLongitudMaxima(idFormulario) {
    var error = "";
    var controles = document.querySelectorAll("#" + idFormulario + " [class*='max-']")
    var control;
    for (var i = 0; i < controles.length; i++) {
        control = controles[i];
        var arrayClase = control.className.split(" ");
        var claseMax = arrayClase.filter(p => p.includes("max-"))[0]
        var valorMaximo = claseMax.replace("max-", "") * 1;
        if (control.value.length > valorMaximo) {
            if (control.name == "nombres") {
                error = "La longitud actual del campo Nombres es de " + control.value.length + ". El campo Nombres no acepta mas de " + valorMaximo + " caracteres.";
            }
            else if (control.name == "numeroDocumento") {
                error = "La longitud actual del campo N° Documento es de " + control.value.length + ". El campo N° Documento no acepta mas de " + valorMaximo + " caracteres.";
            }
            else {
                error = "La longitud actual del campo " + control.name + " es de " + control.value.length + ". El campo " + control.name + " no acepta mas de " + valorMaximo + " caracteres.";
            }
            return error;
        }
    }
    return error;
}

function fetchGet(url, callback) {
    var raiz = document.getElementById("hdfOculto").value;
    var urlAbsoluta = window.location.protocol + "//" +
        window.location.host + raiz + url;
    fetch(urlAbsoluta).then(res => res.json())
        .then(res => {
            callback(res);
        }).catch(err => {
            console.log(err);
        })
}

function fetchGetText(url, callback) {
    var raiz = document.getElementById("hdfOculto").value;
    var urlAbsoluta = window.location.protocol + "//" +
        window.location.host + raiz + url;
    fetch(urlAbsoluta, {
        method: "POST"
    }).then(res => res.text())
        .then(res => {
            callback(res);
        }).catch(err => {
            console.log(err);
        })
}

function fetchPostText(url, frm, callback) {
    var raiz = document.getElementById("hdfOculto").value;
    var urlAbsoluta = window.location.protocol + "//" +
        window.location.host + raiz + url;
    fetch(urlAbsoluta, {
        method: "POST",
        body: frm
    }).then(res => res.text())
        .then(res => {
            callback(res);
        }).catch(err => {
            console.log(err);
        })
}

function recuperarGenericoEspecifico(url, idFormulario, excepciones = []) {
    var elementos = document.querySelectorAll("#" + idFormulario + " [name]")
    var nombreName;
    fetchGet(url, function (res) {
        for (var i = 0; i < elementos.length; i++) {
            nombreName = elementos[i].name
            if (!excepciones.includes(elementos[i].name)) {
                if (elementos[i].type != undefined && elementos[i].type.toUpperCase() == "RADIO") {
                    setC("[type='radio'][name='" + nombreName + "'][value='" + res[nombreName] + "']")
                }
                else {
                    setN(nombreName, res[nombreName]);
                }
            }
        }
    });
}

function llenarCombo(data, id, propiedadMostrar, propiedadId, valueDefecto = "") {
    var contenido = ""
    var elemento;
    contenido += "<option selected='selected' value='" + valueDefecto + "'>-- Seleccione --</option>"
    for (var j = 0; j < data.length; j++) {
        elemento = data[j];
        contenido +=
            "<option value='" + elemento[propiedadId] + "' >" + elemento[propiedadMostrar] + "</option>"
    }
    contenido += "";
    document.getElementById(id).innerHTML = contenido;
}

function inFocusName(name, time) {
    setTimeout(function () { document.getElementsByName(name)[0].focus(); }, time);
}