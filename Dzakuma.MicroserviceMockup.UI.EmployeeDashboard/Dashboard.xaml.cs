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
using Newtonsoft.Json.Linq;

namespace Dzakuma.MicroserviceMockup.UI.EmployeeDashboard
{
	/// <summary>
	/// Interaction logic for Dashboard.xaml
	/// </summary>
	public partial class Dashboard : Window
	{
		private string _executablePath = @"C:\repositories\Dzakuma.MicroserviceMockup.EmployeeData\Dzakuma.MicroserviceMockup.EmployeeData\bin\Debug\Dzakuma.MicroserviceMockup.EmployeeData.exe";
		private InterprocessDataSiphon _dataSiphon = new InterprocessDataSiphon();

		public Dashboard()
		{
			InitializeComponent();
		}

		private void GeneralEmployeeDataRefresh_OnClick(object sender, RoutedEventArgs e)
		{
			var data = _dataSiphon.DeserializeJsonString(_executablePath, "--all");
			PersonnelList.Items.Clear();

			foreach (var child in data.personnelList.Children<JObject>())
			{
				//var a = (string) child["id"];
				//var b = (string) child["first_name"];
				//var c = (string)child["last_name"];
				//var properties = child.Children<JProperty>();
				PersonnelList.Items.Add(
					new {
						Id = (string)child["id"],
						FirstName = (string)child["first_name"],
						LastName = (string)child["last_name"]
					}
				);
			}
		}
	}
}
