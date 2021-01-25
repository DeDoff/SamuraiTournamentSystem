using System;
using System.Collections.Generic;
using System.Text;

namespace SamuraiService.ServiceModel.UiDataModel
{
    public class UiUser 
    {
        public int id { get; set; }
        public string login { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public UiRole[] selectedRoles { get; set; }
    }
}
