namespace SocialMedia.API.Exceptions
{
    public class InvalidUserException : Exception
    {
        public InvalidUserException(string info) : base(info) { }
        public InvalidUserException(string info, Exception innerException) : base(info, innerException) { }
    }
}
