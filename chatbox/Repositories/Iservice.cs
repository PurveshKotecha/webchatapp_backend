using chatbox.Controllers;
using chatbox.Models;
using ChatWebApp.Models;

namespace chatbox.Repositories
{
    public interface Iservice 
    {

        public Task<List<Userdata>> GetUserListAsync();

        public Task<IEnumerable<Userdata>> GetUSerByIdAsync(int id);

        public Task<int> AddUserAsync(Userdata userdata);

        public Task<int> UpdateUserAsync(Userdata userdata);

        public Task<int> DeleteUserAsync(int id);

        public Task<IEnumerable<Messagedata>> GetNewMessage(int chat_id_fk);
        public Task<int> SendNewMessage(Messagedata messagedata);
        public Task<Messagedata> updateMessageSeen(int SenderIdFk, int ChatIdFk);

        public Task<IEnumerable<Messagedata>> GetMessageByChatIdAsync(int ChatIdFk);

        public Task<List<GetIndexDataEntity>> GetIndexListAsync(int recieverId);


    }
}
