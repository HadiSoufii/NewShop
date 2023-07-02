namespace Shop.Application.Utils
{
    public class PathExtension
    {
        #region domain address

        public static string DomainAddress = "http://localhost:5196";

        #endregion

        #region default images

        public static string DefaultAvatar = "/Template/Images/Defaults/Default.jpg";

        #endregion

        #region user

        public static string UserAvatarOrigin = "/Template/Images/User/Origin/";
        public static string UserAvatarOriginServer =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template/Images/User/Origin/");

        public static string UserAvatarThumb = "/Template/Images/User/Thumb/";
        public static string UserAvatarThumbServer =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template/Images/User/Thumb/");

        #endregion
    }
}
