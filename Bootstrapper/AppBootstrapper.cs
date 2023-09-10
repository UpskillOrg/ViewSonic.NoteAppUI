using System;
using ViewSonic.NoteApp.ViewModels;

namespace ViewSonic.NoteApp.Bootstrapper
{
    /// <summary>
    /// Represents the main application bootstrapper.
    /// </summary>
    public class AppBootstrapper : Bootstrapper<MainViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppBootstrapper"/> class.
        /// </summary>
        public AppBootstrapper()
        {            
        }

        protected override void Configure()
        {
            try
            {
                base.Configure();
            }
            catch(Exception ex)
            {
                Logger?.Error(ex);
            }            
        }
    }
}