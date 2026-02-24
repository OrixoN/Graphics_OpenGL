using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using System;

namespace Task01
{
    public class Game : GameWindow
    {
        public Game(NativeWindowSettings settings)
            : base(GameWindowSettings.Default, settings) { }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            if (KeyboardState.IsKeyDown(Keys.Escape)) Close();
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.ClearColor(Color4.White);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 4.0, -1.0, 6.0, -1.0, 1.0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Color3(0.8f, 0.8f, 0.8f);
            GL.Begin(PrimitiveType.Lines);
            for (int i = -1; i <= 4; i++) { GL.Vertex2(i, -1); GL.Vertex2(i, 6); }
            for (int i = -1; i <= 6; i++) { GL.Vertex2(-1, i); GL.Vertex2(4, i); }
            GL.End();

            Vector2[] points = new Vector2[]
            {
        new Vector2(1, 1),
        new Vector2(2, 2),
        new Vector2(3, 4),
        new Vector2(2, 5),
        new Vector2(1, 5),
        new Vector2(0, 4) 
            };

            GL.LineWidth(4.0f);
            GL.Color3((System.Drawing.Color)Color4.Blue);
            GL.Begin(PrimitiveType.LineLoop);
            foreach (var p in points)
            {
                GL.Vertex2(p);
            }
            GL.End();

            GL.Enable(EnableCap.PointSmooth);
            GL.PointSize(15.0f);
            GL.Color3((System.Drawing.Color)Color4.Red);
            GL.Begin(PrimitiveType.Points);
            foreach (var p in points)
            {
                GL.Vertex2(p);
            }
            GL.End();
            GL.Disable(EnableCap.PointSmooth);

            SwapBuffers();
        }
    }
}