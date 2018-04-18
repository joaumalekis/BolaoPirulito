using System;
using System.Threading.Tasks;
using Android.Gms.Extensions;
using BolaoPirulito.Droid.DependencyService;
using BolaoPirulito.Interfaces;
using Firebase;
using Firebase.Auth;
using Java.Lang;

[assembly: Xamarin.Forms.Dependency(typeof(FirebaseAuthenticator))]
namespace BolaoPirulito.Droid.DependencyService
{
    public class FirebaseAuthenticator : IFirebaseAuthenticator
    {
        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                var user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                var token = await user.User.GetIdTokenAsync(false);
                return token.Token;
            }
            catch (FirebaseAuthInvalidCredentialsException)
            {
                throw new System.Exception("Usuário ou senha inválidos");
            }
            catch (FirebaseNetworkException)
            {
                throw new System.Exception("Verifique sua conexão com a internet");
            }
            catch (FirebaseAuthInvalidUserException)
            {
                throw new System.Exception("Usuário ou senha inválidos");
            }
            catch (System.Exception e)
            {
                throw e;
            }
            
        }

        public async Task CreateUserWithEmailAndPassword(string email, string password)
        {
            var user = await FirebaseAuth.Instance.CreateUserWithEmailAndPassword(email, password);
        }
    }

}