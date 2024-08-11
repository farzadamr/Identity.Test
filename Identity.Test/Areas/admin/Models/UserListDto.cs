namespace Identity.Test.Areas.admin.Models
{
    public class UserListDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AccessFailedCount { get; set; }

    }
}
