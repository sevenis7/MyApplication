namespace MyApplicationClient.States
{
    public class UserState
    {
        public string UserName { get; private set; }
        public string Role { get; private set; }

        public void SetUser(string userName, string role)
        {
            UserName = userName;
            Role = role;
        }
    }
}
