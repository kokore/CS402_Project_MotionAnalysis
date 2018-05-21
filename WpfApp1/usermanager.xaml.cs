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
    /// Interaction logic for usermanager.xaml
    /// </summary>
    public partial class usermanager : Page
    {
        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\KoKoR\\Source\\Repos\\CS402_Project_MotionAnalysis\\WpfApp1\\localdb\\local_db.mdf;Integrated Security=True");
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
            Filldata();
        }

        private void Filldata()
        {
            connection.Open();
            SqlCommand com = new SqlCommand("SELECT * FROM user",connection);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(com);
            da.Fill(ds);
            myDataGrid.ItemsSource = ds.Tables[0].DefaultView;

        }
    }
}
