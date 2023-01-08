using FindPrimeNumbers;
using System.Diagnostics;

string? userEnteredString = string.Empty;
int enteredIntegerNumber = 0;
bool isInteger = false;
bool isEnteredIntegerAboveTwo = false;

while (string.IsNullOrWhiteSpace(userEnteredString) || !isInteger || !isEnteredIntegerAboveTwo)
{
    Console.Write("Please, enter any integer above 2 up to render the prime numbers from 0 to your entered number:");
    userEnteredString = Console.ReadLine();
    isInteger = int.TryParse(userEnteredString, out enteredIntegerNumber);

    if (isInteger)
    {
        isEnteredIntegerAboveTwo = enteredIntegerNumber > 2;

        if (!isEnteredIntegerAboveTwo)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"You've entered: {enteredIntegerNumber} number, which is less or equal 2, please try again (!)");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine($"You've entered: {userEnteredString} , which is not a integer number, please try again (!)");
        Console.ForegroundColor = ConsoleColor.White;
    }
}

var limit = enteredIntegerNumber;
var numbers = Enumerable.Range(0, limit).ToList();

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Find below please your results - found prime numbers from 0 to {enteredIntegerNumber}:");
Console.ForegroundColor = ConsoleColor.Green;

//Threads way of the implementation execution
var watchForThreads = Stopwatch.StartNew();
var primeNumbersFromThreads = PrimeNumbersHelper.GetPrimeListUsingThreads(0, limit);
watchForThreads.Stop();
Console.WriteLine($"Prime numbers returned by the 'Threads' way of the implementation execution: {string.Join(", ", primeNumbersFromThreads.Order())}");
Console.WriteLine($"Time spent for the 'Threads' way of the implementation execution: {watchForThreads.ElapsedMilliseconds} ms");

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("------------------------------------------------------------------------");
Console.ForegroundColor = ConsoleColor.Green;

//Task way of the implementation execution
var watchForTask = Stopwatch.StartNew();
var primeNumbersFromTask = PrimeNumbersHelper.GetPrimeListAsync(numbers);
watchForTask.Stop();
Console.WriteLine($"Prime numbers returned by the 'Task' way of the implementation execution: {string.Join(", ", (await primeNumbersFromTask).Order())}");
Console.WriteLine($"Time spent for the 'Task' way of the implementation execution: {watchForTask.ElapsedMilliseconds} ms");

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("------------------------------------------------------------------------");
Console.ForegroundColor = ConsoleColor.Green;

//Parallel.ForEach way of the implementation execution
var watchForParallel = Stopwatch.StartNew();
var primeNumbersFromParallelForeach = PrimeNumbersHelper.GetPrimeListWithParallel(numbers);
watchForParallel.Stop();
Console.WriteLine($"Prime numbers returned by the 'Parallel.ForEach' way of the implementation execution: {string.Join(", ", primeNumbersFromParallelForeach.Order())}");
Console.WriteLine($"Time spent for the 'Parallel.ForEach' way of the implementation execution: {watchForParallel.ElapsedMilliseconds} ms");

Console.WriteLine(String.Empty);
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("Press any key to exit...");
Console.ReadLine();
