using System;
using System.Linq;
using System.Windows;

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

            if(MessageBox.Show("Вы действительно хотите удалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                PaymentDBEntities.GetContext().payments.Remove(currentItem as payments);
                PaymentDBEntities.GetContext().SaveChanges();

                dataGrid.ItemsSource = PaymentDBEntities.GetContext().payments.ToList();
            }

           

        }
    }
}
