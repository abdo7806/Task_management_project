using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using Task_management_BusinessLayer;
using Task_management_DataAccessLayer;

namespace Task_management_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        [HttpGet("All", Name = "GetAllUsers")] // Marks this method to respond to HTTP GET requests.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<UserDTO>> GetAllUsers()
        {
            List<UserDTO> UsersList = Task_management_BusinessLayer.User.GetAllUsers();

            if (UsersList.Count == 0)
            {
                return NotFound("No Users Found!");
            }
            return Ok(UsersList);
        }

        // POST: api/users/register
        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public  ActionResult<UserDTO> Register(UserDTO NewUser)
        {
            if (NewUser == null || string.IsNullOrEmpty(NewUser.Username) || string.IsNullOrEmpty(NewUser.PasswordHash))
            {
                return BadRequest("Invalid User data.");
            }
            // تحقق مما إذا كان اسم المستخدم موجودًا بالفعل
            /*if (Task_management_BusinessLayer.User.Find(NewUser.Id) == null)
            {
                return BadRequest("Username already exists.");
            }*/

            // تشفير كلمة المرور
            using var hmac = new HMACSHA512();
            NewUser.PasswordHash = Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(NewUser.PasswordHash)));


            Task_management_BusinessLayer.User user = new Task_management_BusinessLayer.User(new UserDTO(NewUser.Id, NewUser.Username, NewUser.PasswordHash, NewUser.Role));
            user.Save();

            NewUser.Id = user.Id;
            return CreatedAtRoute("GetUserById", new { id = NewUser.Id }, NewUser);

        }



        [HttpGet("{id}", Name = "GetUserById")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // ارجاع الطالب حسب رقمة
        public ActionResult<UserDTO> GetUserById(int id)
        {

            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }
            Task_management_BusinessLayer.User user = Task_management_BusinessLayer.User.Find(id);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

      
            UserDTO UDTO = user.UDTO;


            return Ok(UDTO);

        }




        [HttpGet("GetUserByUsername/{Username}", Name = "GetUserByUsername")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // ارجاع الطالب حسب رقمة
        public ActionResult<UserDTO> GetUserByUsername(string Username)
        {

    
            var user = Task_management_BusinessLayer.User.GetUserInfoByUsername(Username);

            if (user == null)
            {
                return NotFound($"User with Username {Username} not found.");
            }


            UserDTO UDTO = user;


            return Ok(UDTO);

        }




        //here we use http put method for update
        [HttpPut("{id}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> UpdateUser(int id, UserDTO updatedUser)
        {
            if (id < 1 || updatedUser == null || string.IsNullOrEmpty(updatedUser.Username) || string.IsNullOrEmpty(updatedUser.PasswordHash) || string.IsNullOrEmpty(updatedUser.Role))
            {
                return BadRequest("Invalid User data.");
            }

            //var user = UserDataSimulation.UsersList.FirstOrDefault(s => s.Id == id);

            Task_management_BusinessLayer.User user = Task_management_BusinessLayer.User.Find(id);


            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            // تشفير كلمة المرور
            using var hmac = new HMACSHA512();
            updatedUser.PasswordHash = Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(updatedUser.PasswordHash)));

            user.Username = updatedUser.Username;
            user.PasswordHash = updatedUser.PasswordHash;
            user.Role = updatedUser.Role;
            user.Save();

            //we return the DTO not the full user object.
            return Ok(user.UDTO);

        }


        //here we use HttpDelete method
        [HttpDelete("{id}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteUser(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }



            if (Task_management_BusinessLayer.User.DeleteUser(id))

                return Ok($"User with ID {id} has been deleted.");
            else
                return NotFound($"User with ID {id} not found. no rows deleted!");
        }


        // POST: api/users/login
        /*      [HttpPost("login")]
              public  ActionResult<UserDTO> Login(User loginUser)
              {
                  // تحقق من كلمة المرور
                  using var hmac = new HMACSHA512();
                  var passwordHash = Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(loginUser.PasswordHash)));

                  var user  = Task_management_BusinessLayer.User.GetUserInfoByUsernameAndPassword(loginUser.Username, passwordHash);
                  ;
                  if (user == null) return Unauthorized("Invalid username or password.");


                  return Ok(user); // يمكنك إرسال توكن أو معلومات المستخدم هنا
              }
        */
        // POST: api/users/login
        [HttpPost("login")]
        public ActionResult<UserDTO> Login([FromBody] UserDTO loginUser)
        {
            if (string.IsNullOrEmpty(loginUser.Username) || string.IsNullOrEmpty(loginUser.PasswordHash))
            {
                return BadRequest("اسم المستخدم وكلمة المرور مطلوبان.");
            }

            // استرجاع معلومات المستخدم بناءً على اسم المستخدم
            var user = Task_management_BusinessLayer.User.GetUserInfoByUsername(loginUser.Username);

            if (user == null)
            {
                return Unauthorized("فشل تسجيل الدخول. تحقق من اسم المستخدم وكلمة المرور.");
            }

     
            return Ok(user);
        }

      
        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            var hashOfInput = HashPassword(enteredPassword);
            return hashOfInput == storedHash;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }


    }
}
