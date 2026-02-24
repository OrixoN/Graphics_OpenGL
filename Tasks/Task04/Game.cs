using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;

namespace Task04
{
    public class Game : GameWindow
    {
        private float _x1, _y1, _x2, _y2;
        private List<Vector2> _intersections = new List<Vector2>();

        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings, float x1, float y1, float x2, float y2)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            _x1 = x1; _y1 = y1; _x2 = x2; _y2 = y2;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.ClearColor(0.9f, 0.9f, 0.9f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-200, 200, -200, 200, -1, 1);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            DrawAxes();
            DrawEllipse(120, 60);
            DrawParabola(0.01f);
            DrawLine();
            DrawPoints();

            Context.SwapBuffers();
        }

        private void DrawAxes()
        {
            GL.Color3((System.Drawing.Color)Color4.Gray);
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex2(-200, 0); GL.Vertex2(200, 0);
            GL.Vertex2(0, -200); GL.Vertex2(0, 200);
            GL.End();
        }

        private void DrawEllipse(float a, float b)
        {
            GL.Color3((System.Drawing.Color)Color4.Blue);
            GL.LineWidth(2);
            GL.Begin(PrimitiveType.LineStrip);
            for (float x = -a; x <= a; x += 1f)
            {
                float y = b * (float)Math.Sqrt(Math.Max(0, 1 - (x * x) / (a * a)));
                GL.Vertex2(x, y);
            }
            GL.End();
            GL.Begin(PrimitiveType.LineStrip);
            for (float x = a; x >= -a; x -= 1f)
            {
                float y = -b * (float)Math.Sqrt(Math.Max(0, 1 - (x * x) / (a * a)));
                GL.Vertex2(x, y);
            }
            GL.End();
        }

        private void DrawParabola(float k)
        {
            GL.Color3((System.Drawing.Color)Color4.Green);
            GL.LineWidth(2);
            GL.Begin(PrimitiveType.LineStrip);
            _intersections.Clear();
            Vector2 p1 = new Vector2(-200, k * 40000);
            for (float x = -200; x <= 200; x += 2f)
            {
                float y = k * x * x;
                Vector2 p2 = new Vector2(x, y);
                GL.Vertex2(x, y);
                CheckInt(p1, p2);
                p1 = p2;
            }
            GL.End();
        }

        private void DrawLine()
        {
            GL.Color3((System.Drawing.Color)Color4.Black);
            GL.LineWidth(3);
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex2(_x1, _y1); GL.Vertex2(_x2, _y2);
            GL.End();
        }

        private void DrawPoints()
        {
            GL.PointSize(12);
            GL.Color3((System.Drawing.Color)Color4.Red);
            GL.Begin(PrimitiveType.Points);
            foreach (var p in _intersections) GL.Vertex2(p.X, p.Y);
            GL.End();
        }

        private void CheckInt(Vector2 a, Vector2 b)
        {
            float x1 = a.X, y1 = a.Y, x2 = b.X, y2 = b.Y;
            float x3 = _x1, y3 = _y1, x4 = _x2, y4 = _y2;
            float den = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
            if (Math.Abs(den) < 0.0001f) return;
            float t = ((x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4)) / den;
            float u = -((x1 - x2) * (y1 - y3) - (y1 - y2) * (x1 - x3)) / den;
            if (t >= 0 && t <= 1 && u >= 0 && u <= 1)
                _intersections.Add(new Vector2(x1 + t * (x2 - x1), y1 + t * (y2 - y1)));
        }
    }
}