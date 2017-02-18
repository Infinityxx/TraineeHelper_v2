using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using DataModel;
using TraineeHelper.Logic;
using TraineeHelper.DataModel;
using DataModel.Entities;

namespace TraineeHelper.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //Logic.LoginManager loginMng = new LoginManager();
            //bool checkLogin = loginMng.Login("", "");
            //System.Console.WriteLine(checkLogin);
           
            LoginManager loginMng = new LoginManager();
           
            
            
            System.Console.WriteLine("Mongo DB Test Application");
            System.Console.WriteLine("====================================================");
            System.Console.WriteLine("Started By: Shai Ben-Zvi");
            System.Console.WriteLine("====================================================");
            System.Console.WriteLine("Initializaing connection");
            loginMng.Login();
            
            Trainee trainee1 = new Trainee();
            //trainee1.Id = new ObjectId();
            trainee1.UserType = "Trainee";
            trainee1.Email = "shaibenzvi1@gmail.com";
            trainee1.UserName = "Shai";
            trainee1.Password = "12345";
            //trainee1.Location = null;
            //dtrainee1.CreatioDateUser = DateTime.Now;
            trainee1.Birthday = DateTime.Today;
            trainee1.PhoneNumber = "1234";
            trainee1.PhoneVisibility = true;
            trainee1.UserVisibility = true;
            //trainee1.TraineeId = 2;
            //trainee1.TrainingPlan = new TrainingPlan();
            //trainee1.Achievements = null;
            //trainee1.Birthday = DateTime.Today;
            trainee1.MedicalCondition = new MedicalCondition();
            trainee1.MedicalCondition.Age = 27;
            trainee1.MedicalCondition.FatPercent = 20;
            trainee1.MedicalCondition.MuscleMass = 50;
            trainee1.MedicalCondition.Height = 179;
            trainee1.MedicalCondition.Weight = 70;
            trainee1.Gender = Gender.Male;
            //trainee1.MedicalCondition.Id = ObjectId.GenerateNewId();
            //trainee1.TraineeId = 1;
            
         //   
           //Exercise exercise = new Exercise();
           //exercise.Description = "dsds";
           //exercise.ExerciseId = 1;
           //exercise.ExerciseName = "pushups";
           //exercise.ExerciseType = "Legs";
           //exercise.Note = "some notes";
           //exercise.PlanId = 3;
           //exercise.MuscleName = "leg";
           //exercise.Repetitions = 10;
           //exercise.SetsNum = 3;
           
           ExerciseContext exercise3 = new ExerciseContext();
           exercise3.Description = "slomi";
           exercise3.ExerciseId = 1;
           exercise3.ExerciseName = "press";; 
           exercise3.ExerciseType = "Legssss";
           exercise3.Note = "holololo";
           exercise3.PlanId = 4;
           exercise3.MuscleName = "legss";
           exercise3.Repetitions = 12;
           exercise3.SetsNum = 4;

           RegisterManager RegManager = new RegisterManager();
           //RegManager.RegisterTrainee(trainee1);
            //RegManager.AddExercise(exercise);
           //RegManager.AddExercise(exercise2);
           //RegManager.DeleteExercise("57c80909a719f93bd40afa76");
           //SearchManager searchMng = new SearchManager();
           //Exercise exercise3  = searchMng.SearchExercise("57cd7385a719f93b30da7737");
           
           //UpdateManager updateMng = new UpdateManager();
           //updateMng.UpdateExercise(exercise3);

           System.Console.ReadLine();
        }
    }
}
