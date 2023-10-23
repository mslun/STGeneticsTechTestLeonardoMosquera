
namespace STGeneticsTechTestLeonardoMosquera.Shared.Models
{
    public class AnimalList
    {
        public List<Animal>? Animals { get; set; }

        public string? SearchTerm { get; set; }
        public int ToatalPages { get; set; }
        public int CurrentPage { get; set; }
    }



}
