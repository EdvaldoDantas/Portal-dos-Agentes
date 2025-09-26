using Portal_dos_Agentes.Server.DTOs;
using Portal_dos_Agentes.Server.Models;
using Portal_dos_Agentes.Server.Repositories.Interfaces;
using Portal_dos_Agentes.Server.Services.Interfaces;
using Portal_dos_Agentes.Server.Services.Utilities;

namespace Portal_dos_Agentes.Server.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _genericRepository;
        private readonly IEmailSender sender;

        public UserService(IGenericRepository<User> genericRepository, IEmailSender sender)
        {
            _genericRepository = genericRepository;
            this.sender = sender;
        }

        public async Task<(bool Success, string sms, User? subAgente)> AddSubAgenteAsync(SubAgentDTO model)
        {
            try
            {
                if(await _genericRepository.ExistsAsync(model.AgenteId))
                    return (false, "Agente já existe", null);

                var (hash, senha) = HashHelper.Hashsenha(model.AgenteId);
                var subAgente = new User
                {
                    Nome = model.Nome,
                    Id = model.AgenteId,
                    Telefone = model.Telefone,
                    Email = model.Email,
                    Senha = hash,
                    Role = "user",
                    Endereco = model.Endereco,
                };
                var result = await _genericRepository.CreateAsync(subAgente);
                if(result == null)
                {
                    return (false, "Não foi possível cadastrar o Agente", null);
                }
                var email = new EmailDTO
                {
                    Nome = subAgente.Nome,
                    Message = $"Olá, a sua conta foi criada com sucesso, use o seu Id de Agente esta senha {senha} para fazer login no Portal dos Agentes\n" +
                    $"Não partilhe os seus dados com ninguém",
                    Subject = "Dados de Conta",
                    To = subAgente.Email,
                };
               var (success, msg) = await sender.SendEmailAsync(email);
               return(true,$"Agente criado com sucesso, As credenciais foram enviadas para o email do agente " +
                    $"Se por alguma razão ele não poder ter acesso ao seu email, aqui estão as credenciaia :" +
                    $" Id do Agente={result.Id} Senha = {senha}" +
                    $" Se esta conta não for usada em 5 dias, ela será eliminada.", result);
            }
            catch(Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public async Task<(bool Success, string sms)> Delete(int id)
        {
            var result = await this.GetByIdAsync(id);
            if(result is null)
                return (false, "Agente não encontrado");

            var resultDelete = await _genericRepository.DeleteAsync(id);
            if(resultDelete is null)
                return (false, "Erro ao deletar o agente");
            return (true, "Agente deletado com sucesso");
        }

        public async Task<List<UserSendDTO>> GetAll()
        {
            var users = await _genericRepository.GetAllAsync();
            return users
                .Where(user => user.Role != "adm")
                .Select(user => new UserSendDTO
                {
                    Id = user.Id,
                    Nome = user.Nome,
                    Telefone = user.Telefone,
                    Email = user.Email,
                    Endereco = user.Endereco,
                    DataNascimento = user.DataNascimento,
                })

                .ToList();
        }

        public async Task<UserSendDTO?> GetByIdAsync(int id)
        {
            var result = await _genericRepository.GetByIdAsync(id);

            return result is null ? null : new UserSendDTO
            {
                Id = result.Id,
                Nome = result.Nome,
                Telefone = result.Telefone,
                Email = result.Email,
                Endereco = result.Endereco,
                DataNascimento = result.DataNascimento,
            };
        }

        public async Task<User?> UpdateAsync(int userId,UserDTO userDto)
        {
            var user = await _genericRepository.GetByIdAsync(userId);
            if (user is null)
                return null;
            user.Nome = userDto.Nome;
            user.Telefone = userDto.Telefone;
            user.Email = userDto.Email;
            user.Senha = HashHelper.Hash(userDto.Senha);
            user.Email = userDto.Email;

            return await _genericRepository.UpdateAsync(user);
        }
    }
}
