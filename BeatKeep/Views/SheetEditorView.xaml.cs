using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BeatKeeper.Views
{
    /// <summary>
    /// Interaction logic for SheetEditorView.xaml
    /// </summary>
    public partial class SheetEditorView : UserControl
    {
        public static readonly DependencyProperty CurrentEditorSheetProperty =
            DependencyProperty.Register("CurrentEditorSheet", typeof(object), typeof(SheetEditorView), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));

        public object CurrentEditorSheet
        {
            get { return (object)GetValue(CurrentEditorSheetProperty); }
            set { SetValue(CurrentEditorSheetProperty, value); }
        }

        public SheetEditorView()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = (TextBox)sender;
            string text = textBox.Text;
            int selectedLength = textBox.SelectionLength;

            if (text.Length - selectedLength + e.Text.Length > 3 || !e.Text.All(c => char.IsDigit(c)))
            {
                e.Handled = true;
            }
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                ((TextBox)sender).Text = "40";
            }
        }
    }
}
