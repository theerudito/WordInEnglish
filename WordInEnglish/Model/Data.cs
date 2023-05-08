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
                    new WordEN { IdEN = 1, MyWord = "HELLO" },
                    new WordEN { IdEN = 2, MyWord = "BELLOW" },
                    new WordEN { IdEN = 3, MyWord = "BEHIND" },
                    new WordEN { IdEN = 4, MyWord = "ABOBE" },
                    new WordEN { IdEN = 5, MyWord = "MYSELF" },
                    new WordEN { IdEN = 6, MyWord = "ITSELF" },
                    new WordEN { IdEN = 7, MyWord = "WHOSE" },
                    new WordEN { IdEN = 8, MyWord = "BEEN" },
                    new WordEN { IdEN = 9, MyWord = "ASK" },
                    new WordEN { IdEN = 10, MyWord = "PHONE" }
                };

            _dbContext.WordsEN.AddRange(words);
            _dbContext.SaveChanges();
        }

        public void WORDES()
        {
            var words = new List<WordES>
            {
                    new WordES { IdES = 1, MyWord = "HOLA" },
                    new WordES { IdES = 2, MyWord = "ABAJO" },
                    new WordES { IdES = 3, MyWord = "DETRAS" },
                    new WordES { IdES = 4, MyWord = "ARRIBA" },
                    new WordES { IdES = 5, MyWord = "YO MISMO" },
                    new WordES { IdES = 6, MyWord = "SI MISMO" },
                    new WordES { IdES = 7, MyWord = "CUYO" },
                    new WordES { IdES = 8, MyWord = "ESTADO" },
                    new WordES { IdES = 9, MyWord = "PREGUNTAR" },
                    new WordES { IdES = 10, MyWord = "TELEFONO" }
                };

            _dbContext.WordsES.AddRange(words);
            _dbContext.SaveChanges();
        }
    }
}