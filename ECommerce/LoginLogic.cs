
namespace ECommerce
{
    using ECommerceDataLayer;
    using ECommerceDataModel;
    using ECommerceDataModel.Shared;
    using ECommerce.Helpers;
    using System;
    using System.Security.Cryptography;
    using System.Collections.Specialized;

    public class LoginLogic
    {
        private readonly LoginDataLayer dataLayer = new LoginDataLayer();
        public const int SaltByteSize = 24;
        public const int HashByteSize = 20; // to match the size of the PBKDF2-HMAC-SHA-1 hash 
        public const int Pbkdf2Iterations = 1000;
        public const int IterationIndex = 0;
        public const int SaltIndex = 1;
        public const int Pbkdf2Index = 2;

        /// <summary>
        /// Permite registrar un nuevo usuario
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public ResponseDTO<CustomerDTO> CustomerExecute(RequestDTO<CustomerDTO> customer)
        {
            var customerResponse = new ResponseDTO<CustomerDTO>();
            try
            {
                switch (customer.OperationType)
                {
                    case OperationType.Insert:
                        customer.Item.EncryptedPassword = HashPassword(customer.Item.Password);
                        customerResponse.Success = dataLayer.RegisterUser(customer.Item);
                        break;
                    case OperationType.ChangePassword:
                        customerResponse.Success = CustomerChangePassword(customer.Item);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception exception)
            {
                exception.LogException();
            }
            return customerResponse;
        }

        /// <summary>
        /// Obtiene un item customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public ResponseDTO<CustomerDTO> CustomerGetItem(RequestDTO<CustomerDTO> customer)
        {
            var customerResponse = new ResponseDTO<CustomerDTO>();
            try
            {
                customerResponse.Result = dataLayer.GetCustomerByEmail(customer.Item);
                customerResponse.Success = customerResponse.Result.Identifier > default(int);
            }
            catch (Exception exception)
            {
                exception.LogException();
            }
            return customerResponse;
        }

        /// <summary>
        /// Permite actualizar la contraseña de un usuario
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool CustomerChangePassword(CustomerDTO customer)
        {
            bool isPasswordUpdate = default(bool);
            try
            {
                var customerItem = CustomerGetItem(new RequestDTO<CustomerDTO> { Item = customer });
                if (customerItem.Success && !string.IsNullOrEmpty(customer.Password) && (customerItem.Result.EncryptedPassword == customer.EncryptedPassword))
                {
                    customer.Identifier = customerItem.Result.Identifier;
                    customer.EncryptedPassword = HashPassword(customer.Password);
                    isPasswordUpdate = dataLayer.CustomerChangePassword(customer);
                }
            }
            catch (Exception exception)
            {
                exception.LogException();
            }
            return isPasswordUpdate;
        }

        /// <summary>
        /// Permite encriptar una contraseña
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string HashPassword(string password)
        {
            var cryptoProvider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SaltByteSize];
            cryptoProvider.GetBytes(salt);

            var hash = GetPbkdf2Bytes(password, salt, Pbkdf2Iterations, HashByteSize);
            return Pbkdf2Iterations + ":" +
                   Convert.ToBase64String(salt) + ":" +
                   Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Permite validar una contraseña
        /// </summary>
        /// <param name="password">Contraseña desencriptada</param>
        /// <param name="correctHash">Contraseña encriptada</param>
        /// <returns></returns>
        public bool ValidatePassword(string password, string correctHash)
        {
            char[] delimiter = { ':' };
            var split = correctHash.Split(delimiter);
            var iterations = Int32.Parse(split[IterationIndex]);
            var salt = Convert.FromBase64String(split[SaltIndex]);
            var hash = Convert.FromBase64String(split[Pbkdf2Index]);

            var testHash = GetPbkdf2Bytes(password, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        /// <summary>
        /// Compara las contraseñas byte por byte
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }

        /// <summary>
        /// Convierte la contraseña a un arreglo de bytes
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <param name="iterations"></param>
        /// <param name="outputBytes"></param>
        /// <returns></returns>
        private byte[] GetPbkdf2Bytes(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);
        }

    }
}
