app["comments"] = {};
app.comments = (function () {

    init = function () {
        $('.delete-item-comment').on('click', function (e) {
            e.preventDefault();
            let $this = $(this);
            $.ajax({
                url: $this.attr('href'),
                type: 'delete'
            }).done(res => {
                let commentId = $this.data('comment-id');
                $(`#comment-with-id-${commentId}`).remove();
                console.log(res);
            }).fail(err => {
                console.error(err);
            })
        });
    }

    return {
        init: init
    }
})();