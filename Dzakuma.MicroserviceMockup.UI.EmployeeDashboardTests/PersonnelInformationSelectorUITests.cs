using System.Linq;
using System.Windows.Controls;
using Dzakuma.MicroserviceMockup.UI.EmployeeDashboard;
using Xunit;
using FlaUI;
using FlaUI.Core;
using FlaUI.UIA3;

namespace Dzakuma.MicroserviceMockup.UI.EmployeeDashboardTests
{
	public class PersonnelInformationSelectorUITests
	{
		[Fact]
		public void InitialUITest()
		{
			var applicationPath = @".\Dzakuma.MicroserviceMockup.UI.EmployeeDashboard.exe";
			var application = Application.Launch(applicationPath);

			using (var automationManager = new UIA3Automation())
			{
				var window = application.GetMainWindow(automationManager);
				var employeeList = window.FindFirstDescendant(cf => cf.ByAutomationId("PersonnelList"));

				var employeeNameOne = window.FindFirstDescendant(cf => cf.ByAutomationId("EmployeeName")).Name;
				employeeList.FindChildAt(10)?.AsListBoxItem().FindFirstChild().AsButton()?.Invoke();
				var employeeNameTwo = window.FindFirstDescendant(cf => cf.ByAutomationId("EmployeeName")).Name;

				Assert.NotEqual(employeeNameOne, employeeNameTwo);
			}

			application.Close();
		}
	}
}
