using PriyatnyyShelest.Models;
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

namespace PriyatnyyShelest
{
    /// <summary>
    /// Логика взаимодействия для AgentsListPage.xaml
    /// </summary>
    public partial class AgentsListPage : Page
    {
        private int _currentPage = 1;
        private int _maxPage = 1;
        public AgentsListPage()
        {
            InitializeComponent();
            var context = PriyatnyyShelestDbEntities1.GetContext();
            var agents = context.VW_AgentDetails.ToList();
 
            _maxPage = agents.Count / 10;
            if (agents.Count % 10 > 1)
                _maxPage++;
            UpdateContext(1);
        }

        private void UpdateContext(int page)
        {
            if (FirstBtn is null)
                return;

            var context = PriyatnyyShelestDbEntities1.GetContext();
            var SortedAgents = context.VW_AgentDetails.Where(x => x.Наименование_агента.Contains(SearchQueryText.Text)).ToList();
            
            _maxPage = SortedAgents.Count / 10;
            if (SortedAgents.Count % 10 > 1)
                _maxPage++;

            if(AgentsList.SelectedItems.Count != 0)
            {
                ChangePriorityBtn.Visibility = Visibility.Visible;
            }
            else
            {
                ChangePriorityBtn.Visibility = Visibility.Hidden;
            }

            AgentsList.ItemsSource = SortedAgents.Skip((page * 10) - 10).Take(10);
            if (page == 1)
            {
                FirstBtn.Content = page.ToString();
                SecondBtn.Content = (page + 1).ToString();
                ThirdBtn.Content = (page + 2).ToString();
                FourthBtn.Content = (page + 3).ToString();

                FirstBtn.FontWeight = FontWeights.Bold;
                SecondBtn.FontWeight = FontWeights.Normal;
                ThirdBtn.FontWeight = FontWeights.Normal;
                FourthBtn.FontWeight = FontWeights.Normal;
            }
            else
            {
                switch (_maxPage - page)
                {
                    case 0:
                        FirstBtn.Content = (page - 3).ToString();
                        SecondBtn.Content = (page - 2).ToString();
                        ThirdBtn.Content = (page - 1).ToString();
                        FourthBtn.Content = page.ToString();

                        FirstBtn.FontWeight = FontWeights.Normal;
                        SecondBtn.FontWeight = FontWeights.Normal;
                        ThirdBtn.FontWeight = FontWeights.Normal;
                        FourthBtn.FontWeight = FontWeights.Bold;
                        break;
                    case 1:
                        FirstBtn.Content = (page - 2).ToString();
                        SecondBtn.Content = (page - 1).ToString();
                        ThirdBtn.Content = page.ToString();
                        FourthBtn.Content = (page + 1).ToString();

                        FirstBtn.FontWeight = FontWeights.Normal;
                        SecondBtn.FontWeight = FontWeights.Normal;
                        ThirdBtn.FontWeight = FontWeights.Bold;
                        FourthBtn.FontWeight = FontWeights.Normal;
                        break;
                    default:
                        FirstBtn.Content = (page - 1).ToString();
                        SecondBtn.Content = page.ToString();
                        ThirdBtn.Content = (page + 1).ToString();
                        FourthBtn.Content = (page + 2).ToString();

                        FirstBtn.FontWeight = FontWeights.Normal;
                        SecondBtn.FontWeight = FontWeights.Bold;
                        ThirdBtn.FontWeight = FontWeights.Normal;
                        FourthBtn.FontWeight = FontWeights.Normal;
                        break;
                }
            }
        }
        private void PrevBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 1)
                _currentPage--;
            UpdateContext(_currentPage);
        }

        private void NumberBtn_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = Int32.Parse((sender as Button).Content.ToString());
            UpdateContext(_currentPage);
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage < _maxPage)
                _currentPage++;
            UpdateContext(_currentPage);
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateContext(_currentPage);
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateContext(_currentPage);
        }

        private void SearchQueryText_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateContext(_currentPage);
        }

        private void ChangePriorityBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AgentsList.SelectedItems.Count == 1)
            {
                var selectedItems = AgentsList.SelectedItems;
                var win = new AgentEditPriority(selectedItems[0] as VW_AgentDetails);
                win.ShowDialog();
                UpdateContext(_currentPage);
            }
                
        }

        private void AgentsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateContext(_currentPage);
        }
    }
}
