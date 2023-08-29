using System.Collections.Generic;

namespace Service.Grupo.Application.Base
{
    public class ConnectionString
    {
        public List<Connection> Connections { get; set; }

        public ConnectionString() { }

        public ConnectionString(List<Connection> connections)
        {
            if (connections != null) this.Connections = connections;
            else this.Connections = new List<Connection>();
        }
    }
    public class Connection
    {
        public int Id { get; set; }
        public string Setting { get; set; }
    }
}
