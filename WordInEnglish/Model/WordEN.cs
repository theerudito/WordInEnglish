using System.ComponentModel.DataAnnotations;

namespace WordInEnglish.Model
{
    public class WordEN
    {
        [Key]
        public int IdEN { get; set; }

        public string MyWord { get; set; }
    }
}