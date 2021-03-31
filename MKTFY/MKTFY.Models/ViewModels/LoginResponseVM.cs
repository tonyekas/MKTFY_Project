using IdentityModel.Client;

namespace MKTFY.Models.ViewModels
{
    public class LoginResponseVM
    {
        /// <summary>
        ///     
        /// </summary>
        /// <param name="tokenResponse"></param>
        /// <param name="user"></param>
        public LoginResponseVM(TokenResponse tokenResponse, UserVM user)
        {
            AccessToken = tokenResponse.AccessToken;
            Expires = tokenResponse.ExpiresIn;
            User = user;
        }

        /// <summary>
        /// JWT Token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Number of seconds until the access token expires
        /// </summary>
        public long Expires { get; set; }

        /// <summary>
        /// Additional User data
        /// </summary>
        public UserVM User { get; set; }

    }
}
