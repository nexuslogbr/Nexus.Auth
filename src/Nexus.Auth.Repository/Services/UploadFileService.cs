using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using ExcelDataReader;
using Microsoft.Extensions.Configuration;

using Nexus.Auth.Domain.Enums;
using Nexus.Auth.Repository.Dtos.Auth;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.UploadFile;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Utils;
using Nexus.Auth.Repository.Dtos.VehicleInfo;
using Nexus.Auth.Repository.Models;
using Nexus.Auth.Repository.Interfaces;
using Nexus.Auth.Repository.Dtos.OrderService;
using Nexus.Auth.Repository.Dtos.Model;
using System.Collections.Generic;

namespace Nexus.Auth.Repository.Services;

public class UploadFileService : IUploadFileService
{
    private readonly IAccessDataService _accessDataService;
    private readonly ICustomerService _customerService;
    private readonly IRequesterService _requesterService;
    private readonly IManufacturerService _manufacturerService;
    private readonly IModelService _modelService;
    private readonly IServiceService _serviceService;
    private readonly IPlaceService _placeService;
    private readonly IFileVpcService _fileVpcService;
    private readonly IConfiguration _configuration;
    private Dictionary<string, int> _headers;
    private Dictionary<UploadTypeEnum, string[]> _validHeaders = new();
    private readonly string[] CHASSIS_VALID_HEADERS = { "local", "cliente", "código solicitante", "solicitante", "chassi", "data faturamento", "serviço", "rua", "vaga", "placa" };

    public UploadFileService(IAccessDataService accessDataService, IConfiguration configuration,
        ICustomerService customerService, IRequesterService requesterService, IManufacturerService manufacturerService, IModelService modelService, IServiceService serviceService, IPlaceService placeService, IFileVpcService fileVpcService)
    {
        _accessDataService = accessDataService;
        _configuration = configuration;

        _customerService = customerService;
        _requesterService = requesterService;
        _manufacturerService = manufacturerService;
        _modelService = modelService;
        _serviceService = serviceService;
        _placeService = placeService ?? throw new ArgumentNullException(nameof(placeService));
        _fileVpcService = fileVpcService;
        _validHeaders.Add(UploadTypeEnum.Chassis, CHASSIS_VALID_HEADERS);
    }

