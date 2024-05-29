using AutoMapper;
using Azure;
using DataLibrary.Repositories;
using DataLibrary.Repositories.Interfaces;
using ModelsLibrary.DtoModels;
using ModelsLibrary.DtoModels.Store;
using ModelsLibrary.Enums;
using ModelsLibrary.Exceptions.Store;
using ModelsLibrary.Models;
using ServiceLibrary.Helpers;
using ServiceLibrary.Services.Interfaces;

namespace ServiceLibrary.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;
        public StoreService(IStoreRepository storeRepository, IMapper mapper)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
        }

        public async Task<StoreDto> Add(UpdateStoreDto dto)
        {
            var storeToAdd = await _storeRepository.Add(_mapper.Map<Store>(dto));
            return _mapper.Map<StoreDto>(storeToAdd);
        }

        public async Task Delete(int id)
        {
            var store = await _storeRepository.GetStoreById(id) ?? throw new StoreNotFoundException();
            await _storeRepository.Delete(store);
        }

        public async Task<PageDto<StoreDto>> GetAll(int pageNumber, int pageSize)
        {
            var page = await _storeRepository.GetAll(pageNumber, pageSize);
            PageDto<StoreDto> pageDto = new(_mapper.Map<List<StoreDto>>(page.Data), page.TotalCount);
            return pageDto;
        }

        public async Task<List<StoreDto>> GetAll()
        {
            var stores = await _storeRepository.GetAll();
            return stores.Select(_mapper.Map<StoreDto>).ToList();
        }

        public async Task<List<BuyerStoreDto>> BuyerGetAll()
        {
            var stores = await _storeRepository.GetAll();
            return stores.Select(_mapper.Map<BuyerStoreDto>).ToList();
        }

        public async Task<List<BuyerStoreLocationDto>> BuyerGetAllByLocation(BuyerLocationDto buyerLocationDto)
        {
            var stores = await _storeRepository.GetAll();
            foreach (var store in stores)
            {
                store.Distance = LocationCalculator.CalculateDistance(buyerLocationDto.Latitude, buyerLocationDto.Longitude, store.Latitude, store.Longitude);
            }
            stores.Sort((s1, s2) => s1.Distance.CompareTo(s2.Distance));

            return stores.Select(_mapper.Map<BuyerStoreLocationDto>).ToList();
        }

        public async Task<StoreDto> GetStoreById(int id)
        {
            var store = await _storeRepository.GetStoreById(id) ?? throw new StoreNotFoundException();
            return _mapper.Map<StoreDto>(store);


        }
        public async Task<List<StoreDto>> GetStoresByUserId(int userId)
        {
            var stores = await _storeRepository.GetStoresByUserId(userId);
            return stores.Select(_mapper.Map<StoreDto>).ToList().ToList();
        }

        public async Task Update(UpdateStoreDto dto)
        {
            var store = await _storeRepository.GetStoreById(dto.Id ?? -1) ?? throw new StoreNotFoundException();
            store.Update(dto.Name, dto.Address, dto.UserId);
            await _storeRepository.Update(store);
        }

    }
}
