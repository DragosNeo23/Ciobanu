/////////////////////////* Laborator 3 - Ciobanu Dragos-Alexandru, grupa 3132A *////////////


using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.IO;

namespace ControlObiectCuInput
{
    class Game : GameWindow
    {
        private Vector2 objectPosition = Vector2.Zero;
        private float rotationAngle = 0.0f;
        private Color4 objectColor = Color4.White;

        public Game(int width, int height) : base(width, height, GraphicsMode.Default, "Control Obiect cu Input") { }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color4.LightGray);

            // Încarcă coordonatele obiectului dintr-un fișier text
            if (File.Exists("object_coordinates.txt"))
            {
                string[] lines = File.ReadAllLines("object_coordinates.txt");
                if (lines.Length == 2)
                {
                    float x = float.Parse(lines[0]);
                    float y = float.Parse(lines[1]);
                    objectPosition = new Vector2(x, y);
                }
            }
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            // Controlul unghiului camerei folosind mouse-ul
            MouseState mouse = Mouse.GetState();
            rotationAngle = (mouse.X - Width / 2) / (float)Width;

            // Controlarea culorii obiectului folosind tastele
            KeyboardState keyboard = Keyboard.GetState();
            objectColor.R = keyboard[Key.R] ? 1.0f : 0.0f; // Roșu
            objectColor.G = keyboard[Key.G] ? 1.0f : 0.0f; // Verde
            objectColor.B = keyboard[Key.B] ? 1.0f : 0.0f; // Albastru
            objectColor.A = keyboard[Key.T] ? 1.0f : 0.0f; // Transparență
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            // Aplică rotația
            GL.Rotate(rotationAngle * 360.0f, 0, 0, 1);

            // Desenează triunghiul folosind culoarea setată
            GL.Begin(PrimitiveType.Triangles);
            GL.Color4(objectColor);
            GL.Vertex2(-0.1f, -0.1f);
            GL.Vertex2(0.1f, -0.1f);
            GL.Vertex2(0.0f, 0.1f);
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
