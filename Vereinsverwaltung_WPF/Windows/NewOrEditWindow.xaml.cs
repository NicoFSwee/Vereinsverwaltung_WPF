using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Vereinsverwaltung.Model;

namespace Vereinsverwaltung_WPF.Windows
{
    /// <summary>
    /// Interaction logic for NewOrEditWindow.xaml
    /// </summary>
    public partial class NewOrEditWindow : Window
    {
        Member _member;

        public NewOrEditWindow(Member member)
        {
            InitializeComponent();

            Loaded += NewOrEditWindow_Loaded;
            _member = member;

            if(_member == null)
            {
                tbEdit.Visibility = Visibility.Hidden;
                tbNew.Visibility = Visibility.Visible;
            }
            else
            {
                tbEdit.Visibility = Visibility.Visible;
                tbNew.Visibility = Visibility.Hidden;
            }
        }

        private void NewOrEditWindow_Loaded(object sender, RoutedEventArgs e)
        {
            btnCancle.Click += BtnCancle_Click;
            btnSave.Click += BtnSave_Click;

            if(_member != null)
            {
                DataContext = new Member() { FirstName = _member.FirstName, LastName = _member.LastName, DateOfBirth = _member.DateOfBirth };
            }
            else
            {
                DataContext = new Member();
            }
            
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_member != null)
            {
                Repository.GetInstance().DeleteMember(_member);
                Repository.GetInstance().AddMember(DataContext as Member);
            }
            else
            {
                Repository.GetInstance().AddMember(DataContext as Member);
            }

            Close();
        }

        private void BtnCancle_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
