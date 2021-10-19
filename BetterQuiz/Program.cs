using System;
using System.IO;
using System.Collections.Generic;

namespace BetterQuiz
{
    class Program
    {
        static void Main(string[] args)
        {
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

            List<int> questions = new List<int>();
            for (int i = 0; i < 5; i++)
            {
                Random generator = new Random();

                int pick = generator.Next(output.Length);
                while (questions.Contains(pick))
                {
                    pick = generator.Next(output.Length);
                }
            }
        }
    }
}
