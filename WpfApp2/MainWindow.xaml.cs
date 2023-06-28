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
using Npgsql;
using Dapper;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public NpgsqlConnection conn = new NpgsqlConnection("Host=localhost;Username=postgres;Password=root;Database=vsdb;");
        public NpgsqlCommand cmd = new NpgsqlCommand();

        public MainWindow()
        {
            InitializeComponent();
            MessageBox.Show("Please note that the data table only shows the data and not affect the input and output of database", "Welcome", MessageBoxButton.OK, MessageBoxImage.Information);
            var cn = conn;
            conn.Open();
            string sqlshow = @"SELECT * FROM patients;";
            NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sqlshow, conn);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            ptinfo.ItemsSource = ds.Tables[0].DefaultView;

        }

        // button action to add new patient
        private void btn_add(object sender, RoutedEventArgs e)
        {
            if (addaddress.Text == "" || addage.Text == "" || addname.Text == "")
            {
                MessageBox.Show("No Data To enter, please fill the spaces", "Data entry error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }



            else
            {
                string sql = @"INSERT INTO patients (name, age, address) VALUES (@ptname, @ptage, @ptaddress);";
                var ptdata = new
                {
                    ptname = addname.Text,
                    ptage = int.Parse(addage.Text),
                    ptaddress = addaddress.Text
                };
                // Refresh DataGrid after adding
                conn.Execute(sql, ptdata);
                //clear spaces after entry successful
                addname.Clear();
                addage.Clear();
                addaddress.Clear();
                //
                string sqlshow = @"SELECT * FROM patients;";
                NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sqlshow, conn);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                ptinfo.ItemsSource = ds.Tables[0].DefaultView;
            }
        }

        // Edit button event with id
        private void btn_edit(object sender, RoutedEventArgs e)
        {

            var ptdata = new
            {

                ptname = editname.Text,
                ptage = int.TryParse(editage.Text, out int ageint),
                ptaddress = editaddress.Text,
                ptid = int.TryParse(ptid.Text, out int id)
            };
            string sql = @"UPDATE patients SET name = @ptname, age = @ptage, address = @ptaddress WHERE id = @ptid";
            if (editname.Text == "" || !ptdata.ptage || editaddress.Text == "" && !ptdata.ptid)
            {
                MessageBox.Show("No Data To enter, please fill the spaces", "Data entry error", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else
            {
                var exe = conn.Execute(sql, ptdata);
                editname.Clear();
                editage.Clear();
                editaddress.Clear();
                ptid.Clear();
            };
            

            // refresh datagrid with changes
            string sqlshow = @"SELECT * FROM patients;";
            NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sqlshow, conn);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            ptinfo.ItemsSource = ds.Tables[0].DefaultView;


            }

        // set delete button
        private void btn_delete(object sender, RoutedEventArgs e)
            {
            bool isint = int.TryParse(ptid.Text, out int id);
            string sql = @"DELETE from patients where id = @id";
            if (isint)
            {
                conn.Execute(sql, new { id });
                ptid.Clear();

            }
            else
            {
                MessageBox.Show("Wrong Id please try again!", "Wrong Entry", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            string sqlshow = @"SELECT * FROM patients;";
            NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sqlshow, conn);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            ptinfo.ItemsSource = ds.Tables[0].DefaultView;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool isint = int.TryParse(searchbar.Text, out int id);
            if (isint)
            {
                string sql = @"SELECT * FROM patients WHERE id = @id";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
                NpgsqlDataAdapter adp = new NpgsqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                ptinfo.ItemsSource = ds.Tables[0].DefaultView;

            }
            else if (searchbar.Text == "")
            {
                string sqlshow = @"SELECT * FROM patients;";
                NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sqlshow, conn);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                ptinfo.ItemsSource = ds.Tables[0].DefaultView;
            }
            else
            {
                string sql = @"SELECT * FROM patients WHERE lower(name) like lower(@item)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@item", "%" + searchbar.Text + "%");
                NpgsqlDataAdapter adp = new NpgsqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                ptinfo.ItemsSource = ds.Tables[0].DefaultView;

            }
        }
    }

    }


