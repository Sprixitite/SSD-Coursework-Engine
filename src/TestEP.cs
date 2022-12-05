using System;

using System.Drawing;

namespace Engine {

    public class TestEntryPoint {

        class my_window : System.Windows.Forms.Form {

            public my_window() {
            }

            protected override void OnPaint(System.Windows.Forms.PaintEventArgs pe) {
                Console.WriteLine("Frame!");
                this.CreateGraphics().Clear(Color.Blue);
            }

        }

        [STAThread]
        public static void Main(string[] args) {

            Console.WriteLine(new Graphics.Vector2(-6, 8).dot(new Graphics.Vector2(5, 12)));

            System.Windows.Forms.Application.Run(new my_window());
            Console.WriteLine("Cry!");

            //Windowing.FormWrapper window = new Windowing.FormWrapper(new my_window());

            //window.underlying.MaximumSize = new Size(16, 16);
            //window.underlying.ShowDialog();
            
            System.Drawing.Graphics g = window.underlying.CreateGraphics();

            g.Clear(Color.Blue);

            

            Console.ReadKey();

        }

    }

}