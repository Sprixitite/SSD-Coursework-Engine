using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Engine.UI {

    public sealed class UIWindow : WinformWrapper {

        public UIWindow() {
            derived_underlying = new Engine.Windowing.GenericForm();
            derived_underlying.Resize += invalidate_size;
        }

        public static implicit operator Form(UIWindow self) { return self.derived_underlying; }

        public string title {
            get => underlying.Text;
            set => underlying.Text = value;
        }

        public override Control underlying {
            get => derived_underlying;
            set => derived_underlying = (Form)value;
        }
        private Form derived_underlying;

        private void on_size_changed() {

        }

    }

}