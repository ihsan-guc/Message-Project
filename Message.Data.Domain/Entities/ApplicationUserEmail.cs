namespace Message.Data.Domain.Entities
{
    public class ApplicationUserEmail : BaseGuidEntity
    {
        public string ChangePasswordText { get; set; }
        public string Email { get; set; }
    }
}
