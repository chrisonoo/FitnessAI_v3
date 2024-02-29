$(document).ready(function() {
    $('a[data-target="#editWorkoutModal"]').click(function() {
        let workoutId = $(this).data('workout-id');
        let workoutTitle = $(this).data('workout-title');

        $('#workoutEditId').val(workoutId);
        $('#workoutEditTitle').val(workoutTitle);
    });
});

$(document).ready(function() {
    $('a[data-target="#deleteWorkoutModal"]').click(function() {
        let workoutId = $(this).data('workout-id');
        let workoutTitle = $(this).data('workout-title');

        $('#workoutDeleteId').val(workoutId);
        $('#workoutDeleteTitle').val(workoutTitle);
    });
});

$(document).ready(function() {
    $('.row-workout').click(function() {
        $('.row-workout')
            .css('background-color', '')
            .css('color', '');
        $('.row-workout a')
            .css('color', '');

        $(this)
            .css('background-color', '#2a4db2')
            .css('color', 'white');
        $(this).find('a')
            .css('color', 'white');

        let workoutId = $(this).data('row-workout-id');
        showMyExercises(workoutId);
    });
});

function showMyExercises(workoutId) {
    getMyExercises(workoutId);
}

function getMyExercises(workoutId) {
    $.ajax({
        url: '/api/workoutapi/exercisesforworkout/' + workoutId,
        type: 'GET',
        success: function(data) {
            alert(data);
            // $('#exercises-list').html(data);
        }
    });
}