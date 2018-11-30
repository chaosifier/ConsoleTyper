using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTyper.Windows
{
    class Program
    {
        static void Main(string[] args)
        {
            var theTyper = new TheGame.ConsoleTyper();
            theTyper.EnterGame();
        }
    }
}
