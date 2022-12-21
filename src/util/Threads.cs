using System;
using System.Threading;

namespace Engine.Util {

    public static class ThreadUtil {

        // "Spawns" a thread to run the provided Action
        public static void spawn( Action function ) {

            Thread spawning = new Thread(function.Invoke);
            spawning.Start();

        }

        // Above but parameterized
        public static void spawn( Action<object> function, params object[] arguments ) {

            Thread spawning = new Thread((ParameterizedThreadStart)function.Invoke);
            spawning.Start((object)arguments);

        }

    }

}