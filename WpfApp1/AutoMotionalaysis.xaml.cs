using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LightBuzz.Vitruvius;
using Microsoft.Kinect;
using static System.Windows.Forms.Timer;
using System.Collections;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for auto.xaml
    /// </summary>
    public partial class auto : Page
    {

        KinectSensor sensor;
        MultiSourceFrameReader reader;
        PlayersController players;

        JointType st1 = JointType.ShoulderRight;
        JointType ce1 = JointType.ElbowRight;   //Elbow right
        JointType en1 = JointType.WristRight;

        JointType st2 = JointType.ElbowRight;
        JointType ce2 = JointType.ShoulderRight; //Shoulder right
        JointType en2 = JointType.SpineShoulder;

        JointType st3 = JointType.AnkleRight;
        JointType ce3 = JointType.KneeRight; //Kenn right
        JointType en3 = JointType.HipRight;

        JointType st4 = JointType.KneeRight;
        JointType ce4 = JointType.HipRight;     //Hip right
        JointType en4 = JointType.SpineBase;



        JointType st5 = JointType.ShoulderLeft;
        JointType ce5 = JointType.ElbowLeft;   //Elbow Left
        JointType en5 = JointType.WristLeft;

        JointType st6 = JointType.ElbowLeft;
        JointType ce6 = JointType.ShoulderLeft; //Shoulder Left
        JointType en6 = JointType.SpineShoulder;

        JointType st7 = JointType.AnkleLeft;
        JointType ce7 = JointType.KneeLeft; //Kenn Left
        JointType en7 = JointType.HipLeft;

        JointType st8 = JointType.KneeLeft;
        JointType ce8 = JointType.HipLeft;     //Hip Left
        JointType en8 = JointType.SpineBase;
        private System.Windows.Forms.Timer timer1;
        int motion = 0;
        public static ArrayList list = new ArrayList();

        public auto()
        {
            InitializeComponent();

            sensor = KinectSensor.GetDefault();

            if (sensor != null)
            {

                sensor.Open();

                reader = sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth | FrameSourceTypes.Infrared | FrameSourceTypes.Body);
                reader.MultiSourceFrameArrived += Reader_MultiSourceFrameArrived;

                players = new PlayersController();
                players.Start();
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            if (players != null)
            {
                players.Stop();
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

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
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

                    players.Update(bodies);

                    Body body = bodies.Closest();

                    if (body != null)
                    {
                        
                        if(motion == 0)
                        {
                            Clear();
                            move.Text = "Hip extension right";
                            viewer.DrawBody(body);
                            angle4.Update(body.Joints[st4], body.Joints[ce4], body.Joints[en4], 50); //hip
                            Angle.Text = Math.Abs((((int)angle4.Angle - 100))).ToString();

                            if (t == 1)
                            {
                                list.Add(int.Parse(Angle.Text));
                            }
                        }
                        else if (motion == 1)
                        {
                            Clear();
                            move.Text = "Hip flexion right";
                            viewer.DrawBody(body);
                            angle4.Update(body.Joints[st4], body.Joints[ce4], body.Joints[en4], 50); //hip
                            Angle.Text = Math.Abs((((int)angle4.Angle - 100))).ToString();

                            if (t == 1)
                            {
                                list.Add(int.Parse(Angle.Text));
                            }
                        }
                        else if (motion == 2)
                        {
                            Clear();
                            move.Text = "Kenn flexion right";
                            viewer.DrawBody(body);
                            angle3.Update(body.Joints[st3], body.Joints[ce3], body.Joints[en3], 50); //knee
                            Angle.Text = Math.Abs((((int)angle3.Angle - 180))).ToString();

                            if (t == 1)
                            {
                                list.Add(int.Parse(Angle.Text));
                            }
                        }
                        else if (motion == 3)
                        {
                            Clear();
                            move.Text = "Shoulder flexion right";
                            viewer.DrawBody(body);
                            angle2.Update(body.Joints[st2], body.Joints[ce2], body.Joints[en2], 50); //shoulder
                            Angle.Text = Math.Abs((((int)angle2.Angle - 120))).ToString();

                            if (t == 1)
                            {
                                list.Add(int.Parse(Angle.Text));
                            }
                        }
                        else if (motion == 4)
                        {
                            Clear();
                            move.Text = "Elbow flexion right";
                            viewer.DrawBody(body);
                            angle1.Update(body.Joints[st1], body.Joints[ce1], body.Joints[en1], 50); //elbow
                            Angle.Text = Math.Abs((-1 * ((int)angle1.Angle -180))).ToString();

                            if (t == 1)
                            {
                                list.Add(int.Parse(Angle.Text));
                            }
                        }
                        else if (motion == 5)
                        {
                            Clear();
                            move.Text = "Hip extension left";
                            viewer.DrawBody(body);
                            angle8.Update(body.Joints[st8], body.Joints[ce8], body.Joints[en8], 50); //hip
                            Angle.Text = Math.Abs((-1*((int)angle8.Angle - 270))).ToString();

                            if (t == 1)
                            {
                                list.Add(int.Parse(Angle.Text));
                            }
                        }
                        else if (motion == 6)
                        {
                            Clear();
                            move.Text = "Hip flexion left";
                            viewer.DrawBody(body);
                            angle8.Update(body.Joints[st8], body.Joints[ce8], body.Joints[en8], 50); //hip
                            Angle.Text = Math.Abs(((-1 * ((int)angle8.Angle - 270)))).ToString();

                            if (t == 1)
                            {
                                list.Add(int.Parse(Angle.Text));
                            }
                        }
                        else if (motion == 7)
                        {
                            Clear();
                            move.Text = "Knee flexion left";
                            viewer.DrawBody(body);
                            angle7.Update(body.Joints[st7], body.Joints[ce7], body.Joints[en7], 50); //knee
                            Angle.Text = Math.Abs((-1*((int)angle7.Angle - 180))).ToString();

                            if (t == 1)
                            {
                                list.Add(int.Parse(Angle.Text));
                            }
                        }
                        else if (motion == 8)
                        {
                            Clear();
                            move.Text = "Shoulder flexion left";
                            viewer.DrawBody(body);
                            angle6.Update(body.Joints[st6], body.Joints[ce6], body.Joints[en6], 50); //shoulder
                            Angle.Text = Math.Abs( (-1*((int)angle6.Angle - 240))).ToString();

                            if (t == 1)
                            {
                                list.Add(int.Parse(Angle.Text));
                            }
                        }
                        else if (motion == 9)
                        {
                            Clear();
                            move.Text = "Elbow flexion left";
                            viewer.DrawBody(body);
                            angle5.Update(body.Joints[st5], body.Joints[ce5], body.Joints[en5], 50); //elbow
                            Angle.Text = Math.Abs((((int)angle5.Angle - 180))) .ToString();

                            if(t == 1)
                            {
                                list.Add(int.Parse(Angle.Text));
                            }
                        }
                        else if (motion == 10)
                        {
                            Clear();
                            time.Text = "Done";
                            move.Text = "Done";
                            timer1.Tick += new EventHandler(timer1_Stop);

                            DialogResult result1 = System.Windows.Forms.MessageBox.Show("Do you want to save ?","Save",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            if (result1 == DialogResult.Yes)
                            {
                                NavigationService.Navigate(new InformationPage(list));
                            }
                            
                        }
                    }
                }
            }
        }

        int t;
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Enabled = true;
            t =6;

    }

        private void timer1_Tick(object sender, EventArgs e)
        {
            t--;
            time.Text = t.ToString();
            if(t == 0)
            {

                motion++;
                t = 6;
                timer1.Start();
            }
        }

        private void timer1_Stop(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void Clear()
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
        }
    }
}
