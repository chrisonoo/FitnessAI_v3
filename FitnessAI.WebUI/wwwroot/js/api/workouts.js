$(document).ready(function () {
    $('a[data-target="#editWorkoutModal"]').click(function () {
        let workoutId = $(this).data('workout-id');
        let workoutTitle = $(this).data('workout-title');

        $('#workoutEditId').val(workoutId);
        $('#workoutEditTitle').val(workoutTitle);
    });
});

$(document).ready(function () {
    $('a[data-target="#deleteWorkoutModal"]').click(function () {
        let workoutId = $(this).data('workout-id');
        let workoutTitle = $(this).data('workout-title');

        $('#workoutDeleteId').val(workoutId);
        $('#workoutDeleteTitle').val(workoutTitle);
    });
});

$(document).ready(function () {
    $('.row-workout').click(function () {
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
        showExercises(workoutId);
    });
});

function showExercises(workoutId) {
    getExercises(workoutId)
        .then(data => {
            let workoutExercisesTable = $('#workout-exercises');
            let exercisesToAddTable = $('#exercises-to-add');

            workoutExercisesTable.html(renderWorkoutExercises(data.workout_exercises));
            exercisesToAddTable.html(renderExercisesToAdd(data.exercises_to_add));
            addEventsToExercises();
        })
        .catch(error => console.error('Error:', error));
}

function getExercises(workoutId) {
    return new Promise((resolve, reject) => 
        $.ajax({
            url: '/api/workoutapi/exercisesforworkout/' + workoutId, 
            type: 'GET', 
            success: resolve, 
            error: reject
        })
    );
}

function renderWorkoutExercises(exercises) {
    let html = '';
    for (let exercise of exercises) {
        html += `<tr>
                <td style="width: 55%">${exercise.exerciseTitle}</td>
                <td style="width: 30%">${exercise.exerciseCategory}</td>
                <td style="width: 15%; text-align: end">
                    <a href="#"
                        class="delete-exercise-from-workout"
                        data-exercise-id="${exercise.exerciseId}"
                        data-workout-id="${exercise.workoutId}"
                        style="text-decoration: none !important;">
                        <i class="fas fa-solid fa-trash-can"></i>
                    </a>
                </td>
            </tr>`;
    }
    return html;
}

function renderExercisesToAdd(exercises) {
    let html = '';
    for (let exercise of exercises) {
        html += `<tr>
                <td style="width: 55%">${exercise.exerciseTitle}</td>
                <td style="width: 30%">${exercise.exerciseCategory}</td>
                <td style="width: 15%; text-align: end">
                    <a href="#"
                        class="add-exercise-to-workout"
                        data-exercise-id="${exercise.exerciseId}"
                        data-workout-id="${exercise.workoutId}"
                        style="text-decoration: none !important;">
                        <i class="fas fa-solid fa-circle-plus"></i>
                    </a>
                </td>
            </tr>`;
    }
    return html;
}

function addEventsToExercises() {
    $('.delete-exercise-from-workout').click(function () {
        let exerciseId = $(this).data('exercise-id');
        let workoutId = $(this).data('workout-id');

        changeExerciseAssigment(exerciseId, workoutId, false);
        showExercises(workoutId)
    });

    $('.add-exercise-to-workout').click(function () {
        let exerciseId = $(this).data('exercise-id');
        let workoutId = $(this).data('workout-id');

        changeExerciseAssigment(exerciseId, workoutId, true);
        showExercises(workoutId)
    });
}

function changeExerciseAssigment(exerciseId, workoutId, isAssigned) {
    let data = {
        exerciseId,
        workoutId,
        isAssigned
    };

    $.ajax({
        url: '/api/workoutapi/changeexerciseassigment',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify(data),
        success(response) {
            console.log(response);
        },
        error(error) {
            console.log(error);
        }
    });
}