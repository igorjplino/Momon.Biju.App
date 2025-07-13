
$(document).ready(function() {
    updateCartCount();

    // The event listeners attached to the replaced elements are lost
    // Attach to a static parent that stays in the DOM
    $('.table-responsive').on('click', '.increase-qty, .decrease-qty', function () {
        const $control = $(this).closest('.quantity-control');
        const productId = $control.data('product-id');
        const change = $(this).hasClass('increase-qty') ? 1 : -1;

        let data = {
            productId: productId, change: change
        };

        $.ajax({
            url: '/Cart/Cart/UpdateProductQuantity',
            method: 'POST',
            data: { productId: productId, change: change },
            success: function (result) {
                updateCartCount();
                updateCartPurchaseTotal();
                $('#cartItemsContainer').html(result);
            },
            error: function () {
                alert('Error updating quantity.');
            }
        });
    });
});

function updateCart(productId, quantity) {
    $.ajax({
        url: '/Cart/Cart/UpdateCart',
        type: 'POST',
        data: {
            productId: productId,
            quantity: quantity
            //__RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
        },
        success: function(result) {
            updateCartCount();
            $('#cartItemsContainer').html(result);
        },
        error: function(xhr, status, error) {
            //TODO show error in pop-up
            console.error('Error:', error);
        }
    });
}

function removeFromCart(productId) {
    $.ajax({
        url: '/Cart/Cart/RemoveFromCart',
        type: 'POST',
        data: {
            productId: productId
            //__RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
        },
        success: function(result) {
            updateCartCount();
            $('#cartItemsContainer').html(result);
        },
        error: function(xhr, status, error) {
            //TODO show error in pop-up
            console.error('Error:', error);
        }
    });
}

function updateCartCount() {
    $.ajax({
        url: '/Cart/Cart/CartCount', // Replace with your actual controller route
        type: 'GET',
        success: function(response) {
            if (response.success) {
                $('#cartCount').text(response.cartCount);
            }
        },
        error: function(xhr, status, error) {
            //TODO show error in pop-up
            console.error('Error:', error);
        }
    });
}

function updateCartPurchaseTotal() {
    $.ajax({
        url: '/Cart/Cart/CartPurchaseTotal', // Replace with your actual controller route
        type: 'GET',
        success: function(response) {
            if (response.success) {
                $('#cartPurchaseTotal').text(response.cartPurchaseTotal);
            }
        },
        error: function(xhr, status, error) {
            //TODO show error in pop-up
            console.error('Error:', error);
        }
    });
}

function addToCart(productId, quantity) {
    $.ajax({
        url: '/Cart/Cart/AddToCart/AddToCart', // Replace 'YourController' with your actual controller name
        type: 'POST',
        data: { 
            productId: productId,
            quantity: quantity
            //__RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
        },
        success: function(response) {
            if (response.success) {
                updateCartCount()
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