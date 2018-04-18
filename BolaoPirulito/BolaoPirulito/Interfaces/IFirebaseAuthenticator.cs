using System.Threading.Tasks;

namespace BolaoPirulito.Interfaces
{
    public interface IFirebaseAuthenticator
    {
        Task<string> LoginWithEmailPassword(string email, string password);
        Task CreateUserWithEmailAndPassword(string email, string password);
    }
}