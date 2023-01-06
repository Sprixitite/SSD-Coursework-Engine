using System;
using System.Windows.Forms;

namespace Engine.UI {

    public class UICheckbox : UIElement {

        public UICheckbox() {
            
            derived_underlying = new CheckBox();

        }

        public override Control underlying {
            get => derived_underlying;
            set => derived_underlying = (CheckBox)value;
        }
        private CheckBox derived_underlying;

        public bool ticked {
            get => derived_underlying.Checked;
            set => derived_underlying.Checked = value;
        }

    }

}