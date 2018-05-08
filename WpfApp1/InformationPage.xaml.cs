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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for InformationPage.xaml
    /// </summary>
    public partial class InformationPage : Page
    {
        List<int> li = new List<int>();
        public InformationPage(System.Collections.ArrayList list)
        {
            InitializeComponent();
            
            Setangle(list);
        }

        private void Setangle(ArrayList list)
        {
            int sum = 0,angle= 0;
            
            for(int i = 1; i < (list.Count +1); i++)
            {
                if(sum%15 == 0)
                {
                    li.Sort();
                    int temp = Math.Abs(li[li.Count / 2]);
                    li.Clear();
                    switch (angle)
                    {
                        case 0:
                            hipextensionright.Text = temp.ToString();
                            break;
                        case 1:
                            hipflexionright.Text = temp.ToString();
                            break;
                        case 2:
                            kneeflexionright.Text = temp.ToString();
                            break;
                        case 3:
                            shoulderflexionright.Text = temp.ToString();
                            break;
                        case 4:
                            elbowflexionright.Text = temp.ToString();
                            break;
                        case 5:
                            hipextensionleft.Text = temp.ToString();
                            break;
                        case 6:
                            hipflexionleft.Text = temp.ToString();
                            break;
                        case 7:
                            Kneeflexionleft.Text = temp.ToString();
                            break;
                        case 8:
                            shoulderflexionleft.Text = temp.ToString();
                            break;
                        case 9:
                            elbowflexionleft.Text = temp.ToString();
                            break;
                    }
                    angle++;
                }
                li.Add((int)list[i-1]);
                Console.WriteLine("i = {0} number {1}",i-1,list[i - 1]);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }


        private void Save_Click(object sender, RoutedEventArgs e)
        {
         
        }
    }
}