    public async Task<GenericCommandResult<PageList<UploadFileResponseDto>>> GetAll(PageParams pageParams, string path)
    {
        var result = await _accessDataService.PostDataAsync<PageList<UploadFileResponseDto>>(path, "api/v1/UploadFile/GetAll", pageParams);
        if (result is not null)
            return new GenericCommandResult<PageList<UploadFileResponseDto>>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<PageList<UploadFileResponseDto>>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<UploadFileResponseDto>> GetById(GetById dto, string path)
    {
        var result = await _accessDataService.PostDataAsync<UploadFileResponseDto>(path, "api/v1/UploadFile/GetById", dto);
        if (result is not null)
            return new GenericCommandResult<UploadFileResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<UploadFileResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<TokenDto>> Delete(GetById obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<TokenDto>(path, "api/v1/UploadFile/Delete", obj);
        if (result is not null)
            return new GenericCommandResult<TokenDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<TokenDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<UploadFileResponseDto>> Post(UploadFileDto dto, string path)
    {
        var result = await _accessDataService.PostFormDataAsync<UploadFileResponseDto>(path, "api/v1/UploadFile/Post", dto);
        if (result is not null)
            return new GenericCommandResult<UploadFileResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<UploadFileResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<UploadFileResponseDto>> ChangeInfo(UploadFileChangeInfoDto dto, string path)
    {
        var result = await _accessDataService.PostDataAsync<UploadFileResponseDto>(path, "api/v1/UploadFile/ChangeInfo", dto);
        if (result is not null)
            return new GenericCommandResult<UploadFileResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<UploadFileResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }


    #region Convert spreadsheet methods

    public UploadFileDto FilterFileData(IFormFile file, UploadTypeEnum type)
    {
        var datas = GetFileData(file, type).ToList();
        var model = new UploadFileDto { File = file, Type = type, OrderService = new List<OrderServiceDto>() };

        foreach (var data in datas)
        {
            if (data.Success)
                model.ConcludedRegisters++;
            else
                model.FailedRegisters++;

            model.OrderService.Add(data);
        }

        return model;
    }

    private IEnumerable<OrderServiceDto> GetFileData(IFormFile formFile, UploadTypeEnum type)
    {
        var list = new List<OrderServiceDto>();
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        using (var stream = new MemoryStream())
        {
            formFile.CopyTo(stream);
            stream.Position = 0;
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                var isHeaderLine = true;
                var i = 1;
                while (reader.Read()) //Each row of the file
                {
                    if (i > reader.RowCount)
                        break;

                    if (isHeaderLine)
                    {
                        ValidateHeaders(reader, type);
                        isHeaderLine = false;
                    }
                    else
                    {
                        // Check if all values in the row are null or empty
                        var allValuesEmpty = Enumerable.Range(0, reader.FieldCount).All(colIndex => string.IsNullOrWhiteSpace(reader.GetValue(colIndex)?.ToString()));

                        if (!allValuesEmpty)
                        {
                            var data = GetLineData(reader);
                            var result = ValidateDataAsync(data, i, list).Result;
                            list.Add(result);
                        }
                    }
                    i++;
                }
            }
        }
        return list;
    }

    public async Task<OrderServiceDto> ValidateDataAsync(string data, int line, List<OrderServiceDto> list)
    {
        string[] collums = data.Split(';');

        var collumServices = collums[6].Split('=')[1];
        var services = collumServices.Split(",");

        var orderService = new OrderServiceDto()
        {
            Place = collums[0].Split('=')[1],
            Customer = collums[1].Split('=')[1],
            RequesterCode = collums[2].Split('=')[1],
            Requester = collums[3].Split('=')[1],
            Chassis = collums[4].Split('=')[1],
            Invoicing = ValidadeDate(collums[5].Split('=')[1]),
            Services = new List<FileVpcServiceDto>(),
            Street = collums[7].Split('=')[1],
            Parking = ValidadeNumber(collums[8].Split('=')[1]),
            Plate = collums[9].Split('=')[1],
            Error = "",
            Success = true
        };

        var wmi = orderService.Chassis != "" ? orderService.Chassis.Substring(0, 3) : "";
        var vds = orderService.Chassis != "" && orderService.Chassis.Length > 8 ? orderService.Chassis.Substring(3, 5) : "";

        // Tasks for API calls

        if (!string.IsNullOrEmpty(orderService.Chassis))
        {
            var vehicle = _fileVpcService.GetByChassi(new GetByChassi { Chassis = orderService.Chassis }, _configuration["ConnectionStrings:NexusUploadApi"]);
            await Task.WhenAll(vehicle);

            if (vehicle.Result.Data.Id > 0)
            {
                orderService.Success = false;
                orderService.Error = "Já existe uma ordem de serviço aberta para esse veículo, Linha: " + line + ". ";
            }
            else if (list.Any(x => x.Chassis == orderService.Chassis))
            {
                orderService.Success = false;
                orderService.Error = "Chassi duplicado no arquivo, Linha: " + line + ". ";
            }
            else if (!ValidateChassisNumber(orderService.Chassis))
            {

                orderService.Success = false;
                orderService.Error = "Chassi Inválido, Linha: " + line + ". ";
            }
        }
        else if (!ValidateChassisNumber(orderService.Chassis))
        {

            orderService.Success = false;
            orderService.Error = "Chassi Inválido, Linha: " + line + ". ";
        }

        if (orderService.Invoicing == DateTime.MinValue)
        {
            orderService.Success = false;
            orderService.Error = "Data invalida, Linha: " + line + ". ";
        }

        var place = _placeService.GetByName(new GetByName { Name = orderService.Place }, _configuration["ConnectionStrings:NexusCustomerApi"]);
        var customer = _customerService.GetByName(new GetByName { Name = orderService.Customer }, _configuration["ConnectionStrings:NexusCustomerApi"]);
        var requester = _requesterService.GetByNameOrCode(new GetByNameCodeDto { Name = orderService.Requester, Code = orderService.RequesterCode }, _configuration["ConnectionStrings:NexusVpcApi"]);
        var modelTask = _modelService.GetByVds(new GetByVdsDto { Vds = vds }, _configuration["ConnectionStrings:NexusVehicleApi"]);

        // Wait for all tasks to completed
        await Task.WhenAll(modelTask, place, customer, requester);

        // Results
        orderService.Place = place.Result.Data.Name; if (place.Result.Data.Id == 0) { orderService.Error += "Erro: Local Inválido, Linha: " + line + ". "; orderService.Success = false; }
        orderService.PlaceId = place.Result.Data.Id;
        orderService.Customer = customer.Result.Data.Name; if (customer.Result.Data.Id == 0) { orderService.Error += "Erro: Cliente Inválido, Linha: " + line + ". "; orderService.Success = false; }
        orderService.Requester = requester.Result.Data.Name; if (requester.Result.Data.Id == 0) { orderService.Error += "Erro: Solcitante Inválido, Linha: " + line + ". "; orderService.Success = false; }

        var model = modelTask.Result.Data; if (model.Id == 0) { orderService.Error += "Chassi Inválido, Linha: " + line + ". "; orderService.Success = false; }
        else
        {
            var manufacturer = model.Manufacturer;
            orderService.ModelId = model.Id;
            if (model.Id > 0)
            {
                if (!manufacturer.Wmis.Any(w => w.WMI == wmi)) { orderService.Error += "Chassi Inválido, Linha: " + line + ".  "; orderService.Success = false; };

            }
        }

        if (orderService.Plate.Length != 7)
        {
            orderService.Plate = "";
            orderService.Error += "Placa Inválida" + line + ".  ";
        }

        foreach (var name in services)
        {
            var service = _serviceService.GetByName(new GetByName { Name = name }, _configuration["ConnectionStrings:NexusVpcApi"]);
            await Task.WhenAll(service);
            orderService.Services.Add(new FileVpcServiceDto { Service = service.Result.Data.Name });
            if (service.Result.Data.Id == 0) { orderService.Error += "Serviço Inválido, Linha: " + line + ". "; orderService.Success = false; }
        }

        return orderService;
    }

    private bool ValidateChassisNumber(string chassis)
    {
        if (string.IsNullOrWhiteSpace(chassis) || chassis.Length != 17)
            return false;
        return new Regex("^[A-Za-z0-9]{3,3}[A-Za-z0-9]{6,6}[A-Za-z0-9]{2,2}[A-Za-z0-9]{6,6}$").Match(chassis).Success;
    }

    private DateTime ValidadeDate(string date)
    {
        if (DateTime.TryParse(date, out DateTime result))  return result;
        else return DateTime.MinValue;
    }

    private int ValidadeNumber(string input)
    {
        if (int.TryParse(input, out int number)) return number;
        else return 0;
    }

    private string GetLineData(IExcelDataReader reader)
    {
        var sb = new StringBuilder();
        foreach (var header in _headers)
            sb.Append($"{header.Key}={reader.GetValue(_headers[header.Key])};");
        return sb.ToString();
    }

    private void ValidateHeaders(IExcelDataReader reader, UploadTypeEnum type)
        {
            var dictionary = new Dictionary<string, int>();
            var i = 0;
            while (i < 10)
            {
                var value = reader.GetValue(i)?.ToString();
                if (string.IsNullOrWhiteSpace(value)) break;

                if (_validHeaders[type].Contains(value.ToLower()))
                    dictionary.Add(value.ToLower(), i);

                i++;

            }

            _headers = dictionary;
        }

        #endregion
}