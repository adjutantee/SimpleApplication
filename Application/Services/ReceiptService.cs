using Application.DTO;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models;
using Domain.Repositories.Interfaces;

namespace Application.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly IReceiptRepository _receiptRepository;
        private readonly IReceiptItemRepository _receiptItemRepository;
        private readonly IMapper _mapper;

        public ReceiptService(
            IReceiptRepository receiptRepository,
            IReceiptItemRepository receiptItemRepository,
            IMapper mapper)
        {
            _receiptRepository = receiptRepository;
            _receiptItemRepository = receiptItemRepository;
            _mapper = mapper;
        }

        public async Task<ReceiptDto> CreateReceiptAsync(ReceiptWithItemsDto dto)
        {
            // Валидация бизнес-правил
            if (dto.Receipt == null) throw new ArgumentNullException("Receipt is required");
            if (dto.Items == null || !dto.Items.Any())
                throw new InvalidOperationException("Receipt must have at least one item");

            // Маппинг и создание документа
            var receipt = _mapper.Map<Receipt>(dto.Receipt);
            var createdReceipt = await _receiptRepository.AddAsync(receipt);

            // Создание связанных ресурсов
            foreach (var itemDto in dto.Items)
            {
                var item = _mapper.Map<ReceiptItem>(itemDto);
                item.ReceiptId = createdReceipt.Id;
                await _receiptItemRepository.AddAsync(item);
            }

            return _mapper.Map<ReceiptDto>(createdReceipt);
        }

        public async Task<bool> DeleteReceiptAsync(int id)
        {
            // Бизнес-правило: сначала удаляем ресурсы
            await _receiptItemRepository.DeleteByReceiptIdAsync(id);

            // Затем удаляем документ
            return await _receiptRepository.DeleteAsync(id);
        }

        public async Task<List<ReceiptDto>> GetAllReceiptsAsync()
        {
            var receipts = await _receiptRepository.GetAllWithItemsAsync();
            return _mapper.Map<List<ReceiptDto>>(receipts);
        }

        public async Task<ReceiptWithItemsDto> GetReceiptByIdAsync(int id)
        {
            var receipt = await _receiptRepository.GetByIdWithItemsAsync(id);
            if (receipt == null) return null;

            return new ReceiptWithItemsDto
            {
                Receipt = _mapper.Map<ReceiptDto>(receipt),
                Items = _mapper.Map<List<ReceiptItemDto>>(receipt.ReceiptItems)
            };
        }

        public async Task<ReceiptDto> UpdateReceiptAsync(ReceiptWithItemsDto dto)
        {
            // Валидация
            if (dto.Receipt == null) throw new ArgumentNullException("Receipt is required");
            if (dto.Items == null || !dto.Items.Any())
                throw new InvalidOperationException("Receipt must have at least one item");

            // Обновление документа
            var receipt = _mapper.Map<Receipt>(dto.Receipt);
            await _receiptRepository.UpdateAsync(receipt);

            // Обновление ресурсов: полная замена
            await _receiptItemRepository.DeleteByReceiptIdAsync(receipt.Id);
            foreach (var itemDto in dto.Items)
            {
                var item = _mapper.Map<ReceiptItem>(itemDto);
                item.ReceiptId = receipt.Id;
                await _receiptItemRepository.AddAsync(item);
            }

            return _mapper.Map<ReceiptDto>(receipt);
        }

        public async Task<List<ReceiptDto>> GetFilteredReceiptsAsync(ReceiptFilterDto filter)
        {
            var validatedFilter = ValidateFilter(filter);

            var receipts = await _receiptRepository.GetFilteredAsync(
                validatedFilter.StartDate,
                validatedFilter.EndDate,
                validatedFilter.DocumentNumbers,
                validatedFilter.ResourceIds,
                validatedFilter.UnitIds);

            return _mapper.Map<List<ReceiptDto>>(receipts);
        }

        private ReceiptFilterDto ValidateFilter(ReceiptFilterDto filter)
        {
            // Пример бизнес-правила: ограничение периода фильтрации
            if (filter.StartDate.HasValue && filter.EndDate.HasValue)
            {
                var maxPeriod = TimeSpan.FromDays(365);
                if ((filter.EndDate.Value - filter.StartDate.Value) > maxPeriod)
                {
                    throw new ArgumentException("Filter period cannot exceed 1 year");
                }
            }
            return filter;
        }
    }
}
