using System;

using Engine.Graphics;

namespace Engine.Windowing {

    public class GameWindow : System.Windows.Forms.Form {

        public GameWindow() {
            buffered_graphics_context = new System.Drawing.BufferedGraphicsContext();
        }

        private System.Drawing.BufferedGraphicsContext buffered_graphics_context;

        internal void render(Camera cam, Tri3D[] triangles) {

            cam.surface_dist = 100.0f;

            cam.rotation = new Matrix3x3(new Vector3(0.0f, cam.rotation.euler.y+0.0004f , 0.0f));
            System.Console.WriteLine(cam.rotation.euler.y);

            System.Drawing.BufferedGraphics render_buffer = buffered_graphics_context.Allocate(this.CreateGraphics(), this.DisplayRectangle);

            render_buffer.Graphics.Clear(System.Drawing.Color.Blue);

            foreach (Tri3D tri in triangles) {
                if (tri.project_onto(cam, out Tri2D projected)) {
                    render_buffer.Graphics.FillPolygon(new System.Drawing.SolidBrush(System.Drawing.Color.Green), projected.to_screenspace(new Vector2(this.Width, this.Height)));
                }
            }

            render_buffer.Render();
            render_buffer.Dispose();

            if (cam.rotation.euler.y > 1.5f || cam.rotation.euler.y < -1.5f) Console.ReadKey();
            //Console.ReadKey();

        }

        private void GameWindow_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs _e) {
            Engine.Core.TaskScheduler.should_exit = true;
            this.Close();
            //System.Windows.Forms.Application.Exit();
        }

    }

}