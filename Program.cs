using System;
using System.Diagnostics;

namespace ConsoleTyper
{
    class Program
    {
        private static string _statementToType;
        private static int _currentIndex;
        private static int _totalErrors = 0;
        private static int _totalKeystrokes = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Enter your text : ");
            _statementToType = Console.ReadLine().ToString();

            while (StartGame() && char.ToUpperInvariant(Console.ReadKey().KeyChar) == 'R')
            {
                Console.Clear();
                StartGame();
            }
        }

        private static bool StartGame()
        {
            Console.WriteLine("Press \"Enter\" to begin typing.");
            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                Console.Clear();
                Console.WriteLine("Press \"Enter\" to begin typing.");
            }
            Console.Clear();

            PrintStatement(true);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (_currentIndex < _statementToType.Length)
            {
                PrintStatement(false);
            }
            sw.Stop();

            var totalWords = _statementToType.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
            var totalMinutes = sw.Elapsed.TotalMinutes;
            var wpm = (int)Math.Floor(totalWords / totalMinutes);
            var accuracyPercentage = ((_statementToType.Length - _totalErrors) / (double)_statementToType.Length) * 100;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Congratulations! You have completed the typing test.");
            Console.WriteLine($"Your average typing speed is : {wpm} words per minute");
            Console.WriteLine($"Your typing accuracy is : {accuracyPercentage.ToString("#.##")}%");

            _currentIndex = 0;
            _totalErrors = 0;
            _totalKeystrokes = 0;

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press R to restart the game. Press any other key to exit.");
            return true;
        }

        private static void PrintStatement(bool initial)
        {
            char input = '\0';
            if (!initial)
            {
                input = Console.ReadKey().KeyChar;
                _totalKeystrokes++;
            }

            if (initial || _statementToType[_currentIndex] == input)
            {
                if (!initial)
                    _currentIndex++;

                Console.Clear();

                // completed
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(_statementToType.Substring(0, _currentIndex));
                //}

                if (_currentIndex <= _statementToType.Length - 1)
                {
                    // current
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(char.IsWhiteSpace(_statementToType[_currentIndex]) ? '_' : _statementToType[_currentIndex]);

                    // remaining
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(_statementToType.Substring(_currentIndex + 1, _statementToType.Length - (_currentIndex + 1)));
                }

                Console.WriteLine();
            }
            else
            {
                _totalErrors++;
                Console.Beep(2500, 50);
                Console.Write('\b');
            }
        }
    }
}
