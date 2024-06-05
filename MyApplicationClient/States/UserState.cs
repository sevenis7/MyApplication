
namespace MyApplicationClient.States
{
    public class UserState
    {
        public string UserName { get; private set; }
        public string Role { get; private set; }
        public bool IsLoggedIn { get => UserName is not null; }

        public event Action<string, string> UserChange; 

        public void SetUser(string userName, string role)
        {
            UserName = userName;
            Role = role;

            UserChange?.Invoke(userName, role);
        }
    }
}
