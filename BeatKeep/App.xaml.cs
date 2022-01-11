using BeatKeeper.Models;
using BeatKeeper.Services;
using BeatKeeper.Services.SheetEditorLoader;
using BeatKeeper.Stores;
using BeatKeeper.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

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
            services.AddSingleton<IAudioPlayer>(
                s => new AudioPlayer(
                    s.GetRequiredService<SheetStore>()));
            services.AddSingleton<ISheetEditorLoader>(
                s => new SheetEditorLoader(
                    s.GetRequiredService<SheetStore>(),
                    s.GetRequiredService<INavigationService<SheetEditorViewModel>>()));

            services.AddTransient<INavigationService<SheetListingViewModel>>(
                s => new LayoutNavigationService<SheetListingViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    () => s.GetRequiredService<SheetListingViewModel>()));

            services.AddTransient<INavigationService<SheetEditorViewModel>>(
                s => new LayoutNavigationService<SheetEditorViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    () => s.GetRequiredService<SheetEditorViewModel>()));

            services.AddTransient<SheetListingViewModel>(
                s => new SheetListingViewModel(
                    s.GetRequiredService<MusicBook>(),
                    s.GetRequiredService<ISheetEditorLoader>()));
            services.AddTransient<SheetEditorViewModel>(
                s => new SheetEditorViewModel(
                    s.GetRequiredService<SheetStore>(),
                    s.GetRequiredService<TemplateNotesStore>(),
                    s.GetRequiredService<MusicBook>(),
                    s.GetRequiredService<IAudioPlayer>(),
                    s.GetRequiredService<INavigationService<SheetListingViewModel>>()));
            services.AddTransient<SheetViewModel>(
                s => new SheetViewModel(
                    s.GetRequiredService<Sheet>(),
                    s.GetRequiredService<MusicBook>(),
                    s.GetRequiredService<ISheetEditorLoader>()));
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
    }
}