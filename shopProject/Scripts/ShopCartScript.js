$(function () {
    CountShopCart();
});

function CountShopCart() {
    $.get("/api/ShopCart", function (result) {
        $("#CountShopCartItems").html(result);
    });
}