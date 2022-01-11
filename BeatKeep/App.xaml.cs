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
            _serviceProvider.GetRequiredService<TemplateNotesStore>().Load(_serviceProvider.GetRequiredService<SheetStore>());

            INavigationService<SheetListingViewModel> initialNavigationService = _serviceProvider.GetRequiredService<INavigationService<SheetListingViewModel>>();
            initialNavigationService.Navigate();

            MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}

/*
MusicBook musicBook = new();

Sheet sheet = new Sheet("Sheet 1", 127);
            
Note note1 = new(1 / 4f, 0);
Note note2 = new(1 / 4f, 0);
Note note3 = new(1 / 2f, 1);
Note note4 = new(1 / 8f, 0);
Note note5 = new(1 / 8f, 0);
Note note6 = new(1 / 8f, 0);
Note note7 = new(1 / 8f, 0);
Note note8 = new(1 / 8f, 0);
Note note9 = new(1 / 8f, 0);
Note note10 = new(1 / 4f, 0);
Note note11 = new(1 / 4f, 0);
Note note12 = new(1 / 2f, 1);
Note note13 = new(1 / 8f, 0);
Note note14 = new(1 / 8f, 0);
Note note15 = new(1 / 8f, 0);
Note note16 = new(1 / 8f, 0);
Note note17 = new(1 / 8f, 0);
Note note18 = new(1 / 8f, 0);

sheet.AddNote(note1);
sheet.AddNote(note2);
sheet.AddNote(note3);
sheet.AddNote(note4);
sheet.AddNote(note5);
sheet.AddNote(note6);
sheet.AddNote(note7);
sheet.AddNote(note8);
sheet.AddNote(note9);
sheet.AddNote(note10);
sheet.AddNote(note11);
sheet.AddNote(note12);
sheet.AddNote(note13);
sheet.AddNote(note14);
sheet.AddNote(note15);
sheet.AddNote(note16);
sheet.AddNote(note17);
sheet.AddNote(note18);

var length = sheet.Length;
*/