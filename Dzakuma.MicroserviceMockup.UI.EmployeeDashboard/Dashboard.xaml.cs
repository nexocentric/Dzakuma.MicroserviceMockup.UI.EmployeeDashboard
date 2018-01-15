using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Dzakuma.MicroserviceMockup.UI.EmployeeDashboard
{
	public partial class Dashboard : Window
	{
		//TODO: Illustrate how you detached output from the user interface so that
		//      it can be tested separate from the user interface
		//      personnelSelector is the class that allows this to happen
		PersonnelInformationSelector _personnelSelector = new PersonnelInformationSelector();
		private string _selectedId = "1";

		public Dashboard()
		{
			InitializeComponent();
			RefreshPersonnelList();
			GetBioInfoormation(_selectedId);
			LoadBioPicture(_selectedId);
			LoadAnimalPreference(_selectedId);
		}

		private void MoviePreferences_OnClick(object sender, RoutedEventArgs e)
		{
			var testProgram = new Process();
			testProgram.StartInfo.FileName = @"C:\repositories\Dzakuma.MicroserviceMockup.UI.MoviePreferences\Dzakuma.MicroserviceMockup.UI.MoviePreferences\bin\Debug\Dzakuma.MicroserviceMockup.UI.MoviePreferences.exe";
			testProgram.StartInfo.Arguments = $"--id {_selectedId}";
			testProgram.StartInfo.UseShellExecute = false;
			testProgram.Start();
			testProgram.WaitForExit();
		}

		private void GeneralEmployeeDataRefresh_OnClick(object sender, RoutedEventArgs e)
		{
			RefreshPersonnelList();
		}

		private void Employee_OnClick(object sender, RoutedEventArgs e)
		{
			var selectedItem = (Button)sender;
			_selectedId = (string)selectedItem.Uid;
			GetBioInfoormation(_selectedId);
			LoadBioPicture(_selectedId);
			LoadAnimalPreference(_selectedId);
		}

		public int RefreshPersonnelList()
		{
			//PersonnelList.Items.Clear();
			PersonnelList.ItemsSource = _personnelSelector.GetPersonnelList();
			return PersonnelList.Items.Count;
		}

		private void LoadBioPicture(string id)
		{
			Mug.Source = LoadImageFromUrl(_personnelSelector.GetBioPictureUrl(id));
		}

		private void LoadAnimalPreference(string id)
		{
			AnimalPreference.Source = LoadImageFromUrl(_personnelSelector.GetAnimalPreferenceUrl(id));
		}

		private BitmapImage LoadImageFromUrl(Uri imageSource)
		{
			var picture = new BitmapImage();
			picture.BeginInit();
			picture.UriSource = imageSource;
			picture.EndInit();
			return picture;
		}

		public void GetBioInfoormation(string id)
		{
			var information = _personnelSelector.GetBioInformation(id);
			EmployeeName.Text = $"Employee Name: {information.FirstName} {information.LastName}";
			EmployeeDepartment.Text = $"Department: {information.Department}";
		}
	}
}
