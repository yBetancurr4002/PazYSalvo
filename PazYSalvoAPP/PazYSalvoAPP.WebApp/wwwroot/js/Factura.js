$(document).ready(function () {
    $("#btnVerFacturas").click(async function () {
        const url = $('#facturaContainer').data('listarfacturas');
        const response = await $.ajax({
            url: url,
            method: "GET",
            dataType: "html"
        });

        $("#tblListaDeFacturas").html(response);
        $("#btnVerFacturas").hide();
        $("#btnNuevaFactura").removeClass("visually-hidden"); // visually-hidden

    });

    $("#btnNuevaFactura").click(function () {
        mostrarModal();


    });

    $("#btnActualizarFactura").click(function () {
        const url = "";
        const $formulario = $('#frmModalFactura'); // captura el contenido de lo que contiene la etiqueta form
        const formData = new FormData($formulario[0]); // capturo la data del formulario

        const response = $.ajax({
            url: url,
            method: "POST",
            data: formData,
            
            success: function (data) {
                alert("Factura agregada con éxito");
            },
            error: function () {
                alert("Error! No se agregó factura");
            }
        });
    });
});


function mostrarModal() {

    // Actualiza el título de la modal
    $("#tituloModal").text("Nueva Factura");

    // Limpia los campos del formulario
    $("#frmModalFactura").trigger("reset");

    // Muestra la modal
    $("#modal").modal("show");

}