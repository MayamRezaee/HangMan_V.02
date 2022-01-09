using System;
using static System.Console;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;

namespace HangMan
{
    class Programlist
    {
        //Creating a list of string.
        static string[] wordList = { "stringbuilder", "append", "hangman", "csharp", "programming", "list", "array", "out", "ref" };
        //Randomly choose a word from the list above
        static Random random = new Random();
        static int value = random.Next(0, wordList.Length);
        static string secretWord = wordList[value];


        //Entry point of the project
        static void Main(string[] args)
        {
            WriteLine("This is a hangman game");
            WriteLine("\n The chosen word for you contains {0} letters. ", secretWord.Length);

            //make 3 different options for user 
            bool isAlive = true;
            while (isAlive)
            {

                WriteLine("\n Choose one of the options below: " +
                    "\n 1) Guess the word" +
                    "\n 2) Guess the letters" +
                    "\n 3) Exit");

                int option = ReadLine()[0];

                //What should the program do efter user chosed one of the option in the menu above
                switch (option)
                {
                    case '1':
                        GuessWholWord();
                        break;

                    case '2':
                        PlayTheGame();
                        break;
                    case '3':
                        Clear();
                        ForegroundColor = ConsoleColor.Magenta;
                        WriteLine("\n Thank you for trying my project");
                        ResetColor();
                        isAlive = false;
                        Environment.Exit(0);

                        break;
                    default:
                        WriteLine(" {0} is not a valid selection, please try again", option);
                        break;

                }
            }
        }

        //The method or the first option in the menu.
        public static void GuessWholWord()

        {
            WriteLine("Which word is it you think");

            string wholeWordGuess = ReadLine();

            if (wholeWordGuess == secretWord)
            {

                Write("Fantastisk, you WON the game already." +
                "The word is ");
                ForegroundColor = ConsoleColor.Green;
                Write(secretWord);
                ResetColor();

            }

            else
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine("Unfortunatly you couldnt guess correct at this time.");
                ResetColor();
            }



        }
        //What the program should do if user choose to guess the letters one by one
        public static void PlayTheGame()
        {
            bool won = false;
            int revealedLetters = 0;
            int numberOfchances = 10;
            string input;
            char guess;
            int _incorGuessesIndex = 0;


            //Making a stringbuilder
            StringBuilder displayToPlayer = new StringBuilder(secretWord.Length);
            for (int i = 0; i < secretWord.Length; i++)
                displayToPlayer.Append('_');



            //Making a list of correct and incorrect user guesse 
            ArrayList corGuesses = new ArrayList();
            char[] incorGuesses = new char[numberOfchances];
            


            //Loop through user guesses
            while (!won && numberOfchances > 0)
            {


                Write("\n You have ");
                ForegroundColor = ConsoleColor.Magenta;
                Write(numberOfchances);
                ResetColor();
                WriteLine(" attempt left to guess the correct word");



                Write("Guess a letter: ");
                input = Console.ReadLine();
                guess = input[0];

                //if the user guess a correct letter twice 

                if (corGuesses.Contains(guess))
                {
                    Console.WriteLine("You've already tried '{0}'.", guess);
                    continue;
                }
                //if user guess incorrect twice
                else if (incorGuesses.Contains(guess))
                {
                    Console.WriteLine("You've already tried '{0}'.", guess);


                    continue;
                }

                //If user guess a correct letter from secret word
                if (secretWord.Contains(guess))
                {
                    corGuesses.Add(guess);

                    for (int i = 0; i < secretWord.Length; i++)
                    {
                        if (secretWord[i] == guess)
                        {
                            displayToPlayer[i] = secretWord[i];
                            revealedLetters++;
                        }
                    }

                    if (revealedLetters == secretWord.Length)
                        won = true;
                }
                //a message to user if they guess a incorrect letter
                else
                {
                    incorGuesses[_incorGuessesIndex] = guess;
                    _incorGuessesIndex++;

                    Console.WriteLine("Try another letter, '{0}' is not contained in the word", guess);
                    numberOfchances--;
                }



                Console.WriteLine(displayToPlayer.ToString());

                WriteLine("Your incorrect guesses are as foloow.");
                for (int i = 0; i < incorGuesses.Length; i++)
                {
                    if (incorGuesses[i] == default) continue;
                    Console.WriteLine(incorGuesses[i]);
                }
            }

            //When all the letters are correct 
            if (won)
            {
                ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Great! You won the game.");
                ResetColor();
            }
            //When user lose the game
            else
            {
                ForegroundColor = ConsoleColor.Red;
                Console.Write("Unfortunatly you could't guess the correct word." +
                "\nThe word was : ");

                ForegroundColor = ConsoleColor.Green;
                WriteLine(secretWord);
                ResetColor();

                Console.Write("\n Press enter to leave the game");
                Console.ReadLine();
            }
        }


    }
}

