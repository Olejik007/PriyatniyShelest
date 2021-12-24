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
using System.Windows.Shapes;

namespace PriyatnyyShelest
{
    /// <summary>
    /// Логика взаимодействия для AgentEditPriority.xaml
    /// </summary>
    public partial class AgentEditPriority : Window
    {
        private static Agents _editableAgent;
        public AgentEditPriority(VW_AgentDetails agent)
        {
            InitializeComponent();
            var context = PriyatnyyShelestDbEntities1.GetContext();
            var Agent = context.Agents.FirstOrDefault(x => x.IdAgent == agent.IdAgent);

            _editableAgent = Agent;
            DataContext = _editableAgent;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var context = PriyatnyyShelestDbEntities1.GetContext();
            var agent = context.Agents.FirstOrDefault(x => x.IdAgent == _editableAgent.IdAgent);
            agent.Priority = _editableAgent.Priority;
            context.SaveChanges();
            Close();
        }
    }
}
