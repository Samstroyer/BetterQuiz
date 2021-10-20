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
            foreach (int q in questionsToUse)
            {
                List<string> unsortedAnswers = new List<string> { questionArr[q][1], questionArr[q][2], questionArr[q][3] };
                List<string> sortedAnswers = new List<string>();

                for (int i = 3; i > 0; i--)
                {
                    int temp = generator.Next(0, i);
                    sortedAnswers.Add(unsortedAnswers[temp]);
                    unsortedAnswers.RemoveAt(temp);
                }

                foreach (string item in sortedAnswers)
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}
