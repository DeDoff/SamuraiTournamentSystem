using CsvHelper;
using SamuraiDbModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceInterface.Logic
{
    public static class CompetitorCSVParser
    {
        public static List<Competitor> Parse(byte[] bytes)
        {
            List<Competitor> competitors = new List<Competitor>();
            string header = Encoding.UTF8.GetString(bytes).Split("\n").ToArray()[0];
            string[] required = { "CompetitionId", "FirstName", "LastName", "Grade", "Sex" };
            List<string> listOfHeaders = header.Split(",").ToList();
            if (!required.All(c => header.Contains(c))) throw new IndexOutOfRangeException("Incorrect number of required columns");

            using (var reader = new StreamReader(new MemoryStream(bytes)))
            using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csvReader.Read();
                csvReader.ReadHeader();
                while (csvReader.Read())
                {
                    var compet = new Competitor();
                    foreach (var field in listOfHeaders)
                    {
                        foreach (var competField in compet.GetType().GetProperties())
                        {
                            if (field == competField.Name)
                            {
                                switch (competField.Name)
                                {
                                    case "Weight":
                                        {
                                            decimal.TryParse(csvReader.GetField(field), out decimal result);
                                            competField.SetValue(compet, result);
                                            break;
                                        }
                                    case "Sex":
                                        {
                                            string name = csvReader.GetField(field);
                                            competField.SetValue(compet, new Sex() { Name = name });
                                            break;
                                        }
                                    case "Grade":
                                        {
                                            string name = csvReader.GetField(field);
                                            competField.SetValue(compet, new Grade() { Name = name });
                                            break;
                                        }
                                    case "Age":
                                        {
                                            int.TryParse(csvReader.GetField(field), out int result);
                                            competField.SetValue(compet, result);
                                            break;
                                        }
                                    case "IKO":
                                        {
                                            int.TryParse(csvReader.GetField(field), out int result);
                                            competField.SetValue(compet, result);
                                            break;
                                        }
                                    case "CompetitionId":
                                        {
                                            int.TryParse(csvReader.GetField(field), out int result);
                                            competField.SetValue(compet, result);
                                            break;
                                        }
                                    default:
                                        {
                                            competField.SetValue(compet, csvReader.GetField(field));
                                            break;
                                        }
                                }
                            }
                        }
                    }
                    competitors.Add(compet);
                }
            }
            return competitors;
        }
    }
}
