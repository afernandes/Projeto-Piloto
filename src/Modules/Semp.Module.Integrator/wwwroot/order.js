$(function () {
    function toggleChecked(status) {
        $("#checkboxes input").each(function () {

            // Set the checked status of each to match the
            // checked status of the check all checkbox:
            $(this).prop("checked", status);
        });
    }

    $(document).ready(function () {

        //Set the default value of the global checkbox to true:
        $("#checkall").prop('checked', false);

        // Attach the call to toggleChecked to the
        // click event of the global checkbox:
        $("#checkall").click(function () {
            var status = $("#checkall").prop('checked');
            toggleChecked(status);
        });
    });

    $('body').on('click', '.btn-resend-order', function (e) {
        var $form = $(this).closest("form"),
            $orderId = $(this).data('id');
        /*$quantityInput = $form.find('.quantity-field');*/

        $.ajax({
            type: 'POST',
            url: '/api/integrator/order/resend-confirmation/' + $orderId,
            data: JSON.stringify({ id: $orderId }),
            contentType: "application/json"
        }).done(function (data) {
            $('#shopModal').find('.modal-content').html(data);
            $('#shopModal').modal('show');
            //$('.cart-badge .badge').text($('#shopModal').find('.cart-item-count').text());
        }).fail(function (e) {
            /*jshint multistr: true */
            $('#shopModal').find('.modal-content').html(' \
                        <div class="modal-header"> \
                            <h5 class="modal-title" id="myModalLabel">Opps!</h5> \
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"> \
                                <span aria-hidden="true"></span> \
                            </button > \
                        </div> \
                        <div class="modal-body"> \
                            Erro ao reenviar pedido. \
                        </div>');
            $('#shopModal').modal('show');
        });
    });




    $('body').on('click', '.btn-release-order', function (e) {
        var $form = $(this).closest("form"),
            $orderLegacy = $(this).data('orderlegacy'),
            $orderType = $(this).data('ordertype'),
            $orderIssue = $(this).data('orderissue');
        /*$quantityInput = $form.find('.quantity-field');*/

        $.ajax({
            type: 'POST',
            url: '/api/integrator/order/release-confirmation',
            data: JSON.stringify({ orderLegacy: $orderLegacy, orderType: $orderType, orderIssue: $orderIssue }),
            contentType: "application/json"
        }).done(function (data) {
            $('#shopModal').find('.modal-content').html(data);
            $('#shopModal').modal('show');
            //$('.cart-badge .badge').text($('#shopModal').find('.cart-item-count').text());
        }).fail(function (e) {
            /*jshint multistr: true */
            $('#shopModal').find('.modal-content').html(' \
                        <div class="modal-header"> \
                            <h5 class="modal-title" id="myModalLabel">Opps!</h5> \
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"> \
                                <span aria-hidden="true"></span> \
                            </button > \
                        </div> \
                        <div class="modal-body"> \
                            <p>Erro ao liberar pedido.</p> \
                            <p>' + e.responseJSON.error + '</p> \
                        </div>');
            $('#shopModal').modal('show');
        });
    });
});