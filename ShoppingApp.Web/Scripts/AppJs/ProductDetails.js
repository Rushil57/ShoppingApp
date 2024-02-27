$("body").on("click", ".small-img-prod", function () {
    var src = $(this).attr('src');
    $("#mainProdImage").attr('src', src);
});
