var clickedRow;

$(document).ready(function () {

    manageMenu();

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

function manageMenu() {
    var addr = window.location.href;
    addr = addr.substring(addr.lastIndexOf('/') + 1, addr.length);
    //alert(addr);
    switch (addr) {
        case "":
            $("ul.nav.navbar-nav").find("li:nth-child(1)").addClass("active");
            $("ul.nav.navbar-nav").find("li:nth-child(1)").css("color", "white");
            break;
        case "GetAdminPage":
            $("ul.nav.navbar-nav").find("li:nth-child(2)").addClass("active");
            $("ul.nav.navbar-nav").find("li:nth-child(2)").css("color", "white");
            break;
        case "GetOperationPage":
            $("ul.nav.navbar-nav").find("li:nth-child(3)").addClass("active");
            $("ul.nav.navbar-nav").find("li:nth-child(3)").css("color", "white");
            break;
        default: break;
    }
}

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
    }

    function changeCardStatusInit(elem, val) {
        $(elem).parents("tr").find(".card-status-holder").hide();
        $(elem).parents("tr").find(".card-status-editor").show();
        $(elem).parents("tr").find(".change-card-status-btn").hide();
        $(elem).parents("tr").find(".save-card-status-btn").show();
        var currentCardStatus = $(elem).parents("tr").find(".card-status-value").html();
        $(elem).parents("tr").find("select").val(currentCardStatus);
    }

    function changeCardStatusSave(elem) {
        $(elem).parents("tr").find(".card-status-holder").show();
        $(elem).parents("tr").find(".card-status-editor").hide();
        $(elem).parents("tr").find(".change-card-status-btn").show();
        $(elem).parents("tr").find(".save-card-status-btn").hide();
        var cardStatus = $(elem).parents("tr").find("select").val();
        var cardId = $(elem).parents("tr").find(".card-id-holder").html()
        ///alert(cardStatus);
        //alert(cardId);
        $.ajax({
            url: "/Home/changeCardStatus",
            type: "POST",
            dataType: 'text',
            data: {
                cardId: cardId,
                status: cardStatus
            },
            success: function (partialViewResult) {
                $("#cards-table-holder-admin").html(partialViewResult);
            },
            error: function (error) { alert(error.toString()); }
        });
    }