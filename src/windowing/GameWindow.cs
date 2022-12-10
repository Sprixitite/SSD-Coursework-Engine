using System;

using Engine.Graphics;

namespace Engine.Windowing {

    public class GameWindow : System.Windows.Forms.Form {

        public GameWindow() {
            buffered_graphics_context = new System.Drawing.BufferedGraphicsContext();
        }

        private System.Drawing.BufferedGraphicsContext buffered_graphics_context;

        internal void render(Camera cam, Tri3D[] triangles) {

            System.Drawing.BufferedGraphics render_buffer = buffered_graphics_context.Allocate(this.CreateGraphics(), this.DisplayRectangle);

            foreach (Tri3D tri in triangles) {
                render_buffer.Graphics.FillPolygon(new System.Drawing.SolidBrush(Color.Green), tri.project_onto(cam).to_screenspace(new Vector2(this.Width, this.Height)));
            }

            render_buffer.Render();

        }

    }

}