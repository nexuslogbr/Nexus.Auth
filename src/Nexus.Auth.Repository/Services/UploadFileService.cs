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

namespace Nexus.Auth.Repository.Services;

public class UploadFileService : IUploadFileService
{
    private readonly IAccessDataService _accessDataService;
    private readonly IConfiguration _configuration;
    private Dictionary<string, int> _headers;
    private Dictionary<UploadTypeEnum, string[]> _validHeaders = new();
    private readonly string[] CHASSIS_VALID_HEADERS = { "cliente", "código solicitante", "solicitante", "fabricante", "modelo", "chassi", "data faturamento", "serviço", "rua", "vaga", "placa" };

    public UploadFileService(IAccessDataService accessDataService, IConfiguration configuration)
    {
        _accessDataService = accessDataService;
        _configuration = configuration;
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

    public IEnumerable<string> GetFileData(IFormFile formFile, UploadTypeEnum type)
    {
        var stringList = new List<string>();
        System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
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
                            var res = GetLineData(reader);
                            stringList.Add(res);

                            var valid = ValidateDataAsync(res);
                        }
                    }
                    i++;


                }
            }
        }
        return stringList;
    }

    public async Task<bool> ValidateDataAsync(string data)
    {
        string[] collums = data.Split(';');
        var datasToOS = new List<VehicleDto>();

        var chassi = collums[5].Split('=')[1];
        var invoice = Convert.ToDateTime(collums[6].Split('=')[1]);
        var park = Convert.ToInt32(collums[9].Split('=')[1]);

        var register = new VehicleDto()
        {
            Customer = collums[0].Split('=')[1],
            Requester = collums[1].Split('=')[1],
            RequesterCode = collums[2].Split('=')[1],
            Manufacturer = collums[3].Split('=')[1],
            Model = collums[4].Split('=')[1],
            ChassisNumber = chassi,
            Invoicing = invoice,
            Service = collums[7].Split('=')[1],
            Street = collums[8].Split('=')[1],
            Parking = park,
            Plate = collums[10].Split('=')[1]
        };

        var customer = await _accessDataService.PostDataAsync<CustomerModel>(_configuration["ConnectionStrings:NexusCustomerApi"], "api/v1/Customer/GetByName", new GetByName { Name = register.Customer });
        var requester = await _accessDataService.PostDataAsync<CustomerModel>(_configuration["ConnectionStrings:NexusVpcApi"], "api/v1/Customer/GetByName", new GetByName { Name = register.Customer });
        var manufacturer = await _accessDataService.PostDataAsync<CustomerModel>(_configuration["ConnectionStrings:NexusVehicleApi"], "api/v1/Customer/GetByName", new GetByName { Name = register.Customer });
        var model = await _accessDataService.PostDataAsync<CustomerModel>(_configuration["ConnectionStrings:NexusVehicleApi"], "api/v1/Customer/GetByName", new GetByName { Name = register.Customer });
        var vehicle = await _accessDataService.PostDataAsync<CustomerModel>(_configuration["ConnectionStrings:NexusCustomerApi"], "api/v1/Customer/GetByName", new GetByName { Name = register.Customer });
        var service = await _accessDataService.PostDataAsync<CustomerModel>(_configuration["ConnectionStrings:NexusVpcApi"], "api/v1/Customer/GetByName", new GetByName { Name = register.Customer });



        return true;
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