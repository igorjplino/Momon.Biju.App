
$(document).ready(function() {
    updateCartCount();
});

function updateCartCount() {
    $.ajax({
        url: '/Cart/Cart/CartCount', // Replace with your actual controller route
        type: 'GET',
        success: function(response) {
            if (response.success) {
                setCartCount(response.cartCount);
            }
        },
        error: function(xhr, status, error) {
            //TODO show error in pop-up
            console.error('Error:', error);
        }
    });
}

function addToCart(productId) {
    $.ajax({
        url: '/Cart/Cart/AddToCart/AddToCart', // Replace 'YourController' with your actual controller name
        type: 'POST',
        data: { 
            productId: productId,
            //__RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
        },
        success: function(response) {
            if (response.success) {
                setCartCount(response.cartCount);
            } else {
                console.error('Error: while adding to cart');
            }
        },
        error: function(xhr, status, error) {
            //TODO show error in pop-up
            console.error('Error:', error);
        }
    });
}

function setCartCount(count) {
    $('#cartCount').text(count);
}