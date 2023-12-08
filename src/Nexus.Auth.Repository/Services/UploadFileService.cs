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

namespace Nexus.Auth.Repository.Services;

public class UploadFileService : IUploadFileService
{
    private readonly IAccessDataService _accessDataService;
    private readonly ICustomerService _customerService;
    private readonly IRequesterService _requesterService;
    private readonly IManufacturerService _manufacturerService;
    private readonly IModelService _modelService;
    private readonly IServiceService _serviceService;

    private readonly IConfiguration _configuration;
    private Dictionary<string, int> _headers;
    private Dictionary<UploadTypeEnum, string[]> _validHeaders = new();
    private readonly string[] CHASSIS_VALID_HEADERS = { "cliente", "código solicitante", "solicitante", "fabricante", "modelo", "chassi", "data faturamento", "serviço", "rua", "vaga", "placa" };

    public UploadFileService(IAccessDataService accessDataService, IConfiguration configuration,
        ICustomerService customerService, IRequesterService requesterService, IManufacturerService manufacturerService, IModelService modelService, IServiceService serviceService)
    {
        _accessDataService = accessDataService;
        _configuration = configuration;

        _customerService = customerService;
        _requesterService = requesterService;
        _manufacturerService = manufacturerService;
        _modelService = modelService;
        _serviceService = serviceService;
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
        var datas = GetFileData(file, type);
        var resgisters = new List<string>();

        var errorList = new List<string>();
        var model = new UploadFileDto { File = file, Type = type };

        foreach (var data in datas)
        {
            if (data.Errors.Count > 0)
            {
                foreach (var error in data.Errors)
                {
                    errorList.Add("Erro:" + error.Error + ", Linha:" + error.Line);
                }
                model.FailedRegisters++;
            }
            else
                model.ConcludedRegisters++;
        }

        return model;
    }

    private IEnumerable<UploadFileResult> GetFileData(IFormFile formFile, UploadTypeEnum type)
    {
        var list = new List<UploadFileResult>();
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        using (var stream = new MemoryStream())
        {
            formFile.CopyTo(stream);
            stream.Position = 0;
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                var isHeaderLine = true;
                var i = 0;
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
                            var result = ValidateDataAsync(data, i).Result;
                            list.Add(result);
                        }
                    }
                    i++;
                }
            }
        }
        return list;
    }

    public async Task<UploadFileResult> ValidateDataAsync(string data, int line)
    {
        var result = new UploadFileResult();
        string[] collums = data.Split(';');

        var chassi = collums[5].Split('=')[1];
        if (!ValidateChassisNumber(chassi))
            result.Errors.Add(new UploadFileErrorDto() {  Error = "Chassi number invalid", Line = line });

        var invoice = Convert.ToDateTime(collums[6].Split('=')[1]);
        var park = Convert.ToInt32(collums[9].Split('=')[1]);
        
        var collum = collums[7].Split('=')[1];
        var services = collum.Split(",");

        var orderService = new OrderServiceDto()
        {
            Customer = collums[0].Split('=')[1],
            RequesterCode = collums[1].Split('=')[1],
            Requester = collums[2].Split('=')[1],
            Manufacturer = collums[3].Split('=')[1],
            Model = collums[4].Split('=')[1],
            ChassisNumber = chassi,
            Invoicing = invoice,
            Services = services.ToList(),
            Street = collums[8].Split('=')[1],
            Parking = park,
            Plate = collums[10].Split('=')[1]
        };

        // Tasks for API calls
        var customerTask = _customerService.GetByName(new GetByName { Name = orderService.Customer }, _configuration["ConnectionStrings:NexusCustomerApi"]);
        var manufacturerTask = _manufacturerService.GetByName(new GetByName { Name = orderService.Manufacturer }, _configuration["ConnectionStrings:NexusVehicleApi"]);
        var modelTask = _modelService.GetByName(new GetByName { Name = orderService.Model }, _configuration["ConnectionStrings:NexusVehicleApi"]);
        var requesterTask = _requesterService.GetByName(new GetByName { Name = orderService.Requester }, _configuration["ConnectionStrings:NexusVpcApi"]);

        // Wait for all tasks to completed
        await Task.WhenAll(customerTask, manufacturerTask, modelTask, requesterTask);

        // Results
        var customer = customerTask.Result.Data; if (customer.Id == 0) result.Errors.Add(new UploadFileErrorDto() { Error = "Customer invalid", Line = line });
        var manufacturer = manufacturerTask.Result.Data; if (manufacturer.Id == 0) result.Errors.Add(new UploadFileErrorDto() { Error = "Manufacturer invalid", Line = line });
        var model = modelTask.Result.Data; if (model.Id == 0) result.Errors.Add(new UploadFileErrorDto() { Error = "Model invalid", Line = line });
        var requester = requesterTask.Result.Data; if (requester.Id == 0) result.Errors.Add(new UploadFileErrorDto() { Error = "Requester invalid", Line = line });

        foreach (var name in services)
        {
            var serviceTask = _serviceService.GetByName(new GetByName { Name = name }, _configuration["ConnectionStrings:NexusVpcApi"]);
            await Task.WhenAll(serviceTask);
            var service = serviceTask.Result.Data; if (service.Id == 0) result.Errors.Add(new UploadFileErrorDto() { Error = "service invalid:" + name, Line = line });
        }
        result.OrderService = orderService;

        return result;
    }

    private bool ValidateChassisNumber(string chassis)
    {
        if (string.IsNullOrWhiteSpace(chassis) || chassis.Length != 17)
            return false;
        return new Regex("^[A-Za-z0-9]{3,3}[A-Za-z0-9]{6,6}[A-Za-z0-9]{2,2}[A-Za-z0-9]{6,6}$").Match(chassis).Success;
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
        while (i < 11)
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