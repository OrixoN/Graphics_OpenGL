using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Task01;

var nativeWindowSettings = new NativeWindowSettings()
{
    ClientSize = (800, 600),
    Title = "Lab 01 - Kuzmenko D.O. - Variant 07",
    Profile = ContextProfile.Compatability,
    Flags = ContextFlags.Default
};

using (Game game = new Game(nativeWindowSettings))
{
    game.Run();
}