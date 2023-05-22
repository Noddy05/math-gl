using System;
using System.Runtime.CompilerServices;

namespace MathGL
{
    class Program
    {
        private static Window window;
        public static Window GetWindow() => window;

        static void Main(string[] args)
        {
            Print("Launching ");
            Print("MathGL", ConsoleColor.Red);
            PrintLine(" by Noah D. Dirksen");

            ApproximateZeta(0.5f);

            using (window = new Window())
            {
                window.Run();
            }
        }

        static void ApproximateZeta(float power)
        {
            float a = 0;
            float sum = 0;
            for(int n = 0; n < 100000; n++)
            {
                if (n + a == 0)
                    continue;
                sum += MathF.Pow(n + a, -power);
            }
            Console.WriteLine(sum);
        }

        //For printing an error to the console without stopping debugging.
        public static void ThrowError(string details, [CallerMemberName] string callingMember = null,
            [CallerFilePath] string callingFile = null, [CallerLineNumber] int callingLine = 0)
        {
            ConsoleColor foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("(!) Error (!)");
            Console.WriteLine($"Error from {callingFile}.{callingMember}() at line {callingLine}");
            Console.WriteLine(details);
            Console.WriteLine("(!) Error (!)");
            Console.ForegroundColor = foregroundColor;
        }

        //For printing an error to the console without stopping debugging.
        public static void Print(string details, ConsoleColor foregroundColor = ConsoleColor.White,
            ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            ConsoleColor foregroundColorPrevious = Console.ForegroundColor;
            ConsoleColor backgroundColorPrevious = Console.BackgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(details);
            Console.ForegroundColor = foregroundColorPrevious;
            Console.BackgroundColor = backgroundColorPrevious;
        }
        public static void PrintLine(string details, ConsoleColor foregroundColor = ConsoleColor.White,
           ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            ConsoleColor foregroundColorPrevious = Console.ForegroundColor;
            ConsoleColor backgroundColorPrevious = Console.BackgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(details);
            Console.ForegroundColor = foregroundColorPrevious;
            Console.BackgroundColor = backgroundColorPrevious;
        }
    }
}
