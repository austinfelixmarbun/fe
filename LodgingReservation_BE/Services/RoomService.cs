using LodgingReservation_BE.Controllers;
using LodgingReservation_BE.DTOs;
using LodgingReservation_BE.Models;
using LodgingReservation_BE.Models.Enum;
using LodgingReservation_BE.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LodgingReservation_BE.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRepository<Room> _roomRepository;
        private readonly IRepository<RoomType> _roomTypeRepository;

        public RoomService(IRepository<Room> roomRepository, IRepository<RoomType> roomTypeRepository)
        {
            _roomRepository = roomRepository;
            _roomTypeRepository = roomTypeRepository;
        }

        public async Task<List<RoomResponse>> GetAllAsync(string? search, int page, int limit)
        {
            var rooms = await _roomRepository.GetAllAsync("RoomType");

            if (!string.IsNullOrWhiteSpace(search))
            {
                rooms = rooms
                    .Where(r => r.RoomNumber.Contains(search, StringComparison.OrdinalIgnoreCase)
                             || (r.RoomType?.Name?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false))
                    .ToList();
            }
            if (page < 1) page = 1;
            if (limit < 1) limit = 10;

            rooms = rooms
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToList();

            return rooms.Select(r => new RoomResponse
            {
                Id = r.Id,
                RoomNumber = r.RoomNumber,
                Status = r.Status
            }).ToList();
        }
        public async Task<Room?> GetByIdAsync(long id)
        {
            return await _roomRepository.GetByIdAsync(id, "RoomType");
        }

        public async Task<RoomResponse?> CreateAsync(CreateRoom dto)
        {
            var roomType = await _roomTypeRepository.GetByIdAsync(dto.RoomTypeId);
            if (roomType == null)
            {
                throw new KeyNotFoundException($"RoomType dengan ID {dto.RoomTypeId} tidak ditemukan.");
            }
            // Logika simpan data kamar baru ke database
            var room = new Room
            {
                RoomNumber = dto.RoomNumber,
                RoomTypeId = dto.RoomTypeId,
                Status = RoomStatus.AVAILABLE
            };

            await _roomRepository.AddAsync(room);
            await _roomRepository.SaveChangesAsync();

            return new RoomResponse
            {
                Id = room.Id,
                RoomNumber = room.RoomNumber
            };
        }

    }
}
