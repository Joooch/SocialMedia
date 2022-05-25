namespace SocialMedia.Application.App.Profiles.Responses
{
    public class ProfileDto
    {
        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string Country { get; set; }
    }

    public class ProfileProtectedDto
    {
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}