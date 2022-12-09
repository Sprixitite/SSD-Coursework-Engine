using System;

namespace Engine.Windowing {

    public class GameWindow : System.Windows.Forms.Form {

        public GameWindow() {
            buffered_graphics_context = new System.Drawing.BufferedGraphicsContext();
        }

        private System.Drawing.BufferedGraphicsContext buffered_graphics_context;

        internal void render() {

            System.Drawing.BufferedGraphics render_buffer = buffered_graphics_context.Allocate(this.CreateGraphics(), new System.Drawing.Rectangle(System.Drawing.Point.Empty, this.Size));

            

        }

    }

}