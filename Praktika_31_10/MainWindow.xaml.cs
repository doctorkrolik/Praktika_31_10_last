using System;
using System.Linq;
using System.Windows;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Data;
using Syncfusion.Data;
using Syncfusion.Pdf;

using Syncfusion.Pdf.Grid;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Converters;
using Syncfusion.Pdf.Graphics;

namespace Praktika_31_10
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            dataGrid.ItemsSource = PaymentDBEntities.GetContext().payments.ToList();
            categoryCombo.ItemsSource = PaymentDBEntities.GetContext().categories.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddPaymentWindow addPaymentWindow = new AddPaymentWindow();
            addPaymentWindow.Show();
            this.IsEnabled = false;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            IsEnabled = true;
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.IsEnabled = false;
        }

        private void mainWindowBtnMinus_Click(object sender, RoutedEventArgs e)
        {
            var currentItem = dataGrid.SelectedItem;

            if (MessageBox.Show("Вы действительно хотите удалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                PaymentDBEntities.GetContext().payments.Remove(currentItem as payments);
                PaymentDBEntities.GetContext().SaveChanges();

                dataGrid.ItemsSource = PaymentDBEntities.GetContext().payments.ToList();
            }
        }

        private void categoryCombo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(categoryCombo.SelectedItem != null)
            {
                var id = (categoryCombo.SelectedItem as categories).PK_category_id;

                dataGrid.ItemsSource = PaymentDBEntities.GetContext().payments
                    .Where(p => p.products.categories.PK_category_id == id)
                    .ToList();
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (datepicker1.SelectedDate != null && datepicker2.SelectedDate != null)
            {
                dataGrid.ItemsSource = PaymentDBEntities.GetContext().payments
                    .Where(p => p.date >= datepicker1.SelectedDate && p.date <= datepicker2.SelectedDate)
                    .ToList();
            }
        }

        private void datepicker2_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (datepicker1.SelectedDate != null && datepicker2.SelectedDate != null)
            {
                dataGrid.ItemsSource = PaymentDBEntities.GetContext().payments
                    .Where(p => p.date >= datepicker1.SelectedDate && p.date <= datepicker2.SelectedDate)
                    .ToList();
            }
        }

        private void reportBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            datepicker1.SelectedDate = null;
            datepicker2.SelectedDate = null;
            categoryCombo.SelectedItem = null;
            dataGrid.SelectedItem = null;
        }

        private void reportBtn_Click_1(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                
            }
        }
    }
}
