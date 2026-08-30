using System;
using System.IO;
using System.Net.Http;
using System.Windows;
using Microsoft.Win32;
using SmartXApp.ViewModels;

namespace SmartXApp
{
    public partial class MainWindow : Window
    {
        private static readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7076/")
        };

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
        // Reference: Microsoft Learn - File uploads in ASP.NET Core using MultipartFormDataContent
        // https://learn.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads 
        private async void BtnUploadFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Config files (*.json;*.log;*.txt;*.png)|*.json;*.log;*.txt;*.png|All Files (*.*)|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    using var content = new MultipartFormDataContent();
                    using var fileStream = File.OpenRead(dialog.FileName);

                    content.Add(new StreamContent(fileStream), "file", Path.GetFileName(dialog.FileName));
                    content.Add(new StringContent(TxtMac.Text ?? string.Empty), "macAddress");

                    var response = await client.PostAsync("/api/sensors/upload-config", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Configuration file uploaded successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Upload failed: {response.StatusCode}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"API connection error: {ex.Message}", "Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}