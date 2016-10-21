﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicFall2016.Migrations
{
    public partial class RequiredNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genres",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Artists",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genres",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Artists",
                nullable: true);
        }
    }
}
