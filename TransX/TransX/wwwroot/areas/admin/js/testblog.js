// Document is ready
$(document).ready(function () {

	// Validate Title
	$('#titlecheck').hide();
	$('#title').keyup(function () {
		validateTitle();
	});

	function validateTitle() {
		let usernameValue = $('#title').val();
		if (usernameValue.length == '') {
			$('#titlecheck').show();
			usernameError = false;
			return false;
		}
		else if ((usernameValue.length < 3)) {
			$('#titlecheck').show();
			$('#titlecheck').html
				("**Minimum Title length must be 3");
			usernameError = false;
			return false;
			$('#submitbtn').css('display','none');
		}
		else {
			$('#titlecheck').hide();
		}
	}


	// Validate Content
	$('#contentcheck').hide();
	$('#Content').keyup(function () {
		validateContent();
	});

	function validateContent() {
		let usernameValue = $('#Content').val();
		if (usernameValue.length == '') {
			$('#contentcheck').show();
			usernameError = false;
			return false;
		}
		else if ((usernameValue.length < 3)) {
			$('#contentcheck').show();
			$('#contentcheck').html
				("**Minimum Content length must be 3");
			usernameError = false;
			return false;
			$('#submitbtn').css('display', 'none');
		}
		else {
			$('#contentcheck').hide();
		}
	}



	// Submitt button
	$('#submitbtn').click(function () {
		validateTitle();
		validateContent();
	});
});
