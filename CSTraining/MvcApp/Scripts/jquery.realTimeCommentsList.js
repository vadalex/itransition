(function($) {

    var getCommentItem = function (commentData) {
        var item = $('<div><table class="table table-bordered"><tbody><tr class="success"><td><span>' + commentData.Author +
            '</span><br/><span>' + commentData.Date +
            '</span></td></tr><tr><td><p class="lead">' + commentData.Comment +
            '</p></td></tr></tbody></table></div>');
        return item;
    }

    var updateCommentsList = function (commentsList) {
        $.ajax({
            url: '/Comment/GetAllComments',
            dataType: 'json',
            type: 'post',
            contentType: 'application/json; charset=utf-8',
            async: true,
            processData: false,
            cache: false,
            success: function (data) {
                commentsList.empty();
                $.each(data, function (i, item) {
                    commentsList.append(getCommentItem(item));
                });
            }
        });
    }

    jQuery.fn.realTimeCommentsList = function () {
        var commentsList = this;

        var plugin = function () {
            updateCommentsList(commentsList);
            window.setInterval(function () { updateCommentsList(commentsList); }, 1000);
        }

        return this.each(plugin);
    }
})(jQuery)