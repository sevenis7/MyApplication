
namespace MyApplicationClient.States
{
    public class UserState
    {
        public string UserName { get; set; }
        public string Role { get; set; }
        public bool IsLoggedIn { get => UserName is not null; }

        public event Action? UserChange; 

        public void SetUser(string userName, string role)
        {
            UserName = userName;
            Role = role;

            UserChange?.Invoke();
        }
    }
}
