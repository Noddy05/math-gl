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
            Console.WriteLine("Hello World!");

            using (window = new Window())
            {
                window.Run();
            }
        }

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
    }
}
