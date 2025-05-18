using FluentResults;
using Microsoft.EntityFrameworkCore;
using Restorator.DataAccess.Data;
using Restorator.DataAccess.Data.Entities;
using Restorator.DataAccess.Data.Entities.Enums;
using Restorator.Domain.Models.Templates;
using Restorator.Domain.Services;

namespace Restorator.Application.Server.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly RestoratorDbContext _context;
        private readonly IUserManager _userManager;
        public TemplateService(RestoratorDbContext context,
                               IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Result<int>> CreateRestaurantTemplate(CreateRestaurantTemplateDTO model)
        {
            if (!_userManager.TryGetUserId(out var userId))
                return Result.Fail("Не удалось получить id пользователя");

            var user = await _context.Users.AsNoTracking()
                                           .Include(u => u.Role)
                                           .SingleOrDefaultAsync(u => u.Id == userId);

            if (user is null)
                return Result.Fail("Пользователь не найден");

            if (user.Role != Roles.Admin)
                return Result.Fail("У вас недостаточно прав, чтобы это сделать");

            var template = new RestaurantTemplate()
            {
                Image = model.Scheme,
                Tables = [.. model.Tables.Select(x => new Table()
                {
                    TableTemplateId = x.TemplateId,
                    X = x.X,
                    Y = x.Y,
                })]
            };

            _context.RestaurantTemplates.Add(template);

            await _context.SaveChangesAsync();

            return template.Id;
        }

        public async Task<Result<int>> CreateTableTemplate(CreateTableTempateDTO model)
        {
            if (!_userManager.TryGetUserId(out var userId))
                return Result.Fail("Не удалось получить id пользователя");

            var user = await _context.Users.AsNoTracking()
                                           .Include(u => u.Role)
                                           .SingleOrDefaultAsync(u => u.Id == userId);

            if (user is null)
                return Result.Fail("Пользователь не найден");

            if (user.Role != Roles.Admin)
                return Result.Fail("У вас недостаточно прав, чтобы это сделать");

            var template = new TableTemplate()
            {
                Height = model.Height,
                Width = model.Width,
                Rotation = model.Rotation,
            };

            _context.TableTemplates.Add(template);

            await _context.SaveChangesAsync();

            return template.Id;
        }

        public async Task<Result> DeleteRestaurantTemplate(int restaurantTemplateId)
        {
            if (!_userManager.TryGetUserId(out var userId))
                return Result.Fail("Не удалось получить id пользователя");

            var template = await _context.RestaurantTemplates.SingleOrDefaultAsync(x => x.Id == restaurantTemplateId);

            if (template is null)
                return Result.Fail("Шаблон не найден");

            var user = await _context.Users.AsNoTracking()
                                           .Include(u => u.Role)
                                           .SingleOrDefaultAsync(u => u.Id == userId);

            if (user is null)
                return Result.Fail("Пользователь не найден");

            if (user.Role != Roles.Admin)
                return Result.Fail("Недостаточно прав для отмены бронирования");

            template.Deleted = true;

            _context.RestaurantTemplates.Update(template);

            await _context.SaveChangesAsync();

            return Result.Ok();
        }

        public async Task<IReadOnlyCollection<RestaurantTemplatePreview>> GetRestaurantsTemplatePreview()
        {
            return await _context.RestaurantTemplates.AsNoTracking()
                .Where(x => !x.Deleted)
                .Select(x => new RestaurantTemplatePreview()
                {
                    Id = x.Id,
                    Scheme = x.Image
                }).ToListAsync();
        }

        public async Task<Result<RestaurantTemplateDTO>> GetRestaurantTemplate(int restaurantTemplateId)
        {
            var template = await _context.RestaurantTemplates.AsNoTracking()
                .Where(x => !x.Deleted)
                .Select(x => new RestaurantTemplateDTO
                {
                    Id = x.Id,
                    Scheme = x.Image,
                    Tables = x.Tables.Select(x => new RestaurantTemplateTableDTO
                    {
                        Id = x.Id,
                        TemplateId = x.TableTemplateId,
                        X = x.X,
                        Y = x.Y,
                    })
                }).SingleOrDefaultAsync(x => x.Id == restaurantTemplateId);

            if (template is null)
                return Result.Fail("Шаблон не найден");

            return template;
        }

        public async Task<IReadOnlyCollection<TableTemplateDTO>> GetTableTemplates()
        {
            return await _context.TableTemplates.AsNoTracking()
                .Where(x => x.Rotation == 0)
                .Select(x => new TableTemplateDTO()
                {
                    Id = x.Id,
                    Height = x.Height,
                    Width = x.Width,
                }).ToListAsync();
        }

        public async Task<Result> UpdateRestaurantTemplate(UpdateRestaurantTemplateDTO model)
        {
            if (!_userManager.TryGetUserId(out var userId))
                return Result.Fail("Не удалось получить id пользователя");

            var user = await _context.Users.AsNoTracking()
                                           .Include(u => u.Role)
                                           .SingleOrDefaultAsync(u => u.Id == userId);

            if (user is null)
                return Result.Fail("Пользователь не найден");

            if (user.Role != Roles.Admin)
                return Result.Fail("У вас недостаточно прав, чтобы это сделать");


            var template = await _context.RestaurantTemplates.Include(r => r.Tables)
                                                             .SingleOrDefaultAsync(r => r.Id == model.Id);

            if (template is null)
                return Result.Fail("Не найден шаблон ресторана");

            template.Image = model.Scheme;

            var tables = template.Tables.ToList();

            foreach (var tableModel in model.Tables) //DTO loop
            {
                var table = tables.SingleOrDefault(m => m.Id == tableModel.Id); // from db

                if (table is null)
                {
                    tables.Add(new Table()
                    {
                        TableTemplateId = tableModel.TemplateId,
                        X = tableModel.X,
                        Y = tableModel.Y,
                    });

                    continue;
                }

                table.X = tableModel.X;
                table.Y = tableModel.Y;
                table.TableTemplateId = tableModel.TemplateId;
            }

            template.Tables = tables; //for god sake

            _context.RestaurantTemplates.Update(template);

            await _context.SaveChangesAsync();

            return Result.Ok();
        }
    }
}