using System.Linq;
using Dzakuma.MicroserviceMockup.UI.EmployeeDashboard;
using Xunit;

namespace Dzakuma.MicroserviceMockup.UI.EmployeeDashboardTests
{
	//TODO: run tests for the program and talk about how it can help quality
    public class PersonnelInformationSelectorTests
    {
	    public class GetPersonnelList
	    {
		    [Fact]
		    public void ShouldReturn_NonEmptyIList_WhenCalled()
		    {
			    var testObject = new PersonnelInformationSelector();
				Assert.NotEmpty(testObject.GetPersonnelList());
		    }

		    [Fact]
		    public void EachListItemShouldHave_IdFirstNameAndLastName_WhenIterated()
		    {
			    var testObject = new PersonnelInformationSelector();
			    foreach (var childObject in testObject.GetPersonnelList())
			    {
					Assert.NotEmpty(childObject.Id);
				    Assert.NotEmpty(childObject.FirstName);
				    Assert.NotEmpty(childObject.LastName);
				    Assert.NotEmpty(childObject.Department);
				}
		    }
		}

	    public class GetBioPictureUrl
	    {
		    [Fact]
		    public void ShouldReturn_PictureUrlForMalesAndFemales_WhenCalled()
		    {
				var testObject = new PersonnelInformationSelector();
			    var containsMale = false;
			    var containsFemale = false;
			    var listCount = testObject.GetPersonnelList().Count;

			    foreach (var id in Enumerable.Range(1, listCount))
			    {
				    if (containsMale && containsFemale) { break; }
				    var testUrl = testObject.GetBioPictureUrl(id.ToString()).AbsolutePath;
					if (!containsMale) { containsMale = testUrl.Contains("man"); }
				    if (!containsFemale) { containsFemale = testUrl.Contains("woman"); }
				}
				Assert.True(containsMale && containsFemale);
		    }
	    }

	    public class GetBioInformation
	    {
		    [Fact]
		    public void ShouldReturn_PersonnelItemWithAllPropertiesSet_WhenCalled()
		    {
			    var testObject = new PersonnelInformationSelector().GetBioInformation("1");
				Assert.NotEmpty(testObject.Id);
				Assert.NotEmpty(testObject.FirstName);
				Assert.NotEmpty(testObject.LastName);
				Assert.NotEmpty(testObject.Department);
		    }
		}

	    public class GetAnimalPreferenceUrl
	    {
		    [Fact]
		    public void ShouldReturn_PictureUrlForBothCatsAndDogs_WhenCalled()
		    {
				var testObject = new PersonnelInformationSelector();
			    var containsCat = false;
			    var containsDog = false;
			    var listCount = testObject.GetPersonnelList().Count;

			    foreach (var id in Enumerable.Range(1, listCount))
			    {
				    if (containsCat && containsDog) { break; }
				    var testUrl = testObject.GetAnimalPreferenceUrl(id.ToString()).AbsolutePath;
				    if (!containsCat) { containsCat = testUrl.Contains("kitten"); }
				    if (!containsDog) { containsDog = testUrl.Contains("bear"); }
			    }
			    Assert.True(containsCat && containsDog);
			}
		}

	    public class AnimalPreference
	    {
			[Fact]
			public void ShouldReturn_BinaryPreferenceForDogsAndCats_WhenCalled()
			{
				var testObject = new PersonnelInformationSelector();
				var containsCat = false;
				var containsDog = false;
				var listCount = testObject.GetPersonnelList().Count;

				foreach (var id in Enumerable.Range(1, listCount))
				{
					if (containsCat && containsDog) { break; }
					var testUrl = testObject.AnimalPreference(id.ToString());
					if (!containsCat) { containsCat = testUrl == 0; }
					if (!containsDog) { containsDog = testUrl == 1; }
				}
				Assert.True(containsCat && containsDog);
			}
		}
	}
}
