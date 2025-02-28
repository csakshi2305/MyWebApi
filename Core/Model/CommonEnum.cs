namespace MyWebApi.Core.Model
{

    public class CommonEnumResponse
    {
        public CommonEnum Result { get; set; }
    }
    public enum CommonEnum
    {
       Success = 1,  
       Notfound = 2,
       Failed = 3,
       NameAlreadyExist = 4,
       AlreadyInUse=5
    }
}
