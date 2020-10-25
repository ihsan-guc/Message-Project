namespace Message.Data.DAL.Repository.Core
{
    public interface IUnitOfWork
    {
        IApplicationUserRepository ApplicationUserRepository { get; set;}
        ITokenRepository TokenRepository{ get; set;}
        int Commit();
    }
}
