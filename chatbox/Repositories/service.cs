using chatbox.Models;
using System.Collections;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Dapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using ChatWebApp.Models;

namespace chatbox.Repositories
{
    public class service : Iservice
    {
        private readonly chatboxContext _chatboxContext;
        private readonly IConfiguration _config;
        public service(chatboxContext chatboxContext, IConfiguration config)
        {
            _chatboxContext = chatboxContext;
            _config = config;
        }
        public IDbConnection connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        public async Task<List<Userdata>> GetUserListAsync()
        {
            return await _chatboxContext.userdata.FromSqlRaw<Userdata>("get_all_user_records").ToListAsync();
        }

        public async Task<IEnumerable<Userdata>> GetUSerByIdAsync(int user_id_pk)
        {
            var param = new SqlParameter("@user_id_pk", user_id_pk);

            var userdetails = await Task.Run(() => _chatboxContext.userdata.FromSqlRaw(@"exec read_user_record @user_id_pk", param).ToListAsync());
           
            return userdetails;
        }


        public async  Task<int> AddUserAsync(Userdata userdata)
        {

            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@user_id_pk", userdata.UserIdPk));
            parameter.Add(new SqlParameter("@user_name", userdata.UserName));
            parameter.Add(new SqlParameter("@phone_no_uk", userdata.PhoneNoUk));
            parameter.Add(new SqlParameter("@email", userdata.Email));
            parameter.Add(new SqlParameter("@login_id_uk", userdata.LoginIdUk));
            parameter.Add(new SqlParameter("@password", userdata.Password));

            var result = await Task.Run(() => _chatboxContext.Database.ExecuteSqlRawAsync(@"exec create_user_record @user_id_pk, @user_name, @phone_no_uk, @email, @login_id_uk, @password", parameter.ToArray()));

            return result;
        
        }

        public async Task<int> UpdateUserAsync(Userdata userdata)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@user_id_pk", userdata.UserIdPk));
            parameter.Add(new SqlParameter("@user_name", userdata.UserName));
            parameter.Add(new SqlParameter("@phone_no_uk", userdata.PhoneNoUk));
            parameter.Add(new SqlParameter("@email", userdata.Email));
            parameter.Add(new SqlParameter("@login_id_uk", userdata.LoginIdUk));
            parameter.Add(new SqlParameter("@password", userdata.Password));

            var result = await Task.Run(() => _chatboxContext.Database.ExecuteSqlRawAsync(@"exec update_user_record @user_id_pk,@user_name,@phone_no_uk,@email,@login_id_uk,@password", parameter.ToArray()));
           

            return result;
        }

        public async Task<int> DeleteUserAsync(int user_id)
        {
            return await Task.Run(() => _chatboxContext.Database.ExecuteSqlInterpolatedAsync($"delete_user_record {user_id}"));
        }


        public async Task<IEnumerable<Messagedata>> GetNewMessage(int chat_id_fk)
        {

            var param = new SqlParameter("@chat_id_fk", chat_id_fk);
            var userdetails = await Task.Run(() => _chatboxContext.messagedata.FromSqlRaw(@"exec getnewmessage @chat_id_fk", param));

            return userdetails;
        }

        public async Task<int> SendNewMessage(Messagedata messagedata)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@chat_id_fk", messagedata.ChatIdFk));
            parameter.Add(new SqlParameter("@sender_id_fk", messagedata.SenderIdFk));
            parameter.Add(new SqlParameter("@receiver_id_fk", messagedata.ReceiverIdFk));
            parameter.Add(new SqlParameter("@message", messagedata.Message));

            var result = await Task.Run(() => _chatboxContext.Database.ExecuteSqlRawAsync(@"exec send_new_message @chat_id_fk, @sender_id_fk, @receiver_id_fk, @message", parameter.ToArray()));

            return result;
        }

        public async Task<Messagedata> updateMessageSeen(int SenderIdFk, int ChatIdFk)
        {
            using (IDbConnection con = connection)
            {
                string sQuery = "update_seen_message";
                con.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("@sender_id", SenderIdFk);
                param.Add("@chat_id", ChatIdFk);
                var results = await con.QueryAsync<Messagedata>(sQuery, param, commandType: CommandType.StoredProcedure);
                return results.FirstOrDefault();
            }
        }


        public async Task<IEnumerable<Messagedata>> GetMessageByChatIdAsync(int ChatIdFk)
        {

            var param = new SqlParameter("@chat_id_fk", ChatIdFk);

            var messages = await Task.Run(() => _chatboxContext.messagedata.FromSqlRaw(@"exec GetMessagesByChatId @chat_id_fk", param).ToListAsync());


            if (messages == null || !messages.Any())
            {
                return Enumerable.Empty<Messagedata>();
            }

            return messages;
        }

        public async Task<List<GetIndexDataEntity>> GetIndexListAsync(int recieverId)
        {
            var param = new SqlParameter("@recieverId", recieverId);
            var userdetails = await Task.Run(() => _chatboxContext.IndexDetails.FromSqlRaw<GetIndexDataEntity>(@"exec getIndexDetails @recieverId", param).ToListAsync());
            return userdetails;
            //await _chatboxContext.IndexDetails.FromSqlRaw<GetIndexDataEntity>("[getIndexDetails]").ToListAsync();

        }
    }
}
