

$(function() {
    $("#search").keyup(function () {
        var mPop = $(".messagepop");
        if ($(this).val().length >= 2) {
            $(".temp").remove();
            $.getJSON("/Boards/Result?search=" + $(this).val(), null, function (data) {
                $.each(data, function (i, item) {
                    $("#blist").append("<li class=\"temp\"><a href=\" \">" + item.Name + "</a></li>");
                });
            });
            $.getJSON("/Cards/Results?search=" + $(this).val(), null, function (data) {
                $.each(data, function (i, item) {
                    $("#clist").append("<li class=\"temp\"><a href=\"" + item.Url + "\">" + item.Description + "</a></li>");
                });
            });
        }
           
        if ($(this).val().length == 2 && mPop.css("display") == "none")
            mPop.fadeToggle("fast", "linear");
        if ($(this).val().length < 2 && mPop.css("display") != "none")
            mPop.fadeToggle("fast","linear");
    });
})

