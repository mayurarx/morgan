﻿using System.IO;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace Morgan
{
    /// <summary>
    /// View Model associated with the <see cref="BaseFormPage"/>
    /// </summary>
    public class BaseFormPageViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// List of folder locations to scan for music files
        /// </summary>
        public ObservableCollection<string> LocationsList { get; set; } = new ObservableCollection<string>();

        /// <summary>
        /// Number of file locations stored in the <see cref="LocationsList"/>
        /// </summary>
        public int LocationCount => LocationsList.Count;

        /// <summary>
        /// Flag indicating if there is at least one location in the list
        /// </summary>
        public bool HasLocation => LocationCount > 0;

        #endregion

        #region Commands

        /// <summary>
        /// Command to add a new location to the location list
        /// </summary>
        public ICommand AddLocationCommand { get; set; }

        /// <summary>
        /// Command to load music files from the specified location
        /// </summary>
        public ICommand LoadFilesCommand { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseFormPageViewModel()
        {
            // Initialize Commands
            AddLocationCommand = new ActionCommand(AddLocation);
            LoadFilesCommand = new ActionCommand(LoadFiles);
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Adds a new location to the location list
        /// </summary>
        private void AddLocation()
        {
            var location = IoC.Get<IDirectoryService>().GetLocation();
            if (Directory.Exists(location))
            {
                // Add the new location
                LocationsList.Add(location);

                // Notify the UI
                OnPropertyChanged(nameof(LocationCount));
                OnPropertyChanged(nameof(HasLocation));
            }
        }

        /// <summary>
        /// Loads all the files from the specified directory
        /// </summary>
        private void LoadFiles()
        {
            // Set the root music directory location i a glabal scope
            IoC.Get<ApplicationViewModel>().LocationsList = this.LocationsList;

            // Change the current page of the application
            IoC.Get<ApplicationViewModel>().CurrentPage = ApplicationPage.FileStructurePage;
        }

        #endregion
    }
}
