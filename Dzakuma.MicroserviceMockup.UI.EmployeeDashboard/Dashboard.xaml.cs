using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Dzakuma.MicroserviceMockup.UI.EmployeeDashboard
{
	public partial class Dashboard : Window
	{
		PersonnelInformationSelector _personnelSelector = new PersonnelInformationSelector();

		public Dashboard()
		{
			InitializeComponent();
			RefreshPersonnelList();
			GetBioInfoormation("1");
			LoadBioPicture("1");
			LoadAnimalPreference("1");
		}

		private void PersonelList_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			var selectedItem = (WrapPanel)sender;
			var id = (string)selectedItem.Uid;
			GetBioInfoormation(id);
			LoadBioPicture(id);
			LoadAnimalPreference(id);
		}

		private void MoviePreferences_OnClick(object sender, RoutedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void GeneralEmployeeDataRefresh_OnClick(object sender, RoutedEventArgs e)
		{
			RefreshPersonnelList();
		}

		public int RefreshPersonnelList()
		{
			PersonnelList.Items.Clear();
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
			var information = _personnelSelector.GetBioInfoormation(id);
			EmployeeName.Text = $"Employee Name: {information.FirstName} {information.LastName}";
			EmployeeDepartment.Text = $"Department: {information.Department}";
		}
	}
}
