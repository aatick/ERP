namespace ERP.Web.ViewModels
{
    public class UserLoginViewModel : BaseModel
    {
        public long UserLoginId { get; set; }
        
        public string PersonType { get; set; }
        public int UserId { get; set; }
        public long? PersonId { get; set; }
        public string PersonEmail { get; set; }
    }
}