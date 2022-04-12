using static Osztott_szavazasi_rendszer.Data.Enum;

namespace Osztott_szavazasi_rendszer.Models
{
    public class Form
    {
        public int Id { get; set; }
        public List<string> Questions { get; set; }
        public List<string>? Answers { get; set; }
        public List<QuestionType> QuestionTypes { get; set; }
    }
}
