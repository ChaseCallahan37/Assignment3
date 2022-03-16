using System;
using System.IO;

namespace pa3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            //initialize my variables
            bool continueProgram = true;        //Will be used to continue running over all program
            int gamesPlayed = 0;
            int gamesWon = 0;
            int totalCredits = 50;
            string mainMenuChoice;
            string userName;

            //Begin Program
            DisplayIntroMessage();          //Will display explanation of the program
            userName = GetUserName();       //Get and store the users user name
            mainMenuChoice = GetMainMenuChoice();

            while(continueProgram)      //So long as not proven false, overall main menu will continue to run
            {
                if(mainMenuChoice == "1")
                {
                    TheForceStart(ref totalCredits, ref gamesPlayed, ref gamesWon);
                }
                else if(mainMenuChoice == "2")
                {
                    if(totalCredits >= 20)      //Ensures that user has enough credits to play
                    {
                        BlastersStart(ref totalCredits, ref gamesPlayed, ref gamesWon);
                    }
                    else
                    {
                        System.Console.WriteLine($"You do not have enought credits to play. You must have at least 20 and you currently have {totalCredits} credits.");
                        AskAnyKeyContinue();
                    }
                }
                else if(mainMenuChoice == "3")
                {
                    TheStoryStart(ref totalCredits, ref gamesPlayed, ref gamesWon);
                }
                else if(mainMenuChoice == "4")
                {
                    CheckCredits(totalCredits);     //Will display the total credits that the user has
                }
                else if(mainMenuChoice == "5")
                {
                    CheckStats(gamesPlayed, gamesWon);
                }
                else if(mainMenuChoice == "6")
                {
                    ShowLeaderBoard();
                }
                else if(mainMenuChoice == "7")
                {
                    continueProgram = false;
                }

                if(totalCredits > 0 && totalCredits < 300 && continueProgram)      //Checks to see if total credits meets win/loose condition   
                {
                    mainMenuChoice = GetMainMenuChoice();
                }
                else
                {
                    continueProgram = false;
                }
                Console.Clear();
            }
            CheckWinLoss(totalCredits, userName, gamesWon, gamesPlayed);     //Determines if user won the program or not
            AskAnyKeyContinue();
            System.Console.WriteLine("Here is one final look at your games won and lost.");
            AskAnyKeyContinue();
            CheckStats(gamesPlayed, gamesWon);
            System.Console.WriteLine("Thank you for playing!");
        }

        static string GetUserName()
        {
            System.Console.WriteLine("Please enter your name below.");
            string userName = Console.ReadLine();
            CheckUserName(ref userName);
            return userName;
        }
        static void CheckUserName(ref string userName)
        {
            System.Console.WriteLine($"Is this correct?\nUsername: {userName}\n\t1. Yes\n\t2. No");
            string correctOrNot = Console.ReadLine();       //Input for whether name is correct or not
            
            if(!(correctOrNot == "1" || correctOrNot == "2"))
            {
                bool incorrectName = true;
                while(incorrectName)
                {
                    System.Console.WriteLine("Please enter either '1' or '2'.");
                    correctOrNot = Console.ReadLine();

                    if(correctOrNot == "1" || correctOrNot == "2")
                    {
                        incorrectName = false;
                    }
                }
            }
            
            if(correctOrNot == "2")
            {
                System.Console.WriteLine("Please enter your username.");
                userName = Console.ReadLine();
                Console.Clear();
            }
        }

        static void ShowLeaderBoard()
        {
            string[] userNames = new string[100];       //Stores name of users
            int[] userGamesWon = new int[100];          //Store total amount of games won by user
            int[] userGamesPlayed = new int[100];        //Stores total amount of games played by user
            int count = 0;

            //Open file
            StreamReader inFile = new StreamReader(@"C:\Users\chase\OneDrive\Documents\Source\Repos\mis221\PAs\PA3\highScores.txt");

            //Process File
            string line = inFile.ReadLine();

            while(line != null)
            {
                string[] temp = line.Split("#");
                userNames[count] = temp[0];
                userGamesWon[count] = int.Parse(temp[1]);
                userGamesPlayed[count] = int.Parse(temp[2]);
                line = inFile.ReadLine();
                count ++;
            }
            //Close file
            inFile.Close();

            PrintNames(userNames, userGamesWon, userGamesPlayed, count);
        }

        static void PrintNames(string[] userNames, int[] userGamesWon, int[] userGamesPlayed, int count)
        {

            Console.Clear();
            for(int u = 0; u < count ; u++)
            {
                System.Console.WriteLine($"User: {userNames[u]} \tGames Won: {userGamesWon[u]} \t Games Played: {userGamesPlayed[u]}");
            }
            AskAnyKeyContinue();
        }

        static void DisplayIntroMessage()
        {
            System.Console.WriteLine("Welcome to my program!");
            System.Console.WriteLine("You will be playing a series of games as Luke Skywalker.");
            System.Console.WriteLine("The games following are the ones he used to play with Yoda.");
            System.Console.WriteLine("In order to win the overall game, your goal is to reach 300 credits.");
            System.Console.WriteLine("You will start with 50 credits. If you lose all your credits, then you lose the game.");
            System.Console.WriteLine("\n... Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
        }

        static string GetMainMenuChoice()
        {
            DisplayMainMenuOptions();       //Show main menu options for user
            string menuChoice = Console.ReadLine();
            Console.Clear();
            VerifyMainMenuChoice(ref menuChoice);   //Ensure valid input
            return menuChoice;
        }

        static void DisplayMainMenuOptions()
        {
            Console.Clear();
            System.Console.WriteLine("Here are the available features.\n");
            System.Console.WriteLine("1. Play The Force");
            System.Console.WriteLine("2. Play Blasters");
            System.Console.WriteLine("3. Play The Story");
            System.Console.WriteLine("4. Check Credit Balance");
            System.Console.WriteLine("5. Check Game Statistics");
            System.Console.WriteLine("6. Check Leader Board");
            System.Console.WriteLine("7. Exit Program");
            System.Console.WriteLine("\nPlease enter the number associated with the feature you want to run. ");
        }

        static void VerifyMainMenuChoice(ref string menuChoice)
        {
            while(!(menuChoice == "1" || menuChoice == "2" || menuChoice == "3" || menuChoice == "4" || menuChoice == "5" || menuChoice == "6" || menuChoice == "7"))
            {
                Console.Clear();
                System.Console.WriteLine("You have entered an invalid option, please try again!\n");
                DisplayMainMenuOptions();
                menuChoice = Console.ReadLine();
            }
        }

        static void CheckWinLoss(int totalCredits, string userName, int gamesWon, int gamesPlayed)
        {
            Console.Clear();
            if(totalCredits <= 0)       //If credits are at or below 0 then they loose
            {
                System.Console.WriteLine("You lost all of your credits and have lost :(");
            }
            else if(totalCredits >= 300)        //If credits are at or above 300 then they win
            {
                System.Console.WriteLine($"Congrats, you have obtained {totalCredits} credits and have won!");
                AskAnyKeyContinue();
                PushToHighScores(userName, gamesWon, gamesPlayed);     //Store user name in the file
            }
            else
            {
                System.Console.WriteLine("You ended the program before meeting the win/loss conditions.");
                System.Console.WriteLine($"You earned {totalCredits} credits.");
            }
            
        }

        static void PushToHighScores(string userName, int gamesWon, int gamesPlayed)
        {
            //Initialize Variables
            string[] userNames = new string[100];       //Stores name of users
            int[] userGamesWon = new int[100];          //Store total amount of games won by user
            int[] userGamesPlayed = new int[100];        //Stores total amount of games played by user
            int count = 0;

            //Open file
            StreamReader inFile = new StreamReader(@"C:\Users\chase\OneDrive\Documents\Source\Repos\mis221\PAs\PA3\highScores.txt");

            //Process File
            string line = inFile.ReadLine();

            while(line != null)
            {
                string[] temp = line.Split("#");
                userNames[count] = temp[0];
                userGamesWon[count] = int.Parse(temp[1]);
                userGamesPlayed[count] = int.Parse(temp[2]);
                line = inFile.ReadLine();
                count ++;
            }
            //Close File
            inFile.Close();

            

            //Add the newest user
            userNames[count] = userName;
            userGamesWon[count] = gamesWon;
            userGamesPlayed[count] = gamesPlayed;
            count++;


            WriteToFile(userNames, userGamesWon, userGamesPlayed, count);
            PrintNames(userNames, userGamesWon, userGamesPlayed, count);

            
        }

        static void WriteToFile(string[] userNames, int[] userGamesWon, int[] userGamesPlayed, int count)
        {
            //Open File
            StreamWriter outFile = new StreamWriter(@"C:\Users\chase\OneDrive\Documents\Source\Repos\mis221\PAs\PA3\highScores.txt");
            //Process
            for(int u = 0; u < count; u++)
            {
                outFile.WriteLine($"{userNames[u]}#{userGamesWon[u]}#{userGamesPlayed[u]}");
            }

            System.Console.WriteLine("This is the right after pushing to file");
            Console.ReadKey();
            System.Console.WriteLine($"this is the last user {userNames[count]}");
            Console.ReadKey();

            //Close File
            outFile.Close();


        }

        static void TheForceStart(ref int totalCredits, ref int gamesPlayed, ref int gamesWon)
        {
            //Declare Variables
            bool playGame;      //return value determining if game will continue or not
            bool forceWin;      //Used to test if game was won or lost
            
            ForceInitialMessage();
            playGame = AskContinueGame();

            while(playGame)
            {
                if(totalCredits > 0 && totalCredits < 300)      //So long as they have not already won or lost, then continue asking to continue game
                {
                    forceWin = PlayTheForce(ref totalCredits);             //Start the actual game
                    
                    gamesPlayed++;                              //Add one to the overall game count
                    if(forceWin)
                    {
                        gamesWon++;                             //If game is won, then add won to games won count
                    }
                    if(totalCredits > 0 && totalCredits < 300)      //So long as winning conditions are not met
                    {
                        playGame = AskContinueGame();
                    }
                    else        //If win/loss conditions are met then do this
                    {
                        playGame = false;
                    }
                }
                else                                            // If win/loss condition has been met, return user to main menu
                {
                    playGame = false;
                }
            }

        }

        static void ForceInitialMessage()
        {
            Console.Clear();
            System.Console.WriteLine("This objective of this game is to guess the outcome of a future action.");
            System.Console.WriteLine("You will be given a card, randomly drawn from a deck.");
            System.Console.WriteLine("Next, you will be asked to guess whether you think the next randomly drawn card will be higher\nor lower than the current card.");
            System.Console.WriteLine("The game will go on for a maximum of 10 rounds");
            System.Console.WriteLine("You will have to bet credits before beginning the game.");
            System.Console.WriteLine("\n\tIf you win all 10 rounds, you triple the credits you put up.");
            System.Console.WriteLine("\tIf you win more than 6 but less than 10, you will double the credits you put up.");
            System.Console.WriteLine("\tIf you win 5, you will break even");
            System.Console.WriteLine("\tIf you win less than 5, you will loose all the credits you put up.");
        }

        static bool AskContinueGame()
        {
            //Declare Variables
            bool continueForward = true;
            string continueGameInput;       //Stores user choice to continue game

            DisplayAskContinue();
            continueGameInput = Console.ReadLine();

            //Validate the data entered by user
            ValidateForceContinueGame(ref continueGameInput);

            //Determine if program will continue forward of go back to main menu
            if(continueGameInput == "2")    //If they choose to return to main menu
            {
                continueForward = false;
            }
            else if(continueGameInput == "1")   //If they choose to continue on to the game
            {
                continueForward = true;
            }

            return continueForward;
            
        }

        static void DisplayAskContinue()
        {
            System.Console.WriteLine("\nWhat would you like to do?");
            System.Console.WriteLine("1. Continue to the game");
            System.Console.WriteLine("2. Return to main menu");
        }

        static void ValidateForceContinueGame(ref string userInput)
        {
            while(!(userInput == "1" || userInput == "2"))
            {
                Console.Clear();
                System.Console.WriteLine("You have entered an invalid input, please try again\n");
                DisplayAskContinue();
                userInput = Console.ReadLine();
            }
        }

        static bool PlayTheForce(ref int totalCredits)
        {
            Console.Clear();

            //Declare Variables
            string currentCard;             //Stores name of current card
            string nextCard;                //Stores name of the next card to be drawn
            string userGuess;               //Stores the choice of whether higher or lower
            int currentCardValue = 0;       //Stores the value of the current card
            int nextCardValue = 0;          //Stores the value of the next card
            int count = 0;                  //Count used to keep track of rounds played
            int userBet;                    //Stores the amount that the user is betting
            int cardPosition;               //Will store the subscript of the array being called to store in comparing variables for current and next card            
            bool continueGame = true;       //Will continue running game until this is proven false
            bool correctGuess;
            bool gameWin;

            //Start game
            userBet = GetBet(totalCredits);

            //Create two parallell arrays to be used when drawing cards
            string [] deckOfCards = new string[52];
            int[] cardValues = new int[52];
            CreateDeck(deckOfCards, cardValues);

            //Set current card
            cardPosition = GetCardPosition(cardValues, ref currentCardValue);      //Will retrieve subscript of card and sets the value of the card while in the method via ref
            currentCard = deckOfCards[cardPosition];

            //Set next card
            cardPosition = GetCardPosition(cardValues, ref nextCardValue);      //Will retrieve subscript of card
            nextCard = deckOfCards[cardPosition];

            while(continueGame && count < 10)     //Will continue to run game until loss or 10 rounds occur
            {
                if(count < 10)
                {
                    userGuess = GetForceGuess(currentCard);
                    if(userGuess == "exit")        //Take user out of game and deduct credits put up from total credits
                    {
                        Console.Clear();
                        System.Console.WriteLine("You have chosen to exit the game.");
                        System.Console.WriteLine($"You made it to round {count + 1}.");
                        System.Console.WriteLine("You will loose all the credits you put up.");
                        totalCredits -= userBet;
                        AskAnyKeyContinue();
                        return false;
                    }
                    correctGuess = InterpretForceGuess(userGuess, currentCardValue, nextCardValue);
                    
                    if(correctGuess)    //If guess was correct then do this
                    {
                        Console.Clear();
                        System.Console.WriteLine($"The next card is the {nextCard}.");
                        System.Console.WriteLine("Congrats, you guessed correctly!");
                        if(count < 10)      //Will not display if it is the last round
                        {
                            System.Console.WriteLine($"You will advance on to round {count + 1}");
                        }
                        AskAnyKeyContinue();
                        count++;        //Add one to count to keep track with the rounds.
                    }
                    else if(!correctGuess)
                    {
                        Console.Clear();
                        System.Console.WriteLine($"The next card is the {nextCard}.");
                        System.Console.WriteLine("Sorry, you guessed incorrectly");
                        System.Console.WriteLine($"You made it to round {count + 1}");
                        AskAnyKeyContinue();
                        continueGame = false;
                    }
                    
                    //Set current card equal to next card
                    currentCard = nextCard;
                    currentCardValue = nextCardValue;

                    //Set next card
                    cardPosition = GetCardPosition(cardValues, ref nextCardValue);      
                    nextCard = deckOfCards[cardPosition];
                }
                

            }
            gameWin = FactorForceInBet(ref totalCredits, userBet, count);
            AskAnyKeyContinue();

            return gameWin;
        }

        static int GetBet(int totalCredits)
        {
            Console.Clear();
            AskForBet(totalCredits);        //Prompts the user to enter the amount they would like to bet
            string userBetAmount = Console.ReadLine();
            int userBetAmountInt = ValidateBet(userBetAmount, totalCredits);
            return userBetAmountInt;
        }

        static void AskForBet(int totalCredits)
        {
            System.Console.WriteLine("How much would you like to bet?");
            System.Console.WriteLine("You must bet at least 1 credit, 20 if playing Blasters.");
            System.Console.WriteLine($"You may bet no more than {totalCredits}, your total credits.");
        }

        static int ValidateBet(string userBetAmount, int totalCredits)
        {
            //Declare variables
            bool validEntry = false;      //Will be used to proove that user input is valid
            int userInputInt;               //will accept user input returned as an int

            userInputInt = TestForInt(ref userBetAmount, totalCredits);   //Will go run method containing Try.Parse to see if it is convertable to an integer

            while(!validEntry)       //So long as valid entry is proven false
            {
                if(userInputInt <= totalCredits && userInputInt > 0)
                {
                    validEntry = true;
                }
                else        //if bet amount falls outside the range desired, get new data
                {
                    System.Console.WriteLine($"Please bet an amount between 1 & {totalCredits}");
                    System.Console.WriteLine($"If this is blasters, then please enter a bet between 20 & {totalCredits}");
                    userBetAmount = Console.ReadLine();
                    Console.Clear();
                    userInputInt = TestForInt(ref userBetAmount, totalCredits);
                }
            }
            return userInputInt;
        }

        static int TestForInt(ref string userInput, int totalCredits)
        {
            //Declare variables
            int userInputInt = 0;
            bool isInt;             //Contain data indicating if true/false about user input able to convert to an int

            isInt = int.TryParse(userInput, out userInputInt);

            while(!isInt)
            {
                System.Console.WriteLine($"Please enter a number between 1 & {totalCredits}.");
                System.Console.WriteLine($"If this is blasters, then please enter a number between 20 & {totalCredits}");
                userInput = Console.ReadLine();
                Console.Clear();
                isInt = int.TryParse(userInput, out userInputInt);
            }

            return userInputInt;
        }

        static void AskAnyKeyContinue()
        {
            System.Console.WriteLine("\n... Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
        }

        static void CreateDeck(string[] deckOfCards, int[] cardValues)
        {
            //Create arrays and counter to make deck
            string[] cardSuits = { "Clubs", "Diamonds", "Hearts", "Spades"};
            string[] cardFaces = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King"};
            int counter = 0;

            for(int i = 0; i < 13; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    deckOfCards[counter] = $"{cardFaces[i]} of {cardSuits[j]}";     //array that will display name of card
                    cardValues[counter] = i + 1;                                    //array that will store value of card being called
                    counter++;
                }
            }

        }

        static int GetCardPosition(int[] cardValues, ref int valueToRefBack)
        {
            int randomNumber = RandomCard();    //The RNG used for randomization. Will be returned so that element can be called in other arrays

            while(cardValues[randomNumber] == 0)    //Will continue to run through loop if returning value is 0. If it is 0, then it is because it has already been drawn
            {
                randomNumber = RandomCard();
            }
            valueToRefBack = cardValues[randomNumber];
            cardValues[randomNumber] = 0;
            
            return randomNumber;

        }

        static int RandomCard()
        {
            Random rnd = new Random();
            int number = rnd.Next(0,52);
            return number;
        }

        static string GetForceGuess(string currentCard)
        {
            DisplayForceInGameMenu(currentCard);
            string userInput = Console.ReadLine();
            userInput = userInput.ToLower();
            ValidateOneTwoOrLower(ref userInput, currentCard);   //Validate that input is either a 1,2, or exit.
            return userInput;
        }

        static void DisplayForceInGameMenu(string currentCard)
        {
            Console.Clear();
            System.Console.WriteLine($"The current card is {currentCard}.");
            System.Console.WriteLine("Do you think that the next card will be:");
            System.Console.WriteLine("1. Higher");
            System.Console.WriteLine("2. Lower");
            System.Console.WriteLine("\nPlease enter either 1 or 2.");
            System.Console.WriteLine("\n\nIf you would like to exit the program then type 'exit'");
        }

        static void ValidateOneTwoOrLower(ref string userInput, string currentCard)
        {
            bool validInput = false;

            while(!validInput)      //While it has not been proven that it is valid input
            {
                if(userInput == "1" || userInput == "2" || userInput == "exit")
                {
                    validInput = true;
                }
                else        //If not valid, prompt user for new data, and give them their options again.
                {
                    System.Console.WriteLine("You did not enter a valid input.\n");
                    AskAnyKeyContinue();
                    DisplayForceInGameMenu(currentCard);
                    userInput = Console.ReadLine();
                    userInput = userInput.ToLower();
                }
            }
        }

        static bool InterpretForceGuess(string userChoice, int currentCardValue, int nextCardValue)
        {
            bool correctGuess = false;      //work to proove that the guess is correct

            if(userChoice == "1")    //If user guesses that next card will be higher
            {
                if(currentCardValue < nextCardValue)
                {
                    return true;
                }
            }
            else if(userChoice == "2")       //If user guesses that next card will be lower
            {
                if(currentCardValue > nextCardValue)
                {
                    return true;
                }
            }
            
            return correctGuess;
        }

        static bool FactorForceInBet(ref int totalCredits, int userBet, int count)
        {
            bool gameWin = false;       //Used to determine if game was won or not

            if(count < 5)
            {
                totalCredits -= userBet;
                System.Console.WriteLine($"Since you only won {count} round(s), you will loose {userBet} credits, what you had bet.");
                System.Console.WriteLine($"Your current credit balance is {totalCredits}.");
                gameWin = false;
            }
            else if(count == 5 || count == 6)
            {
                System.Console.WriteLine($"Since you won {count} rounds, you will break even and there will be no change to your credit balance.");
                System.Console.WriteLine($"Your current credit balance is {totalCredits}.");
                gameWin = false;
            }
            else if(count > 6 && count < 10)
            {
                System.Console.WriteLine($"Since you won {count} rounds, you will double the credits you had bet!");
                System.Console.WriteLine($"You will be receiving {userBet * 2} credits!");
                System.Console.WriteLine($"Your current credit balance is {totalCredits}.");
                totalCredits += userBet * 2;
                gameWin = true;
            }
            else if(count == 10)
            {
                System.Console.WriteLine("WOW! You got all 10 rounds correct!");
                System.Console.WriteLine("Because of this, you will have your bet tripled!");
                totalCredits += userBet * 3;
                System.Console.WriteLine($"You will be receiving {userBet * 3} credits!");
                gameWin = true;
            }

            return gameWin;
        }

        static void CheckCredits(int totalCredits)
        {
            Console.Clear();
            System.Console.WriteLine($"You currently have {totalCredits} credits.");
            System.Console.WriteLine("If you drop to or below 0, then you will loose the game.");
            System.Console.WriteLine("If you reach 300 or more, then you will win the game!");
            AskAnyKeyContinue();
        }

        static void CheckStats(int gamesPlayed, int gamesWon)
        {
            // double winLossRatio = GetWinLossRatio(gamesPlayed, gamesWon);   //Go run the ratio calculation and return it
            Console.Clear();
            System.Console.WriteLine($"You have played a total of {gamesPlayed} games and you have won {gamesWon} of those games.");
            // System.Console.WriteLine($"Your win to loss ratio is {winLossRatio}");
            AskAnyKeyContinue();
        }

        // static double GetWinLossRatio(int gamesPlayed, int gamesWon)
        // {
        //     double ratio = 0.00;
        //     double gamesWonDouble = double.Parse(gamesWon);
        //     double gamesPlayedDouble = double.Parse(gamesPlayed);
        //     if(gamesPlayed > 0)     //If long as the number of games played is greater than 0, run this
        //     {
        //         ratio = (gamesWonDouble/gamesPlayedDouble);
        //     }
        //     return ratio;
        // }
        
        static void BlastersStart(ref int totalCredits, ref int gamesPlayed, ref int gamesWon)
        {
            //Declare variables
            bool playGame;
            bool blastersWin;        //blasters is won

            BlastersIntroMessage();
            playGame = AskContinueGame();    //Ask the user if they would like to continue
            
            while(playGame)
            {
                if(totalCredits > 0 && totalCredits < 300)
                {
                    if(19 < totalCredits)
                    {
                        blastersWin = PlayBlasters(ref totalCredits);
                        gamesPlayed++;
                        if(blastersWin)
                        {
                            gamesWon++;
                        }
                        if(totalCredits > 0 || totalCredits < 300)      //if the win/loss conditions are not met
                        {
                            playGame = AskContinueGame();
                        }
                        else        //If credits within win loss condition, then proove false
                        {
                            playGame = false;
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("You do not have at least 20 credits.");
                        playGame = false;
                    }
                }
            }
        }
        static void BlastersIntroMessage()
        {
            Console.Clear();
            System.Console.WriteLine("In this game, your objective is to dodge or deflect the blasters that Yoda is shooting at you");
            System.Console.WriteLine("You will begin with 15 points, if you lose all your points, then you lose the game.\nIf you reach 40 points, then you win.");
            System.Console.WriteLine("Your odds are 50% to dodge and 30% to deflect.");
            System.Console.WriteLine("You will get 5 points if you succesfully dodge and 10 points if you succesfully deflect.");
            System.Console.WriteLine("If you fail to dodge or deflect, you will lose 5 points.");
        }   

        
        static bool PlayBlasters(ref int totalCredits)
        {
            Console.Clear();

            //Declare Variables
            bool continueGame = true;
            bool missBlaster;           //Used to determine whether Luke is not hit by the blaster
            bool gameWon;
            int points = 15;
            int userBet = 0;
            string userDecision;        //Used determine whether user wants to dodge, deflect, or exit

            userBet = GetBlastersBet(totalCredits);

            while(continueGame)
            {
                userDecision = GetDodgeOrDeflect(points);

                if(userDecision == "exit")      //If user enters exit, they will be taken out of the program
                {
                    Console.Clear();
                    System.Console.WriteLine("You have chosen to prematurely exit the program.");
                    System.Console.WriteLine($"Because of this, you will loose all {userBet} credits that you chose to bet.");
                    totalCredits -= userBet;
                    return false;               //Return false so that game will not be counted as a win.
                }
                else if(userDecision == "1")
                {
                    missBlaster = DetermineBlasterSuccess(userDecision, points);
                    if(missBlaster)     //if it is true that luke is not hit by the blaster
                    {
                        System.Console.WriteLine("Congrats, you succesfully dodged!\nYou will get 5 points.");
                        AskAnyKeyContinue();
                        points += 5;
                    }
                    else        //if it is false that luke is hit not hit by the blaster
                    {
                        System.Console.WriteLine("Not fast enough, you were hit by the blaster.\nYou will lose 5 points.");
                        AskAnyKeyContinue();
                        points -= 5;
                    }
                }
                else if(userDecision == "2")        //If user chooses to dodge
                {
                    missBlaster = DetermineBlasterSuccess(userDecision, points);
                    if(missBlaster)     //If succesful then do this
                    {
                        System.Console.WriteLine("Congrats, you succesfully deflected!\nYou will get 10 points now.");
                        AskAnyKeyContinue();
                        points += 10;
                    }
                    else                //If you fail to dodge, then do this
                    {
                        System.Console.WriteLine("You did not succesfully deflect, you will be loosing 5 points.");
                        AskAnyKeyContinue();
                        points -= 5;
                    }
                }
                if(points <= 0 || points >= 40)
                {
                    continueGame = false;
                }
            }
            gameWon = FactorBlastersBet(ref totalCredits, userBet, points);
            return gameWon;

        }

        static int GetBlastersBet(int totalCredits)
        {
            int userBet = GetBet(totalCredits);     //Get the user bet and return it to the force game method
            while(!(19 <  userBet))
            {
                Console.Clear();
                System.Console.WriteLine("You must bet at least 20 credits to play.");
                userBet = GetBlastersBet(totalCredits);
            }
            return userBet;
        }

        static string GetDodgeOrDeflect(int points)
        {
            string userDecision = GetBlastersDecision(points);        //Determine if the user would like to dodge or deflect
            ValidateBlastersOneTwoOrExit(ref userDecision);     //Validate that uer entered a valid input
            return userDecision;
        }

        static string GetBlastersDecision(int points)
        {
            Console.Clear();
            System.Console.WriteLine($"You currently have {points} points.");
            System.Console.WriteLine("Choose one of the following:");
            System.Console.WriteLine("\t1. Dodge\n\t2. Deflect\n\nEnter 'exit' if you would like to exit");
            return Console.ReadLine().ToLower();
        }

        static void ValidateBlastersOneTwoOrExit(ref string userInput)
        {
            bool validInput = false;        //Operate under the assumption that the input is not valid and work to proove it true

            while(validInput)
            {
                if(userInput == "1" || userInput == "2" || userInput == "exit")
                {
                    validInput = true;
                }
                else        //Do this if it is not one of the accepted inputs
                {
                    Console.Clear();
                    System.Console.WriteLine("You have not entered a valid input.\n\nChoose one of the following:\n\n\t1. Dodge\n\t2. Deflect\n\nEnter exit if you would like to exit the program.");
                    userInput = Console.ReadLine().ToLower();
                }
            }
        }

        static bool DetermineBlasterSuccess(string userDecision, int points)
        {
            bool missBlaster = true;        //Assume that they will miss the blaster and then work to proove the opposite

            if(userDecision == "1")     //If user chooses to dodge then do this
            {
                Random rnd = new Random();
                int chance = rnd.Next(0, 2);

                if(chance == 0)     //50% chance to proove that dodge fails
                {
                    Console.Clear();
                    missBlaster = false;   
                }
                else        //If succesful then do this
                {
                    Console.Clear();
                }
            }
            else if(userDecision == "2")        //If user chooses to deflect.
            {
                Random rnd = new Random();
                int chance = rnd.Next(0, 10);

                if(chance > 2)          //70% chance for program to proove that dodge fails
                {
                    Console.Clear();
                    missBlaster = false;
                }
                else
                {
                    Console.Clear();
                }
            }
            return missBlaster;
        }

        static bool FactorBlastersBet(ref int totalCredits, int userBet, int points)
        {
            bool gmaeWon = false;       //Assume game was lost otherwise proven

            if(points <= 0)     //If the points of the user drops to or below 0, then perform this action
            {
                System.Console.WriteLine($"Since your point have dropped to {points}, you have lost.");
                totalCredits -= userBet;
            }
            else if(points >= 40)
            {
                System.Console.WriteLine($"Since your points have reached {points}, you have won!");
                totalCredits += userBet;
                gmaeWon = true;
            }
            return gmaeWon;
        }

        static void TheStoryStart(ref int totalCredits, ref int gamesPlayed, ref int gamesWon)
        {
            StoryIntroMessage();    //Give brief description of the game
            bool continueGame = AskContinueGame();  //Asks the user if they would like to continue the game or not
            while(continueGame)
            {
                if(totalCredits > 0 && totalCredits < 300)      //So long as user has not met the win or loss conditions
                {
                    bool storyWon = PlayTheStory(ref totalCredits);
                    gamesPlayed++;
                    if(storyWon)
                    {
                        gamesWon++;     //If it is proven true that user has won game, then increment the games won by 1
                    }
                    if(totalCredits > 0 && totalCredits < 300)
                    {
                        continueGame = AskContinueGame();
                    }
                    
                }
                else                    //If the is not within the credit range to play.
                {
                    continueGame = false;
                }
            }

        }

        static void StoryIntroMessage()
        {
            System.Console.WriteLine("In this game, you will be asked to bet credits before beginning the game.\n" +
            "If you do not win, then you will loose all your credits. However, if you do win, you will double your credits.\n" +
            "In order to win, you must play through the story of a character.\n" +
            "Depending on the choices that you make, you will either return back to some form of civilization or you, the character, will die.\n");
        }

        static bool PlayTheStory(ref int totalCredits)
        {
            int userBet = GetBet(totalCredits);     //Used to get users bet
            bool storyOutcome = StoryLine();        //Begins the actual game and returns the outcome of the game
            CalculateStoryCredits(ref totalCredits, userBet, storyOutcome);
            return storyOutcome;
        }

        static string ValidateStoryOneTwoOrExit()
        {
            bool validInput = false;        //Assume that the user entered invalid input
            string userInput = Console.ReadLine().ToLower(); 
            while(!validInput)      //So long as it is not a valid input, run the loop
            {
                if(userInput == "1" || userInput == "2" || userInput == "exit")
                {
                    validInput = true;      //If user does enter valid input, run the update read
                }
                else        //If it is not a valid input, then run this
                {
                    System.Console.WriteLine("You have entered an invalid input.\n Please enter a 1, 2, or type 'exit'.");
                    userInput = Console.ReadLine();
                }
            }
            return userInput;
        }

        static int GetRandomChance()
        {
            Random rnd = new Random();
            int number = rnd.Next(100);     //Choose a number between 0 and 99, to get a success on a 100% scale
            return number;  
        }

        static bool StoryLine()
        {
            bool gameWon = false;      //Assume the game is lost.
            Console.Clear();
            System.Console.WriteLine("You wake up on a frozen landscape with no idea of how you got there.\n" +
            "You start to freak out but you are able to slow down your breathing and think clearly.\n" + 
            "Off in the distance, you see a trail of smoke rising. As you are thinking about what could be causing the smoke,\n" +
            "You hear a noise directly behind you.\nWhat do you do?\n\t1. Approach the smoke stack.\n\t2. Turn Around" +
            "\n\nIf you would like to exit the game, type 'exit'");
            string userInput = ValidateStoryOneTwoOrExit();
            
            bool moveOn = false;      //Set a variable equal to false, so long as it is false, you will not move forward
            while(!moveOn)
            {
                int chance = GetRandomChance();     //Get a random number for chance to factor into game play
                if(userInput == "1")
                {
                    moveOn = true;
                    if(chance < 95)
                    {
                        ApproachHill(ref gameWon);
                    }
                    else
                    {
                        TripAndDie(ref gameWon);
                    }
                }
                else if(userInput == "2")
                {
                    if(chance < 90)
                    {
                        Console.Clear();
                        System.Console.WriteLine("You turn around and there is nothing behind you.\n What do you do now?" +
                        "\n\t1. Approach the smoke stack.\n\t2. Turn Around\n\nIf you would like to exit the game, type 'exit'");
                        userInput = ValidateStoryOneTwoOrExit();
                    }
                    else
                    {
                        Console.Clear();
                        System.Console.WriteLine("You turn around to be greeted by a monster that looks strikingly similar to bigfoot.\n" +
                        "The creature mauls you and leaves you on the landscape to freeze to death. You lose!");
                        gameWon = false;
                        moveOn = true;
                        AskAnyKeyContinue();
                    }
                }
                else    //If the user chooses to exit the program then do this
                {
                    gameWon = false;
                    moveOn = true;
                }
                
            }
            return gameWon;
        }

        static void TripAndDie(ref bool gameWon)
        {
            Console.Clear();
            System.Console.WriteLine("You have tripped on a small rock and stumble onto the ground. You freeze to death slowly but surely.");
            gameWon = false;
            AskAnyKeyContinue();
        }

        static void ApproachHill(ref bool gameWon)
        {
            Console.Clear();
            System.Console.WriteLine("As you get closer, you make out a crashed space craft.\n" +
            "There appears to be people moving around, salvaging the wreck.\nWhat do you do?" +
            "\n\t1. Observe the group from behind the rock. \n\t2. Approach them and ask for help.\n\nEnter 'exit' to exit the program.");
            string userInput = ValidateStoryOneTwoOrExit();
            int chance = GetRandomChance();
            if(userInput == "1")
            {
                if(chance < 50)
                {
                    ObserveMurderers(ref gameWon);
                }
                else
                {
                    ObserveTraders(ref gameWon);
                }
            }
            else if(userInput == "2")
            {
                if(chance < 50)
                {
                    NotFriendlyMurderers(ref gameWon);
                }
                else
                {
                    FriendlyMurderers(ref gameWon);
                }
            }
            else
            {
                gameWon = false;
            }
        }

        static void ObserveMurderers(ref bool gameWon)
        {
            Console.Clear();
            System.Console.WriteLine("As you watch from behind the rocks, you notice that they are burying the bodies of the crew.\n" +
            "It appears that they finished any survivors." +
            "\nWhat do you do now?\n\t1. See if they are friendly murderers\n\t2. Prepare yourself to fight\n\nType 'exit' to exit");
            string userInput = ValidateStoryOneTwoOrExit();
            int chance = GetRandomChance();
            if(userInput == "1")
            {
                if(chance < 80)
                {
                    NotFriendlyMurderers(ref gameWon);
                }
                else
                {
                    FriendlyMurderers(ref gameWon);
                }
            }
            else if(userInput == "2")
            {
                if(chance < 70)
                {
                    SuccesfulPrepareFight(ref gameWon);
                }
                else
                {
                    UnsuccesfulPrepareFight(ref gameWon);
                }
            }
            else
            {
                gameWon = false;
            }
        }

        static void NotFriendlyMurderers(ref bool gameWon)
        {
            Console.Clear();
            System.Console.WriteLine("You approach the murderers and they begin to insult your outfit, clearly they are not friendly\n" +
            "You realize that there are only two options now to save your life" +
            "\nWhat do you do?\n\t1. Beg for your life\n\t2. Show them a really cool magic trick.\n\nType 'exit' to exit");
            string userInput = ValidateStoryOneTwoOrExit();
            int chance = GetRandomChance();
            if(userInput == "1")
            {
                if(chance < 10)
                {
                    BegSuccesful(ref gameWon);
                }
                else
                {
                    BegUnsuccesful(ref gameWon);
                }
            }
            else if(userInput == "2")
            {
                if(chance < 40)
                {
                    SuccesfulCardTrick(ref gameWon);
                }
                else
                {
                    UnsuccesfulCardTrick(ref gameWon);
                }
            }
            else
            {
                gameWon = false;
            }
        }

        static void BegSuccesful(ref bool gameWon)
        {
            Console.Clear();
            System.Console.WriteLine("They take pity on your begging and decide to drop you off at the nearest star port.\n" +
            "Congragulations, your cowardice has led you to victory!");
            gameWon = true;
            AskAnyKeyContinue();
        }
        
        static void BegUnsuccesful(ref bool gameWon)
        {
            Console.Clear();
            System.Console.WriteLine("They take no pity on you and choose to end your life.\nYou have lost the game.");
            gameWon = false;
            AskAnyKeyContinue();
        }

        static void UnsuccesfulCardTrick(ref bool gameWon)
        {
            Console.Clear();
            System.Console.WriteLine("The card trick you decide to show them is one they have seen many times before.\nThey are not impressed and choose to end your life for failing to entertain them." +
            "\nYou have lost.");
            gameWon = false;
            AskAnyKeyContinue();
        }

        static void SuccesfulCardTrick(ref bool gameWon)
        {
            Console.Clear();
            System.Console.WriteLine("They are thoroughly impressed by your ability to trick them with cards.\nThey offer you a position on their crew." +
            "You decide to take it and end up working for them for 35 years, until you can draw your pension, and retire.\nYou live a long life this way.\nCongragulations, you win!");
            gameWon = true;
            AskAnyKeyContinue();
        }
        static void FriendlyMurderers(ref bool gameWon)
        {
            Console.Clear();
            System.Console.WriteLine("You draw near to the group and they appear to be quite friendly!" +
            "\nThey offer you to come inside and ask you if you would rather have a cup of black or green tea." +
            "\nWhich one do you take?\n\t1. Take the black tea\n\t2. Take the green tea\n\nType 'exit' to exit.");
            string userInput = ValidateStoryOneTwoOrExit();
            int chance = GetRandomChance();
            if(userInput == "1")
            {
                if(chance < 70)
                {
                    SuccesfulBlackTea(ref gameWon);
                }
                else
                {
                    UnsuccesfulBlackTea(ref gameWon);
                }
            }
            else if(userInput == "2")
            {
                if(chance < 35)
                {
                    SuccesfulGreenTea(ref gameWon);
                }
                else
                {
                    UnsuccesfulGreenTea(ref gameWon);
                }
            }
            else
            {
                gameWon = false;
            }
        }

        static void SuccesfulBlackTea(ref bool gameWon)
        {
            Console.Clear();
            System.Console.WriteLine("You choose to take the black tea. This is a wise choice as it is well accepted among the crew." +
            "\nThey decide they will not kill you and will instead take you back to civilization.\nCongragulations, you win!");
            gameWon = true;
            AskAnyKeyContinue();
        }

        static void UnsuccesfulBlackTea(ref bool gameWon)
        {
            Console.Clear();
            System.Console.WriteLine("You choose to take the black tea.\nThis is a bad choice as Richard has just recently gottten food poisoning from a cup of black tea." +
            "He flies into a rage filled tirade enad ends up accidently killing you.\nYou have lost.");
            gameWon = false;
            AskAnyKeyContinue();
        }

        static void SuccesfulGreenTea(ref bool gameWon)
        {
            Console.Clear();
            System.Console.WriteLine("You choose to drink the green tea. This is a great choice!" +
            "\nEveryone loves green tea and talks about how great it is!\nThis discussion continures on late into the night." +
            "\nThey decide to be generous and take you back to your home.\nCongrats, you win!");
            gameWon = true;
            AskAnyKeyContinue();
        }
        
        static void UnsuccesfulGreenTea(ref bool gameWon)
        {
            Console.Clear();
            System.Console.WriteLine("You choose to drink the green tea. This is a bad choice as you are highly allergic." +
            "\nWhy you chose to drink something you knew you were allergic to is beyond all comprehension." +
            "\nYour throat closes and you slowly suffocate.\nYou have lost!");
            gameWon = false;
            AskAnyKeyContinue();
        }

        static void SuccesfulPrepareFight(ref bool gameWon)
        {
            Console.Clear();
            System.Console.WriteLine("You begin focusing intently and getting yourself in the right headspace." +
            "\nYou approach the murderers and unleash your skills on them. Not a single person is left standing." +
            "\nAfterward, you notice the shuttle that brought the murderers to the crashed ship." +
            "\nWhat do you do now?\n\t1. Steal the shuttle\n\t2. Radio for help\n\nType 'exit' to exit");
            string userInput = ValidateStoryOneTwoOrExit();
            int chance = GetRandomChance();
            if(userInput == "1")
            {
                if(chance < 25)
                {
                    SuccesfulDeparture(ref gameWon);
                }
                else
                {
                    UnsuccesfulDeparture(ref gameWon);
                }
            }
            else if(userInput == "2")
            {
                if(chance < 60)
                {
                    SuccesfulSignalHelp(ref gameWon);
                }
                else
                {
                    UnsuccesfullySignalHelp(ref gameWon);
                }
            }
            else
            {
                gameWon = false;
            }
        }

        static void SuccesfulDeparture(ref bool gameWon)
        {
            Console.Clear();
            System.Console.WriteLine("You power up the ship and succesfully escape from the planet." +
            "\nCongragulations, you win!");
            gameWon = true;
            AskAnyKeyContinue();
        }
        
        static void UnsuccesfulDeparture(ref bool gameWon)
        {
            Console.Clear();
            System.Console.WriteLine("You try to power up the ship but you unfortunately mistake the start button with the self-destruct button." +
            "\nYou blow yourself to smithereens.\nYou do not win!");
            gameWon = true;
            AskAnyKeyContinue();
        }
        
        static void SuccesfulSignalHelp(ref bool gameWon)
        {
            Console.Clear();
            System.Console.WriteLine("You begin fidgeting with the knob on the radio and you are able to get a signal out." +
            "\nYou send out a message asking for help. A few minutes later, a reply comes in asking for your location to send help." +
            "\nYou are rescued by the end of the night.\nCongragulations, you win!");
            gameWon = true;
            AskAnyKeyContinue();
        }

        static void UnsuccesfullySignalHelp(ref bool gameWon)
        {
            Console.Clear();
            System.Console.WriteLine("You get to the radio and with great confidence begin transmitting your location, along with a request for help." +
            "\nA group of space pirates pick up your call and show up to the crashed ship." +
            "\nThey kidnap you and sell you into slavery on a far-away colony planet\nYou lose!");
            gameWon = false;
            AskAnyKeyContinue();
        }

        static void UnsuccesfulPrepareFight(ref bool gameWon)
        {
            Console.Clear();
            System.Console.WriteLine("You try to get yourself in the right headspace to fight but you fail miserable." +
            "\nYou are instead focused on a really funny tic tok that you saw once." +
            "\nAs you go into battle, you make a fool of yourself and the murderers are able to mortally wound you." +
            "\nAs you crawl back to the rock, you lose consciousness and are unable to continue on." +
            "\nYou lose!");
            gameWon = false;
            AskAnyKeyContinue();
        }

        static void ObserveTraders(ref bool gameWon)
        {
            Console.Clear();
            System.Console.WriteLine("As you are watching from behind the rock, you see that it is a group of traders who are going through the wreckage of the ship" +
            "\nYou begin to walk toward them in hopes that they might be able to help you.");
            AskAnyKeyContinue();
            int chance = GetRandomChance();

            if(chance < 70)
            {
                HelpfulTraders(ref gameWon);
            }
            else
            {
                StartleTrader(ref gameWon);
            }
        }

        static void StartleTrader(ref bool gameWon)
        {
            Console.Clear();
            System.Console.WriteLine("As you get closer to the traders, you think that it would be a good idea to tru and scare one of them." +
            "\nYou are successful in your endeavor and the traders, in their fright, shoot you with their blasters, gravely wounding you." +
            "\nWhat do you do now?\n\t1. Allow them to tend to your wounds.\n\t2. Try and fix yourself up.\n\nType 'exit' to exit.");
            string userInput = ValidateStoryOneTwoOrExit();
            int chance = GetRandomChance();
            if(userInput == "1")
            {
                if(chance < 40)
                {
                    TradersSuccessfullyTend(ref gameWon);
                }
                else
                {
                    TradersUnsuccesfullyTend(ref gameWon);
                }
            }
            else if(userInput == "2")
            {
                if(chance < 65)
                {
                    SuccessfullyMendWounds(ref gameWon);
                }
                else
                {
                    UnsuccessfullyMendWounds(ref gameWon);
                }
            }
            else
            {
                gameWon = false;
            }
        }

        static void TradersSuccessfullyTend(ref bool gameWon)
        {
            Console.Clear();
            System.Console.WriteLine("The traders approach you quickly after realizing what happened." +
            "\nOne of them steps forward and claims to trained in handling blaster wounds." +
            "\nHe gets to wouk on you and is able to get you stabilized!\nThey load you up on their shuttle and get you back to civilization to heal." +
            "\nCongragulations, you win!");
            gameWon = true;
            AskAnyKeyContinue();
        }

        static void TradersUnsuccesfullyTend(ref bool gameWon)
        {
            Console.Clear();
            System.Console.WriteLine("After hearing the shots ring out, the traders soon gather around you as you lay there bleeding." +
            "\nNo one feels compelled to step forward and tend to your wounds, so they leave you be." +
            "\nYou eventually fall into unconsciousness due to a loss of blood." +
            "\nYou lose.");
            gameWon = false;
            AskAnyKeyContinue();
        }
        
        static void SuccessfullyMendWounds(ref bool gameWon)
        {
            Console.Clear();
            System.Console.WriteLine("You take a deep breath in and get to work on your wound." +
            "\nLuckily, ever since your grade school days, you have always carried around a first aid kit with you." +
            "\nYou are able to stop the bleeditn and stabilize yourself." +
            "\nIn their great remorse, the traders offer to tak you to the nearest settlement for further assistance." +
            "\nYou accept their care and are carted away." +
            "\nCongragulation, you win!");
            gameWon = true;
            AskAnyKeyContinue();
        }

        static void UnsuccessfullyMendWounds(ref bool gameWon)
        {
            Console.Clear();
            System.Console.WriteLine("As you lay there in the snow, you reach your hand toward your back pocket and notice that your first aid kit is no longer there." +
            "\nIt must have fallen out some time ago. The traders get cak to what they were doing and leave you be." +
            "\nYou eventually fall into a deep sleep and freeze to death." +
            "\nLooser!");
            gameWon = false;
            AskAnyKeyContinue();
        }

        static void HelpfulTraders(ref bool gameWon)
        {
            Console.Clear();
            System.Console.WriteLine("You approach the traders and greet them with a big smile." + 
            "\nThey welcome you in and ask how you ended up here. You tell them that you dont know, but you'd like to go home." +
            "\nThey offer to take you back home after they finished up salvaging the crashed ship." +
            "\nWhat do you do?\n\t1. Accept their offer\n\t2. Turn their offer down\n\nType 'exit' to exit.");
            string userInput = ValidateStoryOneTwoOrExit();
            int chance = GetRandomChance();
            if(userInput == "1")
            {
                if(chance < 73)
                {
                    TradersTakeYouHome(ref gameWon);
                }
                else
                {
                    TradersAttacked(ref gameWon);
                }
            }
            else if(userInput == "2")
            {
                FreezeToDeath(ref gameWon);
            }
            else
            {
                gameWon = false;
            }
        }

        static void TradersTakeYouHome(ref bool gameWon)
        {
            Console.Clear();
            System.Console.WriteLine("The traders finish up their looting as you wait patiently in the crew quarters on their ship." +
            "\nThey get the ship up and running and take you back to the nearest city to drop you off." +
            "\nCongragulaitons, you win!");
            gameWon = true;
            AskAnyKeyContinue();
        }

        static void TradersAttacked(ref bool gameWon)
        {
            Console.Clear();
            System.Console.WriteLine("You go into the crew quarters on their ship as they finish looting the ship." +
            "\n While you are inside, a squadron of Imperial Tie fighters rain down from the sky and conduct an airstrike on your location." +
            "\nthe traders' ship is blown to pieces, with you inside of it.\nYou lose!");
            gameWon = false;
            AskAnyKeyContinue();
        }

        static void FreezeToDeath(ref bool gameWon)
        {
            Console.Clear();
            System.Console.WriteLine("Due to your stubborn refusal to accept help, you watch as they load up their crew and depart." +
            "\nAs you think through the series of actions that brought you to this monment, you freeze to death.\nYou lose!");
            gameWon = false;
            AskAnyKeyContinue();
        }

        static void CalculateStoryCredits(ref int totalCredits, int userBet, bool storyOutcome)
        {
            Console.Clear();
            if(storyOutcome)        //If it is true that the story outcome resulted in a win
            {
                totalCredits += userBet;
                System.Console.WriteLine("Congragulations on your victory!" +
                $"\nYour credits will be increased by {userBet} up to a total of {totalCredits}.");
                AskAnyKeyContinue();
            }
            else
            {
                totalCredits -= userBet;
                System.Console.WriteLine("Sorry about your loss!" +
                $"\nYour credits will be decreased by {userBet} to a total of {totalCredits}.");
                AskAnyKeyContinue();
            }
        }
    }
}
