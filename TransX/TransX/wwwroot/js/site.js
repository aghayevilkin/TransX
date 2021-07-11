$(document).ready(function () {
    $(".commentBTN").click(function () {
        var id = $(this).data().id;
        console.log(id);
        var par = $(this).parent().parent().parent()
        var paaa = par.find("#CommentSub").attr('class');
        $("."+paaa).toggle("slow", function () {

        });
        console.log(paaa);
    });

});