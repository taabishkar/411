using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace team_origin.Migrations
{
    public partial class AddedUserNotificationRefTabletocombineUserandNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserNotificationRef",
                columns: table => new
                {
                    UserNotificationRefId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NotificationId = table.Column<int>(type: "int", nullable: false),
                    RecipientUserId = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotificationRef", x => x.UserNotificationRefId);
                    table.ForeignKey(
                        name: "FK_UserNotificationRef_Notification_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notification",
                        principalColumn: "NotificationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserNotificationRef_User_RecipientUserId",
                        column: x => x.RecipientUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserNotificationRef_NotificationId",
                table: "UserNotificationRef",
                column: "NotificationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserNotificationRef_RecipientUserId",
                table: "UserNotificationRef",
                column: "RecipientUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserNotificationRef");
        }
    }
}
