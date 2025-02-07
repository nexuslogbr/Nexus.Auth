using AutoMapper;
using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.Place;
using Nexus.Auth.Repository.Handlers.Interfaces;
using Nexus.Auth.Repository.Models;
using Nexus.Auth.Repository.Params;
using Nexus.Auth.Repository.Services.Interfaces;

namespace Nexus.Auth.Repository.Handlers
{
    public class PlaceHandler : IPlaceHandler
    {
        private readonly IPlaceService _placeService;
        private readonly IMapper _mapper;

        public PlaceHandler(IPlaceService placeService, IMapper mapper)
        {
            _placeService = placeService;
            _mapper = mapper;
        }

        public async Task<PageList<PlaceModel>> GetAll(PlaceParams pageParams, int placeId)
        {
            var response = (await _placeService.GetAsync(
                pageParams.PageNumber, pageParams.pageSize, pageParams.Filter(), pageParams.OrderByProperty, pageParams.Asc, includeProps: "")).Where(x => x.PlaceData == placeId);

            return new PageList<PlaceModel>(
                _mapper.Map<List<PlaceModel>>(response),
                //await _placeService.CountAsync(),
                response.Count(),
                pageParams.PageNumber,
                pageParams.PageSize);
        }

        public async Task<PlaceModel> GetById(int id)
        {
            return _mapper.Map<PlaceModel>(await _placeService.GetByIdAsync(id, ""));
        }

        public async Task<List<PlaceModel>> GetByIds(List<int> ids)
        {
            return _mapper.Map<List<PlaceModel>>(await _placeService.GetByIdsAsync(ids));
        }

        public async Task<ChangeStatusDto> ChangeStatus(ChangeStatusDto dto)
        {
            var customer = await _placeService.GetByIdAsync(dto.Id);
            if (customer is null)
                throw new Exception("Lugar não encontrado.");
            _placeService.ChangeStatus(customer, dto.Blocked);
            await _placeService.SaveChangesAsync();
            return dto;
        }

        public async Task<PlaceModel> Add(PlaceDto entity)
        {
            var place = await _placeService.AddAsync(_mapper.Map<Place>(entity));
            await _placeService.SaveChangesAsync();
            return _mapper.Map<PlaceModel>(place);
        }

        public async Task<PlaceModel> Update(PlacePutDto entity)
        {
            var place = await _placeService.GetByIdAsync(entity.Id, "");
            if (place is null)
                throw new Exception("Local não encontrado.");

            var updated = _mapper.Map(entity, place);
            var response = _placeService.Update(updated);

            await _placeService.SaveChangesAsync();
            return _mapper.Map<PlaceModel>(response);
        }

        public async Task<bool> Delete(int id)
        {
            await _placeService.DeleteByIdAsync(id);
            return await _placeService.SaveChangesAsync();
        }

        public async Task<PlaceModel> GetByName(string name)
        {
            return _mapper.Map<PlaceModel>(await _placeService.GetByNameAsync(name));
        }
    }
}
