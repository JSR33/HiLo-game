using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiLoGame.Contracts.v1.Responses
{
    /// <summary>
    /// Error response to be retrieved to client
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// List of <see cref="ErrorModel"/> errors to be retrieved to client 
        /// </summary>
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}
