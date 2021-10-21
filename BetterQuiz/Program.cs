using System;
using System.IO;
using System.Collections.Generic;

namespace BetterQuiz
{
    class Program
    {
        static void Main(string[] args)
        {
            StartQuiz();
            Console.WriteLine("done man");
            Console.ReadLine();


            //Hälsa på användaren
            GreetUser();
            //Klar med att häsla på användaren

            //Ge användaren alternativ till vad den vill göra
            bool doneChoices = false;
            string answer;
            while (!doneChoices)
            {
                answer = PlayOrAddQuestion();
                if (answer == "play")
                {
                    doneChoices = true;
                    StartQuiz();
                }
                else if (answer == "add")
                {
                    doneChoices = true;
                }
            }
            //Nu vet vi redan vad användaren vill göra och vi har startat den metoden



            Console.ReadLine();
        }

        private static void GreetUser()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welcome to the better version of my quiz!");
            Console.WriteLine("Quiz 2.0 if you may :)");
            Console.WriteLine("Press any Key to continue");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
            Console.Clear();
        }

        private static string PlayOrAddQuestion()
        {
            Console.WriteLine("Would you like to play the quiz or add a question to it?");
            Console.WriteLine("Answer with ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\"play\" ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("or ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\"add\"\n");
            Console.ForegroundColor = ConsoleColor.White;

            return Console.ReadLine().ToLower();
        }

        private static void StartQuiz()
        {
            //Dags att ladda in quizet i lista
            string[] output = File.ReadAllLines("qna.txt");
            List<string[]> questionArr = new List<string[]>();

            foreach (string line in output)
            {
                questionArr.Add(line.Split(";"));
            }
            /*
            Nu har vi en lista där "första" dimensionen är linjen - det vill säga frågan och alternativen. 
            Den andra dimensionen är allt den innehåller, alltså [0] är frågan, [1] rätt svar, [2] och [3] är alternativen
            */

            //Välj random questions som sen kan användas till quizet när man spelar det
            List<int> questionsToUse = new List<int>();
            Random generator = new Random();

            for (int i = 0; i < 5; i++)
            {
                int pick = generator.Next(output.Length);
                while (questionsToUse.Contains(pick))
                {
                    pick = generator.Next(output.Length);
                }
                questionsToUse.Add(pick);
            }
            //Det finns nu 5 olika nummer, som ska vara motsvarande till frågorna i qna.txt

            //Ladda in svar, alternativ och shuffle'a dem i en arr. Sen ta svaret och se till så att det är rätt eller fel
            int points = 0;
            foreach (int q in questionsToUse)
            {
                List<string> unsortedAnswers = new List<string> { questionArr[q][1], questionArr[q][2], questionArr[q][3] };
                List<string> sortedAnswers = new List<string>();

                for (int i = 3; i > 0; i--)
                {
                    int max = unsortedAnswers.Count;
                    int temp = generator.Next(0, max);
                    sortedAnswers.Add(unsortedAnswers[temp]);
                    unsortedAnswers.RemoveAt(temp);
                }

                //Frågar användaren frågan och visar alla svar
                Console.Clear();
                Console.WriteLine(questionArr[q][0]);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("(answer with number or text)");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"1) {sortedAnswers[0]}");
                Console.WriteLine($"2) {sortedAnswers[1]}");
                Console.WriteLine($"3) {sortedAnswers[2]}");
                Console.ForegroundColor = ConsoleColor.White;

                //Tar svaret och nu testar om det går att converta, om det går så kommer vi göra det och ge svar
                int intSvar;
                string stringSvar = Console.ReadLine();
                bool canConvert = Int32.TryParse(stringSvar, out intSvar);

                //Låt användaren se sitt svar i 0.5 sekunder, sen cleara så att det ser bra ut och visa resultat
                System.Threading.Thread.Sleep(500);
                Console.Clear();

                //Boolen canConvert är om det går att göra det till en int, annars testar den om man skrev svaret
                if (canConvert)
                {
                    if (intSvar >= 1 && intSvar <= 3)
                    {
                        if (sortedAnswers[intSvar - 1] == questionArr[q][1])
                        {
                            Console.WriteLine("Correct, 1 point awarded!");
                            points++;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong, no points awarded!");
                    }
                }
                else if (stringSvar.ToLower() == questionArr[q][1].ToLower())
                {
                    Console.WriteLine("Correct, 1 point awarded!");
                    points++;
                }
                else
                {
                    Console.WriteLine($"Can't convert '{stringSvar}' to an answer.");
                }
                System.Threading.Thread.Sleep(1000);
            }

            //Ge användaren olika svar beroende på hur bra svar man fick
            if (points <= 0)
            {
                Console.WriteLine($"You got {points}/5 points! Time to study more!");
            }
            else if (points <= 3)
            {
                Console.WriteLine($"You got {points}/5 points! About halfway there!");
            }
            else if (points == 4)
            {
                Console.WriteLine("You are as close to perfect as possible. 4/5 points!");
            }
            else
            {
                Console.WriteLine("Wow, you did a great job. 5/5, not bad");
            }
        }

        private static void AddQuestion()
        {

        }
    }
}
