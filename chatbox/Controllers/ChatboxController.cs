using chatbox.Models;
using chatbox.Repositories;
using ChatWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace chatbox.Controllers
{
    public class ChatboxController : Controller
    {
        private readonly Iservice iservice;

        public ChatboxController(Iservice iservice)
        {
            this.iservice = iservice;
        }

        [HttpGet("getallusers")]
        public async Task<List<Userdata>> GetUserListAsync()
        {
            try
            {
                return await iservice.GetUserListAsync();
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("getuserbyid")]
        public async Task<IEnumerable<Userdata>> GetUSerByIdAsync(int id)
        {
            try
            {
                var response = await iservice.GetUSerByIdAsync(id);
                if (response == null)
                {
                    return null;
                }
                return response;
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("adduser")]
        public async Task<IActionResult> AddUserAsync(Userdata userdata)
        {
            if (userdata == null)
            {
                return BadRequest();
            }
            try
            {
               var response = await iservice.AddUserAsync(userdata);
                

                return Ok(response);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("updateuser")]
        public async Task<IActionResult> UpdateUserAsync(Userdata userdata)
        {
            if(userdata == null)
            {
                return BadRequest();
            }

            try
            {
                var result = await iservice.UpdateUserAsync(userdata);
                return Ok(result);
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete("deleteuser")]
        public async Task<int> DeleteUserAsync(int id)
        {
            try
            {
                var response = await iservice.DeleteUserAsync(id);
                return response;
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("getnewmessage")]
        public async Task<IEnumerable<Messagedata>> GetNewMessage(int chat_id_fk)
        {
            try
            {
                var response = await iservice.GetNewMessage(chat_id_fk);
                if (response == null)
                {
                    return null;
                }
                return response;
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("sendnewmessage")]
        public async Task<IActionResult> SendNewMessage(Messagedata messagedata)
        {
            if (messagedata == null)
            {
                return BadRequest();
            }
            try
            {
                var response = await iservice.SendNewMessage(messagedata);


                return Ok(response);
            }
            catch
            {
                throw;
            }
        }


        [HttpPost("updateseenmessage")]
        public async Task<Messagedata> updateMessageSeen(int SenderIdFk, int ChatIdFk)
        {
            return await iservice.updateMessageSeen(SenderIdFk, ChatIdFk);
        }

        [HttpGet("getmessagebyid")]
        public async Task<IEnumerable<Messagedata>> GetMessageByChatIdAsync(int ChatIdFk)
        {
            try
            {
                var response = await iservice.GetMessageByChatIdAsync(ChatIdFk);
                if (response == null)
                {
                    return null;
                }
                return response;
            }
            catch
            {
                throw;
            }
        }




        [HttpGet("getIndexDetails")]
        public async Task<List<GetIndexDataEntity>> GetIndexListAsync(int recieverId)
        {
            try
            {
                var response = await iservice.GetIndexListAsync(recieverId);
                if (response == null)
                {
                    return null;
                }
                return response;


                //return await iservice.GetIndexListAsync();
            }
            catch
            {
                throw;
            }
        }


    }
}
