using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace core._V
{
    public class CommonTab_V : UserControl
    {
        public CommonTab_V()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
