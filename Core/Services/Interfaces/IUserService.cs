using DataLayer.Entities.Store;
using DataLayer.Entities.Supplementary;
using DataLayer.Entities.User;

namespace Core.Services.Interfaces
{
    public interface IUserService
    {
        #region General
        Task<bool> SendVerificationCodeAsync(string code, string phoneNumber);
        public bool SendPassword(string pass, string phoneNumber);
        public bool SendVerification(string code, string phoneNumber);
        public bool SendUserName_and_Password(string userName, string password, string phoneNumber);
        void SaveChanges();
        Task SaveChangesAsync();
        Task<SiteInfo> GetLastSiteInfoAsync();
        #endregion General
        #region User
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);        
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByPasswordAsync(string passWord);
        Task<User> GetUserByPasswordandUserName(string password, string userName);
        Task<User> GetUserByPasswordandCellphone(string password, string cellphone);
        Task<User> GetUserByCellphone(string cellphone); 
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        Task<bool> ExistUserByUserNameAsync(string userName);
        Task<bool> ExistUserByCellphoneAsync(string cellPhone);
        Task<bool> ExistUserByPasswordAsync(string password);
        Task<bool> ExistUserByPasswordUserNameAsync(string password, string userName);
        Task<bool> ExistUserByPasswordcellPhoneAsync(string password, string cellPhone);
        Task<bool> CheckUserIsAdminAsync(int id);
        #endregion User
        #region UserRole
        void CreateUserRole(UserRole userRole);
        #endregion
        #region Role
        Task<List<Role>> GetRolesAsync();
        Task<Role?> GetRoleAsync(int id);
        void CreateRole(Role role);
        void UpdateRole(Role role);
        void DeleteRole(Role role);
        bool ExistRole(int id);
        #endregion
    }
}
