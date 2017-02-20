function initAjax() {

    var token = sessionStorage['authToken'];
    if (token) {
        $.ajaxSetup({
            headers: { "Authorization": 'Basic ' + btoa(token) }
        });
        console.log('initializing ajax token');
    }
}

initAjax();

function login() {
    var data = {
        email: $('#email').val(),
        password: $('#password').val()
    };

    $.post('/get/token', data).success(function (data) {
        //sessionStorage['Token'] = data.Token.AuthToken;

        $.ajaxSetup({
            headers: { "Authorization": 'Basic ' + btoa(data.Token.AuthToken) }
        });

        //$('#loginForm').hide();
        //$('#logoutBtn').show();
        window.location.href = '/Home/Index';
        //alert('success');

        sessionStorage['userId'] = data.userId;
        sessionStorage['authToken'] = data.Token.AuthToken;
        document.cookie = 'userId=' + data.userId + ';path=/';
        debugger;
        $.get('/api/account/GetUserName?id=' + data.userId).success(function (userNameData) {
            debugger;
            document.cookie = 'userName=' + userNameData + ';path=/';
        });

    })
        .fail(function (data) {
            alert('fail');
        });
    

    //document.cookie = 'userName=' + 
}

function testAuth() {

    $.get('/api/Trainee/test').success(function (data) {
        alert(data);
    })
    .fail(function (data) {
        alert('not authorized');
    });


}

function register() {
    var data = {
        email: $('#txtEmail').val(),
        password: $('#txtPassword').val(),
        confirmPassword: $('#txtConfirmPassword').val()
    };
    var existence;

    $.get('/api/AccountApi/CheckUserNameAndEmailExistence?email=' + $('#txtEmail').val() + '&userName=' + $('#txtUserName').val()).success(function (existence) {
        debugger;
        if (existence[0]) {
            alert('email is already exists');
            return;
        }
        else if(existence[1]) {
            alert('User Name is already exists choose another one')
            return;
        }
        else {
            $.post('/api/Account/register?userRole=' + $('#cmbRole').val() + '&userName=' + $('#txtUserName').val(), data).success(function (data) {


                alert('success');

            })
        .fail(function (data) {
            alert('fail');
        });
        }        
    });
}

function logout() {
    $.post('/api/account/logout').success(function () {
        sessionStorage['userId'] = null;
        sessionStorage['authToken'] = null;
        document.cookie = 'userId=;path=/';
        document.cookie = 'userName=;path=/';
        window.location.href = '/Account/Index';
    });


}

function setUserName() {
    var data = {
        username: $('#txtUserName').val()
    };
    var userid = sessionStorage['userId']

    $.post('/api/Account/SetUserName?userNameId=' + sessionStorage['userId'], data).success(function (data) {

        alert('success');
    })
    .fail(function (data) {
        alert('fail');
    });
}


function updateAccountInfo() {
    var data = {
        email: $('#txtEmail').val(),
        phoneVisibility: $('#txtPhoneVisibility').is(":checked"),
        username: $('#txtUserName').val(),
        userVisibility: $('#txtUserVisibility').is(":checked")
    };


    $.post('/Account/UpdateAccount', data).success(function (data) {

        alert('success');
    })
    .fail(function (data) {
        alert('fail');
    });
}

function showTrainingPlan(Id) {
    window.location.href = '/TraineeView/ViewTrainingPlanDetails?trainingPlanId=' +Id;
}

function searchUser() {

    var username = $('#userName').val();
    var userId = sessionStorage['userId']
    $.get('/findUser?userName=' + username + '&userId=' + userId).success(function (data) {
        $('#search-results').html('');

        var result = '';
        data.forEach(function (user) {
            switch (user.UserType) {
                case "Trainer": $('#search-results').append('<br/><a href="/Trainer/ViewTrainer?TrainerId=' + user.Id + '" >' + user.UserName + ' (' + user.UserType + ')</a>');
                    break;
                case "Trainee": $('#search-results').append('<br/><a href="/TraineeView/ViewTrainee?TraineeId=' + user.Id + '" >' + user.UserName + ' (' + user.UserType + ')</a>');
                    break;
                case "Gym": $('#search-results').append('<br/><a href="/Gym/ViewGym?GymId=' + user.Id + '" >' + user.UserName + ' (' + user.UserType + ')</a>');
                    break;

                        //        $('#search-results').append('<br/><a href="/Trainer/ViewTrainer?TrainerId=' + user.Id + '" >' + user.UserName + ' (' + user.UserType + ')</a>');
            }
            
        
        });
    })
        .fail(function (data) {
            alert('not found');
        });
}

