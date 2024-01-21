using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chatbox.Migrations
{
    public partial class webChatapp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "userdata",
                columns: table => new
                {
                    user_id_pk = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    phone_no_uk = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    login_id_uk = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userdata", x => x.user_id_pk);
                });

            migrationBuilder.CreateTable(
                name: "chatdata",
                columns: table => new
                {
                    chat_id_pk = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sender_id_fk = table.Column<int>(type: "int", nullable: false),
                    receiver_id_fk = table.Column<int>(type: "int", nullable: false),
                    last_message_time = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chatdata", x => x.chat_id_pk);
                    table.ForeignKey(
                        name: "FK_userdataUserId_chatdataReceiverId",
                        column: x => x.receiver_id_fk,
                        principalTable: "userdata",
                        principalColumn: "user_id_pk");
                    table.ForeignKey(
                        name: "FK_userdataUserId_chatdataSenderId",
                        column: x => x.sender_id_fk,
                        principalTable: "userdata",
                        principalColumn: "user_id_pk");
                });

            migrationBuilder.CreateTable(
                name: "messagedata",
                columns: table => new
                {
                    message_id_pk = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    chat_id_fk = table.Column<int>(type: "int", nullable: false),
                    sender_id_fk = table.Column<int>(type: "int", nullable: false),
                    message = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    message_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    message_seen = table.Column<bool>(type: "bit", nullable: true),
                    receiver_id_fk = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messagedata", x => x.message_id_pk);
                    table.ForeignKey(
                        name: "FK_chatdataChatId_messagedataChatId",
                        column: x => x.chat_id_fk,
                        principalTable: "chatdata",
                        principalColumn: "chat_id_pk");
                    table.ForeignKey(
                        name: "FK_userdataUserId_messagedataSenderId",
                        column: x => x.sender_id_fk,
                        principalTable: "userdata",
                        principalColumn: "user_id_pk");
                });

            migrationBuilder.CreateIndex(
                name: "IX_chatdata_receiver_id_fk",
                table: "chatdata",
                column: "receiver_id_fk");

            migrationBuilder.CreateIndex(
                name: "IX_chatdata_sender_id_fk",
                table: "chatdata",
                column: "sender_id_fk");

            migrationBuilder.CreateIndex(
                name: "IX_messagedata_chat_id_fk",
                table: "messagedata",
                column: "chat_id_fk");

            migrationBuilder.CreateIndex(
                name: "IX_messagedata_sender_id_fk",
                table: "messagedata",
                column: "sender_id_fk");

            migrationBuilder.CreateIndex(
                name: "Unique_Login_id",
                table: "userdata",
                column: "login_id_uk",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Unique_Phone_no",
                table: "userdata",
                column: "phone_no_uk",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "messagedata");

            migrationBuilder.DropTable(
                name: "chatdata");

            migrationBuilder.DropTable(
                name: "userdata");
        }
    }
}
