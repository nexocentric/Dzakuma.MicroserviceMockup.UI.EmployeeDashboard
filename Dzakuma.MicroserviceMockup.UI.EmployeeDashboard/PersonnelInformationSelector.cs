using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Dzakuma.MicroserviceMockup.UI.EmployeeDashboard
{
	public class PersonnelInformationSelector
	{
		private string _executablePath = @"C:\repositories\Dzakuma.MicroserviceMockup.EmployeeData\Dzakuma.MicroserviceMockup.EmployeeData\bin\Debug\Dzakuma.MicroserviceMockup.EmployeeData.exe";
		private InterprocessDataSiphon _dataSiphon = new InterprocessDataSiphon();

		public class PersonnelItem
		{
			public string Id { get; set; }
			public string FirstName { get; set; }
			public string LastName { get; set; }
			public string Department { get; set; }
		}

		public IList<PersonnelItem> GetPersonnelList()
		{
			var items = new List<PersonnelItem>();
			var data = _dataSiphon.DeserializeJsonString(_executablePath, "--all");

			foreach (var child in data.personnelList.Children<JObject>())
			{
				items.Add(
					new PersonnelItem
					{
						Id = (string)child["id"],
						FirstName = (string)child["first_name"],
						LastName = (string)child["last_name"]
					}
				);
			}

			return items;
		}

		public Uri GetBioPictureUrl(string id)
		{
			var data = _dataSiphon.DeserializeJsonString(_executablePath, $"--general --id {id}");
			var employeeData = data.individualData;
			string genderId = (string)employeeData["gender"] == "Female" ? "woman" : "man";
			return new Uri($@"https://loremflickr.com/300/300/portrait,{genderId}?lock={id}", UriKind.Absolute);
		}

		public PersonnelItem GetBioInfoormation(string id)
		{
			var data = _dataSiphon.DeserializeJsonString(_executablePath, $"--general --id {id}");
			var employeeData = data.individualData;

			return new PersonnelItem
			{
				Id = id,
				FirstName = (string)employeeData["first_name"],
				LastName = (string)employeeData["last_name"],
				Department = (string)employeeData["department"]
			};
		}

		public Uri GetAnimalPreferenceUrl(string id)
		{
			int preference = AnimalPreference(id);
			string imageUrl = preference == 0 ? @"https://loremflickr.com/200/200/cat" : @"https://placebear.com/200/200";
			return new Uri(imageUrl, UriKind.Absolute);
		}

		private int AnimalPreference(string id)
		{
			//TODO: This will always return an error due to the favorite animal not being
			//      available
			try
			{
				var data = _dataSiphon.DeserializeJsonString(_executablePath, $"--general --id {id}");
				var employeeData = data.individualData;

				return (int)employeeData["favorite_animal"];
			}
			catch
			{
				return 0;
			}
		}
	}
}
