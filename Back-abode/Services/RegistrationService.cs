using Back_Abode.Models;
using Back_Proyecto.Models;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;

public class RegistrationService
{
    private readonly IMongoDatabase _mongoDatabase;

    public RegistrationService(IMongoDatabase mongoDatabase)
    {
        _mongoDatabase = mongoDatabase;
    }

    public async Task<RegistrationResult> Register(Registro registro)
    {
        try
        {
            var usersCollection = _mongoDatabase.GetCollection<Users>("Users");

            if (usersCollection.AsQueryable().Any(x => x.email == registro.email))
            {
                return new RegistrationResult
                {
                    Success = false,
                    Message = "El correo electrónico ya está en uso.",
                    Result = ""
                };
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registro.password);

            var nuevoUsuario = new Users
            {
                email = registro.email,
                password = hashedPassword
            };

            await usersCollection.InsertOneAsync(nuevoUsuario);

            return new RegistrationResult
            {
                Success = true,
                Message = "Usuario registrado exitosamente.",
                Result = ""
            };
        }
        catch (Exception ex)
        {
            return new RegistrationResult
            {
                Success = false,
                Message = "Ocurrió un error durante el registro.",
                Result = ex.Message
            };
        }
    }
}
