using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace uBeac.Core.Web
{
    public interface IResultSet
    {
        List<Error> Errors { get; }
        string TraceId { get; set; }
        int Duration { get; set; }
        int Code { get; set; }
    }

    public interface IResultSet<TResult> : IResultSet
    {
        TResult Data { get; }
    }

    public class ResultSet : IResultSet
    {
        public List<Error> Errors { get; } = new List<Error>();
        public string TraceId { get; set; }
        public int Duration { get; set; }
        public int Code { get; set; } = StatusCodes.Status200OK;
    }

    public class ResultSet<TResult> : ResultSet, IResultSet<TResult>
    {
        public TResult Data { get; }
        public ResultSet(TResult data)
        {
            Data = data;
        }
    }
}
