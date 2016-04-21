var post = function (id, message, username, date) {
    this.id = id;
    this.message = message;
    this.username = username;
    this.date = date;
    this.comments = ko.observableArray([]);

    this.addComment = function (context) {
        var comment = $('input[name="comment"]', context).val();
        if (comment.length > 0) {
            $.connection.boardHub.server.addComment(this.id, comment, vm.username())
            .done(function(){
                $('input[name="comment"]', context).val('');
            });
        }
    };
}

var comment = function (id, message, username, date) {
    this.id = id;
    this.message = message;
    this.username = username;
    this.date = date;
}

var vm = {
    posts: ko.observableArray([]),
    notifications: ko.observableArray([]),
    username: ko.observable(),
    signedIn: ko.observable(false),
    signIn: function () {
        vm.username($('#username').val());
        vm.signedIn(true);
    },
    writePost: function () {
        $.connection.boardHub.server.writePost(vm.username(), $('#message').val()).done(function () {
            $('#message').val('');
        });
    },
}
ko.applyBindings(vm);

function loadPosts() {
    $.get('/api/posts', function (data) {
        var postsArray = [];
        $.each(data, function (i, p) {
            var newPost = new post(p.Id, p.Message, p.Username, p.DatePosted);
            newPost.comments.push(newComment);
        });
    });
}

$(function () {
    var hub = $.connection.boardHub;
    $.connection.hub.start().done(function () {
        loadPosts();
    });

    hub.client.receivedNewPost = function (id, username, message, date) {
        var newPost = new post(id, message, username, date);
        vm.posts.unshift(newPost);

        if (username !== vm.username()) {
            vm.notificcations.unshift(username + ' has added a new post.');
        }
    };

    hub.client.receivedNewComment = function (parentPostId, commentId, message, username, date) {
        var postFilter = $.grep(vm.posts(), function (p) {
            return p.id === parentPostId;
        });
        var thisPost = postFilter[0];

        var thisComment = new comment(commentId, message, username, date);
        thisPost.comments.push(thisComment);

        if (thisPost.username === vm.username() && thisComment.username !== vm.username()) {
            vm.notifications.unshift(username + ' has commented on your post.');
        };
    }
});