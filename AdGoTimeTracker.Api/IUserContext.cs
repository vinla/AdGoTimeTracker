namespace AdGoTimeTracker.Api
{
    public interface IUserContext
    {
        User SignedInUser { get; }
    }

    public class DummyUserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        public User SignedInUser => GetUserFromHttpContext();

        private User GetUserFromHttpContext()
        {
            if (httpContextAccessor.HttpContext.Request.Headers.TryGetValue("ApiUser", out var apiUserHeaderVals))
                return new User { Id = apiUserHeaderVals.First()! };
            return new User { Id = "test-user" };
        }
    }


    public class User
    {
        public string Id { get; set; }
    }
}
