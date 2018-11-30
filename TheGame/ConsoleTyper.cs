using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TheGame
{
    public class ConsoleTyper
    {
        private string _statementToType;
        private int _currentIndex;
        private int _totalErrors = 0;
        private int _totalKeystrokes = 0;
        private SentenceGenerator _sentenceGenerator;
        private const ConsoleColor _completedColor = ConsoleColor.DarkGreen;
        private const ConsoleColor _currentColor = ConsoleColor.White;
        private const ConsoleColor _remainingColor = ConsoleColor.Green;
        private const int _randomSentenceMinWords = 50;

        public ConsoleTyper()
        {
            _sentenceGenerator = new SentenceGenerator();
        }

        public void EnterGame()
        {
            Console.WriteLine("WELCOME TO CONSOLE-TYPER");
            Console.WriteLine(new string('#', 25));
            Console.WriteLine();

            char enteredChar = '\0';

            InitializeGame();

            do
            {
                enteredChar = char.ToUpperInvariant(Console.ReadKey().KeyChar);

                Console.Clear();
                if (enteredChar == 'P')
                {
                    BeginTyping();
                }
                else if (enteredChar == 'R')
                {
                    InitializeGame();
                }
            } while (enteredChar == 'P' || enteredChar == 'R');
        }

        private void InitializeGame()
        {
            Console.WriteLine("Press R to play with random sentence. Press C to enter your custom text.");

            char enteredChar = '\0';
            do
            {
                enteredChar = char.ToUpperInvariant(Console.ReadKey().KeyChar);
            }
            while (enteredChar != 'R' && enteredChar != 'C');

            if (enteredChar == 'R')
            {
                _statementToType = _sentenceGenerator.GetRandomStatement(_randomSentenceMinWords);
            }
            else
            {
                Console.Clear();
                Console.Write("Enter your custom text : ");
                _statementToType = Console.ReadLine();
            }

            var sb = new StringBuilder();
            sb.Append(_statementToType);
            while (Console.KeyAvailable)
            {
                var nextLine = Console.ReadLine();
                sb.Append(string.IsNullOrWhiteSpace(nextLine) ? string.Empty : " " + nextLine);
            }

            _statementToType = sb.ToString();

            Console.Clear();
            BeginTyping();
        }

        private void BeginTyping()
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
            Console.WriteLine("Press P to replay. Press R to reset the game. Press any other key to exit.");
        }

        private void PrintStatement(bool initial)
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
                Console.ForegroundColor = _completedColor;
                Console.Write(_statementToType.Substring(0, _currentIndex));
                //}

                if (_currentIndex <= _statementToType.Length - 1)
                {
                    // current
                    Console.ForegroundColor = _currentColor;
                    Console.Write(char.IsWhiteSpace(_statementToType[_currentIndex]) ? '_' : _statementToType[_currentIndex]);

                    // remaining
                    Console.ForegroundColor = _remainingColor;
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
