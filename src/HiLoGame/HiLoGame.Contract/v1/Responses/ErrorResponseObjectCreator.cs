using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiLoGame.Contracts.v1.Responses
{
    public class ErrorResponseObjectCreator
    {
        public static ErrorResponse ErrorResponseObject(string message, string fieldName = "")
        {
            return new ErrorResponse
            {
                Errors = new List<ErrorModel>
                {
                    new ErrorModel
                    {
                        FieldName = fieldName,
                        Message = message
                    }
                }
            };
        }
    }
}
