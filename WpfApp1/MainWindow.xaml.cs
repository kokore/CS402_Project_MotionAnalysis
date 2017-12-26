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
using Microsoft.Kinect;
using LightBuzz.Vitruvius;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KinectSensor sensor;
        MultiSourceFrameReader reader;
        PlayersController player;

        JointType _start1 = JointType.ShoulderRight;
        JointType _center1 = JointType.ElbowRight; //Elbow
        JointType _end1 = JointType.WristRight;

        JointType _start2 = JointType.ElbowLeft;
        JointType _center2 = JointType.ShoulderLeft; //shoulder
        JointType _end2 = JointType.SpineShoulder;

        JointType _start3 = JointType.AnkleRight;
        JointType _center3 = JointType.KneeRight; //Knee
        JointType _end3 = JointType.HipRight;

        JointType _start4 = JointType.FootRight;
        JointType _center4 = JointType.AnkleRight; //Ankle
        JointType _end4 = JointType.KneeRight;

        JointType _start5 = JointType.KneeRight;
        JointType _center5 = JointType.HipRight; //hip
        JointType _end5 = JointType.AnkleRight;



        string value;
        public MainWindow()
        {
            InitializeComponent();
            sensor = KinectSensor.GetDefault();

            
            if (sensor != null)
            {
                sensor.Open();
                reader = sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth | FrameSourceTypes.Infrared | FrameSourceTypes.Body);
                reader.MultiSourceFrameArrived += Reader_MultiSourceFrameArrived;

                player = new PlayersController();
                player.BodyEntered += UserReporter_BodyEntered;
                player.BodyLeft += UserReporter_BodyLeft;
                player.Start();
                
            }
        }

        

        private void combo_Loaded(object sender, RoutedEventArgs e)
        {
            
            List<string> data = new List<string>();
            data.Add("Hip extension");
            data.Add("Hip flexion");
            data.Add("Kenn extension");
            data.Add("Kenn flexion");
            data.Add("Shoulder flexion");
            data.Add("Elbow flexion");
            data.Add("Elbow extension");


            // ... Get the ComboBox reference.
            var comboBox = sender as ComboBox;

            // ... Assign the ItemsSource to the List.
            comboBox.ItemsSource = data;

            

            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get the ComboBox.
            var comboBox = sender as ComboBox;

            // ... Set SelectedItem as Window Title.
            value = comboBox.SelectedItem as string;
            Uri image;
            
            switch (value)
            {
                case "Hip extension":
                    image = new Uri("C:\\Users\\KoKoR\\source\\repos\\WpfApp1\\WpfApp1\\Resources\\hipexten.JPG");
                    rompic.Source = new BitmapImage(image);
                    break;
                case "Hip flexion":
                    image = new Uri("C:\\Users\\KoKoR\\source\\repos\\WpfApp1\\WpfApp1\\Resources\\hipflex.JPG");
                    rompic.Source = new BitmapImage(image);
                    break;
                case "Kenn extension":
                    image = new Uri("C:\\Users\\KoKoR\\source\\repos\\WpfApp1\\WpfApp1\\Resources\\kennexten.JPG");
                    rompic.Source = new BitmapImage(image);
                    break;
                case "Kenn flexion":
                    image = new Uri("C:\\Users\\KoKoR\\source\\repos\\WpfApp1\\WpfApp1\\Resources\\kennflex.JPG");
                    rompic.Source = new BitmapImage(image);
                    break;
                case "Shoulder flexion":
                    image = new Uri("C:\\Users\\KoKoR\\source\\repos\\WpfApp1\\WpfApp1\\Resources\\shoulderflex.JPG");
                    rompic.Source = new BitmapImage(image);
                    break;
                case "Elbow flexion":
                    image = new Uri("C:\\Users\\KoKoR\\source\\repos\\WpfApp1\\WpfApp1\\Resources\\eblowflex.JPG");
                    rompic.Source = new BitmapImage(image);
                    break;
                case "Elbow extension":
                    image = new Uri("C:\\Users\\KoKoR\\source\\repos\\WpfApp1\\WpfApp1\\Resources\\eblowexten.JPG");
                    rompic.Source = new BitmapImage(image);
                    break;
            }
        }



        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            if (player != null)
            {
                player.Stop();
            }

            if (reader != null)
            {
                reader.Dispose();
            }

            if (sensor != null)
            {
                sensor.Close();
            }
        }

        

        private void UserReporter_BodyLeft(object sender, PlayersControllerEventArgs e)
        {
            viewer.Clear();
            angle1.Clear();
            angle2.Clear();
            angle3.Clear();
            angle4.Clear();
            angle5.Clear();
        }

        private void UserReporter_BodyEntered(object sender, PlayersControllerEventArgs e)
        {
            
        }

        public void Reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            var reference = e.FrameReference.AcquireFrame();

            // Color
            using (var frame = reference.ColorFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    if (viewer.Visualization == Visualization.Color)
                    {
                        viewer.Image = frame.ToBitmap();
                    }
                }
            }

            // Body
            using (var frame = reference.BodyFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    var bodies = frame.Bodies();

                    player.Update(bodies);

                    Body body = bodies.Closest();

                    if (body != null)
                    {
                        viewer.DrawBody(body);

                        angle1.Update(body.Joints[_start1], body.Joints[_center1], body.Joints[_end1], 50); //eblow
                        angle2.Update(body.Joints[_start2], body.Joints[_center2], body.Joints[_end2], 50); //shoulder
                        angle3.Update(body.Joints[_start3], body.Joints[_center3], body.Joints[_end3], 50); //kenn
                        angle4.Update(body.Joints[_start4], body.Joints[_center4], body.Joints[_end4], 50); //Ankle
                        angle5.Update(body.Joints[_start5], body.Joints[_center5], body.Joints[_end5], 50); //hip

                        
                        if (value=="Hip extension")
                        {
                            namebody.Text = value;
                            anglebody.Text = ( ((int)angle3.Angle -180)).ToString();

                        }else if (value=="Hip flexion")
                        {
                            
                            namebody.Text = value;
                            anglebody.Text = ( -1*((int)angle3.Angle -180)).ToString();
                        }
                        else if (value == "Kenn extension")
                        {
                           namebody.Text = value;
                          
                           anglebody.Text = ((int)angle3.Angle -180).ToString();
                            
                            
                        }
                        else if (value == "Kenn flexion")
                        {
                            namebody.Text = value;
                            anglebody.Text = (-1*((int)angle3.Angle - 180)).ToString();
                        }
                        else if (value == "Shoulder flexion")
                        {
                            namebody.Text = value;
                            anglebody.Text = ( -1*((int)angle2.Angle -240)).ToString();
                        }
                        else if (value == "Elbow flexion")
                        {
                            namebody.Text = value;
                            anglebody.Text =  ( -1 *((int)angle1.Angle - 180)).ToString();
                        }
                        else if (value== "Elbow extension")
                        {
                            namebody.Text = value;
                            
                            anglebody.Text = ((int)angle1.Angle - 180).ToString();
                            
                            
                        }
                    }
                }
            }
        }

        

    }
}
