namespace Final.Project.API.Responses;
public class UserManagerResponse
{
    //public string? Token { get; set; }
    //public string? Role { get; set; }
    public string? Message { get; set; }
    public bool IsSuccess { get; set; }
    public Object? Data { get; set; }
    public int Status { get; set; }
    public IEnumerable<string>? Errors { get; set; }
}
