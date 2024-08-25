namespace CrmApiV2.Dtos.Response
{
    public class ApiResponseDto<T>
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
    }
}
