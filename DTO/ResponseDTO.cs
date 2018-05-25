namespace netcore_jwt.DTO
{
    public class ResponseDTO
    {
        public static ResponseDTO<T> Create<T>(T data, string message = "", bool error = false) where T : class
        {
            return new ResponseDTO<T>(data, message, error);
        }
    }

    public class ResponseDTO<T> where T : class
    {
        public ResponseDTO()
        {

        }
        public ResponseDTO(T data, string message = "", bool error = false)
        {
            Data = data;
            Message = message;
            Error = error;
        }

        public T Data { get;  set; }

        public string Message { get;  set; }

        public bool Error { get;  set; }
    }
}