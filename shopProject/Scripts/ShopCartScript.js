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

function Remove(id, count) {
    $.ajax({
        url: "/Shop/removeProduct/" + id,
        type: "Get",
        data: { count: count }
    }).done(function (result) {
        CountShopCart();
        UpdateShopCart1();
    });
    
}

function UpdateShopCart1() {
    $("#ShowCartId").load("/Shop/ShowCard");
};