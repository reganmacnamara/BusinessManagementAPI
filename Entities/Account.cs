namespace BusinessManagementAPI.Entities;

public class Account
{
    public long AccountID { get; set; }

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public long RoleID { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime LastLoginDate { get; set; }


    public Role Role { get; set; } = default!;
}
