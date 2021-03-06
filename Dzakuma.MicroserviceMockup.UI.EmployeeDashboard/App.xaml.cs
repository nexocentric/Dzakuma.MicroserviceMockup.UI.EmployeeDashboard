﻿using System;
using System.Runtime.InteropServices;
using System.Windows;
using Mono.Options;

namespace Dzakuma.MicroserviceMockup.UI.EmployeeDashboard
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		[DllImport("Kernel32.dll")]
		public static extern bool AttachConsole(int processId);

		private OptionSet _programOptions;
		private bool _outputData;
		private double _top = 40;
		private double _left = 40;
		private double _width = 800;
		private double _height = 750;
		private int _windowState = 0;
		private string _selectedId;

		protected override void OnStartup(StartupEventArgs args)
		{
			if (ValidateProgramArguments(args.Args) && _outputData && !string.IsNullOrEmpty(_selectedId))
			{
				CreateConsole();
			}

			DisplayGui();
		}

		public void CreateConsole()
		{
			AttachConsole(-1);
			OutputBioPictureUrl();
			Shutdown(0);
		}

		public void OutputBioPictureUrl()
		{
			var personnelSelector = new PersonnelInformationSelector();
			var bioPictureUrl = personnelSelector.GetBioPictureUrl(_selectedId).ToString();
			Console.Error.Write(TransportSafeString.Encode(bioPictureUrl));
		}

		public bool ValidateProgramArguments(string[] args)
		{
			try
			{
				_programOptions = InitializeProgramOptions();
				var extraParameters = _programOptions.Parse(args);
				return true;
			}
			catch (OptionException anomaly)
			{
				return false;
			}
		}

		public OptionSet InitializeProgramOptions()
		{
			return new OptionSet
			{
				{ "t|top=", "Runs diagnostic tests on this service.", o => { if (double.TryParse(o, out var test)) { _top = double.Parse(o); } } },
				{ "l|left=", "Gets information for the person matching the specified ID.", o => { if (double.TryParse(o, out var test)) { _top = double.Parse(o); } } },
				{ "w|width=", "Gets the movie preferences of the specified ID. ID must be specified.", o => { if (double.TryParse(o, out var test)) { _top = double.Parse(o); } } },
				{ "h|height=", "Gets general information for the specified ID. ID must be specified.", o => { if (double.TryParse(o, out var test)) { _top = double.Parse(o); } } },
				{ "d|data", "Gets a list of all personnel.", o => { if (o != null) { _outputData = true; } } },
				{ "i|id=", "Gets a list of all personnel.", o => { if (o != null) { _selectedId = o; } } },
			};
		}

		public void DisplayGui()
		{
			var programWindow = new Dashboard();
			programWindow.Top = _top;
			programWindow.Left = _left;
			programWindow.Width = _width;
			programWindow.Height = _height;
			programWindow.WindowState = (WindowState)_windowState;
			programWindow.ShowDialog();
		}
	}
}
