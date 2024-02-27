function AddProductToCart(productId) {
    var addtoCartUrl = $("#hdnAddtoCartUrl").val();
    $.ajax({
        url: addtoCartUrl,
        data: { productId: productId },
        type: 'POST',
        success: function (response) {
            swal("Success!", response, "success");
        },
        error: function (err) {

        }
    });
}