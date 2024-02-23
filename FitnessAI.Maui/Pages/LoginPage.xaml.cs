using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessAI.Maui.Services;

namespace FitnessAI.Maui.Pages;

public partial class LoginPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void BtnSignIn_Clicked(object? sender, EventArgs e)
    {
        var response = await ApiService.Login(EntEmail.Text, EntPassword.Text);
        if (response)
            Application.Current!.MainPage = new AppShell();
        else
            await DisplayAlert("Error", "Invalid email or password", "Ok");
    }
}