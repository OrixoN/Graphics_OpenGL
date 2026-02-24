using System;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Task03
{
    public class Program
    {
        public static void Main()
        {
            double xMin, xMax;

            while (true)
            {
                Console.WriteLine("=== Налаштування графіка ===");
                try
                {
                    Console.Write("Введіть X min: ");
                    xMin = double.Parse(Console.ReadLine());

                    Console.Write("Введіть X max: ");
                    xMax = double.Parse(Console.ReadLine());

                    if (xMin < xMax) break;
                    else Console.WriteLine("Помилка: Xmin має бути меншим за Xmax!");
                }
                catch
                {
                    Console.WriteLine("Помилка: введіть коректні числа.");
                }
            }

            var nativeWindowSettings = new NativeWindowSettings()
            {
                ClientSize = (800, 600),
                Title = $"Lab 03 - Kuzmenko D.O. - [{xMin} ; {xMax}]",
                Profile = ContextProfile.Compatability,
                Flags = ContextFlags.Default
            };

            using (var game = new Game(nativeWindowSettings, xMin, xMax))
            {
                game.Run();
            }
        }
    }
}