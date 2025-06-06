using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphRAGLlmApi.Domain.Entities;

namespace GraphRAGLlmApi.Application.Queries.GetGraphConnections
{
    public class GraphConnectionsResponse
    {
        public IEnumerable<GraphConnection> Connections { get; set; }
    }
}