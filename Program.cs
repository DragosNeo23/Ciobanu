/////////////////////////* Laborator 3 - Ciobanu Dragos-Alexandru, grupa 3132A *////////////


/*using System;
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
}*/

/////////////////////////* Laborator 4 - Ciobanu Dragos-Alexandru, grupa 3132A *////////////


using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace ColorChangeApp
{
    public class Program : GameWindow
    {
        private float minColorValue = 0.0f;
        private float maxColorValue = 1.0f;
        private float colorChangeSpeed = 0.1f;

        private Mesh cubeMesh;

        public Program()
            : base(800, 600, GraphicsMode.Default, "Color Change App", GameWindowFlags.Default, DisplayDevice.Default, 3, 3, GraphicsContextFlags.ForwardCompatible)
        {
            VSync = VSyncMode.On;
        }

        protected override void OnLoad(EventArgs e)
        {
            cubeMesh = new Mesh(); // Creare obiect Mesh

            GL.ClearColor(new Color4(0.0f, 0.0f, 0.0f, 1.0f)); // Setare culoare de fundal la negru
            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Key.W))
            {
                // Exemplu: Incrementarea valorii canalului R
                Color4 currentColor = cubeMesh.Color;
                float newRValue = MathHelper.Clamp(currentColor.R + colorChangeSpeed, minColorValue, maxColorValue);
                cubeMesh.Color = new Color4(newRValue, currentColor.G, currentColor.B, currentColor.A);
            }

            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            cubeMesh.Render();

            SwapBuffers();
        }

        static void Main()
        {
            using (Program program = new Program())
            {
                program.Run();
            }
        }
    }

    public class Mesh
    {
        private int vertexBuffer;
        private int indexBuffer;

        public Color4 Color { get; set; } = Color4.White; // Culorea implicita este alb

        public Mesh()
        {
            List<Vector3> vertices = new List<Vector3>
            {
                new Vector3(-1.0f, -1.0f, 0.0f),
                new Vector3(1.0f, -1.0f, 0.0f),
                new Vector3(1.0f, 1.0f, 0.0f),
                new Vector3(-1.0f, 1.0f, 0.0f)
            };

            List<int> indices = new List<int>
            {
                0, 1, 2,
                0, 2, 3
            };

            vertexBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Count * Vector3.SizeInBytes, vertices.ToArray(), BufferUsageHint.StaticDraw);

            indexBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexBuffer);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Count * sizeof(int), indices.ToArray(), BufferUsageHint.StaticDraw);
        }

        public void Render()
        {
            GL.Color4(Color);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.VertexPointer(3, VertexPointerType.Float, Vector3.SizeInBytes, IntPtr.Zero);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexBuffer);
            GL.DrawElements(BeginMode.Triangles, 6, DrawElementsType.UnsignedInt, 0);

            GL.DisableClientState(ArrayCap.VertexArray);
        }
    }
}

