using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using static System.Console;

class MainClass
{
    public static Random rnd;
    public static int GameMode;
    public static int lives = 10;
    public static char guessLetter;
    public static string GuessWord;
    public static string secretWord;
    public static int secretWordIndex;
    public static char[] PrintWordArray;
    public static bool firstTurn = true;
    public static char[] SecretWordArray;
    public static bool continueGame = false;
    public static string[] animals = new string[10];
    public static StringBuilder Incorrectletters = new StringBuilder("");

    public static void Main(string[] args)
    {

        Console.WriteLine("Let's play Hangman!");
        // 10 car names as secret word input source
        animals[0] = "volvo";
        animals[1] = "toyota";
        animals[2] = "volksw";
        animals[3] = "honda";
        animals[4] = "mazda";
        animals[5] = "hyundai";
        animals[6] = "citroen";
        animals[7] = "pegout";
        animals[8] = "saab";
        animals[9] = "renault";
        continueGame = true;

        while (continueGame)
        {
            while (firstTurn)
            {
                lives = 10;
                rnd = new Random();
                secretWordIndex = rnd.Next(0, 10);
                Incorrectletters = new StringBuilder("");
                secretWord = animals[secretWordIndex].ToLower();  //select secret word from random index
                SecretWordArray = secretWord.ToCharArray();
                PrintWordArray = new char[SecretWordArray.Length];
                WriteLine(secretWord);
                FillPrintGuessArray();  //initially to fill array with '_'

                GameMode = SelectGameMode();
                firstTurn = false;
            }


            if (GameMode == 1)   // Game mode 1 means play by guessing characters
            {

                WriteLine();
                WriteLine("Start guessing here");
                PrintGuessArray();
                RunByCharacter();  //call the function run by selecting characters


            }
            else if (GameMode == 2)   // Game mode 1 means play by guessing characters
            {
                WriteLine();
                WriteLine("Start guessing here");
                RunByWord();    //call the function to run by entering whole word

            }
            WriteLine();
        }

    }

    public static void PrintGuessArray() // will be updated and printed after every guess attempted
    {
        foreach (char item in PrintWordArray)
        {
            Write("      " + item);
        }
        WriteLine();
    }

    public static void FillPrintGuessArray()  //initially to fill array with '_'
    {
        for (int i = 0; i < PrintWordArray.Length; i++)
        {
            PrintWordArray[i] = '_';
        }
    }

    public static void ExitApplication()  // to exit application
    {
        Environment.Exit(0);
    }

    public static void PrintWrongGuess() // to print the character sequence at the end in order it was attempted.
    {
        WriteLine(Incorrectletters);
    }

    public static void InputGuessCh() // to take input character
    {
        bool CorrectInput = false;
        while (!(CorrectInput))
        {
            try
            {
                Console.Write("Enter yout guess here: ");
                guessLetter = Convert.ToChar(ReadLine());

                if (Char.IsLetter(guessLetter) && guessLetter != ' ')
                {
                    CorrectInput = true;
                }
            }
            catch (Exception)
            {

                WriteLine("Only single letter is acceptible as correct input: ");
            }
        }

    }

    public static void InputGuessWord()
    {

        bool CorrectInput = false;
        while (!(CorrectInput))
        {
            try
            {
                WriteLine("Secret word length is {0} :  ", secretWord.Length);
                Write("Please enter your guess as whole word: ");

                GuessWord = ReadLine().ToLower();

                if (GuessWord.Length > 0)
                {
                    CorrectInput = true;
                }
            }
            catch (Exception)
            {

                WriteLine("Must be atleast 1 character(s) as correct input: ");
            }
        }
    }

    public static int SelectGameMode()
    {
        bool GameModeSelected = false;
        int selection = 0;
        while (!GameModeSelected)
        {
            try
            {
                Console.Write("1 - Guess by letter \n2 - Guess by whole word:\n Game Mode 1 or 2:  ");
                selection = Convert.ToInt32(ReadLine());
                if (selection == 1 || selection == 2)
                {
                    GameModeSelected = true;
                    return selection;
                }

            }
            catch (Exception)
            {

                WriteLine("Only options are allowed to input are 1 or 2");
            }

        }
        return selection;

    }
    public static void PrintSecretwordArray()
    {
        foreach (char item in SecretWordArray)
        {
            Write(item + " ");
        }
    }

