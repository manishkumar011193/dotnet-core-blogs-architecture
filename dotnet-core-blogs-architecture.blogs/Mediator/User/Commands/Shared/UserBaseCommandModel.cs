﻿namespace DT.Identity.Core.User.Commands.Shared;

public abstract class UserBaseCommandModel
{
    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Mobile { get; set; }

    public bool IsActive { get; set; }

    public string PasswordHash { get; set; }

}