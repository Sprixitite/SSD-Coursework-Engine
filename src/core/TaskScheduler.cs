using System;
using System.Collections.Generic;

using Engine.Windowing;

namespace Engine.Core {

    // I have absolutely 0 clue how to use the built in scheduler
    // So let's roll our own!
    public static class TaskScheduler {

        static TaskScheduler() {
            groups = new List<TaskGroup>();
            should_exit = false;
        }

        public static void define_schedule(params TaskGroup[] _groups) {
            groups.AddRange(_groups);
        }

        public static void run() {
            foreach (TaskGroup g in groups) {
                int failures = g.invoke_all();
                Console.WriteLine("Task group \"" + nameof(g) + "\" had " + failures + " failures."); 
            }
        }

        internal static bool should_exit;

        private static System.Windows.Forms.Form main_window;

        public static void handover(object _main_window) {

            GameWindow main_window = (GameWindow)_main_window;

            Engine.Graphics.Camera cam = new Engine.Graphics.Camera();
            cam.position = new Engine.Graphics.Vector3(0.0f, 0.0f, 0.0f);
            cam.rotation = new Engine.Graphics.Matrix3x3(new Engine.Graphics.Vector3(0.0f, 0.0f, 0.0f));
            cam.surface_dist = 1000.0f;//= DateTime.Now.Millisecond*0.005f;
            Engine.Graphics.Tri3D tri = new Engine.Graphics.Tri3D(new Engine.Graphics.Vector3(0.5f, 0.5f, 10.0f), new Engine.Graphics.Vector3(-0.5f, 0.5f, 15.0f), new Engine.Graphics.Vector3(-0.5f, -0.5f, 10.0f));

            define_schedule(
                new TaskGroup( new Task( (Func<bool>)(() => { 
                    main_window.Invoke((System.Windows.Forms.MethodInvoker) delegate {
                        main_window.render(cam, new[]{tri});
                    });
                    return true; 
                })))
            );

            while (!should_exit) {

                run();

            }

        }

        // Group of tasks that need to complete before the next group is executed
        public struct TaskGroup {

            public TaskGroup(params Task[] _tasks) {
                tasks = _tasks;
            }

            public int invoke_all() {

                int failures = 0;

                // Initiate all the tasks in parallel
                foreach (Task t in tasks) {
                    t.begin_invoke();
                }

                // Wait for them to return
                foreach (Task t in tasks) {
                    bool success = t.succeeded;
                    if (!success) { Console.WriteLine("Task \"" + nameof(t) + "\" failed!"); failures++; }
                }

                return failures;

            }

            Task[] tasks;

        }

        private static List<TaskGroup> groups;

    }

}