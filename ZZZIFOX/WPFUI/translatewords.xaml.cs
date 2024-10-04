using Autodesk.AutoCAD.Internal;
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

namespace ZZZIFOX.WPFUI
{
    /// <summary>
    /// translatewords.xaml 的交互逻辑
    /// </summary>
    public partial class translatewords : Window
    {
        // 词库文件路径
        public string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "transWords.txt");
        public List<DictionaryEntry> DictionaryEntries { get; set; }
        public class DictionaryEntry
        {
            public string Chinese { get; set; }
            public string English { get; set; }
        }


        public translatewords()
        {
            InitializeComponent();
            LoadDictionary();
            DictionaryDataGrid.ItemsSource = DictionaryEntries;
        }
        public void LoadDictionary()
        {
            DictionaryEntries = new List<DictionaryEntry>();

            if (File.Exists(filePath))
            {
                foreach (var line in File.ReadAllLines(filePath))
                {
                    var parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        DictionaryEntries.Add(new DictionaryEntry { Chinese = parts[0].Trim(), English = parts[1].Trim() });
                    }
                }
            }
            else
            {
                File.Create(filePath).Close();
            }
        }

        private void insert_Click(object sender, RoutedEventArgs e)
        {
           
            DictionaryEntries.Add(new DictionaryEntry { Chinese = "", English = "" });
            DictionaryDataGrid.Items.Refresh();

        }

        // 查找重复项的方法
        private List<string> FindDuplicates()
        {
            var duplicates = new List<string>();
            var chineseEntries = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var englishEntries = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < DictionaryEntries.Count; i++)
            {
                var entry = DictionaryEntries[i];

                // 检查中文是否重复
                if (!chineseEntries.Add(entry.Chinese))
                {
                    duplicates.Add($"行 {i + 1} 的中文 '{entry.Chinese}' 与之前的某一行重复");
                }

                // 检查英文是否重复
                if (!englishEntries.Add(entry.English))
                {
                    duplicates.Add($"行 {i + 1} 的英文 '{entry.English}' 与之前的某一行重复");
                }
            }

            return duplicates;
        }






        private void save_Click(object sender, RoutedEventArgs e)
        {
            // 查找重复项
            var duplicateEntries = FindDuplicates();

            if (duplicateEntries.Any())
            {
                // 生成提示信息
                string message = "发现重复的条目:\n" + string.Join("\n", duplicateEntries);

                // 弹出提示框
                System.Windows.MessageBox.Show(message, "重复条目提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var entry in DictionaryEntries)
                {
                    writer.WriteLine($"{entry.Chinese},{entry.English}");
                }
            }
            System.Windows.MessageBox.Show("数据已保存");
            
        }


        // 数据行删除按钮
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DictionaryDataGrid.SelectedItem is DictionaryEntry selectedEntry)
            {
                DictionaryEntries.Remove(selectedEntry);
                DictionaryDataGrid.Items.Refresh();
                //SaveDictionary(); // 更新词库文件
            }
            else
            {
                System.Windows.MessageBox.Show("请选中一个词条进行删除！");
            }

        }

        private void seachbutton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchText))
            {
                System.Windows.MessageBox.Show("请输入搜索词！");
                // 显示所有词条
                DictionaryDataGrid.ItemsSource = DictionaryEntries;
                return;
            }

            // 在词库中查找（不区分大小写）
            var foundEntries = DictionaryEntries
                .Where(entry => entry.Chinese.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                entry.English.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            DictionaryDataGrid.ItemsSource = foundEntries;

            if (foundEntries.Count == 0)
            {
                System.Windows.MessageBox.Show("没有找到匹配的词条！");
                // 如果没有找到匹配项，可以选择是否显示所有词条
                // DictionaryDataGrid.ItemsSource = DictionaryEntries; // 取消注释以显示所有词条
            }
        }
    }
}
