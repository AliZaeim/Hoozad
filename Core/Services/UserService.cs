using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.Supplementary;
using DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;

using System.Text;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly MyContext _context;
        
        public UserService(MyContext context)
        {
            _context = context;
            
        }
        public async Task<bool> CheckUserIsAdminAsync(int id)
        {
            bool Existuser = await _context.Users!.AnyAsync(u => u.Id == id);
            if (Existuser)
            {
                List<UserRole> userRoles = await _context.UserRoles!.Where(x => x.UserId == id && x.RoleId == 1 && x.IsActive).ToListAsync();
                if (userRoles.Any())
                {
                    return true;
                }

            }
            return false;
        }

        public void CreateUser(User user)
        {
            _context?.Users!.Add(user);
        }

        public void CreateUserRole(UserRole userRole)
        {
            _context?.UserRoles!.Add(userRole);
        }

        public async void DeleteUser(int id)
        {
            User? user = await _context.Users!.FindAsync(id);
            if (user != null)
            {
                _context?.Users!.Remove(user);
            }
            
        }

        public async Task<bool> ExistUserByCellphoneAsync(string cellPhone)
        {
            return await _context.Users!.AnyAsync(x => x.Cellphone == cellPhone);
        }

        public async Task<bool> ExistUserByPasswordAsync(string password)
        {
            return await _context.Users!.AnyAsync(x => x.Password == password);
        }

        public async Task<bool> ExistUserByPasswordcellPhoneAsync(string password, string cellPhone)
        {
            return await _context.Users!.AnyAsync(x => x.Password == password && x.Cellphone == cellPhone);
        }

        public async Task<bool> ExistUserByPasswordUserNameAsync(string password, string userName)
        {
            return await _context.Users!.AnyAsync(x => x.Password == password && x.UserName == userName);
        }

        public async Task<bool> ExistUserByUserNameAsync(string userName)
        {
            return await _context.Users!.AnyAsync(x => x.UserName == userName);
        }

        public async Task<SiteInfo> GetLastSiteInfoAsync()
        {
            return await _context.SiteInfos!.OrderByDescending(x => x.RegDate).FirstOrDefaultAsync() ?? new SiteInfo();
        }

        public async Task<User> GetUserByCellphone(string cellphone)
        {
            List<User> users = await _context.Users!.Include(x => x.UserRoles!).ToListAsync();
            return users!.SingleOrDefault(x => x.Cellphone == cellphone)!;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            List<User> users = await _context.Users!.Include(x => x.UserRoles!).ToListAsync();
            return users!.SingleOrDefault(x => x.Email == email)!;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            List<User> users = await _context.Users!.Include(x => x.UserRoles!).Include(_ => _.County).Include(_ => _.County!.State).ToListAsync();
            return users!.SingleOrDefault(x => x.Id == id)!;
        }

        public async Task<User> GetUserByPasswordandCellphone(string password, string cellphone)
        {
            List<User> users = await _context.Users!.Include(x => x.UserRoles!).ToListAsync();
            return users.SingleOrDefault(x => x.Password == password && x.Cellphone == cellphone)!;
        }

        public async Task<User> GetUserByPasswordandUserName(string password, string userName)
        {
            List<User> users = await _context.Users!.Include(x => x.UserRoles!).ToListAsync();
            return users.SingleOrDefault(x => x.Password == password && x.UserName == userName)!;
        }

        public async Task<User> GetUserByPasswordAsync(string passWord)
        {
            List<User> users = await _context.Users!.Include(x => x.UserRoles!).ToListAsync();
            return users!.SingleOrDefault(x => x.Password == passWord)!;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            List<User> users = await _context.Users!.Include(x => x.UserRoles!).ToListAsync();
            return users!.SingleOrDefault(x => x.UserName == username)!;
        }

        public async Task<List<User>> GetUsersAsync()
        {            
            return await _context.Users!.Include(x => x.UserRoles).Include(_ => _.County).Include(_ => _.County!.State).ToListAsync();            
        }
        public async Task<bool> SendVerificationCodeAsync(string code, string phoneNumber)
        {
            HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add("X-API-KEY", "VBce8m4NreqXhRfWBFLQR7PsM38FoDkHALPgiZS4SMpBkxABcNeErp70HGhHpdtJ");
            var payload = @"{" + "\n" +
            @" ""mobile"": " + phoneNumber +"," + "\n" +
            @" ""templateId"": 100000," + "\n" +
            @" ""parameters"": [" + "\n" +
            @" {" + "\n" +
            @" ""name"": ""CODE""," + "\n" +
            @" ""value"": "+code +"" + "\n" +
            @" }" + "\n" +
            @" ]" + "\n" +
            @"}";
            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync("https://api.sms.ir/v1/send/verify", content);
            string result = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode;


        }
        public bool SendUserName_and_Password(string userName, string password, string phoneNumber)
        {
            //SmsIr smsIr = new SmsIr("VBce8m4NreqXhRfWBFLQR7PsM38FoDkHALPgiZS4SMpBkxABcNeErp70HGhHpdtJ");

            return true;
        }
        public bool SendPassword(string pass, string phoneNumber)
        {
            //var token = new Token().GetToken("VBce8m4NreqXhRfWBFLQR7PsM38FoDkHALPgiZS4SMpBkxABcNeErp70HGhHpdtJ", "alinaderi&alinaderi");

            //var ultraFastSend = new UltraFastSend()
            //{
            //    Mobile = long.Parse(phoneNumber),
            //    TemplateId = 46671,
            //    ParameterArray = new List<UltraFastParameters>()
            //{
            //    new UltraFastParameters()
            //    {
            //        Parameter = "password" , ParameterValue = pass
            //    }
            //}.ToArray()

            //};

            //UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(token, ultraFastSend);

            //return ultraFastSendRespone.IsSuccessful;
            return true;
        }
        public bool SendVerification(string code, string phoneNumber)
        {
            //var token = new Token().GetToken("VBce8m4NreqXhRfWBFLQR7PsM38FoDkHALPgiZS4SMpBkxABcNeErp70HGhHpdtJ", "alinaderi&alinaderi");

            //var ultraFastSend = new UltraFastSend()
            //{
            //    Mobile = long.Parse(phoneNumber),
            //    TemplateId = 46669,
            //    ParameterArray = new List<UltraFastParameters>()
            //{
            //    new UltraFastParameters()
            //    {
            //         Parameter= "RegisterCode" , ParameterValue = code
            //    }
            //}.ToArray()

            //};

            //UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(token, ultraFastSend);

            //return ultraFastSendRespone.IsSuccessful;
            return true;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateUser(User user)
        {
            _context.Users?.Update(user);
        }
        #region Role
        public async Task<List<Role>> GetRolesAsync()
        {
            return await _context.Roles!.ToListAsync();
        }

        public async Task<Role?> GetRoleAsync(int id)
        {
            return await _context.Roles!.SingleOrDefaultAsync(x => x.RoleId == id);
        }

        public void CreateRole(Role role)
        {
            _context.Roles!.Add(role);
        }

        public void UpdateRole(Role role)
        {
            _context.Roles!.Update(role);
        }

        public void DeleteRole(Role role)
        {
            _context.Roles!.Remove(role);
        }

        public bool ExistRole(int id)
        {
            return _context.Roles!.ToList().Exists(x => x.RoleId == id);
        }
        #endregion
    }
}
