namespace Message.Data.DAL.Repository
{
    public interface IUnitOfWork
    {
        IApplicationUserRepository ApplicationUserRepository { get; set;}
        int Commit();
    }
}
