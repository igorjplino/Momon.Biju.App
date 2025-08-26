$(document).ready(function () {
    $("#finish-purchase").on("submit", function (e) {
        e.preventDefault();

        $.ajax({
            url: "/Cart/Cart/FinishPurchase/",
            method: "POST",
            data: { 
                Name: $("#Name").val(), 
                AdditionalInformation: $("#AdditionalInformation").val() 
            },
            success: function (data) {
                const phone = data.phone;
                const message = encodeURIComponent(data.message);
                const whatsappLink = `https://wa.me/${phone}?text=${message}`;

                $("#whatsapp-link")
                    .attr("href", whatsappLink);

                window.open(whatsappLink, "_blank");
            },
            error: function () {
                alert("Error building message from backend");
            }
        });
    });
});
