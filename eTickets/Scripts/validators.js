$(document).ready(function () {
    $(".change-validator-data-btn").on("click", function () {
        var elem = this;
        $(elem).parents("tr").find(".validator-status-holder").hide();
        $(elem).parents("tr").find(".validator-status-editor").show();
        $(elem).parents("tr").find(".change-validator-data-btn").hide();
        $(elem).parents("tr").find(".save-validator-data-btn").show();
        $(elem).parents("tr").find(".validator-payment-holder").hide();
        $(elem).parents("tr").find(".validator-payment-editor").show();

        var currentValidatorPayment = $(elem).parents("tr").find(".validator-payment-holder").html();
        $(elem).parents("tr").find("input").val(currentValidatorPayment);

        var currenValidatorStatus = $(elem).parents("tr").find(".validator-status-value").html();
        $(elem).parents("tr").find("select").val(currenValidatorStatus);
    })

    $(".save-validator-data-btn").on("click", function () {
        var elem = this;
        $(elem).parents("tr").find(".validator-status-holder").show();
        $(elem).parents("tr").find(".validator-status-editor").hide();
        $(elem).parents("tr").find(".change-validator-data-btn").show();
        $(elem).parents("tr").find(".save-validator-data-btn").hide();
        $(elem).parents("tr").find(".validator-payment-holder").show();
        $(elem).parents("tr").find(".validator-payment-editor").hide();

        var validatorId = $(elem).parents("tr").find(".validator-id-holder").html();
        var validatorStatus = $(elem).parents("tr").find("select").val();
        var payment = $(elem).parents("tr").find("input").val();

       // alert("payment: " + payment + ", status: " + validatorStatus + ", id: " + validatorId);
       
        $.ajax({
            url: "/Home/changeValidatorData",
            type: "POST",
            dataType: 'text',
            data: {
                validatorId: validatorId,
                status: validatorStatus,
                payment: payment
            },
            success: function (partialViewResult) {
                $("#validators-table-holder-admin").html(partialViewResult);
            },
            error: function (error) { alert(error.toString()); }
        });

    })
});