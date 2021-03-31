function AjaxModal() {
    $(document).ready(function () {
        $(function () {
            $.ajaxSetup({ cache: false });

            $("a[data-modal]").on("click",
                function (e) {
                    debugger
                    $("#myModalContent").load(this.href, function () {
                        debugger
                        //$("myModal").modal({ keyboard: true }, 'show');
                        $("myModal").modal();
                        //    bindForm(this);
                    });
                    return false;
                });
        });

        //function bindForm(dialog) {
        //    debugger
        //    $('form', dialog).submit(function () {
        //        $.ajax({
        //            url: this.action,
        //            type: this.method,
        //            data: $(this).serialize(),
        //            success: function (result) {
        //                debugger
        //                if (result.success) {
        //                    $('#myModal').modal('hide');
        //                    $('#EnderecoTarget').load(result.url);
        //                } else {
        //                    $('#myModalContent').html(result);
        //                    bindForm(dialog);
        //                }
        //            }
        //        });
        //        return false;
        //    });
        //}
    });
}