using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public static class Highscore
    {
        public static void AddScoreToDatabase(Score score)
        {
            using (var context = new MinesweeperContext())
            {
                context.Scores.Add(score);
                context.SaveChanges();
            }
        }

        public static List<Score> GetTopScores()
        {
            using (var context = new MinesweeperContext())
            {
                return context.Scores
                    .OrderByDescending(s => s.Points)
                    .ThenBy(s => s.FieldSize)
                    .Take(10)
                    .ToList();
            }

        }
        public static List<List<string>> ConvertHighscorestoList(List<Score> scores)
        {
            var result = new List<List<string>>();

            foreach (var score in scores)
            { 
                var scoreInfo = new List<string>
                {
                    score.Player,
                    score.Points.ToString(),
                    score.Seconds.ToString(),
                    score.Bombs.ToString(),
                    score.FieldSize.ToString()
                };

                result.Add(scoreInfo);
            }

            return result;
        }



    }


}
