$(document).ready(function () {
    $(".commentBTN").click(function () {
        var id = $(this).data().id;
        console.log(id);
        var par = $(this).parent().parent().parent()
        var paaa = par.find("#CommentSub").attr('class');
        $("."+paaa).toggle("slow", function () {

        });
    });


    $(".socialBTN").click(function () {
        var id = $(this).data().id;
        console.log(id);
        var par = $(this).parent().parent().parent()
        var paaa = par.find("#SocialSub").attr('class');
        $("." + paaa).toggle("slow", function () {

        });
    });


    $("#image").click(function () {
        console.log("clicked");
        $("#imgSubBtn").removeClass("d-none");

    });

});


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

function readURLTwo(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#file_uploadTwo')
                .attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}