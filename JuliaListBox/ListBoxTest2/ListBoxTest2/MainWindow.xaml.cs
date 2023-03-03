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
using System.IO;
using System.Windows.Controls.Primitives;

namespace ListBoxTest2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string path = "C:\\Users\\hmh\\Desktop\\juliatestfile.txt";
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                //Update the ListBox content from the textfile
                string[] lines = File.ReadAllLines(path);
                foreach (string line in lines)
                {
                    MyList.Items.Add(line);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Add_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                 // Append text to the file
                File.AppendAllText(path, tb_content.Text + Environment.NewLine );
                
                //Add item to our ListBox
                MyList.Items.Add(tb_content.Text);
                
                //Reseting content of the TexBox 
                tb_content.Text = "";
            }
            catch(Exception ex)
            {
                 MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Show_index_button_Click(object sender, RoutedEventArgs e)
        {
            try{
                 //Clear the items in the listbox control Gui
                MyList.Items.Clear();

                //Open the file to read
                string[] lines = File.ReadAllLines(path);

                //Adding each line with its index to the ListBox
                for(int i = 0; i < lines.Length; i++){
                    MyList.Items.Add(i.ToString() + ": " + lines[i]);
                }

                //Updating label content 
                lb_index.Content = MyList.SelectedIndex;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error showing index from file: " + ex.Message);
            }
           
        }

        private void Delete_button_Click(object sender, RoutedEventArgs e)
        {
            try{
                if (MyList.SelectedIndex >= 0)
                {
                    if (MessageBox.Show("Are you sure?",
                    "Delete item",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        //Deleting item from the ListBox
                        MyList.Items.RemoveAt(MyList.SelectedIndex);
                    }
                }
                else
                {
                    // Show an error message if no item is selected
                    MessageBox.Show("Please select an item to delete.",
                        "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                }

                // Update the new content to that file
                string[] items = new string[MyList.Items.Count];
                for(int counter = 0; counter < MyList.Items.Count; counter++)
                {
                    items[counter] = MyList.Items[counter].ToString();
                }
                File.WriteAllLines(path, items);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error deleting data: " + ex.Message);
            }
        }

        private void Save_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get the items from the ListBox
                string[] items = new string[MyList.Items.Count];
                for (int counter = 0; counter < MyList.Items.Count; counter++)
                {
                    items[counter] = MyList.Items[counter].ToString();
                }

                // Write the items to the file
                File.WriteAllLines(path, items);

                // Update the saved label
                saved_lbl.Content = "Last saved: " + DateTime.Now.ToString("H:mm");

                // Show a success message
                MessageBox.Show("Data saved!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                // Show an error message if something goes wrong
                MessageBox.Show("Error saving data: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
