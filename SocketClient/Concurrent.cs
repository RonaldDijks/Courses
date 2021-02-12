using System;
using System.Threading;
using Sequential;

namespace Concurrent
{
    public class ConcurrentClient : SimpleClient
    {
        public Thread workerThread;

        public ConcurrentClient(int id, Setting settings) : base(id, settings)
        {
            // todo [Assignment]: implement required code
            prepareClient();
        }
        public void run()
        {
            // todo [Assignment]: implement required code
            workerThread = new Thread(this.communicate);
            workerThread.Start();
        }
    }
    public class ConcurrentClientsSimulator : SequentialClientsSimulator
    {
        private ConcurrentClient[] clients;

        public ConcurrentClientsSimulator() : base()
        {
            Console.Out.WriteLine("\n[ClientSimulator] Concurrent simulator is going to start with {0}", settings.experimentNumberOfClients);
            clients = new ConcurrentClient[settings.experimentNumberOfClients];
        }

        public void ConcurrentSimulation()
        {
            try
            {
                // todo [Assignment]: implement required code
                for (int i = 0; i < clients.Length; i++)
                {
                    var client = new ConcurrentClient(i, settings);
                    client.run();
                    clients[i] = client;
                }

                foreach (var client in clients)
                {
                    client.workerThread.Join();
                }

                Thread.Sleep(settings.delayForTermination);

                var terminatingClient = new ConcurrentClient(-1, settings);
                terminatingClient.prepareClient();
                terminatingClient.run();
            }
            catch (Exception e)
            { 
                Console.Out.WriteLine("[Concurrent Simulator] {0}", e.Message); 
            }
        }
    }
}
