using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Engine.Core {

    // I have absolutely 0 clue how to use the built in scheduler
    // So let's roll our own!
    public static class TaskScheduler {

        static TaskScheduler() {
            groups = new List<TaskGroup>();
        }

        public static void define_schedule(params TaskGroup _groups) {
            groups = _groups;
        }

        public static void run() {
            foreach (TaskGroup g in groups) {
                int failures = g.invoke_all();
                Console.WriteLine("Task group \"" + nameof(g) + "\" had " + failures + " failures."); 
            }
        }

        public static void handover(Type window) {

        }

        // Group of tasks that need to complete before the next group is executed
        public struct TaskGroup {

            public TaskGroup(params Task<bool> _tasks) {
                tasks = _tasks;
            }

            public int invoke_all() {

                int failures = 0;

                // Initiate all the tasks in parallel
                foreach (Task<bool> t in tasks) {
                    t.Start();
                }

                // Wait for them to return
                foreach (Task<bool> t in tasks) {
                    bool success = t.Result;
                    if (!success) { Console.WriteLine("Task \"" + nameof(t) + "\" failed!"); failures++; }
                }

                return failures;

            }

            Task<bool>[] tasks;

        }

        private static List<TaskGroup> groups;

    }

}