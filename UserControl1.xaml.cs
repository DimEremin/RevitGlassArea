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
using Autodesk.Revit.DB;

namespace RevitAddin3
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : Window
    {
        public Document document;
        public UserControl1(Document document)
        {
            InitializeComponent();
            this.document = document;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextBlock1.Text = Calc.GlassArea(document).ToString();

        }
    }
}
