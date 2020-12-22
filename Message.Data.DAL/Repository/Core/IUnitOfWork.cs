namespace Message.Data.DAL.Repository.Core
{
    public interface IUnitOfWork
    {
        IApplicationUserEmailRepository ApplicationUserEmailRepository{ get; set; }
        IApplicationAdminRepository  ApplicationAdminRepository{ get; set; }
        IApplicationUserRepository ApplicationUserRepository { get; set;}
        IUserMessageRepository UserMessageRepository { get; set; }
        ITokenRepository TokenRepository{ get; set;}
        int Commit();
    }
}
