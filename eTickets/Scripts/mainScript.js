var clickedRow;

$(document).ready(function () {

    if ($('#operations-table tr').length == 1) {
        $('#operations-table').hide();
        $('#table-empty-message-holder').show();
    }

    $("#display-error-message").hide();

    $("#entry-holder").find('tr').click(function () {

        //alert('You clicked row ' + ($(this).find("td").html()));
        $('input[name="cardId"]').val($(this).find("td").html());

        $("#entry-holder tr").css("background-color", 'white');
        $(this).css("background-color", '#bdbdbd');
    });
});

function validateForm() {
    if (!$('input[name="cardId"]').val()) {
        $("#display-error-message").html("Выберите карту!");
        $("#display-error-message").show();
        return false;
    }
    else if (!$('input[name="amount"]').val()) {
        $("#display-error-message").html("Введите сумму!");
        $("#display-error-message").show();
        return false;
    } else {
        return true;
    }
}

function onSuccesSendingForm() {
    $('input[name="cardId"]').val("");
    $('input[name="amount"]').val("");
    $("#display-error-message").hide();
    $("#display-error-message").html("");
    alert("Баланс пополнен!");
}