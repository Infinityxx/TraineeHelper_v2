using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Models;
using TraineeHelper.ViewModels;

namespace TraineeHelper.Logic.Converters
{
    public static class ExerciseConverter
    {
        public static List<Exercise> ConvertToNewExercises(this List<ExerciseContext> contexts)
        {
            List<Exercise> exercises = new List<Exercise>();
            return exercises;
        }

        public static Exercise ConvertToNewExercise(this ExerciseContext context)
        {
            Exercise exercise = new Exercise();
            return exercise;
        }

        public static ExerciseContext ConvertToExerciseContext(this Exercise exercise)
        {
            ExerciseContext exerciseContext = new ExerciseContext();
            if (null == exercise)
                return exerciseContext;
            exerciseContext.Description = exercise.Description;
            exerciseContext.ExerciseId = exercise.ExerciseId;
            exerciseContext.ExerciseName = exercise.ExerciseName;
            exerciseContext.ExerciseType = exercise.ExerciseType;
            exerciseContext.MuscleName = exercise.MuscleName;
            exerciseContext.Note = exercise.Note;
            exerciseContext.Repetitions = exercise.Repetitions;
            exerciseContext.SetsNum = exercise.SetsNum;
            exerciseContext.Status = exercise.Status;

            return exerciseContext;
        }

        public static Exercise ConvertToExercise(this ExerciseContext context)
        {
            Exercise exercise = new Exercise();
            if (null == context)
                return exercise;
            exercise.Description = context.Description;
            exercise.ExerciseId = context.ExerciseId;
            exercise.ExerciseName = context.ExerciseName;
            exercise.ExerciseType = context.ExerciseType;
            exercise.MuscleName = context.MuscleName;
            exercise.Note = context.Note;
            exercise.Repetitions = context.Repetitions;
            exercise.SetsNum = context.SetsNum;
            exercise.Status = context.Status;

            return exercise;
        }

        public static List<ExerciseContext> ConvertToExerciseContexts(this List<Exercise> exercises)
        {
            List<ExerciseContext> exerciseContexts = new List<ExerciseContext>();
            //int counter = 0;
            if (null == exercises)
                return exerciseContexts;
            foreach (Exercise e in exercises)
            {
                exerciseContexts.Add(e.ConvertToExerciseContext());
            }
            
            return exerciseContexts;
        }

        public static List<Exercise> ToExercises(this List<ExerciseContext> context)
        {
            List<Exercise> exercises = new List<Exercise>();
            //int counter = 0;
            if (null == context)
                return exercises;
            foreach (ExerciseContext ec in context)
            {
                exercises.Add(ec.ConvertToExercise());
            }

            return exercises;
        }
    }
}
