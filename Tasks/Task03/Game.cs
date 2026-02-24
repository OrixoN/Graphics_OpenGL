using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using System;

namespace Task03
{
    public class Game : GameWindow
    {
        private readonly double _xMin;
        private readonly double _xMax;
        private double _yMin, _yMax;
        private int _pointsCount = 1000;

        public Game(NativeWindowSettings settings, double xMin, double xMax)
            : base(GameWindowSettings.Default, settings)
        {
            _xMin = xMin;
            _xMax = xMax;
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            CalculateYLimits();
        }

        private double F(double x)
        {
            double cos4x = Math.Cos(4 * x);
            double cos3x = Math.Cos(3 * x);
            return Math.Sin(x + 1) / (cos4x * cos4x + Math.Pow(cos3x, 3) + 2);
        }

        private void CalculateYLimits()
        {
            _yMin = double.MaxValue;
            _yMax = double.MinValue;

            double step = (_xMax - _xMin) / _pointsCount;
            for (double x = _xMin; x <= _xMax; x += step)
            {
                double y = F(x);
                if (y < _yMin) _yMin = y;
                if (y > _yMax) _yMax = y;
            }

            double margin = (_yMax - _yMin) * 0.1;
            if (margin == 0) margin = 1.0;
            _yMin -= margin;
            _yMax += margin;
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(_xMin, _xMax, _yMin, _yMax, -1.0, 1.0);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.ClearColor(Color4.White);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            DrawGrid();
            DrawGraph();
            DrawRoots();

            SwapBuffers();
        }

        private void DrawGrid()
        {
            GL.LineWidth(2.0f);
            GL.Color4(Color4.Black);
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex2(_xMin, 0); GL.Vertex2(_xMax, 0);
            GL.Vertex2(0, _yMin); GL.Vertex2(0, _yMax);
            GL.End();
            GL.Enable(EnableCap.LineStipple);
            GL.LineStipple(1, 0xAAAA);
            GL.Color4(Color4.LightGray);
            GL.Begin(PrimitiveType.Lines);

            double xStep = Math.Max((_xMax - _xMin) / 10.0, 0.1);
            for (double x = _xMin; x <= _xMax; x += xStep)
            {
                GL.Vertex2(x, _yMin); GL.Vertex2(x, _yMax);
            }
            double yStep = Math.Max((_yMax - _yMin) / 10.0, 0.1);
            for (double y = _yMin; y <= _yMax; y += yStep)
            {
                GL.Vertex2(_xMin, y); GL.Vertex2(_xMax, y);
            }
            GL.End();
            GL.Disable(EnableCap.LineStipple);
        }

        private void DrawGraph()
        {
            GL.Color4(Color4.Blue);
            GL.LineWidth(2.5f);
            GL.Begin(PrimitiveType.LineStrip);
            double step = (_xMax - _xMin) / _pointsCount;
            for (double x = _xMin; x <= _xMax; x += step)
            {
                GL.Vertex2(x, F(x));
            }
            GL.End();
        }

        private void DrawRoots()
        {
            GL.PointSize(10.0f);
            GL.Enable(EnableCap.PointSmooth);
            GL.Color4(Color4.Red);
            GL.Begin(PrimitiveType.Points);

            double step = (_xMax - _xMin) / _pointsCount;
            for (double x = _xMin; x <= _xMax - step; x += step)
            {
                if (Math.Sign(F(x)) != Math.Sign(F(x + step)))
                {
                    GL.Vertex2(x, 0);
                }
            }
            GL.End();
        }
    }
}