using System.Windows;
using System.Windows.Controls;

namespace Presentation
{
    public static class DialogCloser
    {
        public static readonly DependencyProperty DialogResultProperty = DependencyProperty.RegisterAttached(
            "DialogResult",
            typeof(bool?),
            typeof(DialogCloser),
            new PropertyMetadata(DialogResultChanged));

        private static void DialogResultChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as UserControl;
            if (control == null)
                return;

            Window controlWindow = Window.GetWindow(control);
            if (controlWindow == null || controlWindow.Owner == null)
                return;

            controlWindow.DialogResult = e.NewValue as bool?;
        }

        public static void SetDialogResult(UserControl target, bool? value)
        {
            target.SetValue(DialogResultProperty, value);
        }
    }
}
