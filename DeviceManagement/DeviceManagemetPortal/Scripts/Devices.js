//Load Data in Table when documents is ready
$(document).ready(function () {
    loadData();
    closeCPanel();

    $('#btnAddDevice').click(function () {
        clearTextBox();
    });

    $('#btnAdd').click(function () {
        Add();
    });

    $('#btnSwitch').click(function () { 
        var action = '';
        if ($('#btnSwitch').attr('class') == 'btn btn-success') {
            $('#btnSwitch').attr('class', 'btn btn-warning');
            $('#btnSwitch').text('OFF');
            action = 'ON';
            $('#lblStatus').text(action);
        } else {
            $('#btnSwitch').attr('class', 'btn btn-success');
            $('#btnSwitch').text('ON');
            action = 'OFF';
            $('#lblStatus').text(action);
        }

        ControlDevice(action);
    });

    $('#btnResetWifi').click(function () {
        if (confirm('Reseting WiFi setting will remove stored wifi credentials. Do you want to proceed?')) {
            $.ajax({
                url: "/DeviceManagemetPortal/Device/ResetWifi",
                type: "POST",
                dataType: "json",
                data: {
                    DeviceSerial: $('#cpDeviceSerial').text().split(':')[1].trim(),
                    DeviceType: $('#cpDeviceType').text().split(':')[1].trim()
                },
                success: function (result) {
                    if (result == true) {
                        $.alert('Device WiFi credentials has been removed. Connect "AutoHomeConnect" access point from any device and open config portal on browser by entering "192.168.4.1".', 'WiFi Reset');
                    }
                },
                error: function (errormessage) {
                    alert(errormessage.responseText);
                }
            });
        }
    });
});
//Load Data function
function loadData() {
    $.ajax({
        url: "/DeviceManagemetPortal/Device/DeviceDetails",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
                var html = '';
            $.each(result, function (key, item) {
                html += '<div class="bg-info img-rounded row card" onclick="controlPanel(\'' + item.DeviceSerialNumber + '\',\'' + item.DeviceName + '\',\'' + item.DeviceType + '\'); ">';
                    html += '<img class="img-circle" width="40%" style="float:left;" src="Content/Images/' + item.DeviceType + '.png" alt="' + item.DeviceName +'" /> <h4>';
                    html += item.DeviceName + '</h4>';
                    html += '<div class="text-center">'+ item.DeviceSerialNumber +'</div></div>';
                });
            $('#userDevices').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//Function for clearing the textboxes
function clearTextBox() {
    $('#Name').val("");
    $('#Description').val("");
    $('#DeviceSerialNr').val("");
    $('#DeviceType').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#Name').css('border-color', 'lightgrey');
    $('#Description').css('border-color', 'lightgrey');
    $('#DeviceSerialNr').css('border-color', 'lightgrey');
    $('#DeviceType').css('border-color', 'lightgrey');
}
//Valdidation using jquery
function validate() {
    var isValid = true;
    if ($('#Name').val().trim() == "") {
        $('#Name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Name').css('border-color', 'lightgrey');
    }
    if ($('#Description').val().trim() == "") {
        $('#Description').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Description').css('border-color', 'lightgrey');
    }
    if ($('#DeviceSerialNr').val().trim() == "") {
        $('#DeviceSerialNr').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#StDeviceSerialNrate').css('border-color', 'lightgrey');
    }
    if ($('#DeviceType').val().trim() == "") {
        $('#DeviceType').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#DeviceType').css('border-color', 'lightgrey');
    }
    return isValid;
}

function Add() {
    $.ajax({
        url: "/DeviceManagemetPortal/Device/Create",
        type: "POST",
        dataType: "json",
        data: {
            DeviceName: $('#Name').val(),
            DeviceDescription: $('#Description').val(),
            DeviceSerialNumber: $('#DeviceSerialNr').val(),
            DeviceType: $('#DeviceType').val()
        },
        success: function (result) {
            if (result == true) {
                loadData();
                alert('Device successfully added.');
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}


function ControlDevice(action) {
    $.ajax({
        url: "/DeviceManagemetPortal/Device/Control",
        type: "POST",
        dataType: "json",
        data: {
            Action: action,
            DeviceSerial: $('#cpDeviceSerial').text().split(':')[1].trim(),
            DeviceType: $('#cpDeviceType').text().split(':')[1].trim()
        },
        success: function (result) {
            if (result == true) {
                alert('Device action performed successfully.');
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}


function controlPanel(deviceSerial, deviceName, deviceType) {
    $('#devicesColumn').attr('class', 'col-md-5 c-panel');
    $('#cpColumn').show();
    $('#cpColumn').attr('class', 'col-md-5 c-panel');
    $('#deviceName').text('Device Name: '+ deviceName);
    $('#cpDeviceSerial').text('Device Serial: ' + deviceSerial);
    $('#cpDeviceType').text('Device Type: ' + deviceType);
}

function closeCPanel() {
    $('#devicesColumn').attr('class', 'col-md-12');
    $('#cpColumn').hide();
}
