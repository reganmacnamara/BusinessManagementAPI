namespace MacsBusinessManagementAPI.Entities
{

    public class Account
    {
        public long AccountID { get; set; }

        public long CompanyID { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? LastLoginDate { get; set; }

        //Navigational Properties

        public Company Company { get; set; } = default!;
    }

}
