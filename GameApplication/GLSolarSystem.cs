﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math_Implementation;
using OpenTK.Graphics.OpenGL;

namespace GameApplication {
    class GLSolarSystem : Game {
        Grid grid = null;
        float planet1RotSpeed = 15.0f;
        float planet2RotSpeed = 5.0f;
        float moon1RotSpeed = 3.0f;
        float moon2RotSpeed = 3.0f;

        public GLSolarSystem() {
            grid = new Grid();
            GL.Enable(EnableCap.DepthTest);
            Resize(MainGameWindow.Window.Width, MainGameWindow.Window.Height);
        }

        public override void Update(float dTime) {
            planet1RotSpeed += 75 * dTime;
            moon1RotSpeed += 100.0f * dTime;
            planet2RotSpeed += 50.0f * dTime;
            moon2RotSpeed += 75.0f * dTime;
        }
        public override void Render() {
            //set view matrix
            Matrix4 lookAt = Matrix4.LookAt(new Vector3(10.0f, 5.0f, 15.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f));
            GL.LoadMatrix(lookAt.OpenGL);
            //render scene
            grid.Render();
            DrawPlanets(-1.0f, 1.0f, 0.0f);
        }

        protected void DrawPlanets(float worldX, float worldY, float worldZ) {
            //Draw sun
            GL.Color3(1.0f, 1.0f, 0.0f);
            GL.PushMatrix();
            {
                GL.Scale(0.5f, 0.5f, 0.5f);
                GL.Translate(worldX, worldY, worldZ);
                Circle.DrawSphere(3);
                //planet1
                GL.PushMatrix();
                    {
                        GL.Color3(0.0f, 1.0f, 0.0f);
                        GL.Rotate(planet1RotSpeed, 0.0f, 1.0f, 1.0f);
                        GL.Translate(-4.0f, 0.0f, 0.0f);
                        GL.Scale(0.8f, 0.8f, 0.8f);
                        Circle.DrawSphere(1);
                    }
                    GL.PushMatrix();
                        {
                            //moon
                            GL.Color3(1.0f, 0.0f, 0.0f);
                            GL.Rotate(moon1RotSpeed, 1.0f, 1.0f, 1.0f);
                            GL.Translate(-2.0f, 0.5f, 0.0f);
                            GL.Scale(0.8f, 0.8f, 0.8f);
                            Circle.DrawSphere(1);
                        }
                    GL.PopMatrix();
                //planet2
                {
                    GL.Color3(0.0f, 0.0f, 1.0f);
                    GL.Rotate(planet2RotSpeed, 1.0f, 0.0f, 1.0f);
                    GL.Translate(3.0f, 0.0f, 0.0f);
                    GL.Scale(0.5f, 0.5f, 0.5f);
                    Circle.DrawSphere(1);
                }

            }
            GL.PopMatrix();
        }
        public override void Resize(int width, int height) {
            GL.Viewport(0, 0, MainGameWindow.Window.Width, MainGameWindow.Window.Height);
            //set projection matrix
            GL.MatrixMode(MatrixMode.Projection);
            float aspect = (float)MainGameWindow.Window.Width / (float)MainGameWindow.Window.Height;
            Matrix4 perspective = Matrix4.Perspective(60, aspect, 0.01f, 1000.0f);
            GL.LoadMatrix(Matrix4.Transpose(perspective).Matrix);
            //switch to view matrix
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }
    }
}
