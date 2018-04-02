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
    /// Interaction logic for sele.xaml
    /// </summary>
    public partial class sele : Page
    {
        KinectSensor sensor;
        MultiSourceFrameReader reader;
        PlayersController player;

        JointType st1 = JointType.ShoulderRight;
        JointType ce1 = JointType.ElbowRight;   //Elbow right
        JointType en1 = JointType.WristRight;

        JointType st2 = JointType.ElbowRight;
        JointType ce2 = JointType.ShoulderRight; //Shoulder right
        JointType en2 = JointType.SpineShoulder;

        JointType st3 = JointType.AnkleRight;
        JointType ce3 = JointType.KneeRight; //Kenn right
        JointType en3 = JointType.HipRight;

        JointType st4 = JointType.FootRight;
        JointType ce4 = JointType.AnkleRight; //Ankle right
        JointType en4 = JointType.KneeRight;

        JointType st5 = JointType.KneeRight;
        JointType ce5 = JointType.HipRight;     //Hip right
        JointType en5 = JointType.AnkleRight;

        JointType st6 = JointType.ShoulderLeft;
        JointType ce6 = JointType.ElbowLeft;   //Elbow Left
        JointType en6 = JointType.WristLeft;

        JointType st7 = JointType.ElbowLeft;
        JointType ce7 = JointType.ShoulderLeft; //Shoulder Left
        JointType en7 = JointType.SpineShoulder;

        JointType st8 = JointType.AnkleLeft;
        JointType ce8 = JointType.KneeLeft; //Kenn Left
        JointType en8 = JointType.HipLeft;

        JointType st9 = JointType.FootLeft;
        JointType ce9 = JointType.AnkleLeft; //Ankle Left
        JointType en9 = JointType.KneeLeft;

        JointType st10 = JointType.KneeRight;
        JointType ce10 = JointType.HipRight;     //Hip right
        JointType en10 = JointType.AnkleRight;

        string value;

        public sele()
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

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
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

        void Reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
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

                        angle1.Update(body.Joints[st1], body.Joints[ce1], body.Joints[en1], 50); //elbow
                        angle2.Update(body.Joints[st2], body.Joints[ce2], body.Joints[en2], 50); //shoulder
                        angle3.Update(body.Joints[st3], body.Joints[ce3], body.Joints[en3], 50); //knee
                        angle4.Update(body.Joints[st4], body.Joints[ce4], body.Joints[en4], 50);  //ankle
                        angle5.Update(body.Joints[st5], body.Joints[ce5], body.Joints[en5], 50); //hip

                        angle6.Update(body.Joints[st6], body.Joints[ce6], body.Joints[en6], 50); //elbow
                        angle7.Update(body.Joints[st7], body.Joints[ce7], body.Joints[en7], 50); //shoulder
                        angle8.Update(body.Joints[st8], body.Joints[ce8], body.Joints[en8], 50); //kenn
                        angle9.Update(body.Joints[st9], body.Joints[ce9], body.Joints[en9], 50); //ankle
                        angle10.Update(body.Joints[st10], body.Joints[ce10], body.Joints[en10], 50); //hip

                        if(rb1.IsChecked==true)
                        {
                            if (value == "Hip extension")
                            {
                                Angle.Text = (((int)angle5.Angle )).ToString();
                            }
                            else if (value == "Hip flexion")
                            {
                                Angle.Text = (-1 * ((int)angle5.Angle )).ToString();
                            }
                            else if (value == "Kenn flexion")
                            {
                                Angle.Text = (-1 * ((int)angle3.Angle )).ToString();
                            }
                            else if (value == "Shoulder flexion")
                            {
                                Angle.Text = (-1 * ((int)angle2.Angle )).ToString();
                            }
                            else if (value == "Elbow flexion")
                            {
                                Angle.Text = (-1 * ((int)angle1.Angle )).ToString();
                            }
                        }else if (rb2.IsChecked == true)
                        {
                            if (value == "Hip extension")
                            {
                                Angle.Text = (((int)angle10.Angle)).ToString();
                            }
                            else if (value == "Hip flexion")
                            {
                                Angle.Text = (-1 * ((int)angle10.Angle)).ToString();
                            }
                            else if (value == "Kenn flexion")
                            {
                                Angle.Text = (-1 * ((int)angle8.Angle)).ToString();
                            }
                            else if (value == "Shoulder flexion")
                            {
                                Angle.Text = (-1 * ((int)angle7.Angle)).ToString();
                            }
                            else if (value == "Elbow flexion")
                            {
                                Angle.Text = (-1 * ((int)angle6.Angle)).ToString();
                            }
                        }

                    }
                }
            }
        }

        void UserReporter_BodyEntered(object sender, PlayersControllerEventArgs e)
        {
        }

        void UserReporter_BodyLeft(object sender, PlayersControllerEventArgs e)
        {
            viewer.Clear();
            angle1.Clear();
            angle2.Clear();
            angle3.Clear();
            angle4.Clear();
            angle5.Clear();

            angle6.Clear();
            angle7.Clear();
            angle8.Clear();
            angle9.Clear();
            angle10.Clear();
            Angle.Text = "-";
        }

        private void combo_Loaded(object sender, RoutedEventArgs e)
        {

            List<string> data = new List<string>();
            data.Add("Hip extension");
            data.Add("Hip flexion");
            data.Add("Kenn flexion");
            data.Add("Shoulder flexion");
            data.Add("Elbow flexion");


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

        }
    }
}
