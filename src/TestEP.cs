using System;

using System.Drawing;

namespace Engine {

    public class TestEntryPoint {

        class my_window : System.Windows.Forms.Form {

            public my_window() {
            }

            protected override void OnPaint(System.Windows.Forms.PaintEventArgs pe) {
                base.OnPaint(pe);
                System.Drawing.Graphics g = this.CreateGraphics();
                g.Clear(Color.Blue);
                Graphics.Tri<Graphics.Vector2> tri = new Graphics.Tri<Graphics.Vector2>(new Graphics.Vector2(1, 0), new Graphics.Vector2(0, 1), new Graphics.Vector2(-1, 0));

                Pen white = new Pen(Color.White);
                Pen black = new Pen(Color.Black);

                const int FRAME_X = 1280;
                const int FRAME_Y = 720;
                
                for (int i=0; i<FRAME_X; i++) {

                    for (int j=0; j<FRAME_Y; j++) {

                        Graphics.Vector2 pos_screen = new Graphics.Vector2((i-(FRAME_X/2))/(float)(FRAME_X/2), (j-(FRAME_Y/2))/(float)(FRAME_Y/2));

                        switch (tri.contains(pos_screen)) {
                            case true:
                                g.DrawRectangle(white, i, j, 1, 1);
                                break;
                            case false:
                                g.DrawRectangle(black, i, j, 1, 1);
                                break;
                        }

                    }

                }

            }

        }

        [STAThread]
        public static void Main(string[] args) {

            Console.WriteLine(new Graphics.Vector2(-6, 8).dot(new Graphics.Vector2(5, 12)));

            System.Windows.Forms.Application.Run(new my_window());
            Console.WriteLine("Cry!");

            Windowing.FormWrapper window = new Windowing.FormWrapper(new my_window());

            //window.underlying.MaximumSize = new Size(16, 16);
            window.underlying.ShowDialog();
            
            System.Drawing.Graphics g = window.underlying.CreateGraphics();

            g.Clear(Color.Blue);

            

            Console.ReadKey();

        }

    }

}