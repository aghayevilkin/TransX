$(document).ready(function () {
    $(".commentBTN").click(function () {
        var id = $(this).data().id;
        var par = $(this).parent().parent().parent()
        var paaa = par.find("#CommentSub").attr('class');
        $("."+paaa).toggle("slow", function () {

        });
    });


    $(".socialBTN").click(function () {
        var id = $(this).data().id;
        var par = $(this).parent().parent().parent()
        var paaa = par.find("#SocialSub").attr('class');
        $("." + paaa).toggle("slow", function () {

        });
    });


    $("#image").click(function () {
        $("#imgSubBtn").removeClass("d-none");

    });






    
    $(".blog-bookmarked").click(function () {
        var blogId = $(this).data().id;
        var par = $(this).parent().parent().parent()
        var paaa = par.find(".blog-bookmarked").attr('class');
        var This = $(this);
        var IsAdded = false;

        if ($(this).hasClass("added")) {
            IsAdded = true;
        }

        if (IsAdded == true) {
            $.ajax({

                url: "/blog/removeFromBookmarked/",
                type: "get",
                dataType: "json",

                data: { blogId: blogId },
                success: function (response) {
                    if (response == 200) {
                        This.removeClass('fas');
                        This.addClass('far');
                        This.removeClass('added');
                        toastr.success('Blog exited without saving', { timeOut: 2000 });
                        console.log(response)
                    } else {
                        toastr.error('Blog exited without not saving', { timeOut: 2000 });
                    }
                    
                },
                error: function (response) {

                    console.log("error: " + response);
                }

            });
        } else {
            $.ajax({

                url: "/blog/addToBookmarked/",
                type: "get",
                dataType: "json",

                data: { blogId: blogId },
                success: function (response) {
                    if (response == 200) {
                        This.removeClass('far');
                        This.addClass('fas');
                        This.addClass('added');
                        toastr.success('Blog Saved', { timeOut: 2000 });
                        console.log(response)
                    }
                    else {
                        toastr.error('Blog Not Saved.', { timeOut: 2000 });
                    }
                    
                },
                error: function (response) {

                    console.log("error: " + response);
                }

            });
        }

    });




    $("#subscribe-form").submit(function (e) {
        e.preventDefault();

        var email = $("#subscribe-input").val();

        var pattern = /^\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b$/i;

        if (email == "") {
            toastr.error('Please, input your email address.', { timeOut: 2000 });
        }
        else if (!pattern.test(email)) {
            toastr.error('Not a valid e-mail address.', { timeOut: 2000 });
        }
        else {
            $.ajax({

                url: "/contact/addSubscribe/",
                type: "get",
                dataType: "json",

                data: { email: email },
                success: function (response) {
                    if (response == 200) {
                        //success
                        $("#subscribe-input").val('');
                        toastr.success('Now you are our subscriber, Thank you!', { timeOut: 2000 });

                    } else if (response == 411) {
                        //error
                        toastr.error('You can subscribe once with this email!', { timeOut: 2000 });
                    }
                    else {
                        //error
                        toastr.error('Please, input your email address.', { timeOut: 2000 });
                    }
                },
                error: function (response) {

                    console.log("error: " + response);
                }

            });

        }

    });



    $("#message-form").submit(function (e) {
        e.preventDefault();

        var name = $("#name-input").val();
        var email = $("#email-input").val();
        var subject = $("#subject-input").val();
        var content = $("#content-input").val();

        var pattern = /^\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b$/i;

        if (name == "") {
            toastr.error('Please, input your name.', { timeOut: 2000 });

        } else if (email == "") {
            toastr.error('Please, input your email address.', { timeOut: 2000 });
        }
        else if (subject == "") {
            toastr.error('Please, enter subject.', { timeOut: 2000 });
        } else if (content == "") {
            toastr.error('Please, enter message.', { timeOut: 2000 });
        }
        else if (!pattern.test(email)) {
            toastr.error('Not a valid e-mail address.', { timeOut: 2000 });
        }
        else {
            $.ajax({

                url: "/contact/addMessage/",
                type: "get",
                dataType: "json",

                data: { email: email, name: name, subject: subject, content: content },
                success: function (response) {
                    if (response == 200) {
                        //success
                        $("#name-input").val('');
                        $("#email-input").val('');
                        $("#subject-input").val('');
                        $("#content-input").val('');
                        toastr.success('You have now sent us a message, Thank you!', { timeOut: 2000 });

                    } else {
                        //error
                        toastr.error('Please, fill out the form completely.', { timeOut: 2000 });
                    }
                },
                error: function (response) {

                    console.log("error: " + response);
                }

            });

        }

    });


    $("#requestquote-form").submit(function (e) {
        e.preventDefault();

        var from = $("#from-input").val();
        var to = $("#to-input").val();
        var when = $("#when-input").val();
        var service = $("#service-input").val();

        if (from == "") {
            toastr.error('Please, enter from.', { timeOut: 2000 });

        } else if (to == "") {
            toastr.error('Please, enter To.', { timeOut: 2000 });
        }
        else if (when == "") {
            toastr.error('Please, enter when.', { timeOut: 2000 });
        } else if (service == 0) {
            toastr.error('Please, enter transport.', { timeOut: 2000 });
        }
        else {
            $.ajax({

                url: "/home/addRequestQuote/",
                type: "get",
                dataType: "json",

                data: { from: from, to: to, when: when, service: service },
                success: function (response) {
                    if (response == 200) {
                        //success
                        $("#from-input").val('');
                        $("#to-input").val('');
                        $("#when-input").val('');
                        $("#service-input").val(0);
                        toastr.success('You have now sent us a request for quotation, Thank you!', { timeOut: 2000 });

                    } else {
                        //error
                        toastr.error('Please, fill out the form completely.', { timeOut: 2000 });
                    }
                },
                error: function (response) {

                    console.log("error: " + response);
                }

            });

        }

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