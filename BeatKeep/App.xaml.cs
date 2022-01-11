using BeatKeeper.Models;
using BeatKeeper.Services;
using BeatKeeper.Stores;
using BeatKeeper.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using Microsoft.Extensions.Logging;
using BeatKeeper.Commands;

namespace BeatKeeper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<MusicBook>();
            services.AddSingleton<SheetStore>();
            services.AddSingleton<TemplateNotesStore>();
            services.AddSingleton<NavigationStore>();

            services.AddTransient<INavigationService<SheetListingViewModel>>(s => CreateSheetListingNavigationService(s));
            services.AddTransient<INavigationService<SheetEditorViewModel>>(s => CreateSheetEditorNavigationService(s));

            services.AddTransient<SheetListingViewModel>(
                s => new SheetListingViewModel(
                    s.GetRequiredService<SheetStore>(),
                    s.GetRequiredService<MusicBook>(),
                    CreateSheetEditorNavigationService(s)));
            services.AddTransient<SheetEditorViewModel>(
                s => new SheetEditorViewModel(
                    s.GetRequiredService<SheetStore>(),
                    s.GetRequiredService<TemplateNotesStore>(),
                    s.GetRequiredService<MusicBook>(),
                    CreateSheetListingNavigationService(s)));
            services.AddTransient<SheetViewModel>(
                s => new SheetViewModel(
                    s.GetRequiredService<Sheet>(),
                    s.GetRequiredService<SheetStore>(),
                    s.GetRequiredService<MusicBook>(),
                    CreateSheetEditorNavigationService(s)));
            services.AddSingleton<MainViewModel>();

            services.AddSingleton<MainWindow>(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainViewModel>()
            });

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            // Desperate load template notes : Fix later
            _serviceProvider.GetRequiredService<TemplateNotesStore>().Load();

            INavigationService<SheetListingViewModel> initialNavigationService = _serviceProvider.GetRequiredService<INavigationService<SheetListingViewModel>>();
            initialNavigationService.Navigate();

            MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        private INavigationService<SheetListingViewModel> CreateSheetListingNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<SheetListingViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<SheetListingViewModel>());
        }

        private INavigationService<SheetEditorViewModel> CreateSheetEditorNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<SheetEditorViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<SheetEditorViewModel>());
        }
    }
}