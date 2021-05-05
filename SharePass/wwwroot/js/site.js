
 $(function () {
    $("#sharePassForm").submit(function (e) {
        e.preventDefault();

        var formAction = $(this).attr("action");
         var pass = $('#pass-input').val();
        // var dataType = 'application/json; charset=utf-8';
         var data = { 
             "Password": pass 
            }
        var dataType = 'application/x-www-form-urlencoded; charset=utf-8';
        // var data = $('form').serialize();
        $.ajax({
            type: 'post',
            url: formAction,
            dataType: 'json',
            contentType: dataType,
            data: data,
        }).done(function (result) {
            var link = $("#generated-link");
            link.empty();
            link.html("You password link");
            link.attr("href", result);
        });
    });
});