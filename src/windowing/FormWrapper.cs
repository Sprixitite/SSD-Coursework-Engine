using System;
using System.Collections.Generic;

namespace Engine.Windowing {

    internal class GenericForm : System.Windows.Forms.Form {}

    internal class FormWrapper {

        public FormWrapper(System.Windows.Forms.Form wrapping) {
            underlying = wrapping;
        }

        public static FormWrapper create() {
            return new FormWrapper(new GenericForm());
        }

        Dictionary<string, System.Windows.Forms.Control> controls = new Dictionary<string, System.Windows.Forms.Control> {};

        public System.Windows.Forms.Control control_of_key(string key) {
            return controls[key];
        }

        public void add_controls(params object[] control_index_pairs) {
            if (!((control_index_pairs.Length % 2) == 0)) throw new Exception("add_controls must recieve an even number of arguments!");
            underlying.SuspendLayout();
            List<System.Windows.Forms.Control> control_list = new List<System.Windows.Forms.Control>();
            for (int i=0; i<control_index_pairs.Length; i+=2) {
                Type this_type = control_index_pairs[i].GetType();
                Type next_type = control_index_pairs[i+1].GetType();
                if (this_type != typeof(string) || next_type != typeof(System.Windows.Forms.Control)) throw new Exception("add_controls only accepts pairs of control keys and controls!");
                controls.Add((string)control_index_pairs[i], (System.Windows.Forms.Control)control_index_pairs[i+1]);
                control_list.Add((System.Windows.Forms.Control)control_index_pairs[i+1]);
            }
            underlying.Controls.AddRange(control_list.ToArray());
            underlying.ResumeLayout();
        }

        public void remove_controls(params string[] control_index_pairs) {
            underlying.SuspendLayout();
            for (int i=0; i<control_index_pairs.Length; i++) {
                underlying.Controls.RemoveByKey(control_index_pairs[i]);
            }
            underlying.ResumeLayout();
        }

        public System.Windows.Forms.Form underlying {
            get;
            protected set;
        }

    }

}