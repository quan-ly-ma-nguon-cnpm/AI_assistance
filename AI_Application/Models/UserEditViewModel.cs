namespace AI_Application.Models
{
    public class UserEditViewModel
    {
        public string Id { get; set; } = "";
        public string UserName { get; set; } = "";
        public string Email { get; set; } = "";
        public List<string> SelectedRoles { get; set; } = new();
        public List<string> AllRoles { get; set; } = new();
    }
}
