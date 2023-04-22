using BusinessLogic.Validators;
using DataAccess.Repositories;
using DTOs;

namespace BusinessLogic.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ClientValidator _clientValidator;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
            _clientValidator = new ClientValidator();
        }

        public async Task<Result<ClientDto>> CreateAsync(ClientDto clientDto)
        {
            if (clientDto == null)
            {
                return Result<ClientDto>.Failure(MessageConstants.MsgNoDataReceived);
            }
            clientDto.StatusId = ClientStatusEnum.AVAILABLE;


            var result = GetModelValidationResult(clientDto);
            if (!result.IsSuccess) return result;


            clientDto = await _clientRepository.SaveAsync(clientDto);


            return Result<ClientDto>.Success(clientDto);
        }


        public async Task<Result<ClientDto>> GetByIdAsync(int clientId)
        {
            var clientDto = await _clientRepository.GetByIdAsync(clientId);
            if (clientDto == null)
            {
                return Result<ClientDto>.Failure(MessageConstants.MsgClientNotFound);
            }


            return Result<ClientDto>.Success(clientDto);
        }
        public async Task<Result<List<ClientDto>>> GetAllAsync()
        {
            ICollection<ClientDto> clientDtos = await _clientRepository.GetAllAsync();


            return Result<List<ClientDto>>.Success(clientDtos.ToList());
        }
        public async Task<Result<List<ClientDto>>> GetClientsByStatusAsync(ClientStatusEnum status)
        {
            ICollection<ClientDto> clientDtos = await _clientRepository.GetClientsByStatusAsync(status);


            return Result<List<ClientDto>>.Success(clientDtos.ToList());
        }


        public async Task<Result<ClientDto>> UpdateAsync(ClientDto clientDto)
        {
            if (clientDto == null)
            {
                return Result<ClientDto>.Failure(MessageConstants.MsgNoDataReceived);
            }


            var result = GetModelValidationResult(clientDto);
            if (!result.IsSuccess) return result;


            bool updated = await _clientRepository.UpdateAsync(clientDto.Id, clientDto);
            if (!updated) return Result<ClientDto>.Failure(MessageConstants.MsgClientNotFound);


            return Result<ClientDto>.Success(clientDto);
        }


        public async Task<Result<ClientDto>> DeleteAsync(int clientId)
        {
            bool deleted = await _clientRepository.DeleteAsync(clientId);
            if (!deleted) return Result<ClientDto>.Failure(MessageConstants.MsgClientNotFound);


            return Result<ClientDto>.Success();
        }


        private Result<ClientDto> GetModelValidationResult(ClientDto clientDto)
        {
            var validationResult = _clientValidator.Validate(clientDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(x => x.ErrorMessage);
                return Result<ClientDto>.Failure(errors);
            }


            return Result<ClientDto>.Success();
        }
    }
}
