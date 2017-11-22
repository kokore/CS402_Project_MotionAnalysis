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
        JointType _center1 = JointType.ElbowRight;
        JointType _end1 = JointType.WristRight;

        JointType _start2 = JointType.ElbowLeft;
        JointType _center2 = JointType.ShoulderLeft;
        JointType _end2 = JointType.SpineShoulder;

        JointType _start3 = JointType.AnkleRight;
        JointType _center3 = JointType.KneeRight;
        JointType _end3 = JointType.HipRight;

        public MainWindow()
        {
            InitializeComponent();
            sensor = KinectSensor.GetDefault();

            combo.Items.Add("Hip");
            combo.Items.Add("Knee");
            combo.Items.Add("Ankle");
            combo.Items.Add("Shoulder");
            combo.Items.Add("Elbow");

            BitmapImage b = new BitmapImage();
            b.BeginInit();
            switch (combo.ToString())
            {
                case "Hip":
                    b.UriSource = new Uri("hip.JPG",UriKind.Relative);
                    break;
                case "Kenn":
                    b.UriSource = new Uri("Kenn.JPG", UriKind.Relative);
                    break;
                case "Ankle":
                    b.UriSource = new Uri("Ankle.JPG", UriKind.Relative);
                    break;
                case "Shoulder":
                    b.UriSource = new Uri("Shoulder.JPG", UriKind.Relative);
                    break;
                case "Elbow":
                    b.UriSource = new Uri("Eblow.JPG", UriKind.Relative);
                    break;
            }
            b.EndInit();
            rompic.Source = b;
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

            
        }

        private void UserReporter_BodyEntered(object sender, PlayersControllerEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
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

                        angle1.Update(body.Joints[_start1], body.Joints[_center1], body.Joints[_end1], 50);
                        angle2.Update(body.Joints[_start2], body.Joints[_center2], body.Joints[_end2], 50);
                        angle3.Update(body.Joints[_start3], body.Joints[_center3], body.Joints[_end3], 50);

                        
                    }
                }
            }
        }
    }
}
