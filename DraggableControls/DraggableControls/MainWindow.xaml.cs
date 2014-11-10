using RtwControls;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Linq;

namespace DraggableControls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
        }


        private void SaveLayout()
        {
            var layout = new XElement("Layout",
                    new XElement("NameLabel",
                        new XAttribute("Top", nameLabel.TranslatePoint(new Point(0, 0), this).Y),
                        new XAttribute("Left", nameLabel.TranslatePoint(new Point(0, 0), this).X),
                        new XAttribute("Width", nameLabel.ActualWidth),
                        new XAttribute("Height", nameLabel.ActualHeight)
                    ), new XElement("secondLabel",
                        new XAttribute("Top", nameLabel.TranslatePoint(new Point(0, 0), this).Y),
                        new XAttribute("Left", nameLabel.TranslatePoint(new Point(0, 0), this).X),
                        new XAttribute("Width", nameLabel.ActualWidth),
                        new XAttribute("Height", nameLabel.ActualHeight)
                    )

                );

            layout.Save("layouts1.xml");
        }

     

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveLayout();
            MessageBox.Show("Successfully Saved.", "Layout", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Loaded(object sender, RoutedEventArgs e)
        {

            if (File.Exists("layouts1.xml"))
            {
                XElement layout = XElement.Load("layouts1.xml");

                var elements = layout.Elements().Select(
                    el => new { Name = el.Name, Top = (double)el.Attribute("Top"), Left = (double)el.Attribute("Left"), Width = (double)el.Attribute("Width"), Height = (double)el.Attribute("Height") });

                var element = elements.Where(el => el.Name == "NameLabel").Single();
                var initialLocation = nameLabel.TranslatePoint(new Point(0, 0), this);
                nameLabel.RenderTransform = new TranslateTransform(element.Left - initialLocation.X, element.Top - initialLocation.Y);
                nameLabel.Width = element.Width;
                nameLabel.Height = element.Height;


                element = elements.Where(el => el.Name == "secondLabel").Single();
                initialLocation = secondLabel.TranslatePoint(new Point(0, 0), this);
                secondLabel.RenderTransform = new TranslateTransform(element.Left - initialLocation.X, element.Top - initialLocation.Y);
                secondLabel.Width = element.Width;
                secondLabel.Height = element.Height;
            }
        }

        private void draggableLabel_Drag(object sender, DraggableLabelDragEventArgs e)
        {
         //   controlDetails.Content = sender.ToString();
        }

        private void draggableLabel_Resize(object sender, DraggableLabelResizeEventArgs e)
        {
           // controlDetails.Content = sender.ToString();
        }

        //private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    this.DragMove();
        //}



    }
}
