﻿using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceModel.MatchService
{
    [Api("Match service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/CreateMatch", "POST", Summary = @"Create match",
   Notes = "Создает матч")]
    public class CreateMatch:IReturn<UiMatch>
    {
        [ApiMember(Name = "Match",
          Description = "Match information",
          DataType = "UiMatch",
          ParameterType = "model",
          IsRequired = true)]
        public UiMatch Match { get; set; }
    }
}
