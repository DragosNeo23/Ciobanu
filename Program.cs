//////////////////* Laborator 2 EGC, Exercitiul 2, Ciobanu Dragos-Alexandru *//////////

using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace ControlObiectCuInput
{
    class Game : GameWindow
    {
        private Vector2 objectPosition = Vector2.Zero;

        public Game(int width, int height) : base(width, height, GraphicsMode.Default, "Control Obiect cu Input") { }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color4.LightGray);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            // a) Controlarea obiectului folosind tastatura
            KeyboardState keyboard = Keyboard.GetState();
            float speed = 0.1f;
            if (keyboard.IsKeyDown(Key.W))
                objectPosition.Y += speed;
            if (keyboard.IsKeyDown(Key.S))
                objectPosition.Y -= speed;
            if (keyboard.IsKeyDown(Key.A))
                objectPosition.X -= speed;
            if (keyboard.IsKeyDown(Key.D))
                objectPosition.X += speed;

            // b) Controlarea obiectului folosind mouse-ul
            MouseState mouse = Mouse.GetState();
            objectPosition.X = (mouse.X - Width / 2) / (float)(Width / 2);
            objectPosition.Y = (Height / 2 - mouse.Y) / (float)(Height / 2);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Begin(PrimitiveType.Quads);
            GL.Color4(Color4.Black);
            GL.Vertex2(-0.1f + objectPosition.X, -0.1f + objectPosition.Y);
            GL.Vertex2(0.1f + objectPosition.X, -0.1f + objectPosition.Y);
            GL.Vertex2(0.1f + objectPosition.X, 0.1f + objectPosition.Y);
            GL.Vertex2(-0.1f + objectPosition.X, 0.1f + objectPosition.Y);
            GL.End();

            SwapBuffers();
        }

        static void Main(string[] args)
        {
            using (var game = new Game(800, 600))
            {
                game.Run(60.0);
            }
        }
    }
}