function advancedSearch() {

    var userType = $('#User_Types').val();
    if ($('#Genders').is(":visible")) {
        var genderPreferences = $('#Genders').val();
    }
    else {
        var genderPreferences = "";
    }
    
    var locationPreferences = "Everywhere";
    var expertisePreferences = "DoesntMatter";
    if ($('#Reputation').is(":visible")) {
        var reputationPreferences = $('#Reputation').val();
    }
    else {
        var genderPreferences = "1"; 
    }

    if ($('#Price').is(":visible")) {
        var pricePreferences = $('#Price').val();
    }
    else {
        var pricePreferences = ">100";
    }
    
    if ($('#ExperienceYears').is(":visible")) {
        var experienceYearsPreferences = $('#ExperienceYears').val();
    }
    else {
        var experienceYearsPreferences = "";
    }

    if ($('#Showers').is(":visible")) {
        var showersPreferences = $('#Showers').val();
    }
    else {
        var showersPreferences = "";
    }
    
    if ($('#Parking').is(":visible")) {
        var parkinglotPreferences = $('#Parking').val();
    }
    else {
        var parkinglotPreferences = "";
    }

    if (genderPreferences == "") {
        genderPreferences = "DoesntMatter";
    }
    
    if (reputationPreferences == "") {
        reputationPreferences = 1;
    }

    if (pricePreferences == "")
        pricePreferences =  "<100";

    if (showersPreferences == "" || showersPreferences == "2")
        showersPreferences = true;

    if (parkinglotPreferences == "" || parkinglotPreferences == "2")
        parkinglotPreferences = true;
  
    var expertises = [];

    $('#dvExpertisesId :checked').each(function (index, item) {
        expertises.push(item.name);
    });
   
    var request = {
        userType: userType,
        userId: sessionStorage['userId'],
        GenderFilter: genderPreferences,
        Location: locationPreferences,
        Expertise: expertises,
        Reputation: reputationPreferences,
        Price: pricePreferences,
        Showers: showersPreferences,
        Parking: parkinglotPreferences,
        ExperienceYears: experienceYearsPreferences
    };
    

    $.post('/advancedSearch', request).success(function (data) {
        $('#search-results').html('');
    
        var result = '';
        data.forEach(function (user) {
            switch (user.UserType) {
                case "Trainer": $('#search-results').append('<br/><a href="/Trainer/ViewTrainer?TrainerId=' + user.Id + '" >' + user.UserName + ' (' + user.UserType + ')</a>');
                    break;
                case "Trainee": $('#search-results').append('<br/><a href="/TraineeView/ViewTrainee?TraineeId=' + user.Id + '" >' + user.UserName + ' (' + user.UserType + ')</a>');
                    break;
                case "Gym": $('#search-results').append('<br/><a href="/Gym/ViewGym?GymId=' + user.Id + '" >' + user.UserName + ' (' + user.UserType + ')</a>');
                    break;

                    //        $('#search-results').append('<br/><a href="/Trainer/ViewTrainer?TrainerId=' + user.Id + '" >' + user.UserName + ' (' + user.UserType + ')</a>');
            }


        });
    })
        .fail(function (data) {
            alert('not found');
        });
}


function requestConnection(userId, connectionType) {
    debugger;
    var request = {
        Reciever: userId,
        Sender: sessionStorage['userId'],
        ConnectionType: connectionType 
    };
        
    $.post('/RequestConnection', request).success(function (data) {
      
    });

}

function respondToConnectionRequest(connId, response) {

    var actionName = response ? 'ApproveRequest' : 'DeclineRequest';

    $.post('/' + actionName + '?id=' + connId).success(function (data) {
        if(data.actionName == 'DeclineRequest')
            $('#' + requestbtn).show();
    });

    $('#' + connId).hide();
}

function requestMedicalCondition(userId) {

       var url = "/TraineeView/ViewTraineeMedicalCondition?TraineeId=" + userId;
       window.location.href = url;
}

function CreateExercisesView() {
    
    var data = {
        trainerId: sessionStorage['userId'],
        description: $('#Description').val(),
        planName: $('#PlanName').val()
    };
    debugger;
    sessionStorage['TrainingPlan'] = JSON.stringify(data);
    window.location.href = '/Trainer/CreateExercisesView';
}
/*
function addAnotherChallenge() {
    challengeCounter++;
    var data = {
        challengeid: challengeCounter.toString(),
        description: $('#Description').val(),
        challengeId: $('#challengeId').val(),
        traineeId: $('#TraineeId').val(),
        trainerId: $('#TrainerId').val(),
        trainerName: $('#TrainerName').val(),
        traineeName: $('#TraineeName').val(),
        challengeName: $('#ChallengeName').val(),
        challengeIsCompleted: $('#IsCompleted').val()
    };

    app.challenges.push(data);

    $('#Description').val('');
    $('#challengeId').val('');
    $('#TraineeId').val('');
    $('#TrainerId').val('');
    $('#TrainerName').val('');
    $('#TraineeName').val('');
    $('#ChallengeName').val('');
    $('#IsCompleted').val('');
}
*/

