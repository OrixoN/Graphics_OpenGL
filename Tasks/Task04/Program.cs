using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;

namespace Task04
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Введіть координати лінії (наприклад: -150 100 150 100):");
            Console.Write("X1: "); float x1 = float.Parse(Console.ReadLine());
            Console.Write("Y1: "); float y1 = float.Parse(Console.ReadLine());
            Console.Write("X2: "); float x2 = float.Parse(Console.ReadLine());
            Console.Write("Y2: "); float y2 = float.Parse(Console.ReadLine());

            var settings = new NativeWindowSettings()
            {
                ClientSize = (800, 800),
                Title = "Lab 04 - Variant 7",
                Profile = ContextProfile.Compatability,
                Flags = ContextFlags.Default
            };

            using (var game = new Game(GameWindowSettings.Default, settings, x1, y1, x2, y2))
            {
                game.Run();
            }
        }
    }
}