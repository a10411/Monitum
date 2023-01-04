using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumBLL.Utils
{
    /// <summary>
    /// Enum que visa enumerar os HTTP status codes
    /// Facilita o processo de atribuir um status code a uma resposta, sem ter de o fazer pelo seu número, mas sim pelo seu significado
    /// Success, NoContent, etc.
    /// </summary>
    public enum StatusCodes
    {
        SUCCESS = 200,
        NOCONTENT = 204,
        BADREQUEST = 400,
        UNAUTHORIZED = 401,
        NOTFOUND = 404,
        INTERNALSERVERERROR = 500
    }
}
