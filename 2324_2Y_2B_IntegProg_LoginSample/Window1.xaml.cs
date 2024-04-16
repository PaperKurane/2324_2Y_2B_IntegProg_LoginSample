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

namespace _2324_2Y_2B_IntegProg_LoginSample
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        DataClassDataContext _dbConn = null;
        string _currentUser = "";

        public Window1()
        {
            InitializeComponent();
        }

        public Window1(string userName)
        {
            InitializeComponent();

            _currentUser = userName;

            _dbConn = new DataClassDataContext(
                Properties.Settings.Default._2324_2B_LoginSampleConnectionString);

            IQueryable<tblLogin> selectResults = from s in _dbConn.tblLogins
                                                 where s.LoginID == _currentUser
                                                 select s;

            if (selectResults.Count() == 1)
            {
                foreach (tblLogin s in selectResults)
                {
                    lbWelcome.Content = $"Welcome {s.LoginName}!";

                    tblLogin tlogin = new tblLogin();
                    tlogin.LoginID = "";

                    _dbConn.tblLogins.InsertOnSubmit(tlogin);
                    break;
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
        }
    }
}
