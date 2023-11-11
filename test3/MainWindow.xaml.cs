﻿using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
        Color fillColor = Colors.Yellow;
        int strokeThickness = 1;
        Point start, dest;
        string actionType = "draw";
        public MainWindow()
        {
            InitializeComponent();
            strokeColorPicter.SelectedColor = strokeColor;
            fillColorPicter.SelectedColor = fillColor;
        }

        private void ShapeButton_Click(object sender, RoutedEventArgs e)
        {
            var targetRadioButton = sender as RadioButton;
            shapeType = targetRadioButton.Tag.ToString();
            actionType = "draw";
            //MessageBox.Show(shapeType);
        }

        private void myCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            dest = e.GetPosition(myCanvas);
            DisplayStatus();

            switch (actionType)
            {
                case "draw": //繪圖模式
                    if (e.LeftButton == MouseButtonState.Pressed)
                                { 

                                    Point origin;
                                    origin.X = Math.Min(start.X, dest.X);
                                    origin.Y = Math.Min(start.Y, dest.Y);
                                    double width = Math.Abs(start.X - dest.X);
                                    double height = Math.Abs(start.Y - dest.Y);

                                    switch (shapeType)
                                {
                                    case "line":
                                        var line = myCanvas.Children.OfType<Line>().LastOrDefault();
                                        line.X2 = dest.X;
                                        line.Y2 = dest.Y;
                                        break;
                                    case "rectangle":
                                            var rect = myCanvas.Children.OfType<Rectangle>().LastOrDefault();
                                            rect.Width = width;
                                            rect.Height = height;
                                            rect.SetValue(Canvas.LeftProperty, origin.X);
                                            rect.SetValue(Canvas.TopProperty, origin.Y);
                                        break;
                                    case "ellipse":
                                            var ellipes = myCanvas.Children.OfType<Ellipse>().LastOrDefault();
                                            ellipes.Width = width;
                                            ellipes.Height = height;
                                            ellipes.SetValue(Canvas.LeftProperty, origin.X);
                                            ellipes.SetValue(Canvas.TopProperty, origin.Y);
                                            break;
                                    case "polyline":
                                            var polyline = myCanvas.Children.OfType<Polyline>().LastOrDefault();
                                            polyline.Points.Add(dest);
                                            break;
                                    }   
                                }
                    break;
                case "erase": //橡皮擦模式
                    var shape = e.OriginalSource as Shape;
                    myCanvas.Children.Remove(shape);
                    if (myCanvas.Children.Count == 0) myCanvas.Cursor = Cursors.Arrow;
                    break;
            }
                       
        }

        private void myCanvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            start = e.GetPosition(myCanvas);
            DisplayStatus();

            if (actionType == "draw")
            {
            myCanvas.Cursor = Cursors.Cross;
            switch (shapeType)
            {
                case "line":
                    Line line = new Line 
                    { 
                        X1 = start.X,
                        Y1 = start.Y,
                        X2 = dest.X,
                        Y2 = dest.Y,
                        StrokeThickness = 1,
                        Stroke = Brushes.Gray
                    };
                    myCanvas.Children.Add(line);
                    break;
                case "rectangle":
                    Rectangle rect = new Rectangle
                    {
                    Stroke = Brushes.Gray,
                    Fill = Brushes.LightGray
                    };
                    myCanvas.Children.Add(rect);
                    rect.SetValue(Canvas.LeftProperty, start.X);
                    rect.SetValue(Canvas.TopProperty, start.Y);
                    break;
                case "ellipse":
                    Ellipse ellipes = new Ellipse
                    {
                        Stroke = Brushes.Gray,
                        Fill = Brushes.LightGray
                    };
                    myCanvas.Children.Add(ellipes);
                    ellipes.SetValue(Canvas.LeftProperty, start.X);
                    ellipes.SetValue(Canvas.TopProperty, start.Y);
                    break;
                case "polyline":
                    Polyline polyline = new Polyline
                    {
                        Stroke = Brushes.Gray,
                        Fill = Brushes.LightGray
                    };
                    myCanvas.Children.Add(polyline);
                    break;
            }
        }
            }
            

        private void myCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Brush strokeBrush = new SolidColorBrush(strokeColor);
            Brush fillBrush = new SolidColorBrush(fillColor);

            switch (actionType)
            {
                case "draw":
                    switch (shapeType)
                                {
                                    case "line":
                                        var line = myCanvas.Children.OfType<Line>().LastOrDefault();
                                        line.Stroke = strokeBrush;
                                        line.StrokeThickness = strokeThickness;
                                        break;
                                    case "rectangle":
                                        var rect = myCanvas.Children.OfType<Rectangle>().LastOrDefault();
                                        rect.Stroke = new SolidColorBrush(strokeColor);
                                        rect.Fill = fillBrush;
                                        rect.StrokeThickness = strokeThickness;
                                        break;
                                    case "ellipse":
                                        var ellipse = myCanvas.Children.OfType<Ellipse>().LastOrDefault();
                                        ellipse.Stroke = new SolidColorBrush(strokeColor);
                                        ellipse.Fill = fillBrush;
                                        ellipse.StrokeThickness = strokeThickness;
                                        break;
                                    case "polyline":
                                        var polyline = myCanvas.Children.OfType<Polyline>().LastOrDefault();
                                        polyline.Stroke = new SolidColorBrush(strokeColor);
                                        polyline.Fill = fillBrush;
                                        polyline.StrokeThickness = strokeThickness;
                                        break;
                                }
                    break;
                case "erase":
                    break;
            }
            
        }

        private void strokeColorPicter_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            strokeColor = (Color)strokeColorPicter.SelectedColor;
        }

        private void strokeThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            strokeThickness = Convert.ToInt32(strokeThicknessSlider.Value);
        }

        private void fillColorPicter_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            fillColor = (Color)fillColorPicter.SelectedColor;
        }

        private void eraseButton_Click(object sender, RoutedEventArgs e)
        {
            if (myCanvas.Children.Count != 0) myCanvas.Cursor = Cursors.Hand;
            actionType = "erase";
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            myCanvas.Children.Clear();
            DisplayStatus();
        }

        private void SaveCanvas_Click(object sender, RoutedEventArgs e)
        {
            // Show the SaveFileDialog to allow the user to choose the file name and location
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "儲存畫布";
            saveFileDialog.Filter = "Png檔案|*.png|所有檔案|*.*";

            if (saveFileDialog.ShowDialog() == true)
            {
                // Create a RenderTargetBitmap to capture the canvas content
                RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
                    (int)myCanvas.ActualWidth,
                    (int)myCanvas.ActualHeight,
                    64d, 64d, PixelFormats.Default);

                // Render the canvas to the RenderTargetBitmap
                renderBitmap.Render(myCanvas);

                // Create a BitmapEncoder (e.g., PNGEncoder) to save the image
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

                // Create a file stream using the user-selected file name
                string fileName = saveFileDialog.FileName;
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    encoder.Save(fs);
                }

                //MessageBox.Show($"Canvas content saved as {fileName}");
            }
        }

        private void DisplayStatus()
        {
            coordinateLbel.Content = $"座標點:({Math.Round(start.X)},{Math.Round(start.Y)}) - ({dest.X},{dest.Y})";
            int lineCount = myCanvas.Children.OfType<Line>().Count();
            int rectCount = myCanvas.Children.OfType<Rectangle>().Count();
            int ellipseCount = myCanvas.Children.OfType<Ellipse>().Count();
            int polaylineCount = myCanvas.Children.OfType<Polyline>().Count();
            shapeLabelLbel.Content = $"Line: {lineCount}, Rectangle: {rectCount}, Ellipse: {ellipseCount}, Polayline: {polaylineCount}";
        }
    }
}
