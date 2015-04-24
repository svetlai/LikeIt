(function () {
    $('#categories').change(function () {
        $(this).parents('form').submit();
    });
})();

(function () {
    $('#SearchString').on('input', (function () {
        var controller = $(location).attr('pathname');
        var url = controller + '/Search';

        if (this.value.length >= 2) {
            var query = $('#SearchString').val();

            $.get(url, { searchString: query }, function (data) {
                $("#all-pages").hide();

                $("#ajax-search-results").html(data)
                    .show();

                if (data !== 'No pages found') {
                    $("#ajax-search-results").append("Press search for more results")
                }
            });
        }

        if (this.value.length === 0) {
            $("#all-pages").show();
            $("#ajax-search-results").hide()
            //$(this).parents('form').submit();
        }
    }));
})();

(function () {
    $('#all-tags').click(function (event) {
        if ($(event.target).is('a')) {
            $('#current-tag').html(event.target.innerText);
        }
    });
})();
