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
using Vereinsverwaltung.Model;
using Vereinsverwaltung_WPF.Windows;

namespace Vereinsverwaltung_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Member> _members;
        private Member _selectedMember;
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
            btnNew.Click += BtnNew_Click;
            btnDelete.Click += BtnDelete_Click;
            btnEdit.Click += BtnEdit_Click;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            _selectedMember = listViewMembers.SelectedItem as Member;

            if (_selectedMember == null)
            {
                MessageBox.Show("Wählen Sie ein Mitglied zum bearbeiten aus!");
                return;
            }

            NewOrEditWindow newOrEditWindow = new NewOrEditWindow(_selectedMember);
            newOrEditWindow.ShowDialog();

            PopulateListView();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            _selectedMember = listViewMembers.SelectedItem as Member;

            if(_selectedMember == null)
            {
                MessageBox.Show("Wählen Sie ein Mitglied zum löschen aus!");
                return;
            }

            Repository.GetInstance().DeleteMember(_selectedMember);

            PopulateListView();
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            NewOrEditWindow newOrEditWindow = new NewOrEditWindow(_selectedMember);
            newOrEditWindow.ShowDialog();

            PopulateListView();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateListView();
        }

        private void PopulateListView()
        {
            _members = Repository.GetInstance().GetAllMembers();
            listViewMembers.ItemsSource = _members;
        }
    }
}
