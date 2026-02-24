using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Task02;

NativeWindowSettings nativeWindowSettings = new()
{
    ClientSize = (800, 600),
    Title = "Lab 02 - Kuzmenko D.O. - Hexagon Tiling",
    Profile = ContextProfile.Compatability,
    Flags = ContextFlags.Default
};

using var game = new Game(nativeWindowSettings);
game.Run();