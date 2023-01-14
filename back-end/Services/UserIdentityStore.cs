namespace back_end.Services
{
    public class UserIdentityStore
    {
        private string _userId;
        private string _secondaryUserId;
        public bool IsPrimaryUserTokenAcquired { get; set; }
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public string SecondaryUserId
        {
            get { return _secondaryUserId; }
            set { _secondaryUserId = value; }
        }
    }
}