function addAnotherChallenge() {

    var challengeType = $('#Challenge_Types').val();
    var value;
    var timeValue;
    switch (challengeType) {
        case '1':
            value = $('#Running_Distance').val();
            timeValue = $('#Time_Target').val();
            break;
        case '2':
            value = $('#Weights').val();
            timeValue = "";
            break;
        case '3':
            value = $('#Pushups_number').val();
            timeValue = "";
    }

    var data = {
        ChallengeId: "",
        TraineeId: readCookie("watchedUserId"),
        //TraineeId: null,
        TrainerId: sessionStorage['userId'],
        //TrainerId: null,
        ChallengeType: $('#Challenge_Types').val(),
        ChallengeValue: value,
        ChallengeTime: timeValue,
        IsCompleted: false,
        ChallengeStatus: 0
    };
    debugger;
    app.challenges.push(data);

    $('#Challenge_Types').val('');
    $('#Running_Distance').val('');
    $('#Time_Target').val('');
    $('#Weights').val('');
    $('#Pushups_number').val('');
    /*
    $.post('/api/sendChallenge', data).success(function (data) {
        debugger;
    });
    */
}

var challengeCounter = 0;

function sendChallenges() {
    challengeCounter = 1;
    addAnotherChallenge();

    var data = {
        Challenges: app.challenges
    };
    debugger;
    $.post('/api/CreateChallenges', data).success(function (data) {
        debugger;
    });
}



function addAnotherExercise() {

    exercisesCounter++;
    var data = {
        exerciseid: exercisesCounter.toString(),
        description: $('#Description').val(),
        exercisetype: $('#ExerciseType').val(),
        setsNum: $('#SetsNum').val(),
        repetitions: $('#Repetitions').val(),
        note: $('#Note').val(),
        musclename: $('#MuscleName').val(),
        exerciseName: $('#ExerciseName').val(),
        planid: $('#PlanId').val(),

        // TODO add all fields here
    };
    
    // add to exercises array
    app.exercises.push(data);
    // clear the form
    
    $('#SetsNum').val('');
    $('#Description').val('');
    $('#ExerciseType').val('');
    $('#SetsNum').val('');
    $('#Repetitions').val('');
    $('#ExerciseName').val('');
    $('#Note').val('');
    $('#MuscleName').val('');
    //$('#ExerciseId').val(counter.toString());
}

var exercisesCounter = 0;

function createTrainingPlan() {
    exercisesCounter = 1;
    addAnotherExercise();

    debugger;

    var data = {
        trainerId: sessionStorage['userId'],
        traineeId: readCookie("watchedUserId"),
        planName: JSON.parse(sessionStorage['TrainingPlan']).planName,
        description: JSON.parse(sessionStorage['TrainingPlan']).description,
        exercises: app.exercises
    };

    $.post('/api/CreateTrainingPlan', data).success(function (data) {
        debugger;
    });
}

function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function showVisitorProfile(connId) {
    var contact = "";
    $.get('/api/ConnectionInformation' + '?id=' + connId).success(function (data) {
        if (data.Sender == sessionStorage['userId'])
            contact = data.Reciever;
        else {
            contact = data.Sender;
        }     
        $.get('/FinduserById' + '?userId=' + contact).success(function (addressedUserData) {
            switch (addressedUserData.UserType) {
                case "Trainer":
                    window.location.href = '/Trainer/ViewTrainer' + '?TrainerId=' + addressedUserData.Id;
                    
                    $(document).ready(function () {
                        $('#backToSearchbtn').hide();
                        $('#backToConnectionbtn').show();
                    });   
                    break;
                case "Trainee":
                    window.location.href = '/TraineeView/ViewTrainee' +'?TraineeId=' + addressedUserData.Id;
                    break;
                case "Gym":
                    window.location.href = '/Gym/ViewGym' +'?GymId=' + addressedUserData.Id;
                    break;
            }
        });
    });
}

function updateRating(userId) {
    var reputation = parseInt($('input:radio[name=option]:checked').val());
    debugger;
    $.post('/UpdateTrainerReputation' + '?trainerId=' + userId +'&reputation=' + reputation).success(function (data) {
        alert('Thank you for rating');
        var dvRating = document.getElementById("ratingInterface");
        var dvRatingbutton = document.getElementById("rating");
        dvRating.style.display = "none"
        dvRatingbutton.style.display = "none"
    });
}

function updateGymRating(userId) {
    var reputation = parseInt($('input:radio[name=option]:checked').val());
    debugger;
    $.post('/UpdateGymReputation' + '?gymId=' + userId + '&reputation=' + reputation).success(function (data) {
        alert('Thank you for rating');
        var dvRating = document.getElementById("ratingInterface");
        var dvRatingbutton = document.getElementById("rating");
        dvRating.style.display = "none"
        dvRatingbutton.style.display = "none"
    });
}

function showUserProfile(userId) {

    var data = { Id: userId}
    $.ajax({
        type: "POST",
        data: JSON.stringify(data),
        url: '/ViewUser',
        contentType: "application/json",
        success: function(data){
            
            

            if (data.UserType == "Trainer") {
                window.location.href = '/Trainer/viewtrainer';
                $('#profile-form').html('');
                var result = '';
                $('#profile-form').append("Walla");

               
               
                
            }
        },
        fail: function (data) {
            alert('not found');
        }
    });
}

function uploadImage() {

    var data = $("#userImage").val();
    $.post('/api/Account/UploadImage', data, function () {
        alert('success');
    });
}
