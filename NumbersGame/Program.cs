using System;
namespace NumbersGame
{
    class Program
    {
        static void Main()
        {   
            bool isRunning = true;
            while (isRunning)
            {   
                // each time you play again the console is cleared
                Console.Clear();
                Console.WriteLine("Välkommen till spelet nummerGissaren! Välj din nivå: enkel, mellan, svår eller expert.");
                // taking input from user, making it lowercase and then checking if its a valid input, until the input is correct and saved.
                string? answer = Console.ReadLine();
                answer = answer?.ToLower();
                while (answer != "enkel" && answer!= "mellan" && answer != "svår" && answer!= "expert")
                {
                    Console.WriteLine("Fel input, du får endast välja mellan nivåerma : enkel, mellan, svår och expert. Försök igen");
                    answer = Console.ReadLine();
                }
                
                // the answer is deciding level of difficulty, and each level is represented by a "highest number" and a max guess
                int maxGuesses = 0;
                int highestNumber = 0;
                Random random = new Random();

                if (answer == "enkel")
                {
                    highestNumber = 10;
                    maxGuesses = 5;
                }
                else if (answer == "mellan")
                {
                    highestNumber = 40;
                    maxGuesses = 8;
                }
                else if (answer == "svår")
                {
                    highestNumber = 80;
                    maxGuesses = 8;
                }
                else if (answer == "expert")
                {
                    highestNumber = 100;
                    maxGuesses = 3;
                }
                // creating a random number between 1- highest the user can guess between, using the random class
                
                int number = random.Next(1, (highestNumber));
                int guesses = 0;
                bool win = false;
                // time to play. we have two new variables one that checks a win condition and one for counting each valid guess
                Console.WriteLine($"Då kör vi igång! Jag tänker på ett nummer mellan 1-{highestNumber}, kan du gissa vilket? Du får {maxGuesses} försök.");

                for (int i = 0; i < maxGuesses; i++)
                {
                    int guess;
                    /*he check if the input is incorrect, if we can parse it to the int variable guess
                    and if its inside of the guessing bounds of the current level. giving an error message until we get the correct input 
                    and can store the guess in guess. after a guess is correct it gets counted*/
                    while (!int.TryParse(Console.ReadLine(), out guess)|| guess < 1 || guess > highestNumber)
                    {
                        Console.WriteLine($"Fel input, endast siffra mellan 1-{highestNumber} går att gissa på");
                    }
                    guesses++;
                    // we check if we won by inserting the parameters of guess and our randomly generated number. The method returns a bool.
                    win = CheckGuess(guess, number);

                    // if user won = give winning message with how many guesses it took then break out of the for-loop as the guesses are done
                    if (win)
                    {
                        Console.WriteLine($"Du klarade det på {guesses} försök!");
                        break;
                    }
                }
                // if lost, or "not win" ( win == false) there's an errorm message
                if (!win)
                {
                    Console.WriteLine($"Tyvärr lyckades du inte gissa talet på {maxGuesses} försök!");
                }
                
                // letting the player chose if they want to play again or not, also checking for potential errors in input
                Console.WriteLine("Vill du spela igen? (ja/nej)");

                string? playAgain = Console.ReadLine();
                playAgain = playAgain?.ToLower();
                while (playAgain != "ja" && playAgain != "nej")
                {
                    Console.WriteLine("Fel input, du får endast skriva ja eller nej. Försök igen");
                    playAgain = Console.ReadLine();
                 }
                if (playAgain == "nej")
                {
                    Console.WriteLine("Tack för att du spelade!");
                    isRunning = false;
                }
                else
                {
                    Console.WriteLine("Åh va roligt! Då kör vi en gång till!");
                }
            }
        }
        // method that checks how close user guess is to the random number, and gives a message depending on the difference
        static string AreYouClose(int smaller, int bigger)
        {
            string message = "";
            int difference = bigger - smaller;  

            if (difference < 3)
            {
                message = "NÄRA nu";
            }
            else if ( difference < 5)
            {
                message = "inte så långt ifrån";
            }
            else if (difference < 10)
            {
                message = "Inte nära, men mindre än 10 ifrån.";
            }
            else
            {
                message = "LÅNGT ifrån, testa åt helt andra hållet.";
            }
            return message;
        }
        static bool CheckGuess(int guess, int number)
        {
            /* method checking if users guess is the same as  the randomly generated number from computer.
            here we call upon the method AreYouClose, if number is bigger than guess its the first parameter 
            and if guess is bigger than number we change their places.(see the method declaration)*/
            if (number > guess)
            {
                Console.WriteLine("Tyvärr, du gissade för lågt!");
                Console.WriteLine(AreYouClose(guess, number));
            }
            else if (number < guess)
            {
                Console.WriteLine("Tyvärr, du gissade för högt");
                Console.WriteLine(AreYouClose(number, guess));
            }
            else
            {
                Console.WriteLine("Wohoo! Du klarade det!");
                return true;
            }
            return false;
        }
    }
}
