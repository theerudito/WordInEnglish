using System.ComponentModel.DataAnnotations;

namespace WordInEnglish.Model
{
    public class WordEN
    {
        [Key]
        public int IdEN { get; set; }

        [MaxLength(20), Required]
        public string MyWord { get; set; }
    }
}