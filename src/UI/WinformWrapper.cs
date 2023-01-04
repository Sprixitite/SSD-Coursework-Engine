using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Engine.UI {

    public abstract class WinformWrapper {

        public delegate void OnTransformInvalidated();
        public event OnTransformInvalidated transform_invalidated;
        protected void invalidate_size() => transform_invalidated.Invoke();
        protected void invalidate_size(object _s, EventArgs _e) => transform_invalidated.Invoke();

        public static readonly WinformWrapper NULL;
        private sealed class WrapperNull : WinformWrapper { }

        public void fake_event_user() {}

        public WinformWrapper() {
            elements = new List<UIElement>();
            _base_parent = WinformWrapper.NULL;
            transform_invalidated += fake_event_user;
        }

        public void add_element(UIElement element) {

            if (underlying.Controls.Contains(element.underlying)) throw new Exception("Attempted to add control to a " + this.GetType().Name + " more than once!");

            // If the parent isn't null, remove the element from it's old parent
            if (element.parent != WinformWrapper.NULL) { element.parent.remove_element(element); }

            elements.Add(element);
            underlying.Controls.Add(element.underlying);

            element.parent = this;

        }

        public void remove_element(UIElement element) {

            if (!underlying.Controls.Contains(element.underlying)) throw new Exception("Attempted to remove control from a " + this.GetType().Name + " that did not exist!");

            elements.Remove(element);
            underlying.Controls.Remove(element.underlying);

            element.parent = WinformWrapper.NULL;

        }

        public bool contains_element(UIElement element) => underlying.Controls.Contains(element.underlying);

        public virtual Control underlying {
            get;
            set;
        }

        public UIWindow get_window() {

            if (this.GetType() == typeof(UIWindow)) return (UIWindow)this;
            else return this.parent.get_window();

        }

        public virtual WinformWrapper parent {
            get => _base_parent;
            protected set {
                if (_base_parent != WinformWrapper.NULL) _base_parent.transform_invalidated -= _base_on_transform_invalidated;
                value.transform_invalidated += _base_on_transform_invalidated;
                _base_parent = value;
            }
        }

        private WinformWrapper _base_parent;

        private void _base_on_transform_invalidated() {

            Console.WriteLine("Transform Invalidated!");

            on_transform_invalidated();

        }

        protected virtual void on_transform_invalidated() {}

        protected List<UIElement> elements;

    }

}