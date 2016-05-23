
namespace YM.Purchasing
{
    public class AuthorizationResponse
    {
        public bool IsAuthorized { get; private set; }
        public string Text { get; private set; }

        public AuthorizationResponse(bool isAuthorized, string text = null)
        {
            IsAuthorized = isAuthorized;
            Text = text;
        }

        public static AuthorizationResponse Authorized()
        {
            return new AuthorizationResponse(true);
        }

        public static AuthorizationResponse NotAuthorized(string text)
        {
            return new AuthorizationResponse(false, text);
        }
    }
}
