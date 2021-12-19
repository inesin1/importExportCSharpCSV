using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace testImportExport
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Product> products;
        public string[] table;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void import() {
            int cellsCount = productsTable.Columns.Count; //Количество столбцов
            products = new List<Product>(cellsCount); //Создаем список для хранения данных
            string file; //Переменная для файла из которого импортируем

            OpenFileDialog OPF = new OpenFileDialog(); //Создаем диалоговое окно для выбора файла
            OPF.Filter = "Файл Microsoft Excel, содержащий значения, разделенные запятыми(.csv)| *.csv";
            if (OPF.ShowDialog() ?? true) //Если выбрали файл то..
            {
                file = OPF.FileName; //Берем его путь и закидуем в перемнную file
                table = File.ReadAllLines(file); //Читаем из файла все строки в массив table
            }
            else {
                return;
            }

            try
            {
                for (int i = 1; i < table.Length; i++) //Пробиваем весь массив начиная с первого элемента тк там у нас названия столбцов
                {
                    string[] cells = table[i].Split(new char[] { ';' });

                    products.Add(new Product(
                        cells[0],
                        cells[1],
                        cells[2],
                        cells[3],
                        cells[4]
                        ));
                }

                productsTable.ItemsSource = products;
            }
            catch (Exception e) {
                MessageBox.Show($"Ошибка {e}\n\n" +
                    "Невозможно импортировать файл");
            }
        }
        private void export() {
            string file;

            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "Файл Microsoft Excel, содержащий значения, разделенные запятыми (.csv)|*.csv";
            if (OPF.ShowDialog() ?? true)
            {
                file = OPF.FileName;
            }
            else
            {
                return;
            }

            try
            {
                StreamWriter sw = new StreamWriter(file, false, System.Text.Encoding.UTF8);
                for (int i = -1; i < products.Count; i++)
                {
                    if (i == -1)
                    {
                        sw.WriteLine(productsTable.Columns[0].Header + ";" +
                            productsTable.Columns[1].Header + ";" +
                            productsTable.Columns[2].Header + ";" +
                            productsTable.Columns[3].Header + ";" +
                            productsTable.Columns[4].Header + ";"
                            );
                    }
                    else
                    {

                        sw.WriteLine(products[i].ID + ";" +
                            products[i].ProductName + ";" +
                            products[i].Category + ";" +
                            products[i].Unit + ";" +
                            products[i].UnitPrice
                            );
                    }
                }
                sw.Close();

                MessageBox.Show("Экспорт выполнен!");
            }
            catch (Exception e)
            {
                MessageBox.Show($"Ошибка {e}\n\n" +
                    "Невозможно экспортировать файл");
            }
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            import();
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            export();
        }
    }
}
