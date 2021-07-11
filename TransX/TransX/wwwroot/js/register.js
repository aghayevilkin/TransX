$("#login_form").validate();

$(function () {
	$('#Password').keyup(function () {

		if (this.value.length < 8) {
			$("#pw-min-8chars").removeClass().addClass("pwfail");
		}
		else {
			$("#pw-min-8chars").removeClass().addClass("pwpass");
		}


		if (!this.value.match(/[a-z]/)) {
			$("#pw-min-1char").removeClass().addClass("pwfail");
		}
		else {
			$("#pw-min-1char").removeClass().addClass("pwpass");
		}


		if (!this.value.match(/[A-Z]/)) {
			$("#pw-min-1capital").removeClass().addClass("pwfail");
		}
		else {
			$("#pw-min-1capital").removeClass().addClass("pwpass");
		}


		if (!this.value.match(/[0-9]/)) {
			$("#pw-min-1number").removeClass().addClass("pwfail");
		}
		else {
			$("#pw-min-1number").removeClass().addClass("pwpass");
		}

		if (!this.value.match(/[!@#$%^&*]/)) {
			$("#pw-min-symbols").removeClass().addClass("pwfail");
		}
		else {
			$("#pw-min-symbols").removeClass().addClass("pwpass");
		}



		if (this.value.match(/(.)\1\1\1/)) {
			$("#pw-no-repeat").removeClass().addClass("pwfail");
		}
		else {
			$("#pw-no-repeat").removeClass().addClass("pwpass");
		}


		if (this.value.length == 0) {
			$(".pwhints ul li").each(function () {
				$(this).removeClass();
			});
		}

	});
});




/*** Sign Up ***/

$(document).ready(function () {
	$flag = 1;
	/***** Personal Data Validation ****/
	$("#Name").keyup(function () {
		if ($(this).val() == '') {
			$(this).css("border-color", "#cd2d00");
			$('#submit').attr('disabled', true);
			$("#error_name").text("* You have to enter your first name!");
		} else {
			$(this).css("border-color", "#2eb82e");
			$('#submit').attr('disabled', false);
			$("#error_name").text("");

		}
	});
	$("#Surname").keyup(function () {
		if ($(this).val() == '') {
			$(this).css("border-color", "#cd2d00");
			$('#submit').attr('disabled', true);
			$("#error_surname").text("* You have to enter your last name!");
		} else {
			$(this).css("border-color", "#2eb82e");
			$('#submit').attr('disabled', false);
			$("#error_surname").text("");
		}
	});

	/***** Email Validation ****/

	function validateEmail(sEmail) {
		var filter = /^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/;
		if (filter.test(sEmail)) {
			return true;
		} else {
			return false;
		}
	}
	$("#Email").keyup(function () {
		var sEmail = $('#Email').val();
		if ($.trim(sEmail).length == 0) {
			$(this).css("border-color", "#cd2d00");
			$('#submit').attr('disabled', true);
			$("#error_email").text("Please enter valid email address");

			e.preventDefault();
		}
		if (validateEmail(sEmail)) {
			$(this).css("border-color", "#2eb82e");
			$('#submit').attr('disabled', false);
			$("#error_email").text("");;
		} else {
			$(this).css("border-color", "#cd2d00");
			$('#submit').attr('disabled', true);
			$("#error_email").text("Invalid email address");
			e.preventDefault();
		}
	});

	/***** Login Data Validation ****/


	$("#Password").keyup(function () {
		if ($(this).val() == '') {
			$(this).css("border-color", "#cd2d00");
			$('#submit').attr('disabled', true);
			$("#error_password").text("* You have to enter your Password!");
		} else {
			$(this).css("border-color", "#2eb82e");
			$('#submit').attr('disabled', false);
			$("#error_password").text("");
		}
	});
	$("#Confirm").keyup(function () {
		if ($("#Confirm").val() !== $("#Password").val()) {
			$("#Confirm").css("border-color", "#cd2d00");
			$('#submit').attr('disabled', true);
			$("#error_confirm").text("Passwords Do not match!");
		} else {
			$(this).css("border-color", "#2eb82e");
			$('#submit').attr('disabled', false);
			$("#error_confirm").text("");
		}
	});

	/***+* Submit Validation ****/
	$("#submit").click(function () {
		if ($("#Name").val() == '') {
			$("#Name").css("border-color", "#cd2d00");
			$('#submit').attr('disabled', true);
			$("#error_name").text("* You have to enter your first name!");
		}
		if ($("#Surname").val() == '') {
			$("#Surname").css("border-color", "#cd2d00");
			$('#submit').attr('disabled', true);
			$("#error_surname").text("* You have to enter your Last name!");
		}
		if ($("#Email").val() == '') {
			$("#Email").css("border-color", "#FF0000");
			$('#submit').attr('disabled', true);
			$("#error_email").text("* You have to enter your Email  !");
		}
		if ($("#Password").val() == '') {
			$("#Password").css("border-color", "#cd2d00");
			$('#submit').attr('disabled', true);
			$("#error_password").text("Enter a Password");
		}
		if ($("#Confirm").val() == '') {
			$("#Confirm").css("border-color", "#cd2d00");
			$('#submit').attr('disabled', true);
			$("#error_confirm").text("Confirm Password");
		}

	});


});