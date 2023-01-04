using System;

using System.Drawing;

namespace Engine {

    public class TestEntryPoint {

        class my_class {
            public string test_string;
            public int test_int;
            public float test_float;
            public my_class_2 class_2;
        }

        class my_class_2 {
            public string second_test_string;
            public int second_test_int;
        }

        [STAThread]
        public static void Main(string[] args) {

            // //Engine.IO.ForMapper<string>.read_file("/home/rory/regextest.txt");
            // //Console.ReadKey();

            // Engine.IO.Sprixane.SprixaneFile loaded = Engine.IO.Sprixane.load("/home/rory/sprixanetest.txt");
            // my_class loaded_obj = loaded.get_object<my_class>("Object1");
            // System.Console.WriteLine(loaded_obj.class_2.second_test_string);
            // Console.ReadKey();

            // Engine.Windowing.GameWindow game_window = new Windowing.GameWindow();

            // System.Threading.Thread scheduler_thread = new System.Threading.Thread(Engine.Core.TaskScheduler.handover);
            // scheduler_thread.Start(game_window);
            // System.Windows.Forms.Application.Run(game_window);

            UI.UIWindow window = new UI.UIWindow();

            window.title = "Rory's Shiny Brand-Spanking-New Window";

            UI.UIPanel panel = new UI.UIPanel();
            window.add_element(panel);

            panel.position = new UI.UIPosition(0.5f, 0.5f, 0.0f, 0.0f);
            panel.size = new UI.UIPosition(0.25f, 0.25f, 0.0f, 0.0f);

            // panel.add_element(child_panel);

            System.Windows.Forms.Application.Run(window);

        }

    }

}