    public static void RunByCharacter()
    {
        WriteLine("Secret word length is {0} :  ", secretWord.Length);
        while (lives > 0)
        {

            WriteLine();
            InputGuessCh();

            if (SecretWordArray.Contains(guessLetter))
            {
                if (PrintWordArray.Contains(guessLetter))
                {
                    WriteLine("This guess letter is already consumed: ");

                    PrintGuessArray();
                }
                else
                {
                    for (int i = 0; i < SecretWordArray.Length; i++)
                    {
                        if (SecretWordArray[i] == guessLetter)
                        {
                            PrintWordArray[i] = guessLetter;
                        }

                    }
                    lives--;
                    PrintGuessArray();
                }

                WriteLine();
            }

            else if (!(SecretWordArray.Contains(guessLetter)))
            {
                WriteLine("Secret word does not contain " + " " + guessLetter);
                if (!(Incorrectletters.ToString().Contains(guessLetter)))  //if letter is already guessed but not present in secret world
                {

                    lives--;
                }
                Incorrectletters.Append(guessLetter).ToString();
                PrintGuessArray();
            }
            WriteLine();

            if ((SecretWordArray.SequenceEqual(PrintWordArray)))  // to check if secret word and guess sequence become equal (Winning condition).
            {
                WriteLine("CONGRATULATIONS: you have won the game in {0} attempts.", (10 - lives));
                WriteLine("Secret word was:{0} ", secretWord);
                Write("Here is your wrong guess Characters: ");
                foreach (char item in Incorrectletters.ToString())
                {
                    Write(" " + item + " ");
                }
                EndGameOrNot();
            }
            else
            {
                WriteLine("{0} chances are left: ", lives);
            }
            WriteLine();
        }
        if (lives == 0)
        {
            WriteLine("After 10 guess Game is lost now: " + "" + "");
            WriteLine("Secret word was:{0} ", secretWord);
            EndGameOrNot();
        }
    }

    public static void RunByWord()
    {
        {
            while (lives > 0)
            {

                WriteLine();
                InputGuessWord();

                if (!(secretWord == GuessWord))
                {
                    WriteLine(GuessWord + " is Not correct guess Try again: ");

                    if (!(Incorrectletters.ToString().Contains(GuessWord)))  //if word is already guessed but not the secret world
                    {
                        Incorrectletters.Append(GuessWord + "  ");
                        lives--;
                    }

                    WriteLine("{0} chances are left: ", lives);
                }

                else if (secretWord == GuessWord)

                {
                    WriteLine("CONGRATULATIONS: you have won the game in {0} attempts.", (10 - lives));
                    WriteLine("Secret word was:{0} ", secretWord);
                    Write("Here is your wrong guess Words: ");
                    WriteLine(Incorrectletters);
                    EndGameOrNot();

                    WriteLine();
                }

                WriteLine();
            }

            if (lives == 0)
            {
                WriteLine("After 10 guess Game is lost now: " + "" + "");
                WriteLine("Secret word was:{0} ", secretWord);
                EndGameOrNot();
            }
        }
    }

    public static void EndGameOrNot()
    {
        WriteLine();
        Write("Press Y to play a new game or E to Exit the game: ");
        bool InputY_E = false;

        while (!(InputY_E))
        {
            try
            {
                char InputY = Convert.ToChar(ReadLine());
                if (InputY == 'y' || InputY == 'Y')
                {
                    InputY_E = true;
                    continueGame = true;
                    firstTurn = true;
                    lives = -1;
                }
                else if (InputY == 'e' || InputY == 'E')
                {
                    InputY_E = true;
                    ExitApplication();

                }
                else
                {
                    WriteLine("Invalid Input: ");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Only Y to Continue Or E to Exit can be select: ");
            }
        }

    }

}
