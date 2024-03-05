using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCA_VEA.Core.Tools.OperationResult
{
    public class Result<T>
    {
        public T Payload { get; private set; }
        public List<Error> Errors { get; private set; } = new List<Error>();

        public string ErrorMessage => string.Join(Environment.NewLine, Errors.Select(e => $"Error {e.Code}: {e.Message}"));

        public Result(T payload)
        {
            Payload = payload;
        }

        public Result(List<Error> errors)
        {
            Errors.AddRange(errors);
        }

        public bool IsSuccess => Errors.Count == 0;

        public static Result<T> Success(T payload)
        {
            return new Result<T>(payload);
        }

        public static Result<T> Failure(Error error)
        {
            return new Result<T>(new List<Error> { error });
        }

        public static Result<T> Failure(IEnumerable<Error> errors)
        {
            return new Result<T>(new List<Error>(errors));
        }

        public static implicit operator Result<IEnumerable<T>>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Result<IEnumerable<T>>.Success(new List<T> { result.Payload });
            }
            else
            {
                return Result<IEnumerable<T>>.Failure(result.Errors);
            }
        }

        public static implicit operator T(Result<T> result)
        {
            if (!result.IsSuccess)
            {
                throw new InvalidOperationException("Cannot unwrap error result.");
            }
            return result.Payload;
        }
    }
}
