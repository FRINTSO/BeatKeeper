using BeatKeeper.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace BeatKeeper.Components
{
    /// <summary>
    /// Interaction logic for NoteListing.xaml
    /// </summary>
    public partial class TemplateNoteListing : UserControl
    {
        private const double _dragThreshold = 1.0;
        private Point _startPosition;

        public static readonly DependencyProperty AddNoteCommandProperty = DependencyProperty.Register("AddNoteCommand", typeof(ICommand), typeof(TemplateNoteListing), new PropertyMetadata(null));
        public ICommand AddNoteCommand
        {
            get { return (ICommand)GetValue(AddNoteCommandProperty); }
            set { SetValue(AddNoteCommandProperty, value); }
        }

        public TemplateNoteListing()
        {
            InitializeComponent();
        }

        private void TemplateNote_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Point currentPosition = e.GetPosition(this);
            Vector delta = currentPosition - _startPosition;
            if ((delta.Length > _dragThreshold) &&
                e.LeftButton == MouseButtonState.Pressed &&
                sender is FrameworkElement element &&
                e.OriginalSource is Button)
            {
                _ = DragDrop.DoDragDrop(element, new DataObject(DataFormats.Serializable, element.DataContext), DragDropEffects.Copy);
            }
        }

        private void TemplateNote_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
            {
                return;
            }

            _startPosition = e.GetPosition(this);
        }
    }
}
