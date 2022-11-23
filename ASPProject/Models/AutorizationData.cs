namespace Test.Model
{
    public class AutorizationData
    {
		public string Role { get; set; }

		public string FullName { get; set; }

        public string Login { get; set; }
		
		public AutorizationData()
		{
		}
		
		public AutorizationData(string role, string fullName, string login)
		{
			Role = role;
			FullName = fullName;
			Login = login;
		}
	}
}
