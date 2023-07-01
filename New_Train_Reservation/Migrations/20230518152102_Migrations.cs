using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace New_Train_Reservation.Migrations
{
    /// <inheritdoc />
    public partial class Migrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trains",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Train_Number = table.Column<int>(type: "int", nullable: false),
                    Number_of_available_tickets = table.Column<int>(type: "int", nullable: false),
                    Driver_ID = table.Column<int>(type: "int", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pickup_Station = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Pickup = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Number_of_stoppages = table.Column<int>(type: "int", nullable: true),
                    classes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trains", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    National_Pass_Number = table.Column<long>(type: "bigint", nullable: false),
                    User_Fname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Lname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_phone_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number_of_purchased_tickets = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Admin_Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time_Pickup = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pickup_Station = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seat_Number = table.Column<int>(type: "int", nullable: false),
                    Ticket_Money = table.Column<double>(type: "float", nullable: false),
                    Ticket_Classes = table.Column<int>(type: "int", nullable: false),
                    Train_Coach_Number = table.Column<int>(type: "int", nullable: false),
                    Train_Number = table.Column<int>(type: "int", nullable: false),
                    TrainsID = table.Column<int>(type: "int", nullable: true),
                    adminId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admin_Tickets_Admin_adminId",
                        column: x => x.adminId,
                        principalTable: "Admin",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Admin_Tickets_Trains_TrainsID",
                        column: x => x.TrainsID,
                        principalTable: "Trains",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Card_Number = table.Column<int>(type: "int", nullable: false),
                    Expiration_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CVV = table.Column<int>(type: "int", nullable: false),
                    User_Phone_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Current_bank_account = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total_amount_in_card = table.Column<double>(type: "float", nullable: true),
                    Booking_Fee = table.Column<double>(type: "float", nullable: false),
                    usersID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Payment_Users_usersID",
                        column: x => x.usersID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Suggestions_Complaints",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message_Type = table.Column<int>(type: "int", nullable: true),
                    First_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    usersID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suggestions_Complaints", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Suggestions_Complaints_Users_usersID",
                        column: x => x.usersID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "User_Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time_Pickup = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pickup_Station = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seat_Number = table.Column<int>(type: "int", nullable: false),
                    Ticket_Money = table.Column<double>(type: "float", nullable: false),
                    Ticket_Class = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Train_Coach_Number = table.Column<int>(type: "int", nullable: false),
                    Train_Number = table.Column<int>(type: "int", nullable: false),
                    usersID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Tickets_Users_usersID",
                        column: x => x.usersID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admin_Tickets_adminId",
                table: "Admin_Tickets",
                column: "adminId");

            migrationBuilder.CreateIndex(
                name: "IX_Admin_Tickets_TrainsID",
                table: "Admin_Tickets",
                column: "TrainsID");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_usersID",
                table: "Payment",
                column: "usersID");

            migrationBuilder.CreateIndex(
                name: "IX_Suggestions_Complaints_usersID",
                table: "Suggestions_Complaints",
                column: "usersID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Tickets_usersID",
                table: "User_Tickets",
                column: "usersID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin_Tickets");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Suggestions_Complaints");

            migrationBuilder.DropTable(
                name: "User_Tickets");

            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Trains");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
