using System;
using System.Collections;
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
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for InformationPage.xaml
    /// </summary>
    public partial class InformationPage : Page
    {
        int listcount=0,st=0;
        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\KoKoR\\Source\\Repos\\CS402_Project_MotionAnalysis\\WpfApp1\\localdb\\local_db.mdf;Integrated Security=True");
        public InformationPage(System.Collections.ArrayList list)
        {
            InitializeComponent();

           /* while (true)
            {
                if (list.Count != 150)
                {
                    list.Add(0);
                }
                else
                {
                    break;
                }
            }*/

            Setangle(list);
        }

        public void Setangle(ArrayList list)
        {
            int angle= 0;
            listcount = list.Count;
            for(int i = 1; i < (listcount +1); i++)
            {
                if(i%15 == 0)
                {
                    //ArrayList num = list.GetRange(st,i);
                    //int mode = FindMode(num);
                    int mode =(int) list[(i-15)+6];
                    switch (angle)
                    {
                        case 0:
                            hipextensionright.Text = mode.ToString();
                            break;
                        case 1:
                            hipflexionright.Text = mode.ToString();
                            break;
                        case 2:
                            kneeflexionright.Text = mode.ToString();
                            break;
                        case 3:
                            shoulderflexionright.Text = mode.ToString();
                            break;
                        case 4:
                            elbowflexionright.Text = mode.ToString();
                            break;
                        case 5:
                            hipextensionleft.Text = mode.ToString();
                            break;
                        case 6:
                            hipflexionleft.Text = mode.ToString();
                            break;
                        case 7:
                            kneeflexionleft.Text = mode.ToString();
                            break;
                        case 8:
                            shoulderflexionleft.Text = mode.ToString();
                            break;
                        case 9:
                            elbowflexionleft.Text = mode.ToString();
                            break;
                    }
                    angle++;
                    st = st + 15;
                }
            }
        }

       /* private int FindMode(ArrayList num)
        {
            int [] array = num.ToArray(typeof(int)) as int[];

            int mode = array.GroupBy(v => v)
            .OrderByDescending(g => g.Count())
            .First()
            .Key;

            return mode;
        }*/

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }


        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string sex = "N";
            if(gender1.IsChecked == true)
            {
                sex = "M";
            }
            else
            {
                sex = "F";
            }

            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "insert into [user] (firstname , lastname , gender , brithday , hipextensionright , hipflexionright , kneeflexionright , shoulderflexionright , elbowflexionright , hipextensionleft , hipflexionleft , kneeflexionleft , shoulderflexionleft , elbowflexionleft) " +
                "values ('"+ fname.Text + "' , '" + lname.Text + "' , '" + sex + "' , '" + date.SelectedDate.Value.Date.ToShortDateString().ToString() + "' , '" + hipextensionright.Text + "', '" + hipflexionright.Text + "' , '" + kneeflexionright.Text + "' , '" + shoulderflexionright.Text + "' , '" + elbowflexionright.Text + "' " +
                ", '" + hipextensionleft.Text + "' , '" + hipflexionleft.Text + "' , '" + kneeflexionleft.Text + "' , '" + shoulderflexionleft.Text + "' , '" + elbowflexionleft.Text + "')";
            cmd.ExecuteNonQuery();
            connection.Close();
            fname.Text = "";
            lname.Text = "";
            gender1.IsChecked = false;
            gender2.IsChecked = false;
            date.Text = "";

            DialogResult result1 = System.Windows.Forms.MessageBox.Show("Save information Success", "Information", MessageBoxButtons.OK);
            if(result1 == DialogResult.OK)
            {
                NavigationService.Navigate(new MainPage());
            }
        }
    }
            
 }
