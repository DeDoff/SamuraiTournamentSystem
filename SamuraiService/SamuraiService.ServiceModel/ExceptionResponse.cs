using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceInterface.Extensions
{
    public class ExceptionResponse
    {
        public bool IsSuccess { get; set; }
        public string ExceptionMessage { get; set; }
    }
}
