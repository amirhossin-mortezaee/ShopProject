$(function () {
    CountShopCart();
});

function CountShopCart() {
    $.get("/api/ShopCart", function (result) {
        $("#CountShopCartItems").html(result);
    });
}

function AddToCart(id) {
    $.get("/api/ShopCart/" + id, function (result) {
        $("#CountShopCartItems").html(result);
        UpdateShopCart();
    });
}

function UpdateShopCart() {
    $("#ShowCartId").load("/Shop/ShowCard");
};