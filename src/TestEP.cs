using System;

using System.Drawing;

namespace Engine {

    public class TestEntryPoint {

        [STAThread]
        public static void Main(string[] args) {

            //Engine.IO.ForMapper<string>.read_file("/home/rory/regextest.txt");
            //Console.ReadKey();

            Engine.Windowing.GameWindow game_window = new Windowing.GameWindow();

            System.Threading.Thread scheduler_thread = new System.Threading.Thread(Engine.Core.TaskScheduler.handover);
            scheduler_thread.Start(game_window);
            System.Windows.Forms.Application.Run(game_window);

        }

    }

}