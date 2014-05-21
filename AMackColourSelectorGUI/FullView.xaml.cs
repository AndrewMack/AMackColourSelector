using System;
using System.Collections.Generic;
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
using AMackColourSelectorGUI.ViewModels;

namespace AMackColourSelectorGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ColourViewModel viewModel;

        public ColourViewModel ViewModel {
            get { return viewModel; }
        }

        public MainWindow(ColourViewModel vm) {
            viewModel = vm;
            this.DataContext = ViewModel;
            InitializeComponent();
        }

        public MainWindow() {
            ColourViewModel cvm = new ColourViewModel();
            viewModel = cvm;
            this.DataContext = ViewModel;
            InitializeComponent();
        }

        private void SendClickInformationToViewModel(Rectangle rectangle, Point p) {
            Size size = new Size(rectangle.ActualWidth, rectangle.ActualHeight);

            ViewModel.SetColourFromPointInSize(p, size);
        }

        private void ListBoxItemSelected(ListBox lb) {

        }

        #region events
        private void RECT_MouseDown(object sender, MouseButtonEventArgs e) {
            SendClickInformationToViewModel((Rectangle)sender, e.MouseDevice.GetPosition((Rectangle)sender));
        }

        private void RECT_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            SendClickInformationToViewModel((Rectangle)sender, e.MouseDevice.GetPosition((Rectangle)sender));
        }

        private void RECT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            Rectangle r = (Rectangle)sender;
            r.ReleaseMouseCapture();
        }

        private void RECT_MouseMove(object sender, MouseEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                Rectangle r = (Rectangle)sender;
                r.CaptureMouse();

                SendClickInformationToViewModel(r, e.MouseDevice.GetPosition((Rectangle)sender));
            }
        }

        private void CurrentColourRectangle_MouseDown(object sender, MouseButtonEventArgs e) {
            Rectangle r = (Rectangle)sender;
            string rgbCurrentColour = ViewModel.CurrentColour.ToString();
            DragDrop.DoDragDrop(r, rgbCurrentColour, DragDropEffects.Copy);
        }

        private void ListBoxCustomColours_DragEnter(object sender, DragEventArgs e) {
            string dragAndDropValue = (string)e.Data.GetData(DataFormats.StringFormat);
            AMackColourSelectorModels.ColourUtilities.ColourStringParsers.RGBColourStringParser hex = new AMackColourSelectorModels.ColourUtilities.ColourStringParsers.RGBColourStringParser();

            AMackColourSelectorModels.Colour c;
            if (hex.TryParse(dragAndDropValue, out c)) {
                e.Effects = DragDropEffects.None;
            }
        }

        private void ListBoxCustomColours_DragDrop(object sender, DragEventArgs e) {
            string dragAndDropValue = (string)e.Data.GetData(DataFormats.StringFormat);
            AMackColourSelectorModels.ColourUtilities.ColourStringParsers.RGBColourStringParser hex = new AMackColourSelectorModels.ColourUtilities.ColourStringParsers.RGBColourStringParser();

            AMackColourSelectorModels.Colour c;
            if (hex.TryParse(dragAndDropValue, out c)) {
                ViewModel.AddColourToCustomColours(c);
            }
        }

        private void ListBox_MouseDown(object sender, MouseButtonEventArgs e) {
            ListBox lb = (ListBox)sender;
            AMackColourSelectorModels.Colour c = (AMackColourSelectorModels.Colour)lb.SelectedItem;
            if (c != null) {
                ViewModel.SetColour(c);
            }
        }

        private void ListBox_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            ListBox lb = (ListBox)sender;
            DependencyObject rect = (DependencyObject)lb.InputHitTest(e.MouseDevice.GetPosition(lb));

            while (rect != null && rect.GetType() != typeof(ListBoxItem)) {
                rect = VisualTreeHelper.GetParent(rect);
            }

            if (rect == null) {
                return;
            }

            ListBoxItem lbi = (ListBoxItem)rect;

            AMackColourSelectorModels.Colour c = (AMackColourSelectorModels.Colour)lbi.Content;
            if (c != null) {
                ViewModel.SetColour(c);
            }
        }
        #endregion events
    }
}
