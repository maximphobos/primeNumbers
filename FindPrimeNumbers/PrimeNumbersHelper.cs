using System.Collections.Concurrent;

namespace FindPrimeNumbers
{
    public static class PrimeNumbersHelper
    {
        public static IList<int> GetPrimeListUsingThreads(int start, int end)
        {
            var primeNumbers = new List<int>();

            var range = end - start;
            var numberOfThreads = Environment.ProcessorCount;

            var threads = new Thread[numberOfThreads];
            var chunkSize = range / numberOfThreads;

            var lockObject = new object();

            for (int i = 0; i < numberOfThreads; i++)
            {
                var chunkStart = start + i * chunkSize;
                var chunkEnd = i == (numberOfThreads - 1) ? end : chunkStart + chunkSize;

                threads[i] = new Thread(() =>
                {
                    for (var number = chunkStart; number < chunkEnd; ++number)
                    {
                        if (IsPrime(number))
                        {
                            lock (lockObject)
                            {
                                primeNumbers.Add(number);
                            }
                        }
                    }
                });

                threads[i].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            return primeNumbers;
        }

        public static async Task<IList<int>> GetPrimeListAsync(IList<int> numbers)
        {
            return await Task.Run(() => numbers.Where(IsPrime).ToList());
        }

        public static IList<int> GetPrimeListWithParallel(IList<int> numbers)
        {
            var primeNumbers = new ConcurrentBag<int>();

            Parallel.ForEach(numbers, number =>
            {
                if (IsPrime(number))
                {
                    primeNumbers.Add(number);
                }
            });

            return primeNumbers.ToList();
        }

        private static bool IsPrime(int number)
        {
            if (number < 2)
            {
                return false;
            }

            for (var divisor = 2; divisor <= Math.Sqrt(number); divisor++)
            {
                if (number % divisor == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
