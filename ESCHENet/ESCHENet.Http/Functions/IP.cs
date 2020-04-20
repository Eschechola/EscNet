using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace ESCHENet.Http.Functions
{
    public class IP
    {
        private readonly IHttpContextAccessor HttpAcessor;

        public IP(IHttpContextAccessor _httpContextAccessor)
        {
            HttpAcessor = _httpContextAccessor;
        }

        public string GetRequestIP()
        {
            string ip = string.Empty;

            //faz um split nos endereços ips, tentando pegar inicialmente o X-FORWARDED-FOR
            ip = SplitCsv(GetHeaderValueAs<string>("X-FORWARDED-FOR")).FirstOrDefault();

            //caso o IP FORWARDED NÃO EXISTA ele tenta pegar o ip padrão
            if (string.IsNullOrEmpty(ip) && HttpAcessor.HttpContext?.Connection?.RemoteIpAddress != null)
            {
                ip = HttpAcessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }

            //caso o IP Padrão nao exista, ele pega o ip REMOTE_ADDR
            if (string.IsNullOrEmpty(ip))
            {
                ip = GetHeaderValueAs<string>("REMOTE_ADDR");
            }

            return ip;
        }

        //função que vai retornar o valor de acordo com o headerName
        public T GetHeaderValueAs<T>(string headerName)
        {
            StringValues values;

            //tenta pegar o valor de acorodo com header passado
            //caso seja nulo retorna uma string vazia
            if (HttpAcessor.HttpContext?.Request?.Headers?.TryGetValue(headerName, out values) ?? false)
            {
                string rawValues = values.ToString();

                if (!string.IsNullOrEmpty(rawValues))
                {
                    return (T)Convert.ChangeType(values.ToString(), typeof(T));
                }

            }
            return default(T);
        }

        //realiza o SPLIT separando os atributos do IP
        public List<string> SplitCsv(string csvList, bool nullOrWhitespaceInputReturnsNull = false)
        {
            if (string.IsNullOrWhiteSpace(csvList))
                return nullOrWhitespaceInputReturnsNull ? null : new List<string>();

            return csvList
                .TrimEnd(',')
                .Split(',')
                .AsEnumerable<string>()
                .Select(s => s.Trim())
                .ToList();
        }
    }
}
