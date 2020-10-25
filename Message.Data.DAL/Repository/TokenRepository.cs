using Message.Data.DAL.Repository.Core;
using Message.Data.Domain.Entities;

namespace Message.Data.DAL.Repository
{
    public interface ITokenRepository : IRepository<Token>
    {

    }
    public class TokenRepository : EfRepository<Token> , ITokenRepository
    {
        public TokenRepository(MessageContext context) : base(context)
        {

        }
    }
}
