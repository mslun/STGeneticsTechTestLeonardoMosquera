
namespace STGeneticsTechTestLeonardoMosquera.Shared.Models
{
    public class Animal
    {
        public  int Id { get; set; }
        public  string? Name { get; set; }
        public string? Breed { get; set; }
      
        public DateTime? BirthDate { get; set; }
        public  string? Sex { get; set; }
  
        public decimal? Price { get; set; }
        public string? Status { get; set; }
        public bool Selected { get; set; } = false;

        public string GetFormatedBirthDate() { 
            
            return BirthDate?.ToString("yyyy-MM-dd") ?? "";
        
        }

        public string GetFormatedPrice()
        {
            
            return Price?.ToString("C") ?? "";
        
        }

    }
    

}
