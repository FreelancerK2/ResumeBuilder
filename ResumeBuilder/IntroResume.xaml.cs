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
using System.Windows.Shapes;

namespace ResumeBuilder
{
    /// <summary>
    /// Interaction logic for IntroResume.xaml
    /// </summary>
    public partial class IntroResume : Window
    {
        private void CreateCvButton_Click(object sender, RoutedEventArgs e)
        {
            double currentTop = this.Top;
            double currentLeft = this.Left;

            // Open the Resume Creation Page with TabItems
            MainWindow resumePage = new MainWindow();

            resumePage.Top = currentTop;
            resumePage.Left = currentLeft;

            resumePage.Show();
            this.Close();
        }
    }
}
