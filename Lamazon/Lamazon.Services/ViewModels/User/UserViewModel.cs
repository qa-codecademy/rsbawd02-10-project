﻿namespace Lamazon.Services.ViewModels.User;

public class UserViewModel
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }

    public string UserRoleKey { get; set; }
}
