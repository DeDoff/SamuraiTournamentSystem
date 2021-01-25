using SamuraiDbModel;
using SamuraiService.ServiceInterface;
using SamuraiService.ServiceModel.MatchService;
using SamuraiService.ServiceModel.TatamiService;
using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiAPI
{
    public class MatchManager
    {
        public class AppHost : AppSelfHostBase
        {
            public AppHost()
              : base("HttpListener Self-Host", typeof(TatamiService).Assembly) { }

            public override void Configure(Funq.Container container)
            {

            }
        }

        public void Manage()
        {

            Licensing.RegisterLicense(@"8512-e1JlZjo4NTEyLE5hbWU6TGlua2VyIFNvbHV0aW9ucy
                xUeXBlOkluZGllLE1ldGE6MCxIYXNoOmd6alNmYzNzM1d6T
                UZMVDR5SVlrUC9JeXVjVHFHTTl3clR6Z2FOTmhtZHdiREk5
                WjBVejB0dHVvVmtqLysvNGdnVXU1Zm4rd2NRUFcxOHV0ZDl
                Hakw3ckxrL29zTGQwTGJpV3ZRcm1zUkpsSmREOWFTY292MF
                FSMXAwbktJWm5leE9vSE0rbnJ1V1BPc3oybXdLaS9SbkUwd
                UE4ZEt5NHVUSVdqRVhxamtwVT0sRXhwaXJ5OjIwMjEtMDkt
                MTR9");
            var listeningOn = "http://localhost:5000/";

            var client = new JsonServiceClient(listeningOn);

            bool flag = true;
            while (flag)
            {
                try
                {
                    Console.WriteLine("\nВыберите действие");
                    Console.WriteLine("1 - Загрузить матчи на все татами");
                    Console.WriteLine("2 - Загрузить матчи на одно татами");
                    Console.WriteLine("3 - Активировать матч");
                    Console.WriteLine("4 - Перейти к следующему матчу");
                    Console.WriteLine("5 - Выход");

                    int choice;
                    int.TryParse(Console.ReadLine(), out choice);
                    switch (choice)
                    {
                        case 1:
                            {
                                var tatami = client.Get(new GetMatchesForAllTatamies());
                                var tatamiIds = tatami.Select(x => x.tatamiId).Distinct().ToList();
                                foreach (var t in tatamiIds)
                                {
                                    Console.WriteLine("\nТатами " + t);
                                    var matches = tatami.Where(x => x.tatamiId == t).ToList();
                                    Console.WriteLine("Global ID\tMatch ID\tTatami ID\t");
                                    foreach (var m in matches)
                                    {
                                        Console.WriteLine(m.globalMatchNumber + "\t\t" + m.id + "\t\t" + m.tatamiId + "\t");
                                    }
                                }
                                break;
                            }
                        case 2:
                            {
                                Console.WriteLine("Введите ID татами: ");
                                var tatamiId = int.Parse(Console.ReadLine());
                                var singleTatamiMatches = client.Get(new GetMatchesForTatami { TatamiId = tatamiId });
                                Console.WriteLine("\n\nGlobal ID\tMatch ID\tTime\t\tMatchInfo\t");
                                foreach (var s in singleTatamiMatches)
                                {
                                    Console.WriteLine(s.globalMatchNumber + "\t\t" + s.id + "\t\t" + s.matchDuration + "\t" + s.matchInfo + "\t\t");
                                }
                                break;
                            }
                        case 3:
                            {
                                Console.WriteLine("Введите ID матча для активации: ");
                                var matchId = int.Parse(Console.ReadLine());
                                var response = client.Post(new ActivateMatch() { MatchId = matchId });
                                Console.WriteLine("IsSuccess: " + response.IsSuccess);
                                Console.WriteLine("Message: " + response.ExceptionMessage);
                                break;
                            }
                        case 4:
                            {
                                Console.WriteLine("Введите ID матча");
                                var matchId = int.Parse(Console.ReadLine());
                                Console.WriteLine("Введите время матча в формате 00:00");
                                var time = TimeSpan.Parse(Console.ReadLine());
                                Console.WriteLine("Введите ID победителя");
                                var winnerId = int.Parse(Console.ReadLine());
                                var match = new UiMatch()
                                {
                                    id = matchId,
                                    matchDuration = time,
                                    winnerId = winnerId
                                };
                                var response = client.Post(new FinishMatchAndLoadNext() { FinishMatch = match });
                                Console.WriteLine("IsSuccess: " + response.IsSuccess);
                                Console.WriteLine("Message: " + response.ExceptionMessage);
                                break;
                            }
                        case 5:
                            {
                                return;
                            }
                        default:
                            {
                                throw new FormatException("Неправильный формат");
                            }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            Console.ReadKey();
        }
    }
}
