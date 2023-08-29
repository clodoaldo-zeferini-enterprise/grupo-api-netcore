using System.Collections.Generic;

namespace Service.Grupo.Infrastructure.Config
{
    public class ConnectionString
    {
        public List<Connection> Connections { get; set; }

        public ConnectionString() { }

        public ConnectionString(List<Connection> connections)
        {
            if (connections != null) Connections = connections;
            else Connections = new List<Connection>();
        }
    }
    public class Connection
    {
        public int Id { get; set; }
        public string Setting { get; set; }
    }
}
