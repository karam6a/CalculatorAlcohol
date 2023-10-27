using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
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
using MaterialSkin.Controls;
namespace CalculatorAlcohol
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            quantityComboBox.IsDropDownOpen = false;

            Calculating.fillComboBoxFromDictionary(typeComboBox);
        }

        private void typeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedValue = typeComboBox.SelectedItem.ToString();

            int EstimatedCapacity = Calculating.findContainerTypeInDictinory(selectedValue);

            if (EstimatedCapacity > 0)
            {
                capacityTextField.Text = EstimatedCapacity.ToString();
            }
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string containerType = typeComboBox.Text;
            bool containsDigits = containerType.Any(char.IsDigit);



            //обработка данных поля с типом сосуда
            if (containsDigits)
            {
                //если поле содержит неверный формат данных
                MessageBox.Show("Field Glass type cannot contain numbers! Put correct data.");
                typeComboBox.Text = "";
                return;
            }

            //обработка данных поля с объемом сосуда
            string containerCapacityText = capacityTextField.Text;
            int containerCapacity;
            if (string.IsNullOrEmpty(containerCapacityText))
            {
                containerCapacity = 0;
                //пустая строка
            }
            else
            {
                if (int.TryParse(capacityTextField.Text, out containerCapacity))
                {
                    //all good
                }
                else
                {
                    //если поле содержит неверный формат данных
                    MessageBox.Show("Field Capacity cannot contain letters! Put correct data.");
                    capacityTextField.Text = "";
                    return;
                }
            }

            //обработка данных поля с количеством сосудов
            string numberOfContainersText = quantityComboBox.Text;
            int numberOfcontainers;
            if (string.IsNullOrEmpty(numberOfContainersText))
            {
                //делаю проверку на пустую строку
                MessageBox.Show("Field Quantity cannot be empty! Put data in it.");
                return;
             
            }
            else
            {
                if (int.TryParse(quantityComboBox.Text, out numberOfcontainers))
                {

                }
                else
                {
                    //если поле содержит неверный формат данных
                    MessageBox.Show("Field Quantity cannot contain letters! Put correct data.");
                    quantityComboBox.Text = "";
                    return;
                }
            }

            //обработка данных поля с процентным содержанием спирта
            string alcoholProcentText = percentTextBox.Text;
            float alcoholProcent;
            if (string.IsNullOrEmpty(alcoholProcentText))
            {
                //делаю проверку на пустую строку
                MessageBox.Show("Field % cannot be empty! Put data in it.");
                return;
               
            }
            else { 
                if (float.TryParse(percentTextBox.Text, out alcoholProcent))
                {

                }
                else
                {
                    //если поле содержит неверный формат данных
                    MessageBox.Show("Field % cannot contain letters! Put correct data.");
                    percentTextBox.Text = "";
                    return;
                }
            }
            //Проверка на случай если поля с типом и объемом пустые
            if(typeComboBox.Text == "" && capacityTextField.Text == "")
            {
                MessageBox.Show("Fields Glass type and Volume in ml cannot be empty together!\n Put data in one of them.");
                return;
            }


            //заполнение конструкторов в зависимости от данных, введенных пользователем 
            if (containerCapacity == 0)
            {
                Calculating calculatingWithoutCapacity = new Calculating(containerType, numberOfcontainers, alcoholProcent);
                outputLabel.Content = Calculating.calculateAlcohol(calculatingWithoutCapacity);

            }
            else if (containerType == "")
            {
                Calculating calculatingWithoutType = new Calculating(containerCapacity, numberOfcontainers, alcoholProcent);
                outputLabel.Content = Calculating.calculateAlcohol(calculatingWithoutType);
            }   
            else
            {
                Calculating calculatingFullInfo = new Calculating(containerType, containerCapacity, numberOfcontainers, alcoholProcent);
                outputLabel.Content = Calculating.calculateAlcohol(calculatingFullInfo);
            }
           
        }


    }
}
