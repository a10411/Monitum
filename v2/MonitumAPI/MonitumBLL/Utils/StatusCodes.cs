using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumBLL.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public enum StatusCodes
    {
        /// <summary>
        /// Success test
        /// </summary>
        SUCCESS = 200,
        /// <summary>
        /// test
        /// </summary>
        NOCONTENT = 204,
        UNAUTHORIZED = 401,
        INTERNALSERVERERROR = 500
    }
}
