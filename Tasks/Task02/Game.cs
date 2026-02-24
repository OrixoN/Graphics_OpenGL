using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using System;

namespace Task02
{
    public class Game : GameWindow
    {
        private int _horizontalCount = 1;
        private int _verticalCount = 1;
        private PolygonMode _mode = PolygonMode.Fill;

        public Game(NativeWindowSettings settings)
            : base(GameWindowSettings.Default, settings) { }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            var input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape)) Close();

            if (input.IsKeyPressed(Keys.Right)) _horizontalCount++;
            if (input.IsKeyPressed(Keys.Left) && _horizontalCount > 1) _horizontalCount--;
            if (input.IsKeyPressed(Keys.Up)) _verticalCount++;
            if (input.IsKeyPressed(Keys.Down) && _verticalCount > 1) _verticalCount--;

            if (input.IsKeyPressed(Keys.D1)) _mode = PolygonMode.Fill;
            if (input.IsKeyPressed(Keys.D2)) _mode = PolygonMode.Line;
            if (input.IsKeyPressed(Keys.D3)) _mode = PolygonMode.Point;
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);

            double aspect = (double)e.Width / e.Height;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            double range = 300;
            if (aspect >= 1)
                GL.Ortho(-range * aspect, range * aspect, -range, range, -1.0, 1.0);
            else
                GL.Ortho(-range, range, -range / aspect, range / aspect, -1.0, 1.0);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.ClearColor(Color4.White);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            float a = 50f;
            float h = a * (float)Math.Sqrt(3) / 2f;
            float tilt = 20f;

            for (int i = 0; i < _horizontalCount; i++)
            {
                for (int j = 0; j < _verticalCount; j++)
                {
                    GL.PushMatrix();

                    float stepX = a + h + tilt;
                    float stepY = a * 2;
                    GL.Translate(i * stepX - 200, j * stepY - 200, 0);

                    GL.PolygonMode(MaterialFace.FrontAndBack, _mode);

                    GL.Color4(Color4.Red);
                    GL.Begin(PrimitiveType.Triangles);
                    GL.Vertex2(-h, -a / 2);
                    GL.Vertex2(0, -a);
                    GL.Vertex2(0, 0);
                    GL.End();

                    GL.Color4(Color4.Yellow);
                    GL.Begin(PrimitiveType.Triangles);
                    GL.Vertex2(-h, a / 2);
                    GL.Vertex2(0, 0);
                    GL.Vertex2(0, a);
                    GL.End();

                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
                    GL.Color4(Color4.White);
                    GL.Begin(PrimitiveType.Triangles);
                    GL.Vertex2(-h, -a / 2);
                    GL.Vertex2(-h, a / 2);
                    GL.Vertex2(0, 0);
                    GL.End();

                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                    GL.Color4(Color4.Black);
                    GL.Begin(PrimitiveType.Triangles);
                    GL.Vertex2(-h, -a / 2); GL.Vertex2(-h, a / 2); GL.Vertex2(0, 0);
                    GL.End();

                    GL.PolygonMode(MaterialFace.FrontAndBack, _mode);

                    GL.Color4(Color4.Blue);
                    GL.Begin(PrimitiveType.Quads);
                    GL.Vertex2(0, -a);
                    GL.Vertex2(a, -a);
                    GL.Vertex2(a + tilt, 0);
                    GL.Vertex2(tilt, 0);
                    GL.End();

                    GL.Color4(Color4.Green);
                    GL.Begin(PrimitiveType.Quads);
                    GL.Vertex2(tilt, 0);
                    GL.Vertex2(a + tilt, 0);
                    GL.Vertex2(a + tilt * 2, a);
                    GL.Vertex2(tilt * 2, a);
                    GL.End();

                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                    GL.Color4(Color4.Black);
                    GL.Begin(PrimitiveType.LineLoop);
                    GL.Vertex2(-h, -a / 2);
                    GL.Vertex2(-h, a / 2);
                    GL.Vertex2(tilt * 2, a);
                    GL.Vertex2(a + tilt * 2, a);
                    GL.Vertex2(a, -a);
                    GL.Vertex2(0, -a);
                    GL.End();

                    GL.PopMatrix();
                }
            }
            SwapBuffers();
        }
    }
}