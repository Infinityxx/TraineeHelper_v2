using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Models;
using TraineeHelper.ViewModels;

namespace TraineeHelper.Logic.Converters
{
    public static class FiltersConverter
    {
        public static List<SearchFilter> ConvertToSearchFilters(this SmartSearchFilter searchFilterParams)
        {
            if (null == searchFilterParams)
                return null;

            List<SearchFilter> searchFilters = new List<SearchFilter>();
            ValueSearchFilter<string> idSearchFilter = new ValueSearchFilter<string>();
            ValueSearchFilter<string> userTypeSearchFilter = new ValueSearchFilter<string>();
            ValueSearchFilter<int> genderPreferencesFilter = new ValueSearchFilter<int>();
            ValueSearchFilter<string> locationPreferecesFilter = new ValueSearchFilter<string>();
            List<ValueSearchFilter<string>> expertisesPreferencesFilter = new List<ValueSearchFilter<string>>();
            ValueSearchFilter<int> reputationPreferencesFilter = new ValueSearchFilter<int>();
            RangeSearchFilter pricePreferencesFilter = new RangeSearchFilter();
            ValueSearchFilter<bool> showersPreferencesFilter = new ValueSearchFilter<bool>();
            ValueSearchFilter<bool> parkingLotPreferencesFilter = new ValueSearchFilter<bool>();
            RangeSearchFilter yearsofExperiencePreferencesFilter = new RangeSearchFilter();
            

            idSearchFilter.FieldName = "_id";
            idSearchFilter.Value = searchFilterParams.UserId;

            userTypeSearchFilter.FieldName = "UserType";
            userTypeSearchFilter.Value = searchFilterParams.UserType;

                        
            genderPreferencesFilter.FieldName = "Gender";
            genderPreferencesFilter.Value = (int)searchFilterParams.GenderFilter;

            locationPreferecesFilter.FieldName = "Location";
            locationPreferecesFilter.Value = "Everywhere";
            if(searchFilterParams.Expertise != null)
            {
                for (int i = 0; i < searchFilterParams.Expertise.Length; i++)
                {
                    ValueSearchFilter<string> filter = new ValueSearchFilter<string>();
                    filter.FieldName = "Expertise";
                    filter.Value = searchFilterParams.Expertise[i];
                    expertisesPreferencesFilter.Add(filter);
                }
            }
            

            reputationPreferencesFilter.FieldName = "Reputation";
            reputationPreferencesFilter.Value = searchFilterParams.Reputation;

            pricePreferencesFilter.FieldName = "Price";
            if(searchFilterParams.Price == "<100")
            {
                pricePreferencesFilter.FromValue = "0";
                pricePreferencesFilter.ToValue = "100";
            }
            else if(searchFilterParams.Price == "between")
            {
                pricePreferencesFilter.FromValue = "100";
                pricePreferencesFilter.ToValue = "150";
            }
            else
            {
                pricePreferencesFilter.FromValue = "150";
                pricePreferencesFilter.ToValue = "999999";
            }

            yearsofExperiencePreferencesFilter.FieldName = "ExperienceYears";
            if(searchFilterParams.ExperienceYears == "<3")
            {
                yearsofExperiencePreferencesFilter.FromValue = "0";
                yearsofExperiencePreferencesFilter.ToValue = "3";
            }
            else if(searchFilterParams.ExperienceYears == "between")
            {
                yearsofExperiencePreferencesFilter.FromValue = "4";
                yearsofExperiencePreferencesFilter.ToValue = "7";
            }
            else
            {
                yearsofExperiencePreferencesFilter.FromValue = "8";
                yearsofExperiencePreferencesFilter.ToValue = "999999";
            }

            showersPreferencesFilter.FieldName = "Showers";
            showersPreferencesFilter.Value = searchFilterParams.Showers;

            parkingLotPreferencesFilter.FieldName = "ParkingLot";
            parkingLotPreferencesFilter.Value = searchFilterParams.Parking;

            searchFilters.Add(userTypeSearchFilter);
            searchFilters.Add(idSearchFilter);
            if(userTypeSearchFilter.Value != "Gym" || genderPreferencesFilter.Value != (int)Common.Gender.DoesntMatter)
                searchFilters.Add(genderPreferencesFilter);
            searchFilters.Add(locationPreferecesFilter);
            //searchFilters.Add(expertisePreferencesFilter);
            if (userTypeSearchFilter.Value != "Trainee")
            {
                searchFilters.Add(reputationPreferencesFilter);
                searchFilters.Add(pricePreferencesFilter);
                searchFilters.Add(yearsofExperiencePreferencesFilter);
                if(expertisesPreferencesFilter.Count > 0)
                {
                    foreach (ValueSearchFilter<string> e in expertisesPreferencesFilter)
                    {
                        searchFilters.Add(e);
                    }
                }           
            }
            if (userTypeSearchFilter.Value == "Gym")
            {
                searchFilters.Add(showersPreferencesFilter);
                searchFilters.Add(parkingLotPreferencesFilter);
            }

            return searchFilters; 
        }
    }
}
