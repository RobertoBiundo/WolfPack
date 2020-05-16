namespace Common
{
    public class Enums
    {
        public enum ServiceResult
        {
            Ok = 200,
            NotFound = 404
        }
        
        public enum Gender
        {
            None = 0, // In this case 0 is a valid value and means 'not provided'
            Male = 1,
            Female =2
        }
    }
}