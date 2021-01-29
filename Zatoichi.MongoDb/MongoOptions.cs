namespace Zatoichi.MongoDb
{
    using System.Collections.Generic;

    public class MongoOptions
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string AuthMechanism { get; set; }
        public string MongoHost { get; set; }
        public List<string> MongoHosts { get; set; } = new List<string>();
        public List<string> Ports { get; set; } = new List<string>();
        public string DefaultDb { get; set; }
        public string AuthDb { get; set; }
        public string ReplicaSetName { get; set;  }
        public int Port { get; set; }

        public override string ToString()
        {
            // verify hosts and ports lengths are same

            if (MongoHosts.Count != Ports.Count)
            {
                throw new System.Exception($"The Ports ({Ports.Count}) and MongoHosts ({MongoHosts.Count}) counts must match.");
            }

            string[] hosts = new string[Ports.Count];

            for (int i = 0; i < Ports.Count; i++)
            {
                hosts[i] = $"{MongoHosts[i]}:{Ports[i]}";
            }

            return $"mongodb://{Username}:{Password}@{string.Join(",", hosts)}/{AuthDb}?replicaSet={ReplicaSetName}";
        }
    }
}