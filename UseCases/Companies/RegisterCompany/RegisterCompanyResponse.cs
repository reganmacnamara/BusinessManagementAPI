namespace MacsBusinessManagementAPI.UseCases.Companies.RegisterCompany
{

    public class RegisterCompanyResponse
    {
        public long CompanyID { get; set; }
        public DateTime Expiry { get; set; }
        public string Token { get; set; } = string.Empty;
    }

}
