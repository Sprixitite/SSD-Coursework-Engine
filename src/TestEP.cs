using System;

using System.Drawing;

namespace Engine {

    public class TestEntryPoint {

        class my_window : System.Windows.Forms.Form {

            public my_window() {
                //System.Windows.Forms.Application.Idle += new EventHandler(Frame);
                this.DoubleBuffered = true;
                // g = this.CreateGraphics();
                // bgc = System.Windows.Forms.BufferedGraphicsContext.Current;
                // OnResize(EventArgs.Empty);
            }

            // System.Drawing.Graphics g;
            // System.Drawing.BufferedGraphicsContext bgc;
            // System.Drawing.BufferedGraphics dbg;

            // protected override void OnResize(EventArgs e) {
            //     bgc.Invalidate();
            //     dbg = bgc.Allocate(g, new Rectangle(Math.Ceiling(this.Width/2.0f), Math.Ceiling(this.Height/2.0f), this.Width+1, this.Height+1));
            // }



            protected override void OnPaint(System.Windows.Forms.PaintEventArgs e/*object _s, EventArgs _e*/) {
                Console.WriteLine("Frame!2");
                this.CreateGraphics().Clear(Color.Blue);
                Engine.Graphics.Camera cam = new Engine.Graphics.Camera();
                cam.position = new Engine.Graphics.Vector3(0.0f, 0.0f, 0.0f);
                cam.rotation = new Engine.Graphics.Matrix3x3(new Engine.Graphics.Vector3(0.0f, 0.0f, 0.0f));
                cam.surface_dist = 1000.0f;//= DateTime.Now.Millisecond*0.005f;
                Engine.Graphics.Tri3D tri = new Engine.Graphics.Tri3D(new Engine.Graphics.Vector3(0.5f, 0.5f, 10.0f), new Engine.Graphics.Vector3(-0.5f, 0.5f, 15.0f), new Engine.Graphics.Vector3(-0.5f, -0.5f, 10.0f));
                Console.WriteLine(tri.p1);
                Console.WriteLine(tri.p2);
                Console.WriteLine(tri.p3);
                Engine.Graphics.Tri2D tri2d = tri.project_onto(cam).to_screenspace(new Graphics.Vector2(this.Width, this.Height));//.to_pixels(new Engine.Graphics.Vector2(this.Width, this.Height));
                Console.WriteLine(tri2d.p1);
                Console.WriteLine(tri2d.p2);
                Console.WriteLine(tri2d.p3);
                this.CreateGraphics().DrawPolygon(new System.Drawing.Pen(Color.Red), tri2d);
                this.CreateGraphics().FillPolygon(new System.Drawing.SolidBrush(Color.Green), tri2d);
                this.Invalidate();
            }

        }

        [STAThread]
        public static void Main(string[] args) {

            Engine.Windowing.GameWindow game_window = new Windowing.GameWindow();

            System.Threading.Thread scheduler_thread = new System.Threading.Thread(Engine.Core.TaskScheduler.handover);
            scheduler_thread.Start(game_window);
            System.Windows.Forms.Application.Run(game_window);

            //Windowing.FormWrapper window = new Windowing.FormWrapper(new my_window());

            //window.underlying.MaximumSize = new Size(16, 16);
            //window.underlying.ShowDialog();
            
            // System.Drawing.Graphics g = window.underlying.CreateGraphics();

            // g.Clear(Color.Blue);

            

            // Console.ReadKey();

        }

    }

}