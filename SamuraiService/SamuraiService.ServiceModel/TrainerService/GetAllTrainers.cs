using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceModel.TrainerService
{
    [Api("Trainer service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/GetAllTrainers", "GET", Summary = @"Get all trainers",
                    Notes = "Получение всех тренеров")]
    public class GetAllTrainers:IReturn<IEnumerable<UiTrainer>>
    {
    }
}
