using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MysqlImage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {



            string hotelNameString = "";
            string hotelEnameString = "";
            string hotelAddressString = "";
            string hotelZipCodeString = "";
            string hotelPhoneString = "";
            string hotelSiteString = "";
            string hotelPicString = "";


            hotelNameString = hotelNameText.Text;
            hotelEnameString = hotelEnameText.Text;
            hotelAddressString = hotelAddressText.Text;
            hotelZipCodeString = hotelZipCodeText.Text;
            hotelPhoneString = hotelPhoneText.Text;
            hotelSiteString = hotelSiteText.Text;
            hotelPicString = hotelPicText.Text;

            if (!hotelNameString.Equals("") && !hotelAddressString.Equals("") && !hotelPhoneString.Equals("") && !hotelPicString.Equals(""))
            {

                MySql.Data.MySqlClient.MySqlConnection conn;
                MySql.Data.MySqlClient.MySqlCommand cmd;

                conn = new MySql.Data.MySqlClient.MySqlConnection();
                cmd = new MySql.Data.MySqlClient.MySqlCommand();


                conn.ConnectionString = "server=125.227.178.230;uid=itri;" + "pwd=iscae100;database=SBGService;CharSet=utf8;";
                

                string SQL;
                UInt32 FileSize;
                byte[] rawData;
                FileStream fs;

                try
                {
                    fs = new FileStream(hotelPicString, FileMode.Open, FileAccess.Read);
                    FileSize = (UInt32)fs.Length;


                    rawData = new byte[FileSize];
                    fs.Read(rawData, 0, (int)FileSize);
                    fs.Close();

                    conn.Open();
                    /*
                    SQL = "INSERT INTO testblob VALUES(@image_type, @image, @image_size, @image_ctgy, @image_name)";

                    cmd.Connection = conn;
                    cmd.CommandText = SQL;
                    cmd.Parameters.AddWithValue("@image_type", "png");
                    cmd.Parameters.AddWithValue("@image", rawData);                
                    cmd.Parameters.AddWithValue("@image_size", FileSize);
                    cmd.Parameters.AddWithValue("@image_ctgy", "test");
                    cmd.Parameters.AddWithValue("@image_name", System.IO.Path.GetFileNameWithoutExtension("d:/image.png"));
                    */

                    SQL = "INSERT INTO Hotels VALUES(@id,@hotelName, @hotelEname, @hotelAddress, @hotelZipCode, @hotelPhone, @hotelSite, @hotelPic, @hotelPicSize)";

                    cmd.Connection = conn;
                    cmd.CommandText = SQL;
                    cmd.Parameters.AddWithValue("@id", 0);
                    cmd.Parameters.AddWithValue("@hotelName", hotelNameString);
                    cmd.Parameters.AddWithValue("@hotelEname", hotelEnameString);
                    cmd.Parameters.AddWithValue("@hotelAddress", hotelAddressString);
                    cmd.Parameters.AddWithValue("@hotelZipCode", hotelZipCodeString);
                    cmd.Parameters.AddWithValue("@hotelPhone", hotelPhoneString);
                    cmd.Parameters.AddWithValue("@hotelSite", hotelSiteString);
                    cmd.Parameters.AddWithValue("@hotelPic", rawData);
                    cmd.Parameters.AddWithValue("@hotelPicSize", FileSize);
                    
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("旅館資料成功新增到資料庫內!","新增成功!", MessageBoxButton.OK);

                    conn.Close();

                    hotelNameText.Text = "";
                    hotelEnameText.Text = "";
                    hotelAddressText.Text = "";
                    hotelZipCodeText.Text = "";
                    hotelPhoneText.Text = "";
                    hotelSiteText.Text = "";
                    hotelPicText.Text = "";

                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    MessageBox.Show("Error " + ex.Number + " has occurred: " + ex.Message,"Error", MessageBoxButton.OK);
                }
            }else
            {
                MessageBox.Show("資料輸入不齊全");
            }


        }

        private void openfile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                hotelPicText.Text = filename;
            }
        }
    }
}
