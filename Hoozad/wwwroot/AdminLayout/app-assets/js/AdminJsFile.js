function Submit_AddStatus() {
    //alert("start_submit");
    
    if ($("#addStatusForm").valid()) {
        var insertData = $("#addStatusForm").serialize();
        $.ajax({
            url: "/UsersPanel/Reports/AddStatus",
            data: insertData,
            type: "POST",
        }).done(function (data) {
            
            if (data.res == true) {
                var zurl = "/UsersPanel/Reports/ShowCartStatuses";
                $.ajax({
                    url: zurl,
                    data: { cartId: data.cartId },
                    type: "GET"
                }).done(function (result) {
                    Swal.fire({
                        toast: true,
                        icon: "success",
                        title: "عملیات با موفقیت انجام شد",
                        showConfirmButton: false,
                        timer: 2000,
                        timerProgressBar: true,
                        didOpen: (toast) => {
                            toast.addEventListener('mouseenter', Swal.stopTimer)
                            toast.addEventListener('mouseleave', Swal.resumeTimer)
                        }
                    });

                    $("#divStatus").html(result);
                    $("#close-modal").click();
                });
                
            }
        });
    }
};