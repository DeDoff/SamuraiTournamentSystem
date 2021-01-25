using SamuraiDbModel;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SamuraiLogic.CompetitionHistory
{
    public class CompetitionHistoryBuilder
    {
        public static List<SamuraiDbModel.History.CompetitionHistory> GetHistory(Competition competition)
        {
            List<SamuraiDbModel.History.CompetitionHistory> historyList = new List<SamuraiDbModel.History.CompetitionHistory>();
            if (competition == null)
                return historyList;

            try
            {
                foreach (var category in competition?.Categories.Where(x=> x.CompetitionGrid != null))
                {
                    foreach (var match in category?.CompetitionGrid?.Matches)
                    {
                        foreach (var competitor in match?.Competitors)
                        {
                            historyList.Add(new SamuraiDbModel.History.CompetitionHistory()
                            {
                                CompetitionId = competition.Id,
                                CompetitionDatePeriod = competition.StartDate?.ToShortDateString() + " - " + competition.EndDate?.ToShortDateString(),
                                CompetitionName = competition.Name,
                                
                                CompetitionCategoryId = category.Id,
                                CompetitionCategoryName = category.Name,
                                CompetitionWinnerId = category.CompetitionGrid.WinnerId,

                                TatamiId = match.TatamiId,

                                MatchId = match.Id,
                                MatchDuration = match.MatchDuration,
                                MatchInfo = match.MatchInfo,
                                GlobalMatchNumbe = match.GlobalMatchNumber,
                                GridMatchNumber = match.GridMatchNumber,
                                MatchWinnerId = match.WinnerId.HasValue ? match.WinnerId.Value : 0,

                                CompetitorId = competitor.Id,
                                CompetitorAge = competitor.Age,
                                CompetitorAgeGroupName = competitor.AgeGroup.AgeName,
                                CompetitorGrade = competitor.Grade.Name,
                                CompetitorCountry = competitor.Country,
                                CompetitorFirstName = competitor.FirstName,
                                CompetitorLastName = competitor.LastName,
                                CompetitorLevel = competitor.Level,
                                CompetitorRegion = competitor.Region,
                                CompetitorSex = competitor.Sex.Name,

                                CompetitorSportClubName = competitor.ClubName,
                                CompetitorWeight = competitor.Weight,
                                CompetitorWeightGroupName = competitor.WeightGroup.Name,
                                SportsmanId = competitor.SportsmanId
                            });
                        }
                    }
                }
            }
            catch (Exception) { }
            return historyList;
        }
    }
}
