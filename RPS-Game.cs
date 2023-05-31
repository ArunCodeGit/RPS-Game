using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPSGameDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Game Plan
            // 1. Prompt the user for the number of rounds
            Console.WriteLine("Welcome to AI Powered RPS game");
            int numberOfRounds = GetNumberOfRounds("How many rounds would you like to play? ");
            // 2. Get user choice
            int i = 1;
            int UserWins = 0;
            int AIWins = 0;
            int TieCount = 0;
            int StopPossibility = (numberOfRounds / 2) + 1;
            while (i <= numberOfRounds)
            {
                Choice userChoice = GetUserChoice("Enter your choice: 1. Rock, 2. Paper, 3.Scissors: ");

                // 3. Get AI choice
                Choice AIChoice = GetAIChoice();

                // 4. Display Choices
                DisplayChoices(userChoice, AIChoice);

                // 5. Determine Round Winner
                WinningState roundState = DetermineWinner(userChoice, AIChoice);

                // 6. Get winning Message
                //Console.WriteLine(GetWinningMessage(userChoice, AIChoice, roundState));
                string RoundResult = GetWinningMessage(userChoice, AIChoice, roundState);

                // 7. Display Round Result
                //Console.WriteLine($"{i} round status is: {roundState}.");
                Console.WriteLine($"Round {i}: { RoundResult}");

                // 8. Update Game Score
                if (roundState == WinningState.UserWins)
                {
                    UserWins++;
                }
                else if (roundState == WinningState.AIWins)
                {
                    AIWins++;
                }
                else
                {
                    TieCount++;
                }
                // 9. Determine Game Winner
                if (StopPossibility <= UserWins || StopPossibility <= AIWins)
                {
                    Console.WriteLine("No need to proceed further.");
                    break;
                }
                i++;
                Console.WriteLine();
            }
            // 10. Print winner and winning Message
            if (UserWins > AIWins)
            {
                Console.WriteLine($"User won the series by {UserWins} out of {numberOfRounds}.");
            }
            else if(AIWins > UserWins)
            {
                Console.WriteLine($"AI won the series by {AIWins} out of {numberOfRounds}.");
            }
            else
            {
                Console.WriteLine($"Series was drawn.");
            }
            Console.ReadLine();
            #endregion
        }

        #region GetAIChoice
        public static Choice GetAIChoice()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            Choice AIChoice = (Choice)random.Next(1, 4);
            return AIChoice;
        }
        #endregion

        #region Display Choices
        public static void DisplayChoices(Choice userChoice, Choice aiChoice)
        {
            Console.WriteLine(string.Format("User Choice: {0} *********** AI Choice : {1} ", userChoice, aiChoice));
        }
        #endregion

        #region Determine Winner
        public static WinningState DetermineWinner(Choice userChoice, Choice aiChoice)
        {
            WinningState currentState = WinningState.Draw;

            if (userChoice == aiChoice)
            {
                return currentState;
            }
            if (userChoice == Choice.Rock)
            {
                if (aiChoice == Choice.Papers)
                {
                    currentState = WinningState.AIWins;
                }
                else
                {
                    currentState = WinningState.UserWins;
                }
            }
            else if(userChoice == Choice.Scissors)
            {
                if(aiChoice == Choice.Rock)
                {
                    currentState = WinningState.AIWins;
                }
                else
                {
                    currentState = WinningState.UserWins;
                }
            }
            else if(userChoice == Choice.Papers)
            {
                if(aiChoice == Choice.Scissors)
                {
                    currentState= WinningState.AIWins;
                }
                else
                {
                    currentState = WinningState.UserWins;
                }
            }
            return currentState;
        }
        #endregion

        #region Get Winning Message
        public static string GetWinningMessage(Choice Userchoice, Choice AIChoice, WinningState state)
        {
            string Result = "";
            if (state == WinningState.UserWins)
            {
                Result = "User won the match";
            }
            else if(state == WinningState.AIWins)
            {
                Result = "AI won the match";
            }
            else
            {
                Result = "Match Tied";
            }
            return Result;
        }
        #endregion

        #region Get Number of Rounds
        public static int GetNumberOfRounds(string Replace)
        {
            Console.WriteLine("Welcome to the RPS Game: ");
            int GetRounds = GetInt("Enter number of rounds to be played: ");
            return GetRounds;
        }
        #endregion

        #region Get User Choice
        public static Choice GetUserChoice(string message)
        {
            //Console.WriteLine("Getting Input for Rock, Paper and Scissors");
            Console.WriteLine(message);
            int userChoice;

            if (!int.TryParse(Console.ReadLine(), out userChoice))
            {
                return GetUserChoice(message);
            }
            else if (userChoice > 3 || userChoice <= 0)
            {
                return (GetUserChoice(message));
            }
            return (Choice)userChoice;
        }
        #endregion

        #region GetInt
        public static int GetInt(string message)
        {
            Console.Write(message);
            int t;
            if (!int.TryParse(Console.ReadLine(), out t))
            {
                Console.WriteLine("Please Enter the Integer Data type value: ");
                GetInt(message);
            }
            return t;
        }
        #endregion
    }
    #region Custom Enumerations for Winning state and Choices
    public enum WinningState
    {
        Draw=0,
        AIWins=1,
        UserWins=2
    }
    public enum Choice
    {
        Rock=1,
        Papers=2,
        Scissors=3
    }
    #endregion
}
