using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restorator.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddDbFunctions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //GetReservationsGroupedFiltered
            migrationBuilder.Sql("""
                      CREATE FUNCTION [dbo].[GetReservationsGroupedFiltered](@ownnerId int, @date VARCHAR(10), @restraurantId int = NULL)  
                      RETURNS TABLE  
                      AS  
                      RETURN  
                      SELECT DATEPART(dw, ReservationEnd) AS DayOfWeek, COUNT(*) AS Count, rs.Name AS RestaurantName
                      FROM Reservations rv 
                      JOIN Restaurants rs on rv.RestaurantId = rs.Id
                      WHERE rs.OwnerId = @ownnerId AND (@restraurantId IS NULL OR rs.Id = @restraurantId) AND year(ReservationEnd) = year(@date) and month(ReservationEnd) = month(@date)
                      GROUP BY DATEPART(dw, ReservationEnd), rs.Name
                      """);

            //GetMostReservedDay
            migrationBuilder.Sql("""
                      CREATE FUNCTION [dbo].[GetMostReservedDay](@ownnerId int, @date VARCHAR(10), @restraurantId int = NULL)
                      RETURNS TABLE  
                      AS  
                      RETURN 
                      SELECT Cast(ReservationEnd as date) AS Date, COUNT(*) AS Rate, rs.Name AS RestaurantName
                      FROM Reservations rv 
                      JOIN Restaurants rs on rv.RestaurantId = rs.Id
                      WHERE rs.OwnerId = @ownnerId AND (@restraurantId IS NULL OR rs.Id = @restraurantId) AND year(ReservationEnd) = year(@date) and month(ReservationEnd) = month(@date)
                      GROUP BY ReservationEnd, rs.Name
                      """);

            //GetMonthReservationsStatuses
            migrationBuilder.Sql("""
                      CREATE FUNCTION [dbo].[GetMonthReservationsStatuses](@ownnerId int, @date VARCHAR(10), @restraurantId int = NULL)
                      RETURNS TABLE  
                      AS  
                      RETURN
                      (
                      WITH ReservationsRange AS
                      (
                      SELECT rv.Id, rv.ReservationStart, rv.ReservationEnd, rv.Canceled
                      FROM Reservations rv 
                      JOIN Restaurants rs on rv.RestaurantId = rs.Id
                      WHERE rs.OwnerId = @ownnerId AND (@restraurantId IS NULL OR rs.Id = @restraurantId) AND year(ReservationEnd) = year(@date) and month(ReservationEnd) = month(@date)
                      )
                      SELECT 
                      (SELECT COUNT(*) FROM ReservationsRange WHERE Canceled = 1) AS Canceled,
                      (SELECT COUNT(*) FROM ReservationsRange WHERE Canceled = 0 AND ReservationEnd < GETDATE()) AS Finished,
                      (SELECT COUNT(*) FROM ReservationsRange WHERE Canceled = 0 AND ReservationEnd > GETDATE()) AS Waiting
                      )
                      """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
