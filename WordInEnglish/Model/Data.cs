using System.Collections.Generic;
using WordInEnglish.Application_Context;

namespace WordInEnglish.Model
{
    public class Data
    {
        private Application_ContextDB _dbContext = new Application_ContextDB();

        public void WORDEN()
        {
            var words = new List<WordEN>
                {
                    new WordEN { IdEN = 1, MyWord = "hello" },
                    new WordEN { IdEN = 2, MyWord = "below" },
                    new WordEN { IdEN = 3, MyWord = "behind" },
                    new WordEN { IdEN = 4, MyWord = "above" },
                    new WordEN { IdEN = 5, MyWord = "myself" },
                    new WordEN { IdEN = 6, MyWord = "itself" },
                    new WordEN { IdEN = 7, MyWord = "whose" },
                    new WordEN { IdEN = 8, MyWord = "estado" },
                    new WordEN { IdEN = 9, MyWord = "ask" },
                    new WordEN { IdEN = 10, MyWord = "telefono" }
                };

            _dbContext.WordsEN.AddRange(words);
            _dbContext.SaveChanges();
        }

        public void WORDES()
        {
            var words = new List<WordES>
            {
                    new WordES { IdES = 1, MyWord = "hola" },
                    new WordES { IdES = 2, MyWord = "abajo" },
                    new WordES { IdES = 3, MyWord = "detras" },
                    new WordES { IdES = 4, MyWord = "arriba" },
                    new WordES { IdES = 5, MyWord = "yo mismo" },
                    new WordES { IdES = 6, MyWord = "si mismo" },
                    new WordES { IdES = 7, MyWord = "cuyo" },
                    new WordES { IdES = 8, MyWord = "estado" },
                    new WordES { IdES = 9, MyWord = "preguntar" },
                    new WordES { IdES = 10, MyWord = "telefono" }
                };

            _dbContext.WordsES.AddRange(words);
        }
    }
}