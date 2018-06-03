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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Loginpage.xaml
    /// </summary>
    public partial class Loginpage : Page
    {
        public Loginpage()
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

        private void login_btn(object sender, RoutedEventArgs e)
        {
            //connection login id
            SqlConnection sqlcon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\KoKoR\Source\Repos\CS402_Project_MotionAnalysis\WpfApp1\localdb\local_db.mdf;Integrated Security=True");
            string query = "Select * from Login Where username = '" + username_text.Text.Trim() + "' and password = '" + password_text.Password + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);
            if (dtbl.Rows.Count == 1)
            {
                NavigationService.Navigate(new usermanager());
            }
            else
            {
                MessageBox.Show("Wrong username and password !!!");
            }
        }
    }
}
