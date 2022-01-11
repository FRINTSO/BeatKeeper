using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BeatKeeper.Components
{
    /// <summary>
    /// Interaction logic for NoteSheet.xaml
    /// </summary>
    public partial class SheetNote : UserControl
    {
        public static readonly DependencyProperty DeleteNoteCommandProperty =
            DependencyProperty.Register("DeleteNoteCommand", typeof(ICommand), typeof(SheetNote), new PropertyMetadata(null));

        public ICommand DeleteNoteCommand
        {
            get => (ICommand)GetValue(DeleteNoteCommandProperty);
            set => SetValue(DeleteNoteCommandProperty, value);
        }

        public static readonly DependencyProperty IncomingNoteProperty =
            DependencyProperty.Register("IncomingNote", typeof(object), typeof(SheetNote), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public object IncomingNote
        {
            get => GetValue(IncomingNoteProperty);
            set => SetValue(IncomingNoteProperty, value);
        }


        public static readonly DependencyProperty NoteDropCommandProperty =
            DependencyProperty.Register("NoteDropCommand", typeof(ICommand), typeof(SheetNote), new PropertyMetadata(null));

        public ICommand NoteDropCommand
        {
            get => (ICommand)GetValue(NoteDropCommandProperty);
            set => SetValue(NoteDropCommandProperty, value);
        }

        public static readonly DependencyProperty NoteInsertedCommandProperty =
            DependencyProperty.Register("NoteInsertedCommand", typeof(ICommand), typeof(SheetNote), new PropertyMetadata(null));

        public ICommand NoteInsertedCommand
        {
            get => (ICommand)GetValue(NoteInsertedCommandProperty);
            set => SetValue(NoteInsertedCommandProperty, value);
        }

        public static readonly DependencyProperty TargetNoteProperty =
            DependencyProperty.Register("TargetNote", typeof(object), typeof(SheetNote), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public object TargetNote
        {
            get => GetValue(TargetNoteProperty);
            set => SetValue(TargetNoteProperty, value);
        }

        public static readonly DependencyProperty InsertedNoteProperty =
            DependencyProperty.Register("InsertedNote", typeof(object), typeof(SheetNote), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public object InsertedNote
        {
            get => GetValue(InsertedNoteProperty);
            set => SetValue(InsertedNoteProperty, value);
        }

        private const double _dragThreshold = 1.0;
        private Point _startPosition;

        public SheetNote()
        {
            InitializeComponent();
        }

        private void SheetNote_Drop(object sender, DragEventArgs e)
        {
            if ((NoteDropCommand?.CanExecute(null) ?? false) &&
                e.Effects == DragDropEffects.Copy)
            {
                IncomingNote = e.Data.GetData(DataFormats.Serializable);
                NoteDropCommand.Execute(null);
            }
        }

        private void Note_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Point currentPosition = e.GetPosition(this);
            Vector delta = currentPosition - _startPosition;
            if ((delta.Length > _dragThreshold) &&
                e.LeftButton == MouseButtonState.Pressed &&
                sender is FrameworkElement element &&
                e.OriginalSource is Button)
            {
                _ = DragDrop.DoDragDrop(element, new DataObject(DataFormats.Serializable, element.DataContext), DragDropEffects.Move);
            }
        }

        private void Note_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
            {
                return;
            }

            _startPosition = e.GetPosition(this);
        }

        private void Note_DragOver(object sender, DragEventArgs e)
        {
            if (NoteInsertedCommand?.CanExecute(null) ?? false)
            {
                if (sender is FrameworkElement frameworkElement)
                {
                    TargetNote = frameworkElement.DataContext;
                    InsertedNote = e.Data.GetData(DataFormats.Serializable);
                }
                NoteInsertedCommand?.Execute(null);
            }
        }

    }
}
