$(document).ready(function () {
    GetAll();
});

function GetAll() {
    $.ajax({
        type: 'GET',
        url: 'https://ciclistastest.azurewebsites.net/Ciclistas',
        success: function (result) {
            $('#tblCiclista tbody').empty();
            $.each(result, function (i, ciclista) {
                var filas =
                    '<tr>'

          
                    + "<td class='text-center'>" + ciclista.nombre + "</td>"
                    + "<td class='text-center'>" + ciclista.direccion + "</td>"
                    + "<td class='text-center'>" + ciclista.edad + "</td>"
                    + "<td class='text-center'>" + ciclista.nivel + "</ td>"
                    + "<td class='text-center'>" + ciclista.membresiaActiva + "</ td>"
                    + '<td class="text-center"> <button class="btn btn-danger" onclick="Guardar(ciclista)"><span class="glyphicon glyphicon-trash" style="color:#FFFFFF"></span></button></td>'
                   

                $("#tblCiclista tbody").append(filas);
            });
        },

        error: function (result) {
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
        }
    });
};

function Add(ciclista) {
    $.ajax({
        type: 'POST',
        url: 'http://localhost:5215/api/Ciclista/Add',
        dataType: 'json',
        data: JSON.stringify(ciclista),
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            $('#myModal').modal();
            $('#ModalUpdate').modal('hide');

            GetAll();

        },
        error: function (result) {
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
        }
    });
}
