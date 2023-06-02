using System.ComponentModel.DataAnnotations;

namespace eTicket.Models
{
    public class Cinema
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Cinema Logo")]
        [Required]
        public string Logo { get; set; }


        [Display(Name = "Cinema Name")]
        [Required]
        public string Name { get; set; }


        [Display(Name = "Cinema Description")]
        [Required]
        public string Description { get; set; }

        //Relationship
        public List<Movie>? Movies { get; set; }
    }
}
