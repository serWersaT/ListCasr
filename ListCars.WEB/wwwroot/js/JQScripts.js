$(document).ready(function () {
    var carId = 0;
});

function SelectedRow() {   
    $('#RowCount').append(
        '<option>10</option>' +
        '<option>20</option>' +
        '<option>50</option>'
    );
}

function GetPartials(elem) {
    $('.GetPartial').hide('slow');
    $('#' + elem).toggle('slow');
}

function GetAllCars() {
    $.ajax({
        type: "POST",
        url: window.location.origin + "/Home/GetAllCars",
        data: JSON.stringify(),
        contentType: "application/json; charset=utf-8",
        Accept: "application/json",
        dataType: "json",
        async: false,
        success: function (data) {

        }
    });
}
  

function getexcel() {
    if ($("#GeneralStatTable").is(':visible') == true) $("#GeneralStatTable").table2excel({filename: "Table.xls"});
    if ($("#CarReportTable").is(':visible') == true) $("#CarReportTable").table2excel({filename: "Table.xls"});
}



function AddCars() {
    var model = new Object;
    model.NumberCar = $('#txtNumberCar').prop('value');
    model.ModelCar = $('#txtModelCar').prop('value');
    model.ColorCar = $('#txtColorCar').prop('value');
    model.YearCar = $('#txtYearCar').prop('value');
    $.ajax({
        type: "POST",
        url: window.location.origin + "/Home/AddCars",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        Accept: "application/json",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data == 0) alert("Машина с таким номером уже зарегисрирована");
            if (data == 1) alert("Добавлен успешно");
            if (data == 2) alert("Произошла ошибка");
        }
    });
}


function OptionFiltre() {
    var repnum = $('#ListReports option:selected').prop('value');
    if (repnum == 2) $('.calendar').show();
    else $('.calendar').hide();
}

function SelectReports() {
    $('.ReportTable').hide();
    var repnum = $('#ListReports option:selected').prop('value');
    if (repnum == 1) GetStatReports();
    if (repnum == 2) GetCarReports();
    $('#btntoexcel').show();
}

function GetStatReports() {
    var model = new Object;
    model.RepId = Number($('#ListReports option:selected').prop('value'));
    $.ajax({
        type: "POST",
        url: window.location.origin + "/Home/SelectGeneralStatReports",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        Accept: "application/json",
        dataType: "json",
        async: false,
        success: function (data) {
            $('#GeneralStatTable').show();
            $('.rep').detach();
            $.each(data, function (index, value) {                
                $('#GeneralStatTable').append(
                    "<tr class='rep'>" +
                    "<td>" + value['cnt'] + "</td>" +
                    "<td>" + value['startDate'] + "</td>" +
                    "<td>" + value['endDate'] + "</td>" +
                    "</tr>"
                );
            });
        }
    });
}

function GetCarReports() {
    var model = new Object;
    model.RepId = Number($('#ListReports option:selected').prop('value'));
    model.Model = $('#ModelCar').prop('value');

    $.ajax({
        type: "POST",
        url: window.location.origin + "/Home/SelectCarReport",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        Accept: "application/json",
        dataType: "json",
        async: false,
        success: function (data) {
            $('#CarReportTable').show();
            $('.rep').detach();
            $.each(data, function (index, value) {                
                $('#CarReportTable').append(
                    "<tr class='rep'>" +
                    "<td>" + value['model'] + "</td>" +
                    "<td>" + value['created'] + "</td>" +
                    "<td>" + value['lastFetched'] + "</td>" +
                    "<td>" + value['number'] + "</td>" +
                    "<td>" + value['color'] + "</td>" +
                    "<td>" + value['inWork'] + "</td>" +
                    "</tr>"
                );
            });
        }
    });
}

function SelectCar(tm, tx) {
    var model = new Object;

    if (tm == '' && tx == '') {
        model.topmin = 1;
        model.topmax = Number($('#RowCount option:selected').prop('value'));
    }
    else {
        model.topmin = Number(tm);
        model.topmax = Number(tx);
    }

    $.ajax({
        type: "POST",
        url: window.location.origin + "/Home/SelectAllCars",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        Accept: "application/json",
        dataType: "json",
        async: false,
        success: function (data) {
            $('.TableOffice').detach();
            $.each(data, function (index, value) {
                $('#TblAllCars').append(
                    "<tr value=" + value['Id'] + ">" +
                    "<td>" + value['numberRow'] + "</td>" +
                    "<td>" + value['numberCar'] + "</td>" +
                    "<td>" + value['modelCar'] + "</td>" +
                    "<td>" + value['colorCar'] + "</td>" +
                    "<td>" + value['yearCar'] + "</td>" +
                    "<td>" + value['active'] + "</td>" +
                        "<td class='TableCars'>" +
                    "<ul>" +
                    "<li> <a href='#' onclick='UpdateCarsShow(" + index + "," + value['id'] + ")'><i>Редактировать</i></a></li>" +
                                "<li> <a href='#' onclick = 'DeleteCars(" + value['id'] + ")'><i>Удалить</i></a></li>" +
                            "</ul>" + 
                        "</td> " +
                    "</tr>"
                );
            });
        }
    });
};



function DeleteCars(id) {
    $.ajax({
        type: "POST",
        url: window.location.origin + "/Home/DeleteCars",
        data: JSON.stringify(id),
        contentType: "application/json; charset=utf-8",
        Accept: "application/json",
        dataType: "json",
        async: false,
        success: function (data) {
            alert(data);
            SelectCar(0, $('#RowCount option:selected').prop('value'));
        }
    });
}


function UpdateCars() {
    var model = new Object;
    model.NumberCar = $('#modalNumberCars').prop('value');
    model.ModelCar = $('#modalModelCars').prop('value');
    model.ColorCar = $('#modalColorCars').prop('value');
    model.YearCar = $('#modalYearCars').prop('value');
    model.Active = parseInt($('#modalActiveCars').prop('value'));
    model.Id = carId;
    $.ajax({
        type: "POST",
        url: window.location.origin + "/Home/UpdateCars",
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        Accept: "application/json",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data == 'true' || data == 1)
                alert("Данные успешно обновлены");
            else
                alert("Произошла ошибка");
            UpdateCarsHide();
        }
    });
}


function UpdateCarsShow(index, id) {
    carId = id;
    $('#CarsModal').show();
    $('#modalNumberCars').val($('tr').eq(index + 1).children('td').eq(1).text());
    $('#modalModelCars').val($('tr').eq(index + 1).children('td').eq(2).text());
    $('#modalColorCars').val($('tr').eq(index + 1).children('td').eq(3).text());
    $('#modalYearCars').val($('tr').eq(index + 1).children('td').eq(4).text());
    $('#modalActiveCars').val($('tr').eq(index + 1).children('td').eq(5).text());
}

function UpdateCarsHide() {
    $('#CarsModal').hide();
}



function VisibleAllCars() {
    $.post(window.location.origin + "/Home/_GetAllCars",
        function (response) {
            $("#GetAllCars").html(response);
            SelectedRow();
            SelectCar(0, $('#RowCount option:selected').prop('value'));
        });
}


function VisibleAddCars() {
    $.post(window.location.origin + "/Home/_GetAddCars",
        function (response) {
            $("#GetAddCars").html(response);
        });
}

function VisibleStat() {
    $.post(window.location.origin + "/Home/_GetStat",
        function (response) {
            $("#GetStat").html(response);
        });
}
