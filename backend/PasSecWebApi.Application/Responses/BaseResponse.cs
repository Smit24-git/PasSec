namespace PasSecWebApi.Application.Responses
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string>? Errors { get; set; }
    }
}
