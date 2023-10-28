using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace test3
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string shapeType = "line";
        Color strokeColor = Colors.Red;
        Brush strokeBrussh;
        int strokeThickness = 1;
        Point start, dest;
        public MainWindow()
        {
            InitializeComponent();
            strokeColorPicter.SelectedColor = strokeColor;
            strokeBrussh = new SolidColorBrush(strokeColor);
        }

        private void ShapeButton_Click(object sender, RoutedEventArgs e)
        {
            var targetRadioButton = sender as RadioButton;
            shapeType = targetRadioButton.Tag.ToString();
            //MessageBox.Show(shapeType);
        }

        private void myCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            dest = e.GetPosition(myCanvas);
            DisplayStatus();

            switch (shapeType)
            {
                case "line":                  
                    break;
                case "rectangle":
                    break;
                case "ellipse":
                    break;
            }
        }

        private void myCanvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            start = e.GetPosition(myCanvas);
            DisplayStatus();

            myCanvas.Cursor = Cursors.Cross;
            switch (shapeType)
            {
                case "line":
                    DrawLine(strokeColor , 1);
                    break;
                case "rectangle":
                    break;
                case "ellipse":
                    break;
            }
        }

        private void DisplayStatus()
        {
            coordinateLbel.Content = $"座標點:({Math.Round(start.X)},{Math.Round(start.Y)}) - ({dest.X},{dest.Y})";
        }

        private void DrawLine(Color color, int thickness)
        {
            var brush = new SolidColorBrush(color);
            Line line = new Line
            {
                Stroke = brush,
                StrokeThickness = thickness,
                X1 = start.X,
                Y1 = start.Y,
                X2 = dest.X,
                Y2 = dest.Y
            };
            myCanvas.Children.Add(line);
        }
    }
}
