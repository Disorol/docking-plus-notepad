using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace plc_soldier_wpf
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

        public void SaveAsFileCommand(object sender, RoutedEventArgs e)
        {
            string fileText = TabItem1.IsSelected ? TextBox1.Text : TextBox2.Text;

            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
            };

            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, fileText);

                ApplicationState.SetValue("FileName", dialog.FileName);
            }
        }

        public void SaveFileCommand(object sender, RoutedEventArgs e)
        {
            string fileText = TabItem1.IsSelected ? TextBox1.Text : TextBox2.Text;

            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
            };

            if (ApplicationState.GetValue<string>("FileName") != null)
            {
                dialog.FileName = ApplicationState.GetValue<string>("FileName");

                File.WriteAllText(dialog.FileName, fileText);
            }
            else if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, fileText);

                ApplicationState.SetValue("FileName", dialog.FileName);
            }
        }

        public static class ApplicationState
        {
            private static Dictionary<string, object> _values =
                       new Dictionary<string, object>();
            public static void SetValue(string key, object value)
            {
                if (_values.ContainsKey(key))
                {
                    _values.Remove(key);
                }
                _values.Add(key, value);
            }
            public static T GetValue<T>(string key)
            {
                if (_values.ContainsKey(key))
                {
                    return (T)_values[key];
                }
                else
                {
                    return default(T);
                }
            }
        }
    }
}
