using SocialMedia.Core.Enumerators;

namespace SocialMedia.Core.Entities
{
    public partial class Security : BaseEntity
    {
        public string User { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public RoleType Role { get; set; }
    }
}
