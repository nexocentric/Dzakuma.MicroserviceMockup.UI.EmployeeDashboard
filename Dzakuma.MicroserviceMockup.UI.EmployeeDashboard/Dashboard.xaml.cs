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
			RefreshPersonnelList();
			GetBioInfoormation("1");
			LoadBioPicture("1", "Male");
			LoadAnimalPreference(0);
		}

		private void GeneralEmployeeDataRefresh_OnClick(object sender, RoutedEventArgs e)
		{
			RefreshPersonnelList();
		}

		public int RefreshPersonnelList()
		{
			var data = _dataSiphon.DeserializeJsonString(_executablePath, "--all");
			PersonnelList.Items.Clear();

			foreach (var child in data.personnelList.Children<JObject>())
			{
				PersonnelList.Items.Add(
					new
					{
						Id = (string)child["id"],
						FirstName = (string)child["first_name"],
						LastName = (string)child["last_name"]
					}
				);
			}

			return PersonnelList.Items.Count;
		}

		private void LoadBioPicture(string id, string gender)
		{
			string genderId = gender == "Female" ? "woman" : "man";
			var picture = new BitmapImage();
			picture.BeginInit();
			picture.UriSource = new Uri($@"https://loremflickr.com/300/300/portrait,{genderId}?lock={id}", UriKind.Absolute);
			picture.EndInit();
			Mug.Source = picture;
		}

		public void GetBioInfoormation(string id)
		{
			var data = _dataSiphon.DeserializeJsonString(_executablePath, $"--general --id {id}");
			var employeeData = data.individualData;

			EmployeeName.Text = string.Format(
				"Employee Name: {0} {1}",
				(string)employeeData["first_name"],
				(string)employeeData["last_name"]
			);

			EmployeeDepartment.Text = string.Format(
				"Department: {0}",
				(string)employeeData["department"]
			);
		}

		private void LoadAnimalPreference(int preference)
		{
			string imageUrl = preference == 0 ? @"https://loremflickr.com/200/200/cat" : @"https://placebear.com/200/200";
			var picture = new BitmapImage();
			picture.BeginInit();
			picture.UriSource = new Uri(imageUrl, UriKind.Absolute);
			picture.EndInit();
			AnimalPreference.Source = picture;
		}

		private void PersonelList_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			try
			{
				var selectedItem = (WrapPanel)sender;
				var id = (string)selectedItem.Uid;
				GetBioInfoormation(id);
				LoadBioPicture(id, "Male"); //this is a part of the demo that needs to be changed
				LoadAnimalPreference(0);
			}
			catch (Exception anomaly)
			{
				
			}
		}
	}
}
