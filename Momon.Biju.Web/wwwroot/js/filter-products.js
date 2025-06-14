$(function () {
    $('#filter-products-form').on('submit', function (e) {
        e.preventDefault();

        const $form = $(this);
        const query = $form.serialize();
        const url = $form.attr('action');

        $.get(url, query, function (data) {
            $('#product-list-container').html(data);
        });
    });
});