using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Models;
using TraineeHelper.ViewModels;

namespace TraineeHelper.Logic.Converters
{
    public static class MedicalConditionConverter
    {
        public static MedicalCondition ToNewMedicalCondition(this MedicalConditionContext context)
        {
            MedicalCondition medicalCondition = new MedicalCondition();
            return medicalCondition;
        }

        public static MedicalConditionContext ToMedicalConditionContext(this MedicalCondition medicalCondition)
        {
            MedicalConditionContext medicalConditionContext = new MedicalConditionContext();
            if (null == medicalCondition)
                return medicalConditionContext;
            medicalConditionContext.Age = medicalCondition.Age;
            medicalConditionContext.FatPercent = medicalCondition.FatPercent;
            medicalConditionContext.Height = medicalCondition.Height;
            medicalConditionContext.MuscleMass = medicalCondition.MuscleMass;
            medicalConditionContext.Weight = medicalCondition.Weight;
            return medicalConditionContext;
        }

        public static MedicalCondition toMedicalCondition(this MedicalConditionContext context)
        {
            MedicalCondition medicalCondition = new MedicalCondition();
            if (null == context)
                return medicalCondition;
            medicalCondition.Age = context.Age;
            medicalCondition.FatPercent = context.FatPercent;
            medicalCondition.Height = context.Height;
            medicalCondition.MuscleMass = context.MuscleMass;
            medicalCondition.Weight = context.Weight;
            return medicalCondition;
        }
    }
}
