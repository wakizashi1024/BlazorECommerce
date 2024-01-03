using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorECommerce.Server.Migrations
{
    public partial class AddProductSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { 1, "《麥田捕手》（英語：The Catcher in the Rye），中國大陸譯為《麥田裡的守望者》，為美國作家J.D.沙林傑於1951年發表的長篇小說。這部有爭議的作品原本是面向成年讀者的，但迅速因其青春期焦慮和隔絕的主題而在青少年讀者中流行。該書以主人公荷頓·考菲爾德第一人稱口吻講述自己被學校開除學籍後在紐約城遊蕩將近兩晝夜，企圖逃出虛偽的成人世界、去尋求純潔與真理的經歷與感受。", "https://upload.wikimedia.org/wikipedia/commons/thumb/8/89/The_Catcher_in_the_Rye_%281951%2C_first_edition_cover%29.jpg/413px-The_Catcher_in_the_Rye_%281951%2C_first_edition_cover%29.jpg", 9.99m, "麥田捕手" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { 2, "《三體》是中國大陸作家劉慈欣於2006年5月至12月在《科幻世界》雜誌上連載的一部長篇科幻小說，出版後成為中國大陸最暢銷的科幻長篇小說之一。2008年，該書的單行本由重慶出版社出版。本書是「三體系列」（系列原名：地球往事三部曲）的第一部。該系列第二部《三體II：黑暗森林》於2008年5月出版；2010年11月，第三部《三體III：死神永生》出版發行。2011年，在台灣陸續出版。小說的英文版獲得美國科幻奇幻作家協會2014年度「星雲獎」提名，並榮獲2015年雨果獎最佳小說獎。", "https://upload.wikimedia.org/wikipedia/zh/0/0f/Threebody.jpg", 7.99m, "三體" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Price", "Title" },
                values: new object[] { 3, "《安妮的日記》（荷蘭語：Het Achterhuis）由安妮·弗蘭克所寫，此書發行版的內容摘錄自安妮在納粹佔領荷蘭的時期所寫的日記內容，並於戰後由她倖存的父親奧托·弗蘭克加以整理出版。", "https://upload.wikimedia.org/wikipedia/zh/4/44/First_edition.jpg", 6.99m, "安妮日記" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
