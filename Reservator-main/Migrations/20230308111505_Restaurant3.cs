﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservator.Migrations
{
    public partial class Restaurant3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Restaurants",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Restaurants");
        }
    }
}
