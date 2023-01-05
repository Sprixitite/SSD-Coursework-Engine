using System;

using System.Drawing;

using Engine.UI;

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

            UIWindow window = new UIWindow();
            window.title = "Rory's Shiny Brand-Spanking-New Window";

            UIBuilder<UIButton> button_builder = new UIBuilder<UIButton>();

            UIButton button1 = button_builder.set_field("size", new UIPosition(0.33f, 0.25f, -5.0f, -5.0f))
                                             .set_field("anchor", new UIAnchor(AnchorX.CENTER, AnchorY.TOP))
                                             .set_field("text", "This is my button!")
                                             .set_field("position", new UIPosition(0.17f, 0.0f, 0.0f, 5.0f))
                                             .build();

            UIButton button2 = button_builder.set_field("text", "This is my second button!")
                                             .set_field("position", new UIPosition(0.5f, 0.0f, 0.0f, 5.0f))
                                             .build();

            UIButton button3 = button_builder.set_field("text", "This is my third button!")
                                             .set_field("position", new UIPosition(0.83f, 0.0f, 0.0f, 5.0f))
                                             .build();

            button1.button_clicked += () => { Console.WriteLine("Button1 Pressed!"); };
            button2.button_clicked += () => { Console.WriteLine("Button2 Pressed!"); };
            button3.button_clicked += () => { Console.WriteLine("Button3 Pressed!"); };

            UITextBox textbox = new UITextBox();
            textbox.text = "TEXTBOX TEST";
            textbox.position = new UIPosition(0.5f, 0.5f, 0.0f, 0.0f);
            textbox.size = new UIPosition(0.0f, 0.0f, 69.0f, 69.0f);
            textbox.anchor = new UIAnchor(AnchorX.CENTER, AnchorY.CENTER);

            UICheckbox checkbox = new UICheckbox();
            checkbox.position = new UIPosition(0.5f, 0.5f, 74.0f, 0.0f);
            checkbox.size = new UIPosition(0.0f, 0.0f, 64.0f, 64.0f);
            checkbox.anchor = new UIAnchor(AnchorX.CENTER, AnchorY.CENTER);

            window.add_element(button1);
            window.add_element(button2);
            window.add_element(button3);
            window.add_element(textbox);
            window.add_element(checkbox);

            IO.SprixaneSerializer a = new IO.SprixaneSerializer();
            a.Serialize(System.IO.Stream.Null, Decimal.MaxValue);

            // panel.add_element(child_panel);

            window.run();

        }

    }

}