using System.ComponentModel.DataAnnotations;

namespace WordInEnglish.Model
{
    public class WordES
    {
        [Key]
        public int IdES { get; set; }

        public string MyWord { get; set; }
    }
}