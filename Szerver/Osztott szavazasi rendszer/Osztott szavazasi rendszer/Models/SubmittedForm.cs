namespace Osztott_szavazasi_rendszer.Models
{
    public class SubmittedForm
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Email { get; set; }
        public int FormId { get; set; }
        public Form? Form { get; set; }
        public List<string?> Answers { get; set; }
        public string? Comments { get; set; }
    }
}
