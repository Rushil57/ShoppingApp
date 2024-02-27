
$(document).ready(function () {
    CalculateTotalOnCheckout();
});

function CalculateQty(currentControl, mode) {
    var inputControl = $(currentControl).closest('.cart_quantity').find('.cart_quantity_input');
    
    var qtyValue = parseInt($(inputControl).val());

    if (mode == "Add") {
        qtyValue++;
    }
    else {
        if (qtyValue > 1)
            qtyValue--;
    }
    $(inputControl).val(qtyValue);
    CalculateTotal($(currentControl).closest('.main-tr'))
    
}
function CalculateTotal(mainTr) {
    var qtyValue = parseInt($(mainTr).find('.cart_quantity_input').val());
    var price = parseFloat($(mainTr).attr('data-price'));
    var total = price * qtyValue;
    $(mainTr).find('.cart_total_price').html("$" + parseFloat(total).toFixed(2));
    CalculateTotalOnCheckout();
    UpdateProductListSession();
}
function qtyChanged(currentInput) {
    CalculateTotal($(currentInput).closest('.main-tr'))
}

function RemoveProductFromCart(currentRow, productId) {
    var removeFromCartUrl = $("#hdnRemoveFromCartUrl").val();
    $.ajax({
        url: removeFromCartUrl,
        data: { productId: productId },
        type: 'POST',
        success: function (response) {
            $(currentRow).closest('.main-tr').remove();
            CalculateTotalOnCheckout();
            swal("Success!", response, "success");
        },
        error: function (err) {

        }
    });
}

function CalculateTotalOnCheckout() {
    if ($("#hdnIsCheckout").val() != undefined && $("#hdnIsCheckout").val() == "true") {
        var allRows = $("#tblCart").find('.main-tr');
        var cartSubTotal = 0.0;
        var cartExoTax = 0.0;
        var cartFinalTotal = 0.0;
        $.each(allRows, function () {
            cartSubTotal += parseFloat($(this).find('.cart_total_price').html().replace(/\$/g, ''));
        });
        cartExoTax = (parseFloat(cartSubTotal) * 5) / 100;
        cartFinalTotal = parseFloat(cartSubTotal) + parseFloat(cartExoTax);
        //cart-sub-total
        //cart-exo-tax
        //cart-final-total


        $("#cart-sub-total").html("$"+ cartSubTotal.toFixed(2));
        $("#cart-exo-tax").html("$" + cartExoTax.toFixed(2));
        $("#cart-final-total").html("$" + cartFinalTotal.toFixed(2));
        
    }
}

function UpdateProductListSession() {
    var updateProductListUrl = $("#hdnUpdateProductListUrl").val();
    var prodList = [];
    var allRows = $("#tblCart").find('.main-tr');
    $.each(allRows, function () {
        var prod = {};
        prod.ProductId = $(this).attr('data-productid');
        prod.Qty = $(this).find('.cart_quantity_input').val();
        prod.TotalAmount = $(this).find('.cart_total_price').html();
        prodList.push(prod);
    });
    $.ajax({
        url: updateProductListUrl,
        data: { prodList: prodList },
        type: 'POST',
        success: function (response) {
            
        },
        error: function (err) {

        }
    });
}