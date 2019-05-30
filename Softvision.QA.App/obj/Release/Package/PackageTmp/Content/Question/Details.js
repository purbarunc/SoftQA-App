$(document).ready(function () {
    $("a#q_uv").click(function () {
        var point = $(this).attr("data-point");
        var questionId = $(this).attr("data-question");
        var target = $(this).attr("data-ref");

        Vote(questionId, point, target, "Question");
        $(this).removeClass("vote-up-off").addClass("vote-up-on");
        $("a#q_dv").removeClass("vote-down-on").addClass("vote-down-off");
    });

    $("a#q_dv").click(function () {
        var point = $(this).attr("data-point");
        var questionId = $(this).attr("data-question");
        var target = $(this).attr("data-ref");

        Vote(questionId, point, target, "Question");
        $(this).removeClass("vote-down-off").addClass("vote-down-on");
        $("a#q_uv").removeClass("vote-up-on").addClass("vote-up-off");
    });

    $("a#a_accepted,a#a_not_accepted").click(function () {
        var status = $(this).attr("data-status");
        var answerId = $(this).attr("data-answer");
        var loader = $(this).next(".ansLoader");
        AnswerStatus(answerId, status, loader);
    });
});

function Vote(qid, p, target, type) {
    if (type == "Question") {
        var data = JSON.stringify({ questionId: qid, point: p });
        $.ajax({
            type: "POST",
            url: "/Question/QuestionVote",
            data: data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $(target).text(data.Total);
            }
        });
    }
    else {
        var data = JSON.stringify({ answerId: qid, point: p });
        $.ajax({
            type: "POST",
            url: "/Question/AnswerVote",
            data: data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $(target).text(data.Total);
            }
        });
    }
}

function AnswerStatus(aid, s, loader) {
    var data = JSON.stringify({ answerId: aid, status: s });
    $.ajax({
        type: "POST",
        url: "/Question/AnswerStatus",
        data: data,
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $(loader).show();
        },
        success: function (data) {
            if (data.Status == "Success") {
                location.reload(true);
            }
        }
    });
}