(function ($) {
    "use strict"; // Start of use strict

    CKEDITOR.replace('Content');
    CKEDITOR.replace('About');





})(jQuery); // End of use strict


function confirmDelete(uniqueId, isTrue) {

    var deleteSpan = 'deleteSpan_' + uniqueId;
    var confirmDeleteSpan = 'confirmDeleteSpan_' + uniqueId;

    if (isTrue) {
        $('#' + deleteSpan).hide();
        $('#' + confirmDeleteSpan).show();
    } else {
        $('#' + deleteSpan).show();
        $('#' + confirmDeleteSpan).hide();
    }
}


function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#file_upload')
                .attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}


/*Clear Table*/

function clearData() {
    $('table').html("");
};


function RolesData() {
    var url = '/Admin/Account/JsonRoles'
    $('table').html();
    var thead = '<thead><tr><th>Id</th><th>Name</th></tr></thead>';
    $('table').append(thead);



    $.getJSON(url, function (data) {
        var array = $.map(data, function (value, index) {
            return value;
        });


        $.each(array, function (indis, value) {
            var guncelle = '<a data-id=' + Object.values(value)[0] + ' href="/Admin/account/Updaterole/' + Object.values(value)[0] + '" class="btn btn-primary guncelle">Update</a>';
            var deleteRole = '<button data-id=' + Object.values(value)[0] + ' class="btn btn-primary deleteRole">Delete</button>';
            var rol = '<tbody><tr><td>' + Object.values(value)[0] + '</td><td>' + Object.values(value)[1] + '</td><td>' + guncelle + '</td><td>' + deleteRole + '</td></tr></tbody>';
            $('table').append(rol);
        });

    });





    $(document).on("click", "#addRole", async function () {

        const { value: formValues } = await Swal.fire({
            title: 'Add Role',
            showCancelButton: true,
            cancelButtonColor: '#d43f3a',
            cancelButtonText: 'Cancel',
            confirmButtonText: 'Add',
            html:
                '<input id="Name" placeholder="Name" class="swal2-input">',
            focusConfirm: false,
            preConfirm: () => {
                return [
                    document.getElementById('Name').value,
                ]
            }
        })


        $.ajax({
            url: '/Admin/Account/AddRole',
            type: 'Post',
            dataType: 'json',
            data: { Name: formValues[0] },
            success: function (gelenDeg) {
                if (gelenDeg == '1') {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: 'Added',
                        toast: true,
                        text: 'Added Role !',
                        showConfirmButton: false,
                        timer: 1500
                    })

                    clearData();
                    RolesData();
                }
                else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Not Added!',
                        text: 'Something went wrong'
                    })
                }
            }
        });

    });


    


    $(document).on("click", ".deleteRole", async function () {

        var id = $(this).attr('data-id');

        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: '/Admin/Account/DeleteJson',
                    type: 'Post',
                    dataType: 'json',
                    data: { 'id': id },
                    success: function (gelenDeg) {
                        if (gelenDeg == '1') {
                            Swal.fire({
                                position: 'top-end',
                                icon: 'success',
                                title: 'Deleted',
                                toast: true,
                                text: 'Deleted Role !',
                                showConfirmButton: false,
                                timer: 1500
                            })

                            clearData();
                            RolesData();
                        }
                        else {
                            Swal.fire({
                                icon: 'error',
                                title: 'Not Added!',
                                text: 'Something went wrong'
                            })
                        }
                    }
                });
            }
        })

        

    });

}

