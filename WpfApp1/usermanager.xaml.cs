using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for usermanager.xaml
    /// </summary>
    public partial class usermanager : Page
    {
        //connection local db
        SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\KoKoR\\Source\\Repos\\CS402_Project_MotionAnalysis\\WpfApp1\\localdb\\local_db.mdf;Integrated Security=True");
        public usermanager()
        {
            InitializeComponent();

            
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Filldata(); //load all data to datagrid
        }

        private void Filldata() //connection db and load data to datagrid
        {
            conn.Open();
            SqlCommand comm = new SqlCommand("SELECT * FROM [user]", conn);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(comm);
            da.Fill(ds);
            myDataGrid.ItemsSource = ds.Tables[0].DefaultView;
            conn.Close();
        }

        private void insert_Click(object sender, RoutedEventArgs e) //insert data 
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            string sex = "N"; //choose radio button
            if (gender1.IsChecked == true)
            {
                sex = "M";
            }
            else
            {
                sex = "F";
            }
            //fill data
            cmd.CommandText = "insert into [user] (firstname , lastname , gender , brithday , hipextensionright , hipflexionright , kneeflexionright , shoulderflexionright , elbowflexionright , hipextensionleft , hipflexionleft , kneeflexionleft , shoulderflexionleft , elbowflexionleft) " +
                "values ('" + firstnameinput.Text + "' , '" + lastnameinput.Text + "' , '" + sex + "' , '" + date.SelectedDate.Value.Date.ToShortDateString().ToString() + "' , '" + herinput.Text + "', '" + hfrinput.Text + "' , '" + kfrinput.Text + "' , '" + sfrinput.Text + "' , '" + efrinput.Text + "' " +
                ", '" + helinput.Text + "' , '" + hflinput.Text + "' , '" + kflinput.Text + "' , '" + sflinput.Text + "' , '" + eflinput.Text + "')";
            cmd.ExecuteNonQuery(); 
            conn.Close();
            //warn user
            DialogResult result1 = System.Windows.Forms.MessageBox.Show("Insert Success", "Information", MessageBoxButtons.OK);
            if (result1 == DialogResult.OK)
            {
                Filldata(); //Reload griddata
            }
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            if(String.IsNullOrEmpty(idinput.Text))
            {
                //warn user fill id
                DialogResult result1 = System.Windows.Forms.MessageBox.Show("Please choose id in field", "Information", MessageBoxButtons.OK);
                if (result1 == DialogResult.OK)
                {
                    
                }
            }
            else
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                string sex = "N"; //choose radio button
                if (gender1.IsChecked == true)
                {
                    sex = "M";
                }
                else
                {
                    sex = "F";
                }
                //update db
                cmd = new SqlCommand("update [user] set firstname=@firname , lastname=@lasname ,gender=@gen   ,brithday=@bday" +
                    " ,hipextensionright=@hipexright ,hipflexionright=@hipflexright , kneeflexionright=@kneeflexright " +
                    ", shoulderflexionright=@shoulderflexright ,elbowflexionright=@elbowflexright ,hipextensionleft=@hipextenleft " +
                    ", hipflexionleft=@hipflexleft , kneeflexionleft=@kneeflexleft , shoulderflexionleft=@shouldeflexleft " +
                    ", elbowflexionleft=@elbowflexleft where Id=@id", conn);
                cmd.Parameters.AddWithValue("@id", idinput.Text);
                cmd.Parameters.AddWithValue("@firname", firstnameinput.Text);
                cmd.Parameters.AddWithValue("@lasname", lastnameinput.Text);
                cmd.Parameters.AddWithValue("@gen", sex);
                cmd.Parameters.AddWithValue("@bday", date.SelectedDate.Value.Date.ToShortDateString().ToString() );

                cmd.Parameters.AddWithValue("@hipexright", herinput.Text);
                cmd.Parameters.AddWithValue("@hipflexright", hfrinput.Text);
                cmd.Parameters.AddWithValue("@kneeflexright", kfrinput.Text);
                cmd.Parameters.AddWithValue("@shoulderflexright", sfrinput.Text);
                cmd.Parameters.AddWithValue("@elbowflexright", efrinput.Text);

                cmd.Parameters.AddWithValue("@hipextenleft", helinput.Text);
                cmd.Parameters.AddWithValue("@hipflexleft", hflinput.Text);
                cmd.Parameters.AddWithValue("@kneeflexleft", kflinput.Text);
                cmd.Parameters.AddWithValue("@shouldeflexleft", sflinput.Text);
                cmd.Parameters.AddWithValue("@elbowflexleft", eflinput.Text);
                

                cmd.ExecuteNonQuery();
                conn.Close();
                DialogResult result1 = System.Windows.Forms.MessageBox.Show("Update Success", "Information", MessageBoxButtons.OK);
                if (result1 == DialogResult.OK)
                {
                    Filldata(); //Reload griddata
                }
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(idinput.Text))
            {
                //warn user fill id
                DialogResult result1 = System.Windows.Forms.MessageBox.Show("Please choose id in field", "Warn", MessageBoxButtons.OK);
                if (result1 == DialogResult.OK)
                {

                }
            }
            else
            {
                conn.Open();
                //delete by id
                SqlCommand comm = new SqlCommand("DELETE FROM [user] WHERE Id=" +
                idinput.Text + "", conn);
                comm.ExecuteNonQuery();
                conn.Close();
                //warn user
                DialogResult result1 = System.Windows.Forms.MessageBox.Show("Delete Success", "Information", MessageBoxButtons.OK);
                if (result1 == DialogResult.OK)
                {
                    Filldata();//Reload griddata
                }
            }

        }
    }
}
