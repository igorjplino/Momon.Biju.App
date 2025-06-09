$('#duallistbox_subcategory').bootstrapDualListbox({
    nonSelectedListLabel: 'Subcategorias disponíveis',
    selectedListLabel: 'Subcategorias selecionadas',
    moveAllLabel: 'Mover todos',
    moveSelectedLabel: 'Mover selecionados',
    removeAllLabel: 'Remover todos',
    removeSelectedLabel: 'Remover selecionados',
    preserveSelectionOnMove: 'moved',
    moveOnSelect: false,
    showFilterInputs: false,
    infoText: false
});

function previewImage(event) {
    const input = event.target;
    const preview = document.getElementById('imagePreview');

    if (input.files && input.files[0]) {
        const reader = new FileReader();

        reader.onload = function (e) {
            preview.src = e.target.result;
        };

        reader.readAsDataURL(input.files[0]);
    }
}