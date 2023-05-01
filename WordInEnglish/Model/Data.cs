using WordInEnglish.Application_Context;

namespace WordInEnglish.Model
{
    public class Data
    {
        private Application_ContextDB _dbContext = new Application_ContextDB();

        public void WORDEN()
        {
            _dbContext.Add(new WordEN { IdEN = 1, MyWord = "hello" });
            _dbContext.Add(new WordEN { IdEN = 2, MyWord = "below" });
            _dbContext.Add(new WordEN { IdEN = 3, MyWord = "behind" });
            _dbContext.Add(new WordEN { IdEN = 4, MyWord = "above" });
            _dbContext.Add(new WordEN { IdEN = 5, MyWord = "myself" });
            _dbContext.Add(new WordEN { IdEN = 6, MyWord = "itself" });
            _dbContext.Add(new WordEN { IdEN = 7, MyWord = "whose" });
            _dbContext.Add(new WordEN { IdEN = 8, MyWord = "been" });
            _dbContext.Add(new WordEN { IdEN = 9, MyWord = "ask" });
            _dbContext.Add(new WordEN { IdEN = 10, MyWord = "phone" });

            _dbContext.SaveChanges();
        }

        public void WORDES()
        {
            //var wordES = new WordES();
            // {
            //     new WordES { IdES = 1, MyWord = "hola" };
            //     new WordES { IdES = 2, MyWord = "abajo" };
            //     new WordES { IdES = 3, MyWord = "detras" };
            //     new WordES { IdES = 4, MyWord = "arriba" };
            //     new WordES { IdES = 5, MyWord = "yo mismo" };
            //     new WordES { IdES = 6, MyWord = "si mismo" };
            //     new WordES { IdES = 7, MyWord = "cuyo" };
            //     new WordES { IdES = 8, MyWord = "estado" };
            //     new WordES { IdES = 9, MyWord = "preguntar" };
            //     new WordES { IdES = 10, MyWord = "phone" };
            // }

            _dbContext.Add(new WordES { IdES = 1, MyWord = "hola" });
            _dbContext.Add(new WordES { IdES = 2, MyWord = "abajo" });
            _dbContext.Add(new WordES { IdES = 3, MyWord = "detras" });
            _dbContext.Add(new WordES { IdES = 4, MyWord = "arriba" });
            _dbContext.Add(new WordES { IdES = 5, MyWord = "yo mismo" });
            _dbContext.Add(new WordES { IdES = 6, MyWord = "si mismo" });
            _dbContext.Add(new WordES { IdES = 7, MyWord = "cuyo" });
            _dbContext.Add(new WordES { IdES = 8, MyWord = "estado" });
            _dbContext.Add(new WordES { IdES = 9, MyWord = "preguntar" });
            _dbContext.Add(new WordES { IdES = 10, MyWord = "telefono" });

            _dbContext.SaveChanges();
        }
    }
}