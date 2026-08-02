namespace StudentPortalWeb.Services
{
    public interface IHagerStampService{
    
        string Stamp { get; }
        string Owner { get; }
    }

    public class HagerStampService : IHagerStampService {
        public string Owner => "Hager Fathy"; 

        public string Stamp { get; }

        public HagerStampService()
        {
            Stamp = Guid.NewGuid().ToString().Substring(0, 8);
        }
    }
}
