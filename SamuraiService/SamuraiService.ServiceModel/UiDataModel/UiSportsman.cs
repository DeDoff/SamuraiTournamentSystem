namespace SamuraiService.ServiceModel.UiDataModel
{
    public class UiSportsman:UiAbstractSportsman
    {
        public int id { get; set; }
        public string club { get; set; }
        public string trainer { get; set; }
        public string imgUrl { get; set; }
    }

    public abstract class UiAbstractSportsman
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public decimal weight { get; set; }
        public int age { get; set; }
        public string grade { get; set; }
        public int iko { get; set; }
        public string sex { get; set; }
        public string birthDate { get; set; }
        
    }
}
